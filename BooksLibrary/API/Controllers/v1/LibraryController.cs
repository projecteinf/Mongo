using System.Net;
using mba.BooksLibrary.Model;
using mba.BooksLibrary.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mba.BooksLibrary.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
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
    public async Task<IActionResult> Post(Library newLibrary)
    {
        
        IActionResult result;
        try { 
            await _libraryService.CreateAsync(newLibrary);
            result = CreatedAtAction(nameof(Get), new { id = newLibrary.Id }, newLibrary);
        }
        catch (MongoWriteException ex)
        {
            string message = "{Category:"+ ex.WriteError.Category.ToString()+", Code: "+ex.WriteError.Code.ToString()+", Message: \""+ex.WriteError.Message+"\"}";
            return Conflict(
                /*
                    DuplicateKey: A write operation resulted in an error. WriteError: { Category : "DuplicateKey", Code : 11000, Message : "E11000 duplicate key error collection: booksdb.library index: Name_1 dup key: { Name: "Municipal contemporània" }" }.
                */
                message
            );
        }
        return result;
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Library updatedLibrary)
    {
        var library = await _libraryService.GetAsync(id);

        if (library is null)
        {
            return NotFound();
        }

        updatedLibrary.Id = library.Id;

        await _libraryService.UpdateAsync(id, updatedLibrary);

        return NoContent();
    }

    [HttpPut("{id:length(24)}/adduser")]
    public async Task<IActionResult> AddUser(string id, User user)
    {
        var library = await _libraryService.GetAsync(id);

        if (library is null)
        {
            return NotFound();
        }

        await _libraryService.AddUserAsync(id, user);

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