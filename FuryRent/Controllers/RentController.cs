using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Rent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FuryRent.Controllers
{
	[Authorize]
	public class RentController : Controller
	{
		private readonly IRentService rents;

		public RentController(IRentService _rents)
		{
			rents = _rents;
		}

		public async Task<IActionResult> All()
		{
			var userId = GetUserId();

			var model = await rents.All(userId);

			return View(model);
		}

		[HttpGet]
		public IActionResult Add()
		{
		
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddRentViewModel rentModel, int Id)
		{
			var userId = GetUserId();

			await rents.Add(rentModel,userId, Id);

			return RedirectToAction("All", "Rent");
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
