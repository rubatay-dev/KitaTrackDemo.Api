using KitaTrackDemo.Api.Models.Entities;

namespace KitaTrackDemo.Api.Models.DTOs;

public class TransactionResponseDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public TransactionType? TransactionType { get; set; }
    public string Reference { get; set; } = string.Empty;    
    public double Amount { get; set; }
    public double Fee { get; set; }
}