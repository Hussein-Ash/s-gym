using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Entities;
using EvaluationBackend.Interface;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Respository;

public class MuscleRepository : GenericRepository<Muscle, Guid>, IMuscleRepository
{
    public MuscleRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
