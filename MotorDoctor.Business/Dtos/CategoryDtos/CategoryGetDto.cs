﻿using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.Dtos;

public class CategoryGetDto : IDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public CategoryGetDto? Parent { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? ImagePath { get; set; }
    public List<CategoryRelationDto> Children { get; set; } = [];
    public List<ProductCategory> ProductCategories { get; set; } = [];
}



public class CategoryFeatureGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? ImagePath { get; set; }
    public List<ProductGetDto> Products { get; set; } = [];
}
