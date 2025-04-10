using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestService.Models
{
    internal class SearchRequest
    {

        public string search { get; set; } = string.Empty;

        public string sort { get; set; } = string.Empty;

        public int pageNumber { get; set; }

        public int pageSize { get; set; }

    }
}
