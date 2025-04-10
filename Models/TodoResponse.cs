using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestService.Models
{
    internal class TodoResponse
    {
        public long id { get; set; }

        public string title { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public string deadline { get; set; } = string.Empty;

        public bool completed { get; set; }

        public string ownerEmail { get; set; } = string.Empty;

        public long parentId { get; set; }

        public bool shared { get; set; }

        public priority priority { get; set; } = new priority();

        public List<string> categories { get; set; } = new List<string>();

        public string createdAt { get; set; } = string.Empty;

        public string updatedAt { get; set; } = string.Empty;



    }
}
