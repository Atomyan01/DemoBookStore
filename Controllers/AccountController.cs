using DemoBookStore.Data;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoBookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly DemoBookStoreContext _context;
        private readonly UserManager<UserModel> _userManager;

        public AccountController(SignInManager<UserModel> signInManager, DemoBookStoreContext context,
            UserManager<UserModel> userManager)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;


        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }

        public async Task<IActionResult> LogoutConfirmed()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Remove()
        {
            var user = _context.UserModel.Find(User);

            if (user != null)
            {
                List<OrderModel> orders = await _context.Orders.ToListAsync();
                foreach (OrderModel order in orders)
                {
                    if (order.User.Id == user.Id) _context.Orders.Remove(order);
                }
                await _signInManager.SignOutAsync();
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return BadRequest("User does not exist");

        }

    }
}
