using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using KitaTrackDemo.Api.Data;
using KitaTrackDemo.Api.Interfaces;
using KitaTrackDemo.Api.Models.DTOs;
using KitaTrackDemo.Api.Models.Entities;

namespace KitaTrackDemo.Api.Repositories;

[Authorize]
public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Write      
    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
    }

    public void Update(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
    }    

    public void Delete(Transaction transaction)
    {
        _context.Transactions.Remove(transaction);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    #endregion


    #region Read
    public async Task<Transaction> GetByIdAsync(Guid id, Guid userId)
    {
        return await _context.Transactions
            .Include(t => t.TransactionType)
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
    } 

    public async Task<Transaction> GetByIdWithNoTrackingAsync(Guid id, Guid userId)
    {
        return await _context.Transactions
            .Include(t => t.TransactionType)
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
    }     
        
    public async Task<(IEnumerable<Transaction> Items, int TotalRecords)> GetHistoryAsync(TransactionFilterDto filter, Guid userId)
    {
        var query = _context.Transactions
            .Include(t => t.TransactionType)
            .Where(t => t.UserId == userId)
            .AsNoTracking()
            .AsQueryable();

       if (filter.StartDate.HasValue)
            query = query.Where(t => t.Date >= filter.StartDate);
        if (filter.EndDate.HasValue)
            query = query.Where(t => t.Date <= filter.EndDate);
        if (filter.TypeId != Guid.Empty)
            query = query.Where(t => t.TransactionTypeId == filter.TypeId);
        if (!string.IsNullOrWhiteSpace(filter.Reference))
            query = query.Where(t => t.Reference.Contains(filter.Reference));

        var totalRecords = await query.CountAsync();

        query = query
            .OrderByDescending(t => t.Date)
            .ThenByDescending(t => t.CreatedAt)
            .ThenByDescending(t => t.UpdatedAt);

        var items = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return (items, totalRecords);
    }

    #endregion
}
