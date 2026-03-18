using System;
using System.ComponentModel.DataAnnotations;

namespace KitaTrackDemo.Api.Models.DTOs;

public class RegisterRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
