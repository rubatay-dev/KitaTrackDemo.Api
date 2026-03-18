using KitaTrackDemo.Api.Interfaces;
using KitaTrackDemo.Api.Repositories;
using KitaTrackDemo.Api.Services;

namespace KitaTrackDemo.Api.Extensions;

public static class ApiServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITransactionTypeService, TransactionTypeService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ITransactionQueryService, TransactionQueryService>();

        return services;
    }
}