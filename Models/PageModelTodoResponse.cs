using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestService.Models
{
    internal class PageModelTodoResponse
    {

        public List<TodoResponse> content { get; set; } = new List<TodoResponse>();

        public pageMetaData page { get; set; } = new pageMetaData();



    }
}
