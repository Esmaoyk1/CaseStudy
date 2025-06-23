using Auth.Core.Entities;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        //string GenerateToken(ApplicationUser user);

        (string AccessToken, string RefreshToken) GenerateTokenWithRefresh(ApplicationUser user);
    }
}

