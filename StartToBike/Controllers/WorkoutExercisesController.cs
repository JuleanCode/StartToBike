using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StartToBike.Data;
using StartToBike.Models;

namespace StartToBike.Controllers
{
    public class WorkoutExercisesController : Controller
    {
        private readonly StartToBikeContext _context;

        public WorkoutExercisesController(StartToBikeContext context)
        {
            _context = context;
        }

        // GET: WorkoutExercises
        public async Task<IActionResult> Index()
        {
            var startToBikeContext = _context.WorkoutExercise.Include(w => w.Exercise).Include(w => w.Workout);
            return View(await startToBikeContext.ToListAsync());
        }

        // GET: WorkoutExercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutExercise = await _context.WorkoutExercise
                .Include(w => w.Exercise)
                .Include(w => w.Workout)
                .FirstOrDefaultAsync(m => m.WorkoutID == id);
            if (workoutExercise == null)
            {
                return NotFound();
            }

            return View(workoutExercise);
        }

        // GET: WorkoutExercises/Create
        public IActionResult Create()
        {
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID");
            ViewData["WorkoutID"] = new SelectList(_context.Workouts, "ID", "ID");
            return View();
        }

        // POST: WorkoutExercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkoutID,ExerciseID")] WorkoutExercise workoutExercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workoutExercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID", workoutExercise.ExerciseID);
            ViewData["WorkoutID"] = new SelectList(_context.Workouts, "ID", "ID", workoutExercise.WorkoutID);
            return View(workoutExercise);
        }

        // GET: WorkoutExercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutExercise = await _context.WorkoutExercise.FindAsync(id);
            if (workoutExercise == null)
            {
                return NotFound();
            }
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID", workoutExercise.ExerciseID);
            ViewData["WorkoutID"] = new SelectList(_context.Workouts, "ID", "ID", workoutExercise.WorkoutID);
            return View(workoutExercise);
        }

        // POST: WorkoutExercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkoutID,ExerciseID")] WorkoutExercise workoutExercise)
        {
            if (id != workoutExercise.WorkoutID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutExercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutExerciseExists(workoutExercise.WorkoutID))
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
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ID", "ID", workoutExercise.ExerciseID);
            ViewData["WorkoutID"] = new SelectList(_context.Workouts, "ID", "ID", workoutExercise.WorkoutID);
            return View(workoutExercise);
        }

        // GET: WorkoutExercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutExercise = await _context.WorkoutExercise
                .Include(w => w.Exercise)
                .Include(w => w.Workout)
                .FirstOrDefaultAsync(m => m.WorkoutID == id);
            if (workoutExercise == null)
            {
                return NotFound();
            }

            return View(workoutExercise);
        }

        // POST: WorkoutExercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workoutExercise = await _context.WorkoutExercise.FindAsync(id);
            _context.WorkoutExercise.Remove(workoutExercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutExerciseExists(int id)
        {
            return _context.WorkoutExercise.Any(e => e.WorkoutID == id);
        }
    }
}
