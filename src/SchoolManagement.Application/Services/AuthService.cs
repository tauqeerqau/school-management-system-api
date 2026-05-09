using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Application.Common;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ApiResponse<string>> RegisterAsync(RegisterDto dto)
        {
            var existingUser =
                await _userRepository.GetByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                return new ApiResponse<string>(
                    false,
                    "Email already exists",
                    null,
                    null
                );
            }

            var passwordHash =
                BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new AppUser
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Role = "User"
            };

            await _userRepository.AddAsync(user);

            return new ApiResponse<string>(
                true,
                "User registered successfully",
                null,
                null
            );
        }

        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto dto)
        {
            var user =
                await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
            {
                return new ApiResponse<AuthResponseDto>(
                    false,
                    "Invalid email or password",
                    null,
                    null
                );
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            if (!isPasswordValid)
            {
                return new ApiResponse<AuthResponseDto>(
                    false,
                    "Invalid email or password",
                    null,
                    null
                );
            }

            var token = GenerateJwtToken(user);

            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            user.RefreshTokenExpiryTime =
                DateTime.Now.AddDays(7);

            await _userRepository.UpdateAsync(user);

            var response = new AuthResponseDto
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                RefreshToken = refreshToken
            };

            return new ApiResponse<AuthResponseDto>(
                true,
                "Login successful",
                response,
                null
            );
        }

        public async Task<ApiResponse<AuthResponseDto>>
    RefreshTokenAsync(
        RefreshTokenRequestDto dto)
        {
            var user =
                await _userRepository
                    .GetByRefreshTokenAsync(dto.RefreshToken);

            if (user == null)
            {
                return new ApiResponse<AuthResponseDto>(
                    false,
                    "Invalid refresh token",
                    null,
                    null
                );
            }

            if (user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new ApiResponse<AuthResponseDto>(
                    false,
                    "Refresh token expired",
                    null,
                    null
                );
            }

            var newJwtToken = GenerateJwtToken(user);

            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            user.RefreshTokenExpiryTime =
                DateTime.Now.AddDays(7);

            await _userRepository.UpdateAsync(user);

            var response = new AuthResponseDto
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };

            return new ApiResponse<AuthResponseDto>(
                true,
                "Token refreshed successfully",
                response,
                null
            );
        }

        private string GenerateJwtToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Username),

                new Claim(
                    ClaimTypes.Email,
                    user.Email),

                new Claim(
                    ClaimTypes.Role,
                    user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
