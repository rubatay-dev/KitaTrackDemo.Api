using System;
using KitaTrackDemo.Api.Common;
using KitaTrackDemo.Api.Models.DTOs;

namespace KitaTrackDemo.Api.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponseDto>> Register(RegisterRequestDto request);
    Task<Result<AuthResponseDto>> Login(LoginRequestDto request);
}
