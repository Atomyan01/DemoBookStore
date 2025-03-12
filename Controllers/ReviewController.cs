using DemoBookStore.Data;
using DemoBookStore.Helpers;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace DemoBookStore.Users.Controllers
{
    public class ReviewController : Controller
    {
        private readonly DemoBookStoreContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly string SessionKey = "OrderSession";

        public ReviewController(DemoBookStoreContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult AddReview(int? id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([Bind("Title,Stars,Description")] ReviewModel reviewModel, int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserModel? user = await _context.UserModel.FindAsync(userId);
            var book = await _context.BookModel.FindAsync(bookId);

            if (user == null) return Unauthorized();
            if (book == null) return NotFound();

            if (reviewModel.Stars < 0)
            {
                reviewModel.Stars = 0;
            }
            else if (reviewModel.Stars  > 10)
            {
                reviewModel.Stars = 10;
            }

            ReviewModel review = new ReviewModel
            {
                Title = reviewModel.Title,
                Description = reviewModel.Description,
                Stars = reviewModel.Stars,
                User = user,
                Book = book
            };

            _context.ReviewModel.Add(review);
            await _context.SaveChangesAsync();

            

            return RedirectToAction("Details", "Book", new { id = bookId });
        }

        
    }
}