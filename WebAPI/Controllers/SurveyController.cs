using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveyTool.Models;

namespace DNDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly SurveyContext _context;

        public SurveyController(SurveyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var surveys = _context.Surveys.ToList();
            return View(surveys);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var survey = new Survey
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1)
            };

            return View(survey);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Survey survey, string action)
        {
            if (ModelState.IsValid)
            {
                survey.Questions.ForEach(q =>
                {
                    q.CreatedOn = DateTime.Now;
                    q.ModifiedOn = DateTime.Now;
                });

                _context.Surveys.Add(survey);
                _context.SaveChanges();

                TempData["success"] = "The survey was successfully created!";
                return RedirectToAction("Edit", new { id = survey.Id });
            }
            else
            {
                TempData["error"] = "An error occurred while attempting to create this survey.";
                return View(survey);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var survey = _context.Surveys.Include("Questions").Single(x => x.Id == id);
            survey.Questions = survey.Questions.OrderBy(q => q.Priority).ToList();

            return View(survey);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(Survey model)
        {
            foreach (var question in model.Questions)
            {
                question.SurveyId = model.Id;

                if (question.Id == 0)
                {
                    question.CreatedOn = DateTime.Now;
                    question.ModifiedOn = DateTime.Now;
                    _context.Entry(question).State = EntityState.Added;
                }
                else
                {
                    question.ModifiedOn = DateTime.Now;
                    _context.Entry(question).State = EntityState.Modified;
                    _context.Entry(question).Property(x => x.CreatedOn).IsModified = false;
                }
            }

            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Edit", new { id = model.Id });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Survey survey)
        {
            _context.Entry(survey).State = EntityState.Deleted;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
