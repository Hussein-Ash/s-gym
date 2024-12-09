using System;
using AutoMapper;
using EvaluationBackend.DATA.DTOs.Offer;
using EvaluationBackend.Entities;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Services;
public interface IOfferService
{
    Task<(List<OfferDto>? offerDtos, int? totalCount, string? error)> GetAll(OfferFilter filter);
    Task<(OfferDto? offerDto, string? error)> GetById(Guid id);
    Task<(OfferDto? offerDto, string? error)> Add(OfferForm Form);
    Task<(OfferDto? offerDto, string? error)> Update(OfferUpdate Update, Guid Id);
    Task<(Offer? set, string? error)> Delete(Guid id);
}


public class OfferService : IOfferService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    public OfferService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;

    }
    public async Task<(OfferDto? offerDto, string? error)> Add(OfferForm Form)
    {
        var newOffer = _mapper.Map<Offer>(Form);
        var result = await _repositoryWrapper.Offer.Add(newOffer);
        if (result == null) return (null, "Error Adding Entity");
        var offerDto = _mapper.Map<OfferDto>(result);

        return (offerDto, null);
    }

    public async Task<(Offer? set, string? error)> Delete(Guid id)
    {
        var offer = await _repositoryWrapper.Offer.Get<OfferDto>(u => u.Id == id);
        if (offer == null) return (null, "already deleted");
        var deleteOffer = await _repositoryWrapper.Offer.Delete(id);
        return (deleteOffer, null);

    }

    public async Task<(List<OfferDto>? offerDtos, int? totalCount, string? error)> GetAll(OfferFilter filter)
    {
        var (offers, totalCount) = await _repositoryWrapper.Offer.GetAll<OfferDto>(filter.PageNumber, filter.PageSize);
        return (offers, totalCount, null);
    }

    public async Task<(OfferDto? offerDto, string? error)> GetById(Guid id)
    {
        var Offer = await _repositoryWrapper.Offer.GetById(id);
        if (Offer == null) return (null, "not found");
        var OfferDto = _mapper.Map<OfferDto>(Offer);
        return (OfferDto, null);
    }

    public async Task<(OfferDto? offerDto, string? error)> Update(OfferUpdate Update, Guid Id)
    {
        var offer = await _repositoryWrapper.Offer.Get(u => u.Id == Id);
        if (offer == null) return (null, "not found");
        _mapper.Map(Update, offer);


        await _repositoryWrapper.Offer.Update(offer);

        var offerDto = _mapper.Map<OfferDto>(offer);

        return (offerDto, null);
    }
}
