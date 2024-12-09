using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EvaluationBackend.DATA;
using EvaluationBackend.DATA.DTOs.Courses;
using EvaluationBackend.Entities;
using EvaluationBackend.Repository;
using Microsoft.EntityFrameworkCore;

namespace EvaluationBackend.Services;
public interface ICourseService
{
    Task<(List<CourseDto>? exerciseDtos, int? totalCount, string? error)> GetAllCourses(CourseFilter filter);
    Task<(CourseDto? exerciseDto, string? error)> GetCourseById(Guid id);
    Task<(CourseDto? exerciseDto, string? error)> AddCourse(CourseForm Form);
    Task<(CourseDto? exerciseDto, string? error)> UpdateCourse(CourseUpdate Update, Guid Id);
    Task<(Course? course, string? error)> DeleteCourse(Guid id);
    Task<(Day? day, string? error)> DeleteDay(Guid id);
    Task<(DayExercise? dayExercise, string? error)> DeleteExercise(Guid id);

    // Task<(List<DayExerciseDto>? dayDtos, int? totalCount, string? error)> GetAllDayExercises(DayExerciseFilter filter);

}

public class CourseService : ICourseService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CourseService(IRepositoryWrapper repositoryWrapper, IMapper mapper, DataContext context)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _context = context;

    }

    public async Task<(CourseDto? exerciseDto, string? error)> AddCourse(CourseForm Form)
    {

        var section = await _repositoryWrapper.Section.GetById(Form.SectionId);
        if (section == null) return (null, "no sections found");


        var course = new Course
        {
            Name = Form.Name,
            SectionId = Form.SectionId,
        };

        await _context.Courses.AddAsync(course);
        if (await _context.SaveChangesAsync() <= 0) return (null, "error adding course");



        // var dayList = new List<Day>();
        foreach (var day in Form.Days)
        {
            var newDay = new Day
            {
                CourseId = course.Id,
                DaySeq = day.DaySeq

            };
            await _context.Days.AddAsync(newDay);
            if (await _context.SaveChangesAsync() <= 0) return (null, "error adding day");



            if (day.Exercises == null || !day.Exercises.Any())
                return (null, "no exercises");


            var exerciseList = new List<DayExercise>();
            foreach (var exercise in day.Exercises)
            {
                var muscle = await _repositoryWrapper.Muscle.GetById(exercise.MuscleId);
                if (muscle == null) return (null, "no muscle found");
                var exercise1 = await _repositoryWrapper.Exercise.GetById(exercise.ExerciseId);
                if (exercise1 == null) return (null, "no exercise found");
                var set1 = await _repositoryWrapper.Sets.GetById(exercise.SetsId);
                if (set1 == null) return (null, "no sets found");

                var newExercise = new DayExercise
                {
                    DayId = newDay.Id,
                    MuscleId = muscle.Id,
                    ExerciseId = exercise1.Id,
                    SetsId = set1.Id,
                    Super = exercise.Super,
                };
                if (newExercise.Super == true)
                {
                    exercise1.ExerciseId = exercise.Exercise2Id;
                    set1.SetId = exercise.Sets2Id;
                    // await _repositoryWrapper.Exercise.Update(exercise1);
                    // await _repositoryWrapper.Sets.Update(set1);

                }
                exerciseList.Add(newExercise);
            }

            await _context.DayExercises.AddRangeAsync(exerciseList);
            if (await _context.SaveChangesAsync() <= 0) return (null, "error2");

        }
        var days = _context.Days.Where(x => x.CourseId == course.Id);
        course.DayCount = days.Count();
        await _repositoryWrapper.Course.Update(course);

        var result = _mapper.Map<CourseDto>(course);
        if (result == null) return (null, "error mapping");

        return (result, null);

    }




    public async Task<(Course? course, string? error)> DeleteCourse(Guid id)
    {
        var course = await _repositoryWrapper.Course.Get<CourseDto>(u => u.Id == id);
        if (course == null) return (null, "already deleted");
        var deleteCourse = await _repositoryWrapper.Course.Delete(id);
        return (deleteCourse, null);
    }

    public async Task<(Day? day, string? error)> DeleteDay(Guid id)
    {
        var day = await _repositoryWrapper.Day.Get<DayDto>(u => u.Id == id);
        if (day == null) return (null, "already deleted");
        var deleteDay = await _repositoryWrapper.Day.Delete(id);
        return (deleteDay, null);
    }

    public async Task<(DayExercise? dayExercise, string? error)> DeleteExercise(Guid id)
    {
        var exercise = await _repositoryWrapper.DayExercise.Get<DayExerciseDto>(u => u.Id == id);
        if (exercise == null) return (null, "already deleted");
        var deleteExercise = await _repositoryWrapper.DayExercise.Delete(id);
        return (deleteExercise, null);
    }

    public async Task<(List<CourseDto>? exerciseDtos, int? totalCount, string? error)> GetAllCourses(CourseFilter filter)
    {

        var (courses, totalCount) = await _repositoryWrapper.Course.GetAll<CourseDto>(
            x =>
            (filter.SectionId == null || x.SectionId == filter.SectionId) &&
            (filter.MuscleId == null || x.Days!.Any(day => day.Exercises!
                    .Any(exercise => exercise.MuscleId == filter.MuscleId))) &&
            (filter.ExerciseId == null || x.Days!.Any(day => day.Exercises!
                    .Any(exercise => exercise.ExerciseId == filter.ExerciseId)))
            , filter.PageNumber, filter.PageSize);
        return (courses, totalCount, null);
    }




    public async Task<(CourseDto? exerciseDto, string? error)> GetCourseById(Guid id)
    {
        var course = await _repositoryWrapper.Course.Get<CourseDto>(x => x.Id == id);
        if (course == null) return (null, "not found");
        return (course, null);
    }


    public async Task<(CourseDto? exerciseDto, string? error)> UpdateCourse(CourseUpdate Update, Guid Id)
    {
        if (Update.SectionId != null)
        {
            var section = await _repositoryWrapper.Section.Get(x => x.Id == Update.SectionId);
            if (section == null) return (null, "section not found");
        }
        var course = await _context.Courses
            .Include(u => u.SectionName)
            .Include(x => x.Days)!
                .ThenInclude(x => x.Exercises)!
                    .AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        if (course == null) return (null, " course not found");
        if (Update.Name != null) course.Name = Update.Name;
        if (Update.SectionId != null) course.SectionId = Update.SectionId;


        if (Update.Days != null)
        {

            foreach (var dayUpdate in Update.Days)
            {

                if (course.Days == null) return (null, "course has no days");

                var day = course.Days.FirstOrDefault(x => x.Id == dayUpdate.Id);
                if (day != null)
                {
                    if (day.DaySeq != dayUpdate.DaySeq && dayUpdate != null)
                    {
                        var daySeq = course.Days.FirstOrDefault(x => x.DaySeq == dayUpdate.DaySeq);
                        if (daySeq != null)
                            daySeq.DaySeq = day.DaySeq;
                    }
                    _mapper.Map(dayUpdate, day);
                }
                else if (day == null)
                {
                    if (dayUpdate.Id == null && dayUpdate.Id != Guid.Empty)
                    {
                        day = _mapper.Map<Day>(dayUpdate);
                        course.Days.Add(day);
                        await _context.SaveChangesAsync();

                    }
                    if (dayUpdate.Id == Guid.Empty)
                    {
                        return (null, "wrong id for day");
                    }
                }



                if (dayUpdate!.Exercises != null)

                {
                    if (day == null) return (null, "day not found");


                    foreach (var exerciseUpdate in dayUpdate.Exercises)
                    {

                        var set = await _repositoryWrapper.Sets.Get(x => x.Id == exerciseUpdate.SetsId);
                        var exer = await _repositoryWrapper.Exercise.Get(x => x.Id == exerciseUpdate.ExerciseId);
                        if (exerciseUpdate.SetsId != null)
                        {
                            if (set == null) return (null, "set not found");
                        }
                        if (exerciseUpdate.ExerciseId != null)
                        {
                            if (exer == null) return (null, "exercise not found");
                        }
                        if (day.Exercises == null) return (null, "day has no exercise");
                        var exercise = day.Exercises.FirstOrDefault(x => x.Id == exerciseUpdate.Id);
                        if (exercise != null)
                        {
                            _mapper.Map(exerciseUpdate, exercise);
                        }
                        if (exercise == null)
                        {
                            if (exerciseUpdate.Id == null && exerciseUpdate.Id != Guid.Empty)
                            {
                                exercise = _mapper.Map<DayExercise>(exerciseUpdate);
                                day.Exercises.Add(exercise);
                                await _context.SaveChangesAsync();

                            }
                            if (dayUpdate.Id == Guid.Empty)
                            {
                                return (null, "wrong id for day");
                            }
                        }


                        if (exerciseUpdate.Super == true)
                        {
                            if (exerciseUpdate.Sets2Id != null) set.SetId = exerciseUpdate.Sets2Id;
                            _context.Sets.Update(set);
                            if (exerciseUpdate.Exercise2Id != null) exer.ExerciseId = exerciseUpdate.Exercise2Id;
                            _context.Exercises.Update(exer);
                        }

                    }
                }

            }
        }

        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
        Console.WriteLine($" Name: {course.SectionName}");

        var courseDto = await _repositoryWrapper.Course.Get<CourseDto>(u => u.Id == Id);
        ;

        return (courseDto, null);
    }


}
