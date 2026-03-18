using KitaTrackDemo.Api.Common;
using KitaTrackDemo.Api.Common.Pagination;
using KitaTrackDemo.Api.Models.DTOs;

namespace KitaTrackDemo.Api.Interfaces;

public interface ITransactionQueryService
{
    Task<Result<TransactionResponseDto>> GetByIdAsync(Guid id, Guid userId);
    Task<Result<PagedResponse<GetHistoryResponseDto>>> GetHistoryAsync(TransactionFilterDto filter, Guid userId);
}