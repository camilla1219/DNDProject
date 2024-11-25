using Microsoft.AspNetCore.Mvc;
using WebAPI.models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly FileService _fileservice;
        private List<Survey> _surveys;

        public SurveyController(FileService fileService)
        {
            _fileservice = fileService;
            _surveys = _fileservice.LoadSurveys();
        }

        [HttpGet]
        public ActionResult<List<Survey>> GetSurveys()
        {
            return Ok(_surveys);
        }

        [HttpGet("{Id}")]
        public ActionResult<Survey> GetSurvey(int Id)
        {
            var survey = _surveys.FirstOrDefault(s => s.Id == Id);
            if (survey == null) return NotFound();
            return Ok(survey);
        }

        [HttpPost]
        public ActionResult AddSurvey(Survey newSurvey)
        {
                for (int i = 0; i < newSurvey.Questions.Count; i++)
            {
                newSurvey.Questions[i].Id = i + 1;
            }
            newSurvey.Id = _surveys.Any() ? _surveys.Max(s => s.Id) + 1 : 1;
            _surveys.Add(newSurvey);
            _fileservice.SaveSurveys(_surveys);
            return CreatedAtAction(nameof(GetSurvey), new { id = newSurvey.Id }, newSurvey);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateSurvey(int id, Survey updatedSurvey)
        {
            var survey = _surveys.FirstOrDefault(s => s.Id == id);
            if (survey == null) return NotFound();

            survey.Title = updatedSurvey.Title;
            survey.Description = updatedSurvey.Description;
            survey.Questions = updatedSurvey.Questions;

            _fileservice.SaveSurveys(_surveys);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSurvey(int id)
        {
            var survey = _surveys.FirstOrDefault(s => s.Id == id);
            if (survey == null) return NotFound();

            _surveys.Remove(survey);
            _fileservice.SaveSurveys(_surveys);
            return NoContent();
        }
    }
}
