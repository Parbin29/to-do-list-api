using System.ComponentModel.DataAnnotations;

namespace to_do_list_api.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public DateTime Deadline { get; set; }

        public int? ProjectId { get; set; }
        public Project? Project { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
        
    }
}   