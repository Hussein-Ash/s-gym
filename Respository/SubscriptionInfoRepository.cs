using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Entities;
using EvaluationBackend.Interface;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Respository;

public class SubscriptionInfoRepository : GenericRepository<SubscriptionInfo, Guid>, ISubscriptionInfoRepositroy
{
    public SubscriptionInfoRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
