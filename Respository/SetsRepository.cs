using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Entities;
using EvaluationBackend.Interface;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Respository;

public class SetsRepository : GenericRepository<Set, Guid>, ISetsRepository
{
    public SetsRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
