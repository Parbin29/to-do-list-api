using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace to_do_list_api.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Status { get; set; } = "Active";

        public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}