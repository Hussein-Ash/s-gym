using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Entities;
using EvaluationBackend.Interface;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Respository;

public class SectionsRepository : GenericRepository<Section, Guid>, ISectionRepository
{
    public SectionsRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
