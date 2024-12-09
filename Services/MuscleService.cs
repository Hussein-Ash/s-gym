using System;
using AutoMapper;
using EvaluationBackend.DATA;
using EvaluationBackend.DATA.DTOs.Muscles;
using EvaluationBackend.Entities;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Services;
public interface IMuscleService
{
    Task<(List<MuscleDto>? muscleDtos, int? totalCount, string? error)> GetAll(MuscleFilter filter);
    Task<(MuscleDto? muscleDto, string? error)> GetById(Guid id);
    Task<(MuscleDto? muscleDto, string? error)> Add(MuscleForm Form);
    Task<(MuscleDto? muscleDto, string? error)> Update(MuscleUpdate Update, Guid Id);
    Task<(Muscle? set, string? error)> Delete(Guid id);
}

public class MuscleService : IMuscleService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public MuscleService(IRepositoryWrapper repositoryWrapper, IMapper mapper, DataContext context)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _context = context;

    }

    public async Task<(MuscleDto? muscleDto, string? error)> Add(MuscleForm Form)
    {
        var newMuscle = _mapper.Map<Muscle>(Form);
        var result = await _repositoryWrapper.Muscle.Add(newMuscle);
        if (result == null) return (null, "Error Adding Entity");
        var muscleDto = _mapper.Map<MuscleDto>(result);

        return (muscleDto, null);
    }

    public async Task<(Muscle? set, string? error)> Delete(Guid id)
    {
        var muscle = await _repositoryWrapper.Muscle.Get<MuscleDto>(u => u.Id == id);
        if (muscle == null) return (null, "already deleted");
        var deleteMuscle = await _repositoryWrapper.Muscle.Delete(id);
        return (deleteMuscle, null);
    }

    public async Task<(List<MuscleDto>? muscleDtos, int? totalCount, string? error)> GetAll(MuscleFilter filter)
    {
        var (muscles, totalCount) = await _repositoryWrapper.Muscle.GetAll<MuscleDto>(
            x=>
            filter.Name == null || x.Name!.Contains(filter.Name)

            ,filter.PageNumber, filter.PageSize);
        return (muscles, totalCount, null);
    }

    public async Task<(MuscleDto? muscleDto, string? error)> GetById(Guid id)
    {
        var muscle = await _repositoryWrapper.Muscle.GetById(id);
        if (muscle == null) return (null, "not found");
        var muscleDto = _mapper.Map<MuscleDto>(muscle);
        return (muscleDto, null);
    }

    public async Task<(MuscleDto? muscleDto, string? error)> Update(MuscleUpdate Update, Guid Id)
    {
        var muscle = await _repositoryWrapper.Muscle.Get(u => u.Id == Id);
        if (muscle == null) return (null, "not found");
        _mapper.Map(Update, muscle);


        await _repositoryWrapper.Muscle.Update(muscle);

        var muscleDto = _mapper.Map<MuscleDto>(muscle);

        return (muscleDto, null);
    }
}
