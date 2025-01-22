using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace to_do_list_api.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? ProjectId { get; set; }

        public Project? Project { get; set; }

        public int? TaskId { get; set; }

        public Tasks? Task { get; set; } = null!;
    }
}