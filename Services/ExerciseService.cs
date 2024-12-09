using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.DATA.DTOs.Exercises;
using EvaluationBackend.Entities;
using EvaluationBackend.Repository;
using Microsoft.EntityFrameworkCore;

namespace EvaluationBackend.Services;
public interface IExerciseService
{
    Task<(List<ExerciseDto>? exerciseDtos, int? totalCount, string? error)> GetAll(ExerciseFilter filter);
    Task<(ExerciseDto? exerciseDto, string? error)> GetById(Guid id);
    Task<(ExerciseDto? exerciseDto, string? error)> Add(ExerciseForm Form);
    Task<(ExerciseDto? exerciseDto, string? error)> Update(ExerciseUpdate Update, Guid Id);
    Task<(Exercise? exercise, string? error)> Delete(Guid id);

}

public class ExerciseService : IExerciseService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public ExerciseService(IRepositoryWrapper repositoryWrapper, IMapper mapper, DataContext context)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _context = context;

    }

    public async Task<(ExerciseDto? exerciseDto, string? error)> Add(ExerciseForm Form)
    {
        var newExercise = _mapper.Map<Exercise>(Form);
        var result = await _repositoryWrapper.Exercise.Add(newExercise);
        if (result == null) return (null, "Error Adding Entity");
        var resultWithInclude = await _context.Exercises
                .Include(x => x.MuscleName)
                .FirstOrDefaultAsync(x => x.Id == result.Id);

        var exerciseDto = _mapper.Map<ExerciseDto>(resultWithInclude);

        return (exerciseDto, null);
    }

    public async Task<(Exercise? exercise, string? error)> Delete(Guid id)
    {
        var exercise = await _repositoryWrapper.Exercise.Get<ExerciseDto>(u => u.Id == id);
        if (exercise == null) return (null, "already deleted");
        var deleteExercise = await _repositoryWrapper.Exercise.Delete(id);
        return (deleteExercise, null);
    }

    public async Task<(List<ExerciseDto>? exerciseDtos, int? totalCount, string? error)> GetAll(ExerciseFilter filter)
    {
        var (exercises, totalCount) = await _repositoryWrapper.Exercise.GetAll<ExerciseDto>(
            x => 
            (filter.MuscleId == null || x.MuscleId == filter.MuscleId) &&
            (filter.MuscleName == null || x.MuscleName!.Name!.Contains(filter.MuscleName))&&
            (filter.Name == null || x.Name!.Contains(filter.Name))

            ,filter.PageNumber, filter.PageSize);
        return (exercises, totalCount, null);
    }

    public async Task<(ExerciseDto? exerciseDto, string? error)> GetById(Guid id)
    {
        var exercise = await _repositoryWrapper.Exercise.GetById(id);
        if (exercise == null) return (null, "not found");
        var resultWithInclude = await _context.Exercises
                .Include(x => x.MuscleName)
                .FirstOrDefaultAsync(x => x.Id == exercise.Id);

        var exerciseDto = _mapper.Map<ExerciseDto>(resultWithInclude);
        return (exerciseDto, null);
    }

    public async Task<(ExerciseDto? exerciseDto, string? error)> Update(ExerciseUpdate Update, Guid Id)
    {
        var exercise = await _repositoryWrapper.Exercise.Get(u => u.Id == Id);
        if (exercise == null) return (null, "not found");
        _mapper.Map(Update, exercise);


        await _repositoryWrapper.Exercise.Update(exercise);
        var resultWithInclude = await _context.Exercises
            .Include(x => x.MuscleName)
            .FirstOrDefaultAsync(x => x.Id == Id);

        var exerciseDto = _mapper.Map<ExerciseDto>(resultWithInclude);

        return (exerciseDto, null);
    }
}
