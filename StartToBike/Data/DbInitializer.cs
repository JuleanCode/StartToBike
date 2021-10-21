using StartToBike.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartToBike.Data
{
    public class DbInitializer
    {
        public static void Initialize(StartToBikeContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var Users = new User[]
            {
                new User{ID=1,Name="Admin",Email="Admin@admin.com",Password="P@ssw0rd",Sex=1,Height=175,Age=25,WeightGoal=75,Rol=1},
                new User{ID=2,Name="Julean",Email="Julean@admin.com",Password="P@ssw0rd",Sex=1,Height=175,Age=25,WeightGoal=75,Rol=2},
                new User{ID=3,Name="Stef",Email="Stef@admin.com",Password="P@ssw0rd",Sex=1,Height=175,Age=25,WeightGoal=75,Rol=2},
                new User{ID=4,Name="Broertje",Email="Broertje@admin.com",Password="P@ssw0rd",Sex=1,Height=175,Age=25,WeightGoal=75,Rol=2}
            };
            foreach (User u in Users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var Weights = new Weight[]
            {
                new Weight{ID=1,Content=80,UserID=2},
                new Weight{ID=2,Content=80,UserID=3},
                new Weight{ID=3,Content=80,UserID=4}
            };
            foreach (Weight w in Weights)
            {
                context.Weights.Add(w);
            }
            context.SaveChanges();

            var Diets = new Diet[]
            {
                new Diet{ID=1,UserID=2},
                new Diet{ID=2,UserID=3},
                new Diet{ID=3,UserID=4}
            };
            foreach (Diet d in Diets)
            {
                context.Diets.Add(d);
            }
            context.SaveChanges();

            var Foods = new Food[]
            {
                new Food{ID=1,Description="Pizza",Calories=900},
                new Food{ID=2,Description="Kwark",Calories=450}
            };
            foreach (Food f in Foods)
            {
                context.Foods.Add(f);
            }
            context.SaveChanges();

            var Workouts = new Workout[]
            {
                new Workout{ID=1,UserID=2},
                new Workout{ID=2,UserID=3},
                new Workout{ID=3,UserID=4}
            };
            foreach (Workout w in Workouts)
            {
                context.Workouts.Add(w);
            }
            context.SaveChanges();

            var Exercises = new Exercise[]
            {
                new Exercise{ID=1,Description="Pull Ups",Reps=5,Sets=5},
                new Exercise{ID=2,Description="Push Ups",Reps=29,Sets=5}
            };
            foreach (Exercise e in Exercises)
            {
                context.Exercises.Add(e);
            }
            context.SaveChanges();
        }
    }
}
