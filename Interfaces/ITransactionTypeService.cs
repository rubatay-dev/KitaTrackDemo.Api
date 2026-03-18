using System;
using KitaTrackDemo.Api.Common;
using KitaTrackDemo.Api.Models.DTOs;

namespace KitaTrackDemo.Api.Interfaces;

public interface ITransactionTypeService
{
    Task<Result<IEnumerable<GetAllTransactionTypesResponse>>> GetAllAsync();
}
