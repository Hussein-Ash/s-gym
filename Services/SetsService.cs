using System;
using AutoMapper;
using EvaluationBackend.DATA.DTOs.Sets;
using EvaluationBackend.Entities;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Services;
public interface ISetsService
{
    Task<(List<SetsDto>? setsDtos, int? totalCount, string? error)> GetAll(SetsFilter filter);
    Task<(SetsDto? user, string? error)> GetById(Guid id);
    Task<(SetsDto? setDto, string? error)> Add(SetsForm setsForm);
    Task<(SetsDto? setDto, string? error)> Update(SetsUpdate setsUpdate, Guid Id);
    Task<(Set? set, string? error)> Delete(Guid id); 
}

public class SetsService : ISetsService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    public SetsService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        
    }
    public async Task<(SetsDto? user, string? error)> GetById(Guid id)
    {
        var sets = await _repositoryWrapper.Sets.GetById(id);
        if(sets == null) return (null,"not found");
         var setsDto = _mapper.Map<SetsDto>(sets);
        return (setsDto ,null);

    }
    
    public async Task<(Set? set, string? error)> Delete(Guid id)
    {

        var set = await _repositoryWrapper.Sets.Get<SetsDto>(u => u.Id == id);
        if (set == null) return (null, "already deleted") ;
        var deleteSet = await _repositoryWrapper.Sets.Delete(id);
        return (deleteSet, null);

    }


    public async Task<(SetsDto? setDto, string? error)> Add(SetsForm setsForm)
    {
        var newSet = _mapper.Map<Set>(setsForm);
        var result = await _repositoryWrapper.Sets.Add(newSet);
        if(result == null) return (null, "Error Adding Entity");
        var SetDto = _mapper.Map<SetsDto>(result);

        return (SetDto,null);

    }
    public async Task<(SetsDto? setDto, string? error)> Update(SetsUpdate setsUpdate, Guid Id)
    {
        var set = await _repositoryWrapper.Sets.Get(u => u.Id == Id);
        if (set == null) return (null, "not found");
        _mapper.Map(setsUpdate, set);


        await _repositoryWrapper.Sets.Update(set);

        var setDto = _mapper.Map<SetsDto>(set);
        
        return (setDto, null);
    }
    
    public async Task<(List<SetsDto>? setsDtos, int? totalCount, string? error)> GetAll(SetsFilter filter)
    {
        var (sets, totalCount) = await _repositoryWrapper.Sets.GetAll<SetsDto>(filter.PageNumber, filter.PageSize);
        return (sets, totalCount, null);
    }


}
