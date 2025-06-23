using Auth.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse> RegisterAsync(string email, string password);
        Task<TokenResponse> LoginAsync(string email, string password);

    }
}
