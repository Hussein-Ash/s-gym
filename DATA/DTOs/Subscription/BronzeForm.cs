using System;

namespace EvaluationBackend.DATA.DTOs.Subscription;

public class BronzeForm
{
    public required string Since { get; set; }
    public required string Height { get; set; }
    public required string Weight { get; set; }
    public List<string>? Imges { get; set; }
    public required string GymName { get; set; }
    public string? Name { get; set; }
    public required string GymAddress { get; set; }



}
