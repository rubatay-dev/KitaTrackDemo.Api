using KitaTrackDemo.Api.Models.Entities;

namespace KitaTrackDemo.Api.Interfaces;

public interface ITransactionTypeRepository
{
    Task<TransactionType> GetByIdAsync(Guid id);
    Task<IEnumerable<TransactionType>> GetAllAsync();
}
