using Microsoft.AspNetCore.Mvc;
using KriniteAuthServer.ResourceDataClient.Models;
using KriniteAuthServer.ResourceDataClient.ApiServices;

namespace KriniteAuthServer.ResourceDataClient.Controllers;

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

    //// GET: ComplaintModels/Details/5
    //public async Task<IActionResult> Details(Guid? id)
    //{
    //    if (id == null || _context.ComplaintModel == null)
    //    {
    //        return NotFound();
    //    }

    //    var complaintModel = await _context.ComplaintModel
    //        .FirstOrDefaultAsync(m => m.Id == id);
    //    if (complaintModel == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(complaintModel);
    //}

    //// GET: ComplaintModels/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    //// POST: ComplaintModels/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("Id,Title,Description,Priority,Status,Created")] ComplaintModel complaintModel)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        complaintModel.Id = Guid.NewGuid();
    //        _context.Add(complaintModel);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(complaintModel);
    //}

    //// GET: ComplaintModels/Edit/5
    //public async Task<IActionResult> Edit(Guid? id)
    //{
    //    if (id == null || _context.ComplaintModel == null)
    //    {
    //        return NotFound();
    //    }

    //    var complaintModel = await _context.ComplaintModel.FindAsync(id);
    //    if (complaintModel == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(complaintModel);
    //}

    //// POST: ComplaintModels/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,Priority,Status,Created")] ComplaintModel complaintModel)
    //{
    //    if (id != complaintModel.Id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(complaintModel);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ComplaintModelExists(complaintModel.Id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(complaintModel);
    //}

    //// GET: ComplaintModels/Delete/5
    //public async Task<IActionResult> Delete(Guid? id)
    //{
    //    if (id == null || _context.ComplaintModel == null)
    //    {
    //        return NotFound();
    //    }

    //    var complaintModel = await _context.ComplaintModel
    //        .FirstOrDefaultAsync(m => m.Id == id);
    //    if (complaintModel == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(complaintModel);
    //}

    //// POST: ComplaintModels/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(Guid id)
    //{
    //    if (_context.ComplaintModel == null)
    //    {
    //        return Problem("Entity set 'ComplaintContext.ComplaintModel'  is null.");
    //    }
    //    var complaintModel = await _context.ComplaintModel.FindAsync(id);
    //    if (complaintModel != null)
    //    {
    //        _context.ComplaintModel.Remove(complaintModel);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //private bool ComplaintModelExists(Guid id)
    //{
    //    return (_context.ComplaintModel?.Any(e => e.Id == id)).GetValueOrDefault();
    //}
}
