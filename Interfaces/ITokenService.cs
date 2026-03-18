using KitaTrackDemo.Api.Models.Entities;

namespace KitaTrackDemo.Api.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
