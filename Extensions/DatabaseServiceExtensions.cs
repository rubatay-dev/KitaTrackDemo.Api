using Microsoft.EntityFrameworkCore;
using KitaTrackDemo.Api.Data;

namespace KitaTrackDemo.Api.Extensions;

public static class DatabaseServiceExtensions
{
    public static void ApplyInitialData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (context.Database.IsInMemory()) context.Database.EnsureCreated();
    }
}