﻿namespace MotorDoctor.Business.Dtos;

public class OrderGetDto : IDto
{
    public int Id { get; set; }
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
    public List<OrderItemGetDto> OrderItems { get; set; } = [];
    public decimal TotalPrice { get; set; }
    public decimal DiscountedTotalPrice { get; set; }
    public StatusGetDto Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public UserGetDto AppUser { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public bool IsPaid { get; set; }
    public PaymentGetDto? Payment { get; set; }
}

