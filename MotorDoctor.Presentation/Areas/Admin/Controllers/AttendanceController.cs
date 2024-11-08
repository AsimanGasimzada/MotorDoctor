using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;

namespace MotorDoctor.Presentation.Areas.Admin.Controllers;
[Area("Admin")]
public class AttendanceController : Controller
{
    private readonly IAttendanceService _service;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _service = attendanceService;
    }


    public async Task<IActionResult> Index()
    {
        var result = await _service.GetAllAsync();

        return View(result);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AttendanceCreateDto dto)
    {
        var result = await _service.CreateAsync(dto, ModelState);

        if (result is false)
            return View(dto);

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        var result = await _service.GetUpdatedDtoAsync(id);

        if (result is null)
            return NotFound();

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(AttendanceUpdateDto dto)
    {
        var result = await _service.UpdateAsync(dto, ModelState);


        if (result is false)
            return View(dto);

        return RedirectToAction(nameof(Index));
    }
}
