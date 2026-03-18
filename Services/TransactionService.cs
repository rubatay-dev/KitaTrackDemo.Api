using Microsoft.AspNetCore.Mvc.ModelBinding;
using KitaTrackDemo.Api.Common;
using KitaTrackDemo.Api.Interfaces;
using KitaTrackDemo.Api.Models.DTOs;
using KitaTrackDemo.Api.Models.Entities;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace KitaTrackDemo.Api.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repo;
    private readonly ITransactionTypeRepository _typeRepo;
    public TransactionService(ITransactionRepository repo, ITransactionTypeRepository typeRepo)
    {
        _repo = repo;
        _typeRepo = typeRepo;
    }
    public async Task<Result<TransactionResponseDto>> CreateAsync(CreateTransactionRequestDto request, Guid userId)
    {
        if (userId == Guid.Empty)
            return Result<TransactionResponseDto>.Failure("User Id is required.");
        if (!request.TransactionTypeId.HasValue)
            return Result<TransactionResponseDto>.Failure("Transaction Type Id is required.");
        var type = await _typeRepo.GetByIdAsync(request.TransactionTypeId.Value);
        if (type == null)
            return Result<TransactionResponseDto>.Failure("Invalid Transaction Type Id");            
        if (request.Amount <= 0)
            return Result<TransactionResponseDto>.Failure("Amount should be greater than zero.");
        if (request.Fee < 0)
            return Result<TransactionResponseDto>.Failure("Fee should be greater than or equal to zero.");

        var entity = new Transaction
        {
            UserId = userId,
            Date = request.Date,
            TransactionTypeId = request.TransactionTypeId,
            Reference = request.Reference,
            Amount = request.Amount,
            Fee = request.Fee
        };

        await _repo.AddAsync(entity);

        var isSuccess = await _repo.SaveChangesAsync();

        if (!isSuccess)

            return Result<TransactionResponseDto>.Failure("Transaction not added.");
        
        entity.TransactionType = type;
        return Result<TransactionResponseDto>.Success(MapToResponse(entity));
    }

    public async Task<Result<TransactionResponseDto>> EditAsync(EditTransactionRequestDto request, Guid userId)
    {
        if (userId == Guid.Empty)
            return Result<TransactionResponseDto>.Failure("User Id is required.");

        var existing = await _repo.GetByIdAsync(request.Id, userId);

        if (existing == null)
            return Result<TransactionResponseDto>.Failure("Transaction not found.");
        if (!request.TransactionTypeId.HasValue)
            return Result<TransactionResponseDto>.Failure("Transaction Type Id is required.");
        var type = await _typeRepo.GetByIdAsync(request.TransactionTypeId.Value);
        if (type == null)
            return Result<TransactionResponseDto>.Failure("Invalid Transaction Type Id"); 
        if (request.Amount <= 0)
            return Result<TransactionResponseDto>.Failure("Amount should be greater than zero.");
        if (request.Fee < 0)
            return Result<TransactionResponseDto>.Failure("Fee should be greater than or equal to zero.");

        existing.Date = request.Date;
        existing.TransactionTypeId = request.TransactionTypeId;
        existing.Reference = request.Reference;
        existing.Amount = request.Amount;
        existing.Fee = request.Fee;

        //_repo.Update(existing);

        var isSuccess = await _repo.SaveChangesAsync();

        if (!isSuccess)
            return Result<TransactionResponseDto>.Failure("Transaction not updated.");

        existing.TransactionType = type;
        return Result<TransactionResponseDto>.Success(MapToResponse(existing));
    }
    public async Task<Result<Guid>> DeleteAsync(Guid id, Guid userId)
    {
        
        if (userId == Guid.Empty)
            return Result<Guid>.Failure("User Id is required.");

        var existing = await _repo.GetByIdAsync(id, userId);

        if (existing == null)
            return Result<Guid>.Failure("Transaction not found.");

        _repo.Delete(existing);
        var isSuccess = await _repo.SaveChangesAsync();

        if (!isSuccess)
            return Result<Guid>.Failure("Transaction not deleted.");

        return Result<Guid>.Success(id);
    }


    private static TransactionResponseDto MapToResponse(Transaction entity) => new()
    {
            Id = entity.Id,
            Date = entity.Date,
            TransactionType = entity.TransactionType,
            Reference = entity.Reference,
            Amount = entity.Amount,
            Fee = entity.Fee
    };
}
