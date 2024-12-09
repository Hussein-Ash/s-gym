using System;

namespace EvaluationBackend.DATA.DTOs.Subscription;

public class SilverForm
{
    public required string Goal { get; set; }
    public required string Injurse { get; set; }
    public required string Height { get; set; }
    public required string Weight { get; set; }
    public required string Notes { get; set; }
    public List<string>? Imges { get; set; }
    public required string GymName { get; set; }
    public string? Name { get; set; }
    public required string GymAddress { get; set; }
    public DateTime Expire { get; set; }



}
