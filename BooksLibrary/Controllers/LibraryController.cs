using mba.BooksLibrary.Model;
using mba.BooksLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace mba.BooksLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController : ControllerBase
{
    private readonly LibraryService _libraryService;

    public LibraryController(LibraryService LibraryService) =>
        _libraryService = LibraryService;

    [HttpGet]
    public async Task<List<Library>> Get() =>
        await _libraryService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Library>> Get(string id)
    {
        var library = await _libraryService.GetAsync(id);

        if (library is null)
        {
            return NotFound();
        }

        return library;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Library newMaterial)
    {
        await _libraryService.CreateAsync(newMaterial);

        return CreatedAtAction(nameof(Get), new { id = newMaterial.Id }, newMaterial);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Library updatedMaterial)
    {
        var library = await _libraryService.GetAsync(id);

        if (library is null)
        {
            return NotFound();
        }

        updatedMaterial.Id = library.Id;

        await _libraryService.UpdateAsync(id, updatedMaterial);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var library = await _libraryService.GetAsync(id);

        if (library is null)
        {
            return NotFound();
        }

        await _libraryService.RemoveAsync(id);

        return NoContent();
    }
}