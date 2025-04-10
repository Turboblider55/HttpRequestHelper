using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestService.Models
{
    internal class LoginResponse
    {
        public string accessToken { get; set; } = string.Empty;

        public long expiresAt { get; set; }

        public string refreshToken { get; set; } = string.Empty;

        public long refreshExpiresAt { get; set; }

        public string tokenType { get; set; } = string.Empty;

        public string idToken { get; set; } = string.Empty;

    }
}
