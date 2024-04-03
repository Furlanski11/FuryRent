using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Vip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
		public IActionResult Become()
		{
			
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Become(VipUserServiceModel model)
		{
			model.UserId = GetUserId();

			if(model.UserId == null)
			{
				return BadRequest(ModelState);
			}

			await vipUsers.Become(model);

			return RedirectToAction("Index", "Home");
		}

		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
