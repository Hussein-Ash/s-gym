using System;
using API.Extensions;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.DATA.DTOs.Message;
using EvaluationBackend.Entities;
using EvaluationBackend.Repository;
using EvaluationBackend.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OneSignalApi.Model;

namespace API.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly DataContext _context;


        public MessageHub(IMessageService messageService, IMapper mapper,
        IRepositoryWrapper repositoryWrapper, DataContext context
        )
        {
            _messageService = messageService;
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("connecteddddddddddddd");
            var httpContext = Context.GetHttpContext();
            var otheruserId = Guid.Parse(httpContext?.Request.Query["user"]!);
            // Console.WriteLine(otheruserId);
            var otherUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == otheruserId);

            if (Context.User == null || otheruserId == Guid.Empty)
                throw new Exception("Cannot join");

            var user = await _context.Users.Include(x => x.Sub).FirstOrDefaultAsync(x => x.Id == Context.User.GetUserId());
            // if (user.Sub!.Type != SubType.Gold && user.Sub.CourseName != null)
            //     throw new Exception("Only Gold Members can join");

            var groupName = GetGroupName(Context.User.GetUserFullName(), otherUser!.FullName);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await AddToGroup(groupName);

            var messages = await _messageService.GetMessageThread(Context.User.GetUserId(), otheruserId!);
            await Clients.Group(groupName).SendAsync("ReceiveMessages", messages);

        }

        public async Task SendMessage(MessageForm form)
        {
            var userId = Context.User?.GetUserId() ?? throw new Exception("Couldn't get user");

            if (userId == form.RecipientId)
                throw new HubException("You cannot message yourself");

            var recipient = await _repositoryWrapper.User.Get(u => u.Id == form.RecipientId);
            var sender = await _repositoryWrapper.User.Get(u => u.Id == userId);


            if (recipient == null || sender == null || sender.UserName == null || recipient.UserName == null)
                throw new HubException("Cannot send message right now");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = form.Content,
                Imgs = form.Imgs,
                VoiceMsgs = form.VoiceMsgs

            };

            var groupName = GetGroupName(sender.FullName!, recipient.FullName);
            // var group = await _messageService.GetMessageGroup(groupName);

            // if (group != null && group.Connections.Any(x => x.Username == recipient.UserName))
            // {
            //     message.DateRead = DateTime.UtcNow;
            // }
            await _repositoryWrapper.Message.Add(message);
            await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await RemoveFromMessageGroup();
            // await base.OnDisconnectedAsync(exception);
        }

        private async Task<bool> AddToGroup(string groupName)
        {
            var username = Context.User?.GetUserFullName() ?? throw new Exception("Cannot get FullName");
            var group = await _messageService.GetMessageGroup(groupName);
            var connection = new Connection { ConnectionId = Context.ConnectionId, Username = username };

            if (group == null)
            {
                group = new Group { Name = groupName };
                await _messageService.AddGroup(group);
            }
            group.Connections.Add(connection);
            return await _context.SaveChangesAsync() <= 0;
        }

        private async Task RemoveFromMessageGroup()
        {
            var connection = await _messageService.GetConnection(Context.ConnectionId);
            if (connection != null)
            {
                await _messageService.RemoveConnection(connection);
//                 _context.Connections.Remove(connection)
// ;
//                 await _context.SaveChangesAsync();
            }
        }

        private string GetGroupName(string caller, string? other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}
