using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestService.Models
{
    internal class LoginRequest
    {
        
        [MinLength(1)]
        public string code { get; set; } = string.Empty;
        [MinLength(1)]
        public string codeVerifier { get; set; } = string.Empty;
        [MinLength(1)]
        public string redirectUri { get; set; } = string.Empty;
        
    }
}
