using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.DTOs
{
    public class RefreshTokenRequest
    {
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
    }
}
