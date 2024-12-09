using System;
using EvaluationBackend.Entities;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Interface;

public interface ISubscriptionRepository : IGenericRepository<Subscription,Guid>
{

}
