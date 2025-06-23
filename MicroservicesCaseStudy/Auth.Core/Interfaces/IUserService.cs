using Auth.Core.DTOs;
using Auth.Core.Entities;
using Auth.Core.Models;
using System.Threading.Tasks;

namespace Auth.Core.Interfaces
{
    public interface IUserService
    {
        Task<DTOs.TokenResponse> RegisterAsync(RegisterRequest request);
        Task<DTOs.TokenResponse> LoginAsync(LoginRequest request);
        Task<DTOs.TokenResponse> RefreshTokenAsync(RefreshTokenRequest request);

        //Task<string> RegisterAsync(RegisterRequest request);
        //Task<string> LoginAsync(LoginRequest request);
    }
}