using System;

namespace KitaTrackDemo.Api.Models.DTOs;

public class GetAllTransactionTypesResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
