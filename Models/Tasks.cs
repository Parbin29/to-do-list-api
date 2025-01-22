using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace to_do_list_api.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTime Deadline { get; set; }

        public ICollection<Tag>? Tags { get; set; } = new List<Tag>(); 

        public int? ProjectId { get; set; }
        public Project? Project { get; set; } = null!;
    }
}