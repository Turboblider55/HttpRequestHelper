using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestService.Models
{
    internal class TodoShareResponse
    {
        public string email { get; set; } = string.Empty;

        public accessLevel accessLevel { get; set; } = new accessLevel();



    }
}
