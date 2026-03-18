namespace KitaTrackDemo.Api.Models.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }
    public virtual User User { get; set; }


    public DateTime Date { get; set; } = DateTime.UtcNow;
    public Guid? TransactionTypeId { get; set; }
    public TransactionType? TransactionType { get; set; }
    public string Reference { get; set; } = string.Empty;
    public double Amount { get; set; }
    public double Fee { get; set; }
}
