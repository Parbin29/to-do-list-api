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
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public string Status { get; set; } = "Active";

        public List<Models.Task> Tasks { get; set; } = new List<Models.Task>();

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}