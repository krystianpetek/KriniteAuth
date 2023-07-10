using KriniteAuth.ResourceDataApp.ApiServices;
using KriniteAuth.ResourceDataApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KriniteAuth.ResourceDataApp.Controllers;

[Authorize]
public class ComplaintController : Controller
{
	private readonly IComplaintService _complaintService;

	public ComplaintController(IComplaintService complaintService)
	{
		_complaintService = complaintService;
	}

	// GET: ComplaintModels
	public async Task<IActionResult> Index()
	{
		return View(await _complaintService.GetAllAsync());
	}

	public async Task Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
	}

	// GET: ComplaintModels/Details/5
	public async Task<IActionResult> Details(Guid? id)
	{
		var complaintModel = await _complaintService.GetByIdAsync(id.Value);
		if (complaintModel == default)
			return NotFound();

		return View(complaintModel);
	}

	// GET: ComplaintModels/Create
	public IActionResult Create()
	{
		return View();
	}

	// POST: ComplaintModels/Create
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Title,Description,Priority,Status,Created")] ComplaintModel complaintModel)
	{
		if (ModelState.IsValid)
		{
			complaintModel.Id = Guid.NewGuid();
			await _complaintService.AddAsync(complaintModel);
			return RedirectToAction(nameof(Index));
		}
		return View(complaintModel);
	}

	// GET: ComplaintModels/Edit/5
	public async Task<IActionResult> Edit(Guid? id)
	{
		if (id == default)
			return NotFound();

		var complaintModel = await _complaintService.GetByIdAsync(id.Value);
		if (complaintModel == null)
			return NotFound();

		return View(complaintModel);
	}

	// POST: ComplaintModels/Edit/5
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,Priority,Status,Created")] ComplaintModel complaintModel)
	{
		if (id != complaintModel.Id)
			return NotFound();

		if (ModelState.IsValid)
			await _complaintService.UpdateAsync(complaintModel);
		return View(complaintModel);
	}

	// GET: ComplaintModels/Delete/5
	public async Task<IActionResult> Delete(Guid? id)
	{
		if (id == default)
			return NotFound();

		var complaintModel = await _complaintService.GetByIdAsync(id.Value);
		if (complaintModel == default)
			return NotFound();

		return View(complaintModel);
	}

	// POST: ComplaintModels/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(Guid id)
	{
		var complaintModel = await _complaintService.GetByIdAsync(id);
		if (complaintModel != null)
			await _complaintService.DeleteAsync(id);

		return RedirectToAction(nameof(Index));
	}
}
