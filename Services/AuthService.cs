using System;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity.Data;
using KitaTrackDemo.Api.Common;
using KitaTrackDemo.Api.Interfaces;
using KitaTrackDemo.Api.Models.DTOs;
using KitaTrackDemo.Api.Models.Entities;

namespace KitaTrackDemo.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repo;
    private readonly ITokenService _token;
    public AuthService(IUserRepository repo, ITokenService token)
    {
        _repo = repo;
        _token = token;
    }

    public async Task<Result<AuthResponseDto>> Register(RegisterRequestDto request)
    {
        //get existing record by email
        var existing = await _repo.GetByEmailAsync(request.Email);

        //return error if not exists
        if (existing != null) 
            return Result<AuthResponseDto>.Failure("Email already exists.");

        //create new User wherein PH is using bcrypt PH
        var entity = new User
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        //save to db
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();

        //return response
        var loginRequest = new LoginRequestDto
        {
            Email = request.Email, 
            Password = request.Password
        };
        return await Login(loginRequest);
    }

    public async Task<Result<AuthResponseDto>> Login(LoginRequestDto request)
    {
        var existing = await _repo.GetByEmailAsync(request.Email);
        
        if (existing == null)
            return Result<AuthResponseDto>.Failure("Invalid credentials.");

        var isValid = BCrypt.Net.BCrypt.Verify(request.Password, existing.PasswordHash);
        if (!isValid)
            return Result<AuthResponseDto>.Failure("Invalid credentials.");

        var token = _token.GenerateToken(existing);

        var response = new AuthResponseDto
        {
          Email = existing.Email,
          Token = token  
        };

        return Result<AuthResponseDto>.Success(response);
    }
}
