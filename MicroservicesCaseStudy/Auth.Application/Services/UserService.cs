using Auth.Core.DTOs;
using Auth.Core.Entities;
using Auth.Core.Interfaces;
using Auth.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IConfiguration config,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _config = config;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<TokenResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception("Kayıt başarısız: " + errors);
            }

            var tokens = _jwtTokenGenerator.GenerateTokenWithRefresh(user);
            await _userManager.UpdateAsync(user);

            return new TokenResponse
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new Exception("Giriş başarısız");

            var tokens = _jwtTokenGenerator.GenerateTokenWithRefresh(user);
            await _userManager.UpdateAsync(user);

            return new TokenResponse
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };
        }

        public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new Exception("Refresh token geçersiz veya süresi dolmuş.");

            var tokens = _jwtTokenGenerator.GenerateTokenWithRefresh(user);
            await _userManager.UpdateAsync(user);

            return new TokenResponse
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };
        }
    }
}
