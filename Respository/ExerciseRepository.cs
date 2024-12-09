using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Entities;
using EvaluationBackend.Interface;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Respository;

public class ExerciseRepository : GenericRepository<Exercise, Guid>, IExerciseRepository
{
    public ExerciseRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
