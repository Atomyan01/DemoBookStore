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
using System.Drawing.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DemoBookStore.Controllers
{
	public class UserController : Controller
	{
		private readonly DemoBookStoreContext _context;
		private readonly SignInManager<UserModel> _signInManager;
		
		private readonly UserManager<UserModel> _userManager;

		public UserController(DemoBookStoreContext context, SignInManager<UserModel> signInManager,  UserManager<UserModel> userManager)
		{
			_context = context;
			_signInManager = signInManager;
			
			_userManager = userManager;
			

		}

		// GET: User
		public async Task<IActionResult> Index()
		{
			return View(await _context.UserModel.ToListAsync());
		}

		// GET: User/Details/5
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userModel = await _context.UserModel
				.FirstOrDefaultAsync(m => m.Id == id);
			if (userModel == null)
			{
				return NotFound();
			}

			return View(userModel);
		}


		

		// GET: User/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: User/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Age,Address,Id,FirstName,LastName,Email,Password")] UserModel userModel, string password)
		{
			userModel.LockoutEnabled = false;
			userModel.NormalizedEmail = _userManager.NormalizeEmail(userModel.Email);
			userModel.NormalizedUserName = userModel.Email;
			userModel.PasswordHash = HashPassword.ProceedData(password);

			if (ModelState.IsValid)
			{
				_context.Add(userModel);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			
			return View(userModel);
		}

		// GET: User/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userModel = await _context.UserModel.FindAsync(id);
			if (userModel == null)
			{
				return NotFound();
			}
			return View(userModel);
		}

		// POST: User/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, [Bind("Age,Address,Id,FirstName,LastName,Email,Password")] UserModel userModel)
		{
			

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(userModel);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					
				}
				return RedirectToAction(nameof(Index));
			}
			return View(userModel);
		}

		// GET: User/Delete/5
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userModel = await _context.UserModel
				.FirstOrDefaultAsync(m => m.Id == id);
			if (userModel == null)
			{
				return NotFound();
			}

			return View(userModel);
		}

		// POST: User/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var userModel = await _context.UserModel.FindAsync(id);
			if (userModel != null)
			{
				_context.UserModel.Remove(userModel);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task <IActionResult> Login (string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if(user!= null)
			{
				string hashPassword = HashPassword.ProceedData(password);
				var result = await _signInManager.PasswordSignInAsync(user, hashPassword, false, false);
				if (result.Succeeded)
				{
					return RedirectToAction("Index","Home");
					
				}
				else
				{
					ModelState.AddModelError("", "Invalid Email or Password");
				}
			}
			else
			{
				ModelState.AddModelError("", "User Not Found");
			}

			return View();
		}

		private bool UserModelExists(string id)
		{
			return _context.UserModel.Any(e => e.Id == id);
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
		private UserModel? SearchByEmail(string email)
		{
			return _context.UserModel.FirstOrDefault(user => user.Email == email);
		}

	}
}
