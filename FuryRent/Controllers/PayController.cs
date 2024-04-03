using FuryRent.Core.Contracts;
using FuryRent.Core.Models.Pay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FuryRent.Controllers
{
	[Authorize]
	public class PayController : Controller
	{
		private readonly IPayService payments;

		public PayController(IPayService _payments)
		{
			payments = _payments;
		}

		[HttpGet]
		public async Task<IActionResult> PayRent(int id)
		{
			var rent = await payments.GetRentById(id);

			var model = new PayServiceViewModel()
			{
				PaymentAmount = rent.PaymentAmount,
				RentId = rent.RentId,
				PaymentTypes = await payments.GetPaymentTypes()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> PayRent(PayServiceViewModel model, int id)
		{
			var rent = await payments.GetRentById(id);

			model.PaymentAmount = rent.PaymentAmount;
			model.RentId = rent.RentId;

			if (!ModelState.IsValid)
            {
                model.PaymentTypes = await payments.GetPaymentTypes();

                return RedirectToAction(nameof(PayRent));
            }

			await payments.Pay(model);

            return RedirectToAction("All", "Rent");
		}
	}
}
