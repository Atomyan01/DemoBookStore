using DemoBookStore.Data;
using DemoBookStore.Helpers;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookStore.Controllers
{
	


	public class OrderSessionController : Controller
	{
		private readonly DemoBookStoreContext _context;
		private readonly string SessionKey = "OrderSession";

		public OrderSessionController(DemoBookStoreContext context)
		{
			_context = context;
		}


		public IActionResult Index()
		{
			var order = HttpContext.Session.GetObject<List<BookModel>>(SessionKey) ?? new List<BookModel>();

			Dictionary<int,int> counts = new Dictionary<int,int>();
			Dictionary<int, decimal>  sums = new Dictionary<int, decimal>();
			List<BookModel> uniqueBooks = new List<BookModel>();

			foreach(var o in order)
			{
				if (counts.ContainsKey(o.ID))
				{
					counts[o.ID] += 1;
					sums[o.ID] += o.Price;
				}
				else
				{
					counts.Add(o.ID, 1);
					sums.Add(o.ID, o.Price);
					uniqueBooks.Add(o);
				}

			}

			ViewBag.Counts = counts;
			ViewBag.Sums = sums;
			ViewBag.Sum = sums.Values.Sum();
			return View(uniqueBooks);

		}


        public IActionResult Reviews()
        {
            var reviews = HttpContext.Session.GetObject<List<ReviewModel>>(SessionKey) ?? new List<ReviewModel>();

            Dictionary<int, int> starsCount = new Dictionary<int, int>();
            List<ReviewModel> uniqueReviews = new List<ReviewModel>();

            foreach (var review in reviews)
            {
                if (!starsCount.ContainsKey(review.Id))
                {
                    starsCount.Add(review.Id, review.Stars);
                    uniqueReviews.Add(review);
                }
            }

            ViewBag.StarsCount = starsCount;
            return View(uniqueReviews);
        }


        [Authorize]
		public IActionResult AddToOrder(int id)
		{
			var book = _context.BookModel.Find(id);
			if (book == null) return NotFound();

			var order = HttpContext.Session.GetObject<List<BookModel>>(SessionKey) ?? new List<BookModel>();
			order.Add(book);
			
			HttpContext.Session.SetObject(SessionKey, order);
			return RedirectToAction("Index");
		}


		[Authorize]
		public IActionResult RemoveFromOrder(int id)
		{
			var order = HttpContext.Session.GetObject<List<BookModel>>(SessionKey) ?? new List<BookModel>();
			var bookToRemove = order.FirstOrDefault(b => b.ID == id);

			if (bookToRemove != null)
			{
				order.Remove(bookToRemove);
				HttpContext.Session.SetObject(SessionKey,order);

			}
			return RedirectToAction("Index");

		}


		[Authorize]
		public IActionResult ClearOrder()
		{
			HttpContext.Session.Remove(SessionKey);
			return RedirectToAction("Index");
		}


	}
	


}
