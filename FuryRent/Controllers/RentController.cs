using FuryRent.Core.Contracts;
using FuryRent.Core.Exceptions;
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

		[Authorize(Roles = "Admin, User")]
		public async Task<IActionResult> All()
		{
			var userId = GetUserId();

			var model = await rents.All(userId);

			return View(model);
		}

		[HttpGet]
		[Authorize(Roles = "Admin, User")]
		public IActionResult Add()
		{
		
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Admin, User")]
		public async Task<IActionResult> Add(AddRentViewModel rentModel, int Id)
		{
			var userId = GetUserId();

			try
			{
                await rents.Add(rentModel, userId, Id);
            }
			catch (NoSuchCarException)
			{

				return NotFound();
			}
			catch(InvalidOperationException exc)
			{
                TempData["message"] = exc.Message;

                return RedirectToAction("Details", "Car", new {Id});
            }
			catch(Exception exc)
			{
				TempData["message"] = exc.Message;

				return RedirectToAction("Become", "Vip");
			}

            TempData["message"] = "You have successfully rented a car";

            return RedirectToAction("All", "Rent");
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
