// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KriniteAuth.Server.Quickstart.Diagnostics;

[SecurityHeaders]
[Authorize]
public class DiagnosticsController : Controller
{
	private readonly IHostEnvironment _environment;
	public DiagnosticsController(IHostEnvironment environment)
	{
		_environment = environment;
	}

	public async Task<IActionResult> Index()
	{
		var localAddresses = new string[] { "127.0.0.1", "::1", HttpContext.Connection.LocalIpAddress.ToString() };
		if (!_environment.IsEnvironment("Docker") && !localAddresses.Contains(HttpContext.Connection.RemoteIpAddress.ToString()))
			return NotFound();

		var model = new DiagnosticsViewModel(await HttpContext.AuthenticateAsync());
		return View(model);
	}
}