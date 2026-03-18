using KitaTrackDemo.Api.Models.DTOs;
using KitaTrackDemo.Api.Models.Entities;

namespace KitaTrackDemo.Api.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> GetByIdAsync(Guid id, Guid userId);
    Task<(IEnumerable<Transaction> Items, int TotalRecords)> GetHistoryAsync(TransactionFilterDto filter, Guid userId);
    Task AddAsync(Transaction transaction);
    void Update(Transaction transaction);
    void Delete(Transaction transaction);
    Task<bool> SaveChangesAsync();
}
