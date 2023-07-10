using Microsoft.AspNetCore.Mvc;

namespace KriniteAuth.ResourceDataAppHybrid.Controllers;

public class AccountController : Controller
{

	public IActionResult AccessDenied()
	{
		return View();
	}
}
