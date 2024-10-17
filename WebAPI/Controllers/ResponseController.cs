using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResponsesController : ControllerBase
{
    private readonly SurveyContext _context;

    public ResponsesController(SurveyContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseDTO>>> GetResponses()
    {
        return await _context.Responses
            .Select(x => ResponseToDTO(x))
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDTO>> GetResponse(long id)
    {
        var response = await _context.Responses.FindAsync(id);

        if (response == null)
        {
            return NotFound();
        }

        return ResponseToDTO(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutResponse(long id, ResponseDTO responseDTO)
    {
        if (id != responseDTO.Id)
        {
            return BadRequest();
        }

        var response = await _context.Responses.FindAsync(id);
        if (response == null)
        {
            return NotFound();
        }

        response.Answer = responseDTO.Answer;
        response.QuestionId = responseDTO.QuestionId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!ResponseExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDTO>> PostResponse(ResponseDTO responseDTO)
    {
        var response = new Response
        {
            Answer = responseDTO.Answer,
            QuestionId = responseDTO.QuestionId
        };

        _context.Responses.Add(response);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetResponse),
            new { id = response.Id },
            ResponseToDTO(response));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResponse(long id)
    {
        var response = await _context.Responses.FindAsync(id);
        if (response == null)
        {
            return NotFound();
        }

        _context.Responses.Remove(response);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ResponseExists(long id)
    {
        return _context.Responses.Any(e => e.Id == id);
    }

    private static ResponseDTO ResponseToDTO(Response response) =>
       new ResponseDTO
       {
           Id = response.Id,
           Answer = response.Answer,
           QuestionId = response.QuestionId
       };
}
