﻿namespace MotorDoctor.Core.Entities;

public class Language : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
    public ICollection<CategoryDetail> CategoryDetails { get; set; } = [];
    public ICollection<SliderDetail> SliderDetails { get; set; } = [];
}
