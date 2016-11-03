using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Microsoft.Extensions.Configuration;
using BusinessLogic;

namespace UI.Controllers
{
	public class BlogController : Controller
	{
		private readonly BlogContext _context;

		public BlogController(IConfiguration configuration)
		{
			_context = new BlogContext(	configuration["Data:Blog:ConnectionString"]);
		}


		// GET: /Blog/
		public IActionResult Index()
		{
			var blog = _context.Set<Blog>().First();
			return View(blog);
		}

		// GET: /Blog/Create
		public IActionResult Create()
		{
			return View(new Blog());
		}

		// POST: /Blog/Save
		[HttpPost]
		public IActionResult Save(Blog blog)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			return RedirectToAction("Index");
		}

	}
}
