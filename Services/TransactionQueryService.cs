using System;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using KitaTrackDemo.Api.Common;
using KitaTrackDemo.Api.Common.Pagination;
using KitaTrackDemo.Api.Interfaces;
using KitaTrackDemo.Api.Models.DTOs;
using KitaTrackDemo.Api.Repositories;

namespace KitaTrackDemo.Api.Services;

public class TransactionQueryService : ITransactionQueryService
{
    private readonly ITransactionRepository _repo;
    private readonly ITransactionTypeRepository _typeRepo;
    public TransactionQueryService(ITransactionRepository repo, ITransactionTypeRepository typeRepo)
    {
        _repo = repo;
        _typeRepo = typeRepo;
    }

    public async Task<Result<TransactionResponseDto>> GetByIdAsync(Guid id, Guid userId)
    {
        var existing = await _repo.GetByIdAsync(id, userId);

        if (existing == null)
            return Result<TransactionResponseDto>.Failure("Transaction not found.");

        var type = await _typeRepo.GetByIdAsync((Guid)existing.TransactionTypeId);

        var response = new TransactionResponseDto
        {
          Id = existing.Id,
          Date = existing.Date,
          TransactionType = type,
          Reference = existing.Reference,
          Amount = existing.Amount,
          Fee = existing.Fee  
        };

        return Result<TransactionResponseDto>.Success(response);
    }

    public async Task<Result<PagedResponse<GetHistoryResponseDto>>> GetHistoryAsync(TransactionFilterDto filter, Guid userId)
    {
        var (items, totalRecords) = await _repo.GetHistoryAsync(filter, userId);

        var data = items.Select(t => new GetHistoryResponseDto 
        {
                Id = t.Id,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt,
                Date = t.Date,
                TransactionType = t.TransactionType,
                Reference = t.Reference,
                Amount = t.Amount,
                Fee = t.Fee            
        });
        
        var response = PaginationHelper.Create(
            data,
            totalRecords,
            filter.PageNumber,
            filter.PageSize
        );

        return Result<PagedResponse<GetHistoryResponseDto>>.Success(response);
    }
}
