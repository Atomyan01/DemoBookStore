using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoBookStore.Data;
using DemoBookStore.Models;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace DemoBookStore.Controllers
{
    public class AuthorModelsController : Controller
    {
        private readonly DemoBookStoreContext _context;

        public AuthorModelsController(DemoBookStoreContext context)
        {
            _context = context;
        }

        // GET: AuthorModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.AuthorModel.ToListAsync());
        }

        // GET: AuthorModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorModel = await _context.AuthorModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorModel == null)
            {
                return NotFound();
            }

            return View(authorModel);
        }

        // GET: AuthorModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthorModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AverageScore,Id,FirstName,LastName,Email,Password")] AuthorModel authorModel)
        {
            authorModel.Password = HashPassword.ProceedData(authorModel.Password);
            if (ModelState.IsValid && !CheckEmail(authorModel.Email))
            {
                _context.Add(authorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorModel);
        }


      
        
            

        


        // GET: AuthorModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorModel = await _context.AuthorModel.FindAsync(id);
            if (authorModel == null)
            {
                return NotFound();
            }
            return View(authorModel);
        }

        // POST: AuthorModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AverageScore,Id,FirstName,LastName,Email,Password")] AuthorModel authorModel)
        {
            if (id != authorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorModelExists(authorModel.Id))
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
            return View(authorModel);
        }

        // GET: AuthorModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorModel = await _context.AuthorModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorModel == null)
            {
                return NotFound();
            }

            return View(authorModel);
        }

        // POST: AuthorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorModel = await _context.AuthorModel.FindAsync(id);
            if (authorModel != null)
            {
                _context.AuthorModel.Remove(authorModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorModelExists(int id)
        {
            return _context.AuthorModel.Any(e => e.Id == id);
        }

        private bool CheckEmail(string email)
        {
            List<AuthorModel> authors = _context.AuthorModel.ToListAsync().Result;

            foreach (AuthorModel author in authors)
            {
                if (author.Email == email)
                {
                    return true;
                }
            }

            string stugum1 = "^\\S+@\\S+\\.\\S+$";
            Regex regex1 = new Regex(stugum1);
            return !regex1.IsMatch(email);    
        }


		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> Login(AuthorModel authorModel)
		{
			var author = SearchByEmail(authorModel.Email);
			if (author != null)
			{
				return View(author);
			}
			return RedirectToAction(nameof(Index));
		}



		private AuthorModel? SearchByEmail(string email)
		{
			return _context.AuthorModel.FirstOrDefault(author => author.Email == email);
		}

	}
}
