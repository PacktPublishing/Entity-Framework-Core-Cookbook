using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using DataAccess;

namespace UI.Controllers
{
	public class BlogController : Controller
	{
		private readonly BlogContext _blogContext;
		private readonly IBlogRepository _repository;

		public BlogController(IBlogRepository repository)
		{
			_repository = repository;
		}

		public BlogController(IConfiguration config)
		{
			_blogContext = new BlogContext(config["Data:Blog:ConnectionString"]);
		}

		public IActionResult Index()
		{
			var blog = (_blogContext?.Blogs ?? _repository.Set()).First();
			//var blog = _repository.Set().First();
			return View(blog);
		}
	}
}
