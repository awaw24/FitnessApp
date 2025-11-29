using AutoMapper;
using CoachBuddy.Application.Exercise;
using CoachBuddy.Application.Exercise.Commands.CreateExercise;
using CoachBuddy.Application.Exercise.Commands.DeleteExercise;
using CoachBuddy.Application.Exercise.Commands.EditExercise;
using CoachBuddy.Application.Exercise.Queries.GetAllExercises;
using CoachBuddy.Application.Exercise.Queries.GetExerciseByEncodedName;
using CoachBuddy.Application.Exercise.Queries.GetExercisesBySearch;
using CoachBuddy.Infrastructure.Persistence;
using CoachBuddy.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoachBuddy.MVC.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly CoachBuddyDbContext _context;

        public ExerciseController(IMediator mediator, IMapper mapper, CoachBuddyDbContext context)
        {
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetAllExercisesQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginatedResult = await _mediator.Send(query);
            return View(paginatedResult);
        }

        [Route("Exercise/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetExerciseByEncodedNameQuery(encodedName));
            return View(dto);
        }

        [Route("Exercise/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetExerciseByEncodedNameQuery(encodedName));

            if (dto == null)
            {
                return NotFound();
            }

            EditExerciseCommand model = _mapper.Map<EditExerciseCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Route("Exercise/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditExerciseCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Exercise '{command.Name}' has been updated.");
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateExerciseCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Exercise '{command.Name}' has been created successfully.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Exercise/Delete/{encodedName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string encodedName)
        {
            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(e => e.EncodedName == encodedName);

            if (exercise == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ExerciseDto>(exercise);
            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [Route("Exercise/Delete/{EncodedName}")]
        public async Task<IActionResult> DeleteConfirmed(string encodedName)
        {
            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(e => e.EncodedName == encodedName);

            if (exercise == null)
            {
                return NotFound();
            }

            var command = new DeleteExerciseCommand { Id = exercise.Id };
            await _mediator.Send(command);

            this.SetNotification("success", $"Exercise '{exercise.Name}' has been deleted.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction(nameof(Index));
            }

            var query = new GetExercisesBySearchQuery(searchTerm, pageNumber, pageSize);
            var exercises = await _mediator.Send(query);

            return View("Index", exercises);
        }
    }
}
