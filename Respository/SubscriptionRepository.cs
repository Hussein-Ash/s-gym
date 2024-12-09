using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Entities;
using EvaluationBackend.Interface;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Respository;

public class SubscriptionRepository : GenericRepository<Subscription, Guid>, ISubscriptionRepository
{
    public SubscriptionRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
