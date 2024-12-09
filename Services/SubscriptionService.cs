using System;
using System.Text.Json;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.DATA.DTOs.Subscription;
using EvaluationBackend.Entities;
using EvaluationBackend.Helpers.OneSignal;
using EvaluationBackend.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.IO;

namespace EvaluationBackend.Services;
public interface ISubscriptionService
{
    Task<(List<SubDto>? subDtos, int? totalCount, string? error)> GetAll(SubFilter filter);
    Task<(SubDto? subDto, string? error)> GetById(Guid id);
    Task<(SubDto? subDto, string? error)> Add(SubForm subForm);
    Task<(SubDto? subDto, string? error)> AddCourse(SubCourseForm form, Guid subId);
    Task<(SubDto? subDto, string? error)> Fill(SubInfoForm subInfoForm, Guid id);
    Task<(SubDto? subDto, string? error)> Update(SubUpdate subUpdate, Guid Id);
    Task<(Subscription? subscription, string? error)> Delete(Guid id);
    Task<(List<Subscription>? subscriptions, string? error)> MultiDelete(MultDelete multDelete);


}

public class SubscriptionService : ISubscriptionService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public SubscriptionService(IRepositoryWrapper repositoryWrapper, IMapper mapper, DataContext context)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _context = context;

    }

    public async Task<(List<SubDto>? subDtos, int? totalCount, string? error)> GetAll(SubFilter filter)
    {
        var (subscription, totalCount) = await _repositoryWrapper.Subscription.GetAll<SubDto>(
            x =>
            (filter.SectionName == null || x.SectionName!.Name!.Contains(filter.SectionName)) &&
            (filter.Type == null || x.Type == filter.Type) &&
            (filter.PhoneNumber == null || x.PhoneNumber!.Contains(filter.PhoneNumber!))
            , filter.PageNumber, filter.PageSize);
        return (subscription, totalCount, null);
    }
    public async Task<(SubDto? subDto, string? error)> Add(SubForm subForm)
    {
        if (subForm == null) return (null, "empty form");

        var newSub = new Subscription();
        var section = await _repositoryWrapper.Section.GetById(subForm.SectionId);
        if (section == null) return (null, "No section found");

        if (subForm.UserId != null)
        {
            var user = await _repositoryWrapper.User.Get(x => x.Id == subForm.UserId);
            if (user == null) return (null, "User not found");
            var existingSubscription = await _context.Subscriptions.FirstOrDefaultAsync(x => x.UserId == subForm.UserId);
            if (existingSubscription != null)
            {
                return (null, "User already has an existing subscription");
            }
            var newSubscription = new Subscription
            {
                SectionId = subForm.SectionId,
                UserId = subForm.UserId,
                Type = subForm.Type,
                Status = PlayerStatus.New,
                PlayerPhoto = user.Img == null ? null : user.Img,
                SectionName = section,
                PhoneNumber = user.PhoneNumber ?? subForm.PhoneNumber!.Trim()
            };
            newSub = newSubscription;
            user.SubId = newSubscription.Id;
            var noti = new Notification
            {
                UserId = user.Id,
                Title = "الاشتراك",
                Body = $"تم انشاء اشتراك {newSub.Type}"

            };
            _context.Users.Update(user);
            await _context.Notifications.AddAsync(noti);
            await _context.Subscriptions.AddAsync(newSub);
            if (await _context.SaveChangesAsync() <= 0) return (null, "error saving entity");
            OneSignal.SendNoitications(noti, user.FullName!);

        }
        else
        {
            var newSubscription = new Subscription
            {
                SectionId = subForm.SectionId,
                UserId = subForm.UserId,
                Type = subForm.Type,
                Status = PlayerStatus.New,
                PlayerPhoto = null,
                SectionName = section,
                PhoneNumber = subForm.PhoneNumber!.Trim()
            };
            newSub = newSubscription;
            await _context.Subscriptions.AddAsync(newSub);
            if (await _context.SaveChangesAsync() <= 0) return (null, "error saving entity");


        }

        var subDto = _mapper.Map<SubDto>(newSub);
        return (subDto, null);
    }



    public async Task<(SubDto? subDto, string? error)> Fill(SubInfoForm subInfoForm, Guid id)
    {
        if (subInfoForm == null)
        {
            return (null, "Subscription info form cannot be null");
        }
        var sub = await _repositoryWrapper.Subscription.GetById(id);
        if (sub == null) return (null, "No subscription found");
        var user = await _repositoryWrapper.User.Get(x => x.Id == sub.UserId);
        if (user == null) return (null, "User not found");

        SubscriptionInfo subInfo;

        switch (sub.Type)
        {
            case SubType.Bronze:
                if (subInfoForm.BronzeInformations == null)
                    return (null, "Bronze information is missing");
                subInfo = _mapper.Map<SubscriptionInfo>(subInfoForm.BronzeInformations);
                break;

            case SubType.Silver:
                if (subInfoForm.SilverInformations == null)
                    return (null, "Silver information is missing");
                subInfo = _mapper.Map<SubscriptionInfo>(subInfoForm.SilverInformations);
                break;

            case SubType.Gold:
                if (subInfoForm.GoldInformations == null)
                    return (null, "Gold information is missing");
                subInfo = _mapper.Map<SubscriptionInfo>(subInfoForm.GoldInformations);
                if (!subInfo.UseHrmon) subInfo.Hrmon = null;
                break;

            default:
                return (null, "Invalid subscription type");
        }

        subInfo.SubId = sub.Id;

        var existingSubInfo = await _repositoryWrapper.SubscriptionInfo.Get(x => x.Id == sub.SubInfoId);
        if (existingSubInfo == null)
        {
            var result = await _repositoryWrapper.SubscriptionInfo.Add(subInfo);
            if (result == null) return (null, "Error adding Subscription Info");
        }
        else
        {
            var result = await _repositoryWrapper.SubscriptionInfo.Update(subInfo);
            if (result == null) return (null, "Error updating Subscription Info");
        }


        sub.SubInfoId = subInfo.Id;
        sub.Status = PlayerStatus.NeedCourse;
        await _repositoryWrapper.Subscription.Update(sub);

        var subDto = _mapper.Map<SubDto>(sub);
        return (subDto, null);
    }

    public async Task<(SubDto? subDto, string? error)> GetById(Guid id)
    {

        // var subscription = await _repositoryWrapper.Subscription.GetById(id);
        var subscription = await _context.Subscriptions
        .Include(x => x.SectionName)
        .Include(x => x.User)
        .Include(x => x.SubInfo)
        .FirstOrDefaultAsync(x => x.Id == id);
        if (subscription == null) return (null, "not found");
        var subscriptionDto = _mapper.Map<SubDto>(subscription);
        return (subscriptionDto, null);

    }

    public async Task<(Subscription? subscription, string? error)> Delete(Guid id)
    {

        var subscription = await _repositoryWrapper.Subscription.Get<SubDto>(u => u.Id == id);
        if (subscription == null) return (null, "already deleted");
        var deletesubScription = await _repositoryWrapper.Subscription.Delete(id);
        return (deletesubScription, null);

    }

    public async Task<(SubDto? subDto, string? error)> Update(SubUpdate subUpdate, Guid Id)
    {
        var subscription = await _repositoryWrapper.Subscription.Get(u => u.Id == Id);
        if (subscription == null) return (null, "not found");
        _mapper.Map(subUpdate, subscription);

        await _repositoryWrapper.Subscription.Update(subscription);
        var result = await _context.Subscriptions
        .Include(x => x.SectionName)
        .Include(x => x.User)
        .Include(x => x.SubInfo)
        .FirstOrDefaultAsync(x => x.Id == Id);

        var subscriptionDto = _mapper.Map<SubDto>(result);


        return (subscriptionDto, null);
    }

    public async Task<(List<Subscription>? subscriptions, string? error)> MultiDelete(MultDelete multDelete)
    {
        if (multDelete?.Ids == null || !multDelete.Ids.Any())
        {
            return (null, "You sent an empty list");
        }

        var subscriptionsToDelete = await _context.Subscriptions
            .Where(u => multDelete.Ids.Contains(u.Id))
            .ToListAsync();

        if (subscriptionsToDelete == null || !subscriptionsToDelete.Any())
        {
            return (null, "No subscriptions found");
        }

        _context.Subscriptions.RemoveRange(subscriptionsToDelete);
        await _context.SaveChangesAsync();

        return (subscriptionsToDelete, null);
    }

    public async Task<(SubDto? subDto, string? error)> AddCourse(SubCourseForm form, Guid subId)
    {
        var sub = await _context.Subscriptions
                .Include(x => x.SectionName)
                .Include(x => x.User)
                .Include(x => x.SubInfo)
                .FirstOrDefaultAsync(x => x.Id == subId);
        if (sub == null) return (null, "Subscription not found");
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == form.CourseId);
        if (course == null) return (null, "Course not found");

        sub.CourseId = course.Id;
        sub.ResgisterDate = DateTime.UtcNow;
        sub.Status = PlayerStatus.Finished;
        var noti = new Notification
        {
            UserId = sub.UserId,
            Title = "الاشتراك",
            Body = "تم اضافة كورس"

        };

        _context.Subscriptions.Update(sub);
        await _context.Notifications.AddAsync(noti);
        if (await _context.SaveChangesAsync() <= 0) return (null, "error saving entity");

        var result = _mapper.Map<SubDto>(sub);
        if (result == null) return (null, "error mapping");
        return (result, null);
    }
}
