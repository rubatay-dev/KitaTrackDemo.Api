using KitaTrackDemo.Api.Data;
using KitaTrackDemo.Api.Interfaces;
using KitaTrackDemo.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace KitaTrackDemo.Api.Repositories;

public class TransactionTypeRepository : ITransactionTypeRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TransactionType>> GetAllAsync()
    {
        return await _context.TransactionTypes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TransactionType> GetByIdAsync(Guid id)
    {
        return await _context.TransactionTypes.FindAsync(id);
    }
 
}
