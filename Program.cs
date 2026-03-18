using Microsoft.EntityFrameworkCore;
using KitaTrackDemo.Api.Data;
using KitaTrackDemo.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controller & API Setup
builder.Services.AddControllers();

// Custom Extension Methods
builder.Services.AddApplicationServices(); // Repos & Services
builder.Services.AddIdentityServices(builder.Configuration); // JWT Auth
builder.Services.AddSwaggerDocumentation(); // Swagger with Auth Button

//----------------------------------------------------------------------------------------------

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KitaTrack Demo API V1");
    c.RoutePrefix = string.Empty; 
});

if (app.Environment.IsDevelopment())
{
    app.ApplyInitialData();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();