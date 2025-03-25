using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;

namespace MotorDoctor.Presentation.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DensityController : Controller
{
    private readonly IDensityService _service;

    public DensityController(IDensityService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _service.GetAllAsync();

        return View(sliders);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(DensityCreateDto dto)
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
    public async Task<IActionResult> Update(DensityUpdateDto dto)
    {
        var result = await _service.UpdateAsync(dto, ModelState);


        if (result is false)
            return View(dto);

        return RedirectToAction(nameof(Index));
    }
}
