using System;
using KitaTrackDemo.Api.Models.Entities;

namespace KitaTrackDemo.Api.Interfaces;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}
