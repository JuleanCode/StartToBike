using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StartToBike.Data;
using StartToBike.Models;

namespace StartToBike.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly StartToBikeContext _context;

        public WorkoutsController(StartToBikeContext context)
        {
            _context = context;
        }

        // GET: Workouts
        public async Task<IActionResult> Index()
        {
            var startToBikeContext = _context.Workouts.Include(w => w.User);
            return View(await startToBikeContext.ToListAsync());
        }

        // GET: Workouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workouts/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID");
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", workout.UserID);
            return View(workout);
        }

        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", workout.UserID);
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID")] Workout workout)
        {
            if (id != workout.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutExists(workout.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", workout.UserID);
            return View(workout);
        }

        // GET: Workouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.ID == id);
        }

        public async Task<IActionResult> UserWorkout()
        {
            var startToBikeContext = _context.Workouts.Where(d => d.UserID == Int32.Parse(HttpContext.Session.GetString("CurentUserID")))
                                        .Include(df => df.WorkoutExercises).ThenInclude(f => f.Exercise);
            return View(await startToBikeContext.ToListAsync());
        }

        public async Task<IActionResult> UserWorkoutAdd()
        {
            return View(await _context.Exercises.ToListAsync());
        }

        public async Task<IActionResult> UserWorkoutAddFood(int id)
        {
            var userWorkout = _context.Workouts.Where(d => d.UserID == Int32.Parse(HttpContext.Session.GetString("CurentUserID")));
            Workout temp = new Workout();

            foreach (Workout workout in userWorkout)
            {
                temp = workout;
            }


            WorkoutExercise workoutExercise = new WorkoutExercise();
            workoutExercise.ExerciseID = id;
            workoutExercise.WorkoutID = temp.ID;

            _context.Add(workoutExercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserWorkout));
        }

        public async Task<IActionResult> UserWorkoutDelete(int id)
        {
            var userWorkout = _context.Workouts.Where(d => d.UserID == Int32.Parse(HttpContext.Session.GetString("CurentUserID")));
            Workout temp = new Workout();

            foreach (Workout workout in userWorkout)
            {
                temp = workout;
            }

            var dep = _context.WorkoutExercises.Where(d => d.WorkoutID == temp.ID && d.ExerciseID == id).First();
            _context.WorkoutExercises.Remove(dep);
            _context.SaveChanges();

            return RedirectToAction(nameof(UserWorkout));
        }
    }
}
