﻿using DemoBookStore.Data;
using DemoBookStore.Helpers;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookStore.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        private readonly DemoBookStoreContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly string SessionKey = "OrderSession";

        public OrderController(DemoBookStoreContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<IActionResult> PlaseOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var orderBooks = HttpContext.Session.GetObject<List<BookModel>>(SessionKey);

            if (!orderBooks.Any()) return BadRequest("Your order is empty");

            OrderModel newOrder = new OrderModel
            {
                User = user,
                Books = orderBooks,
                Date = DateTime.Now,
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
    }
}

