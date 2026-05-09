using SchoolManagement.Application.Common;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> RegisterAsync(RegisterDto dto);

        Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto dto);

        Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto);
    }
}
