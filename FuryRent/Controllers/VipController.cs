using System.Security.Claims;
using FuryRent.Core.Contracts;
using FuryRent.Core.Exceptions;
using FuryRent.Core.Models.Vip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace FuryRent.Controllers
{
	[Authorize]
	public class VipController : Controller
	{
		private readonly IVipService vipUsers;

		public VipController(IVipService _vipUsers)
		{
			vipUsers = _vipUsers;
		}

		[HttpGet]
		[Authorize(Roles = "Admin, User")]
		public IActionResult Become()
		{
			
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Admin, User")]
		public async Task<IActionResult> Become(VipUserServiceModel model)
		{
			model.UserId = GetUserId();

			if(model.UserId == null)
			{
				return BadRequest();
			}

			try
			{
				await vipUsers.Become(model);
			}
			catch (AlreadyVipException)
			{
                TempData["message"] = "You are a VIP user already";

                return RedirectToAction("Index", "Home");
			}
			catch(InvalidOperationException)
			{
                TempData["message"] = "You must have 3 rents to become a VIP user";

                return RedirectToAction("All", "Rent");
            }

            TempData["message"] = "Congratulations you are a VIP user now!";

            return RedirectToAction("Index", "Home");
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
