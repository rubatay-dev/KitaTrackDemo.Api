using KitaTrackDemo.Api.Common;
using KitaTrackDemo.Api.Models.DTOs;

namespace KitaTrackDemo.Api.Interfaces;

public interface ITransactionService
{
    Task<Result<TransactionResponseDto>> CreateAsync(CreateTransactionRequestDto request, Guid userId);
    Task<Result<TransactionResponseDto>> EditAsync(EditTransactionRequestDto request, Guid userId);
    Task<Result<Guid>> DeleteAsync(Guid id, Guid userId);
}
