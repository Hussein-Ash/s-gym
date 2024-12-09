using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.Entities;
using EvaluationBackend.Interface;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Respository;

public class CourseRepository : GenericRepository<Course, Guid>, ICourseRepository
{
    public CourseRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
