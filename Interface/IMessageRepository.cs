using System;
using EvaluationBackend.Entities;

namespace EvaluationBackend.Interface;

public interface IMessageRepository : IGenericRepository<Message,Guid>
{

}
