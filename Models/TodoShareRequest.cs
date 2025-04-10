using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestService.Models
{
    internal class TodoShareRequest
    {
        public string email { get; set; } = string.Empty;

        [Range(0,2)]
        public int accessLevel { get; set; }

    }
}
