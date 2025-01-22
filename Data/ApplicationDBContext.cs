using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using to_do_list_api.Models;

namespace to_do_list_api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions)
        : base(dbContextOptions) // ApplicationDBContext(Parameters)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many Relationship
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks) // A Project has many Tasks
                .WithOne(t => t.Project) // Each Task has one Project
                .HasForeignKey(t => t.ProjectId) // Foreign Key in Tasks table
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}