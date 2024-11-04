﻿using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class SliderUpdateDto : IDto
{
    public int Id { get; set; }
    public IFormFile? Image { get; set; }
    public string? ImagePath { get; set; }
    public List<SliderDetailUpdateDto> SliderDetails { get; set; } = [];
}