﻿namespace MotorDoctor.Business.Dtos;

public class AttedanceDetailUpdateDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int LanguageId { get; set; }
}
