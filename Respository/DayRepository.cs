using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Entities;
using EvaluationBackend.Interface;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Respository;

public class DayRepository : GenericRepository<Day, Guid>, IDayRepository
{
    public DayRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
