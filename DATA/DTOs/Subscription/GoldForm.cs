using System;

namespace EvaluationBackend.DATA.DTOs.Subscription;

public class GoldForm
{
    public required string Goal { get; set; }
    public required string UnWantedFood { get; set; }
    public required string Injurse { get; set; }
    public required int Age { get; set; }
    public required string Height { get; set; }
    public required string Weight { get; set; }
    public required string Work { get; set; }
    public required string Sleep { get; set; }
    public required bool UseHrmon { get; set; }
    public string? Hrmon { get; set; }
    public required string Notes { get; set; }
    public required List<string> Imges { get; set; }
    public required string Tests { get; set; }
    public required string GymName { get; set; }
    public required string Name { get; set; }
    public required string GymAddress { get; set; }
    public DateTime Expire { get; set; }



}
