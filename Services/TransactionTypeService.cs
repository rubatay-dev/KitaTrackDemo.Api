using System;
using KitaTrackDemo.Api.Common;
using KitaTrackDemo.Api.Interfaces;
using KitaTrackDemo.Api.Models.DTOs;

namespace KitaTrackDemo.Api.Services;

public class TransactionTypeService : ITransactionTypeService
{
    private readonly ITransactionTypeRepository _repo;
    public TransactionTypeService(ITransactionTypeRepository repo)
    {
      _repo = repo;  
    }

    public async Task<Result<IEnumerable<GetAllTransactionTypesResponse>>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();

        var response = items.Select(t => new GetAllTransactionTypesResponse
        {
            Id = (Guid)t.Id,
            Name = t.Name
        });

        return Result<IEnumerable<GetAllTransactionTypesResponse>>.Success(response.ToList());
    }
}
