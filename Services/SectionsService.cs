using System;
using AutoMapper;
using EvaluationBackend.DATA.DTOs.Sections;
using EvaluationBackend.Entities;
using EvaluationBackend.Repository;

namespace EvaluationBackend.Services;
public interface ISectionsService
{
    Task<(List<SectionDto>? sectionDtos, int? totalCount, string? error)> GetAll(SectionFilter filter);
    Task<(SectionDto? user, string? error)> GetById(Guid id);
    Task<(SectionDto? sectionDto, string? error)> Add(SectionForm sectionForm);
    Task<(SectionDto? sectionDto, string? error)> Update(SectionUpdate sectionUpdate, Guid Id);
    Task<(Section? section, string? error)> Delete(Guid id);

}

public class SectionsService : ISectionsService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    public SectionsService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        
    }
    public async Task<(SectionDto? user, string? error)> GetById(Guid id)
    {
        var section = await _repositoryWrapper.Section.GetById(id);
        if(section == null) return (null,"not found");
         var sectionDto = _mapper.Map<SectionDto>(section);
        return (sectionDto ,null);

    }
    
    public async Task<(Section? section, string? error)> Delete(Guid id)
    {

        var section = await _repositoryWrapper.Section.Get<SectionDto>(u => u.Id == id);
        if (section == null) return (null, "already deleted") ;
        var deleteSection = await _repositoryWrapper.Section.Delete(id);
        return (deleteSection, null);

    }


    public async Task<(SectionDto? sectionDto, string? error)> Add(SectionForm sectionForm)
    {
        var newSection = _mapper.Map<Section>(sectionForm);
        var result = await _repositoryWrapper.Section.Add(newSection);
        if(result == null) return (null, "Error Adding Entity");
        var sectionDto = _mapper.Map<SectionDto>(result);

        return (sectionDto,null);

    }
    public async Task<(SectionDto? sectionDto, string? error)> Update(SectionUpdate sectionUpdate, Guid Id)
    {
        var section = await _repositoryWrapper.Section.Get(u => u.Id == Id);
        if (section == null) return (null, "not found");
        _mapper.Map(sectionUpdate, section);


        await _repositoryWrapper.Section.Update(section);

        var sectionDto = _mapper.Map<SectionDto>(section);
        
        return (sectionDto, null);
    }
    
    public async Task<(List<SectionDto>? sectionDtos, int? totalCount, string? error)> GetAll(SectionFilter filter)
    {
        var (section, totalCount) = await _repositoryWrapper.Section.GetAll<SectionDto>(
            x=> 
            filter.Name == null || x.Name!.Contains(filter.Name)
            ,filter.PageNumber, filter.PageSize);
        return (section, totalCount, null);
    }

}
