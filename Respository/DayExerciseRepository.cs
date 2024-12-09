using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Entities;
using EvaluationBackend.Interface;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Respository;

public class DayExerciseRepository : GenericRepository<DayExercise, Guid>, IDayExerciseRepository
{
    public DayExerciseRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
