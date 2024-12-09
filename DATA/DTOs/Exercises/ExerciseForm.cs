using System;

namespace EvaluationBackend.DATA.DTOs.Exercises;

public class ExerciseForm
{
    
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? YouTubeLink { get; set; }
    public string? Img { get; set; }
    public required Guid MuscleId { get; set; }



}
