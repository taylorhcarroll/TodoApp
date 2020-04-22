using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<TodoItem> TodoItem { get; set; }
        public DbSet<TodoStatus> TodoStatus { get; set; }

        //Seeds Database with some Todo Statuses
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoStatus>().HasData(
                new TodoStatus()
                {
                    Id = 1,
                    Title = "To Do"
                },
                new TodoStatus()
                {
                    Id = 2,
                    Title = "In Progress"
                },
                new TodoStatus()
                {
                    Id = 3,
                    Title = "Done"
                }
                );
        }
    }
}
