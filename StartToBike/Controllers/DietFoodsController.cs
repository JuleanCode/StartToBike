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
    public class DietFoodsController : Controller
    {
        private readonly StartToBikeContext _context;

        public DietFoodsController(StartToBikeContext context)
        {
            _context = context;
        }

        // GET: DietFoods
        public async Task<IActionResult> Index()
        {
            var startToBikeContext = _context.DietFoods.Include(d => d.Diet).Include(d => d.Food);
            return View(await startToBikeContext.ToListAsync());
        }

        // GET: DietFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dietFood = await _context.DietFoods
                .Include(d => d.Diet)
                .Include(d => d.Food)
                .FirstOrDefaultAsync(m => m.DietID == id);
            if (dietFood == null)
            {
                return NotFound();
            }

            return View(dietFood);
        }

        // GET: DietFoods/Create
        public IActionResult Create()
        {
            ViewData["DietID"] = new SelectList(_context.Diets, "ID", "ID");
            ViewData["FoodID"] = new SelectList(_context.Foods, "ID", "ID");
            return View();
        }

        // POST: DietFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DietID,FoodID")] DietFood dietFood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dietFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DietID"] = new SelectList(_context.Diets, "ID", "ID", dietFood.DietID);
            ViewData["FoodID"] = new SelectList(_context.Foods, "ID", "ID", dietFood.FoodID);
            return View(dietFood);
        }

        // GET: DietFoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dietFood = await _context.DietFoods.FindAsync(id);
            if (dietFood == null)
            {
                return NotFound();
            }
            ViewData["DietID"] = new SelectList(_context.Diets, "ID", "ID", dietFood.DietID);
            ViewData["FoodID"] = new SelectList(_context.Foods, "ID", "ID", dietFood.FoodID);
            return View(dietFood);
        }

        // POST: DietFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DietID,FoodID")] DietFood dietFood)
        {
            if (id != dietFood.DietID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dietFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DietFoodExists(dietFood.DietID))
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
            ViewData["DietID"] = new SelectList(_context.Diets, "ID", "ID", dietFood.DietID);
            ViewData["FoodID"] = new SelectList(_context.Foods, "ID", "ID", dietFood.FoodID);
            return View(dietFood);
        }

        // GET: DietFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dietFood = await _context.DietFoods
                .Include(d => d.Diet)
                .Include(d => d.Food)
                .FirstOrDefaultAsync(m => m.DietID == id);
            if (dietFood == null)
            {
                return NotFound();
            }

            return View(dietFood);
        }

        // POST: DietFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dietFood = await _context.DietFoods.FindAsync(id);
            _context.DietFoods.Remove(dietFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DietFoodExists(int id)
        {
            return _context.DietFoods.Any(e => e.DietID == id);
        }
    }
}
