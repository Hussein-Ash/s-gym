using System;

namespace EvaluationBackend.DATA.DTOs.Exercises;

public class ExerciseDto : BaseDto<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? YouTubeLink { get; set; }
    public string? Img { get; set; }
    public string? MuscleName { get; set; }

}
