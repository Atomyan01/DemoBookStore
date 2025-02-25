using DemoBookStore.Data;
using DemoBookStore.Helpers;
using DemoBookStore.Models;
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
			return View(order);
		}

		public IActionResult AddToOrder(int id)
		{
			var book = _context.BookModel.Find(id);
			if (book == null) return NotFound();

			var order = HttpContext.Session.GetObject<List<BookModel>>(SessionKey) ?? new List<BookModel>();
			order.Add(book);
			
			HttpContext.Session.SetObject(SessionKey, order);
			return RedirectToAction("Index");
		}

		

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

		

		public IActionResult ClearOrder()
		{
			HttpContext.Session.Remove(SessionKey);
			return RedirectToAction("Index");
		}


	}


}
