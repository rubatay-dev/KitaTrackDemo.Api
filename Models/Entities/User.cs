using System;

namespace KitaTrackDemo.Api.Models.Entities;

public class User
{
    public Guid Id { get; set; }
    public String Email { get; set; } = string.Empty;
    public String PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
