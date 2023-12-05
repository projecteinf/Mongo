using mba.BooksLibrary.Model;
using mba.BooksLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace mba.BooksLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaterialController : ControllerBase
{
    private readonly MaterialService _materialService;

    public MaterialController(MaterialService materialService) =>
        _materialService = materialService;

    [HttpGet]
    public async Task<List<Material>> Get() =>
        await _materialService.GetAsync();

    [HttpGet("notreturned")]
    public async Task<List<Material>> GetNotReturned() =>
        await _materialService.NotReturnedAsync();

    // Material not returned by user
    [HttpGet("notreturned/{id:length(24)}")]
    public async Task<List<Material>> GetNotReturnedByUser(string id)
    {
        return await _materialService.GetNotReturnedByUser(id);;
    }


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

    [HttpPut("{id:length(24)}/prestec")]
    public async Task<IActionResult> AfegirPrestec(string id, Prestecs prestec)
    {
        var material = await _materialService.GetAsync(id);

        if (material is null) return NotFound();

        await _materialService.AfegirPrestecAsync(id, prestec);

        return NoContent();
    }

    [HttpPut("{id:length(24)}/return/{userId:length(24)}")]
    public async Task<IActionResult> ReturnMaterial(string id, string userId)
    {
        var material = await _materialService.GetAsync(id);

        if (material is null) return NotFound();

        await _materialService.ReturnMaterialAsync(id, userId);

        return NoContent();
    }

    [HttpPut("{id:length(24)}/assigntolibrary/{libraryId:length(24)}")]
    public async Task<IActionResult> AssignToLibrary(string id, string libraryId)
    {
        var material = await _materialService.GetAsync(id);

        if (material is null) return NotFound();

        await _materialService.AssignLibraryAsync(id, libraryId);

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