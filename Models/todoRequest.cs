using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestService.Models
{
    public class todoRequest
    {
        public string title { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public string deadline { get; set; } = string.Empty;

        [Required]
        public bool completed { get; set; }
        public long parent { get; set; }

        [Required]
        public int priority { get; set; }
        public List<string> categories { get; set; } = new List<string>();

    }
}
