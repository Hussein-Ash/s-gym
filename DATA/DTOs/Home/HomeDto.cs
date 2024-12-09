using System;

namespace EvaluationBackend.DATA.DTOs.Home;

public class HomeDto : BaseDto<Guid>
{
    public string? Title { get; set; }
    public string? HeroSubTitle { get; set; }
    public string? About { get; set; }
    public string? ApplicantsHero { get; set; }
}
