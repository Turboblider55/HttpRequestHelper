using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestService.Models
{
    internal class pageMetaData
    {

        public long size { get; set; }

        public long number { get; set; }

        public long totalElements { get; set; }

        public long totalPages { get; set; }

    }
}
