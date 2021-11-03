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
        
        public DbSet<User> Users { get; set; }
        public DbSet<Weight> Weights { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<DietFood> DietFoods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DietFood>()
                .HasKey(at => new { at.DietID, at.FoodID });

                modelBuilder.Entity<DietFood>()
                .HasOne(at => at.Diet)
                .WithMany(a => a.DietFoods)
                .HasForeignKey(at => at.DietID);

                modelBuilder.Entity<DietFood>()
                .HasOne(at => at.Food)
                .WithMany(t => t.DietFoods)
                .HasForeignKey(at => at.FoodID);

            modelBuilder.Entity<WorkoutExercise>()
                .HasKey(at => new { at.WorkoutID, at.ExerciseID });

                modelBuilder.Entity<WorkoutExercise>()
                .HasOne(at => at.Workout)
                .WithMany(a => a.WorkoutExercises)
                .HasForeignKey(at => at.WorkoutID);

                modelBuilder.Entity<WorkoutExercise>()
                .HasOne(at => at.Exercise)
                .WithMany(t => t.WorkoutExercises)
                .HasForeignKey(at => at.ExerciseID);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Weight>().ToTable("Weight");
        }

        public DbSet<StartToBike.Models.WorkoutExercise> WorkoutExercise { get; set; }
    }
}
