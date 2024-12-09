using System;

namespace EvaluationBackend.DATA.DTOs.Exercises;

public class ExerciseUpdate
{

    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? YouTubeLink { get; set; }
    public string? Img { get; set; }
    public Guid? MuscleId { get; set; }



}
