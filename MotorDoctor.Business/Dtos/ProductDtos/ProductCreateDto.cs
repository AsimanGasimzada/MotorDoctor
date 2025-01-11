﻿using Microsoft.AspNetCore.Http;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.Dtos;

public class ProductCreateDto : IDto
{
    public string Code { get; set; } = null!;
    public List<CategoryRelationDto>? Categories { get; set; } = [];
    public List<int> CategoryIds { get; set; } = [];
    public List<BrandRelationDto>? Brands { get; set; } = [];
    public int BrandId { get; set; }
    public string Slug { get; set; } = null!;
    public IFormFile MainImage { get; set; } = null!;
    public List<IFormFile> Images { get; set; } = [];
    public List<ProductSizeCreateDto> ProductSizes { get; set; } = [];
    public List<ProductDetailCreateDto> ProductDetails { get; set; } = [];
}
