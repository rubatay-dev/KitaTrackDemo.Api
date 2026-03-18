using System;
using Microsoft.EntityFrameworkCore;
using KitaTrackDemo.Api.Data;
using KitaTrackDemo.Api.Interfaces;
using KitaTrackDemo.Api.Models.Entities;

namespace KitaTrackDemo.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == email);
    }    

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
