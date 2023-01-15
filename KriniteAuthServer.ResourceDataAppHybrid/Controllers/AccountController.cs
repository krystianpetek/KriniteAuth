using Microsoft.AspNetCore.Mvc;

namespace KriniteAuthServer.ResourceDataAppHybrid.Controllers;

public class AccountController:Controller
{

    public IActionResult AccessDenied()
    {
        return View();
    }
}
