using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Entities;
using EvaluationBackend.Interface;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Respository;

public class MessageRepository : GenericRepository<Message, Guid>, IMessageRepository
{
    public MessageRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
