using System;

namespace KitaTrackDemo.Api.Common.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try {
            await _next(context); // Try to run the API request
        }
        catch (Exception ex) {
            _logger.LogError(ex, "An unhandled exception occurred."); // Log it for the devs
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var response = new { 
                StatusCode = 500, 
                Message = "Internal Server Error. Please try again later." 
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}