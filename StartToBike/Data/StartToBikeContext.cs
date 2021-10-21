using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StartToBike.Models;
using Microsoft.EntityFrameworkCore;

namespace StartToBike.Data
{
    public class StartToBikeContext : DbContext
    {
        public StartToBikeContext(DbContextOptions<StartToBikeContext> options) : base(options)
        {
        }

        public DbSet<Diet> Diets { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weight> Weights { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diet>().ToTable("Diet");
            modelBuilder.Entity<Exercise>().ToTable("Exercise");
            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Weight>().ToTable("Weight");
            modelBuilder.Entity<Workout>().ToTable("Workout");
        }
    }
}
