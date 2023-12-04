using mba.BooksLibrary.Model;
using mba.BooksLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace mba.BooksLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController : ControllerBase
{
    private readonly MaterialService _materialService;

    public LibraryController(MaterialService materialService) =>
        _materialService = materialService;

    [HttpGet]
    public async Task<List<Material>> Get() =>
        await _materialService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Material>> Get(string id)
    {
        var material = await _materialService.GetAsync(id);

        if (material is null)
        {
            return NotFound();
        }

        return material;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Material newMaterial)
    {
        await _materialService.CreateAsync(newMaterial);

        return CreatedAtAction(nameof(Get), new { id = newMaterial.Id }, newMaterial);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Material updatedMaterial)
    {
        var material = await _materialService.GetAsync(id);

        if (material is null)
        {
            return NotFound();
        }

        updatedMaterial.Id = material.Id;

        await _materialService.UpdateAsync(id, updatedMaterial);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var material = await _materialService.GetAsync(id);

        if (material is null)
        {
            return NotFound();
        }

        await _materialService.RemoveAsync(id);

        return NoContent();
    }
}