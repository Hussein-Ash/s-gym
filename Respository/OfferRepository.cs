using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Entities;
using EvaluationBackend.Interface;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Respository;

public class OfferRepository : GenericRepository<Offer, Guid>, IOfferRepository
{
    public OfferRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
