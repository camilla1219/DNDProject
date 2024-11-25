using Microsoft.AspNetCore.Mvc;
using WebAPI.models;
using WebAPI.Services;


[ApiController]
[Route("api/[controller]")]
public class SurveyResponseController : ControllerBase
{
    private readonly FileService _fileService;
    private List<SurveyResponse> _responses;

    public SurveyResponseController(FileService fileService)
    {
        _fileService = fileService;
        _responses = _fileService.LoadResponses() ?? new List<SurveyResponse>();
    }

    [HttpGet]
    public ActionResult<List<SurveyResponse>> GetResponses()
    {
        return Ok(_responses);
    }

    [HttpGet("{id}")]
    public ActionResult<SurveyResponse> GetResponse(int id)
    {
        var response = _responses.FirstOrDefault(r => r.Id == id);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpPost]
    public ActionResult AddResponse(SurveyResponse newResponse)
    {
        newResponse.Id = _responses.Any() ? _responses.Max(r => r.Id) + 1 : 1;
        newResponse.SubmittedAt = DateTime.UtcNow;
        _responses.Add(newResponse);
        _fileService.SaveResponses(_responses);
        return CreatedAtAction(nameof(GetResponse), new { id = newResponse.Id }, newResponse);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteResponse(int id)
    {
        var response = _responses.FirstOrDefault(r => r.Id == id);
        if (response == null) return NotFound();
        _responses.Remove(response);
        _fileService.SaveResponses(_responses);
        return NoContent();
    }
}
