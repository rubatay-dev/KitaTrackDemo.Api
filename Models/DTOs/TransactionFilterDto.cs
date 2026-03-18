using System;
using KitaTrackDemo.Api.Models.Entities;

namespace KitaTrackDemo.Api.Models.DTOs;

public class TransactionFilterDto
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid TypeId { get; set; }
    public String? Reference { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
