using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StartToBike.Data;
using StartToBike.Models;
using Microsoft.AspNetCore.Http;

namespace StartToBike.Controllers
{
    public class DietsController : Controller
    {
        private readonly StartToBikeContext _context;

        public DietsController(StartToBikeContext context)
        {
            _context = context;
        }

        // GET: Diets
        public async Task<IActionResult> Index()
        {
            var startToBikeContext = _context.Diets.Where(d => d.UserID == Int32.Parse(HttpContext.Session.GetString("CurentUserID")))
                                        .Include(df => df.DietFoods).ThenInclude(f => f.Food);
            return View(await startToBikeContext.ToListAsync());
        }

        // GET: Diets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diet = await _context.Diets
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (diet == null)
            {
                return NotFound();
            }

            return View(diet);
        }

        // GET: Diets/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID");
            return View();
        }

        // POST: Diets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID")] Diet diet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", diet.UserID);
            return View(diet);
        }

        // GET: Diets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diet = await _context.Diets.FindAsync(id);
            if (diet == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", diet.UserID);
            return View(diet);
        }

        // POST: Diets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID")] Diet diet)
        {
            if (id != diet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DietExists(diet.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", diet.UserID);
            return View(diet);
        }

        // GET: Diets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diet = await _context.Diets
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (diet == null)
            {
                return NotFound();
            }

            return View(diet);
        }

        // POST: Diets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diet = await _context.Diets.FindAsync(id);
            _context.Diets.Remove(diet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DietExists(int id)
        {
            return _context.Diets.Any(e => e.ID == id);
        }

        public async Task<IActionResult> UserDiet()
        {
            var startToBikeContext = _context.Diets.Where(d => d.UserID == Int32.Parse(HttpContext.Session.GetString("CurentUserID")))
                                        .Include(df => df.DietFoods).ThenInclude(f => f.Food);
            return View(await startToBikeContext.ToListAsync());
        }
        
        public async Task<IActionResult> UserDietAdd()
        {
            return View(await _context.Foods.ToListAsync());
        }

        public async Task<IActionResult> UserDietAddFood(int id)
        {
            var userDiet = _context.Diets.Where(d => d.UserID == Int32.Parse(HttpContext.Session.GetString("CurentUserID")));
            Diet temp = new Diet();

            foreach(Diet diet in userDiet)
            {
                temp = diet;
            }


            DietFood dietFood = new DietFood();
            dietFood.FoodID = id;
            dietFood.DietID = temp.ID;

            _context.Add(dietFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserDiet));
        }

        public async Task<IActionResult> UserDietDelete(int id)
        {
            var userDiet = _context.Diets.Where(d => d.UserID == Int32.Parse(HttpContext.Session.GetString("CurentUserID")));
            Diet temp = new Diet();

            foreach (Diet diet in userDiet)
            {
                temp = diet;
            }

            var dep = _context.DietFoods.Where(d => d.DietID == temp.ID && d.FoodID == id).First();
            _context.DietFoods.Remove(dep);
            _context.SaveChanges();

            return RedirectToAction(nameof(UserDiet));
        }
    }
}
