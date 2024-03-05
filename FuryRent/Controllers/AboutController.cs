using Microsoft.AspNetCore.Mvc;

namespace FuryRent.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
