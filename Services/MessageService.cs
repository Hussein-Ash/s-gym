using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.DATA.DTOs.Message;
using EvaluationBackend.Entities;
using EvaluationBackend.Repository;
using Microsoft.EntityFrameworkCore;

namespace EvaluationBackend.Services;
public interface IMessageService
{
    Task<(List<MessageDto>? messageDtos, int? totalCount, string? error)> GetMessagesForUser(MessageFilter filter);
    Task<(List<MessageDto>? messageDtos, string? error)> GetMessageThread(Guid currentId,Guid recipientId);
    Task<Group> GetMessageGroup(string groupName);


    Task<(MessageDto? messageDto, string? error)> GetMessageById(Guid id);
    Task<(MessageDto? messageDto, string? error)> AddMessage(MessageForm Form,Guid senderId);
    Task<Group>? AddGroup(Group Form);
    Task<(MessageDto? message, string? error)> UpdateMessage(MessageUpdate up,Guid id,Guid userId);


    Task<(MessageDto? message, string? error)> DeleteMessage(Guid id,Guid userId);

    Task<Connection> GetConnection(string connectionId);
    Task<Connection?> RemoveConnection(Connection connection);

    // Task<(MessageDto? messageDto, string? error)> UpdateCourse(MessageUpdate Update, Guid Id);

}


public class MessageService : IMessageService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public MessageService(IRepositoryWrapper repositoryWrapper, IMapper mapper, DataContext context)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _context = context;

    }

    public async Task<Group>? AddGroup(Group Form)
    {
        await _context.Groups.AddAsync(Form);
        await _context.SaveChangesAsync();
        return Form;

    }

    public async Task<(MessageDto? messageDto, string? error)> AddMessage(MessageForm Form,Guid senderId)
    {
        var sender = await _context.Users.FirstOrDefaultAsync(x=>x.Id == senderId);
        if(sender == null) return(null,"didnt get the id from token");
        if(Form == null || Form.RecipientId == Guid.Empty) return(null,"u cannot send empty form");
        var recipient = await _context.Users.FirstOrDefaultAsync(x=>x.Id == Form.RecipientId);
        if(recipient == null) return(null,"the one u trying to message is not found");
        var newMessage = new Message
        {
            SenderUsername = sender.FullName,
            RecipientUsername = recipient.FullName,
            SenderId = sender.Id,
            RecipientId = recipient.Id,
            Content = Form.Content,
            Imgs = Form.Imgs,
            VoiceMsgs = Form.VoiceMsgs
        };
        await _context.Messages.AddAsync(newMessage);
        if (await _context.SaveChangesAsync() <= 0) return (null, "Error saving Entity");
        var messageDto = _mapper.Map<MessageDto>(newMessage);

        return (messageDto, null);
    }

    public async Task<(MessageDto? message, string? error)> DeleteMessage(Guid id,Guid userId)
    {
        var message = await _repositoryWrapper.Message.Get(u => u.Id == id);
        if(message.SenderId != userId) return(null,"u cannot delete this message");
        if (message == null) return (null, "already deleted");
        var deleteMessage = await _repositoryWrapper.Message.SoftDelete(id);
        var messageDto = _mapper.Map<MessageDto>(deleteMessage);
        return (messageDto, null);
    }

    public async Task<Connection> GetConnection(string connectionId)
    {
        var connection = await _context.Connections.FindAsync(connectionId);
        return connection;
    }

    public async Task<(MessageDto? messageDto, string? error)> GetMessageById(Guid id)
    {
        var message = await _repositoryWrapper.Message.GetById(id);
        if (message == null) return (null, "not found");
        var messageDto = _mapper.Map<MessageDto>(message);
        return (messageDto, null);
    }

    public async Task<Group> GetMessageGroup(string groupName)
    {
        var group = await _context.Groups
           .Include(x => x.Connections)
           .FirstOrDefaultAsync(x => x.Name == groupName);
        return group;
    }

    public async Task<(List<MessageDto>? messageDtos, int? totalCount, string? error)> GetMessagesForUser(MessageFilter filter)
    {
        var (message, totalCount) = await _repositoryWrapper.Message.GetAll<MessageDto>(
            x =>
            filter.Username == null || x.Sender!.FullName!.Contains(filter.Username)
            , filter.PageNumber, filter.PageSize);
        return (message, totalCount, null);

    }

    public async Task<(List<MessageDto>? messageDtos, string? error)> GetMessageThread(Guid currentId,Guid recipientId)
    {
        var recipient = await _repositoryWrapper.User.GetById(recipientId);
        if(recipient == null) return (null, "user not found");
        var current = await _repositoryWrapper.User.GetById(currentId);
        if(current == null) return (null,"didnt get the user");
        
        
        var messages = await _context.Messages
           .Include(x => x.Sender)
           .Include(x => x.Recipient)
           .Where(
               x => (x.RecipientId == recipient.Id && x.SenderId == current.Id ||
               x.SenderId == recipient.Id &&x.RecipientId == current.Id) && x.Deleted == false
               )
               .OrderBy(x => x.MessageSent)
               .ToListAsync();
        var messageDto = _mapper.Map<List<MessageDto>>(messages);

        return (messageDto,null);


    }

    public async Task<Connection?> RemoveConnection(Connection connection)
    {
        _context.Connections.Remove(connection);
        await _context.SaveChangesAsync();
        return connection ;
    }

    public async Task<(MessageDto? message, string? error)> UpdateMessage(MessageUpdate up, Guid id, Guid userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id == userId);
        var message = await _context.Messages.FirstOrDefaultAsync(x=>x.Id == id);
        if(message == null) return (null,"message not found");
        if(message.SenderId != userId || message.RecipientId == userId) return(null ,"you cannot edit this message");
        message.Content = up.Content;
        await _repositoryWrapper.Message.Update(message);
        var result = _mapper.Map<MessageDto>(message);
        return(result,null);
    }
}
