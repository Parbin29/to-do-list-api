using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace to_do_list_api.Models
{
    public class CreateTaskDto
    {
        public string Name { get; set; } = string.Empty;

        public DateTime Deadline { get; set; }

    }
}