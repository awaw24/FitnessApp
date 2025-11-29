using AutoMapper;
using CoachBuddy.Application.TrainingPlan;
using CoachBuddy.Application.TrainingPlan.Commands.CreateTrainingPlan;
using CoachBuddy.Application.TrainingPlan.Commands.DeleteTrainingPlan;
using CoachBuddy.Application.TrainingPlan.Commands.EditTrainingPlan;
using CoachBuddy.Application.TrainingPlan.Queries.GetAllTrainingPlans;
using CoachBuddy.Application.TrainingPlan.Queries.GetTrainingPlanByEncodedName;
using CoachBuddy.Application.TrainingPlan.Queries.GetTrainingPlanById;
using CoachBuddy.Application.TrainingPlan.Queries.GetTrainingPlansBySearch;
using CoachBuddy.Infrastructure.Persistence;
using CoachBuddy.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoachBuddy.MVC.Controllers
{
    public class TrainingPlanController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly CoachBuddyDbContext _context;

        public TrainingPlanController(IMediator mediator, IMapper mapper, CoachBuddyDbContext context)
        {
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetAllTrainingPlansQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginatedResult = await _mediator.Send(query);
            return View(paginatedResult);
        }

        [Route("TrainingPlan/{id:int}/Details")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var dto = await _mediator.Send(new GetTrainingPlanByIdQuery(id));
                return View(dto);
            }
            catch (NotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while retrieving the training plan.";
                return RedirectToAction("Index");
            }
        }

        [Route("TrainingPlan/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetTrainingPlanByEncodedNameQuery(encodedName));

            if (dto == null)
            {
                return NotFound();
            }

            EditTrainingPlanCommand model = _mapper.Map<EditTrainingPlanCommand>(dto);
            return View(model);
        }

        [HttpPost]
        [Route("TrainingPlan/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditTrainingPlanCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Training plan '{command.Name}' has been updated.");
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateTrainingPlanCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Training plan '{command.Name}' has been created successfully.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("TrainingPlan/Delete/{encodedName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string encodedName)
        {
            var trainingPlan = await _context.TrainingPlans
                .FirstOrDefaultAsync(tp => tp.EncodedName == encodedName);

            if (trainingPlan == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<TrainingPlanDto>(trainingPlan);
            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [Route("TrainingPlan/Delete/{EncodedName}")]
        public async Task<IActionResult> DeleteConfirmed(string encodedName)
        {
            var trainingPlan = await _context.TrainingPlans
                .FirstOrDefaultAsync(tp => tp.EncodedName == encodedName);

            if (trainingPlan == null)
            {
                return NotFound();
            }

            var command = new DeleteTrainingPlanCommand { Id = trainingPlan.Id };
            await _mediator.Send(command);

            this.SetNotification("success", $"Training plan '{trainingPlan.Name}' has been deleted.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction(nameof(Index));
            }

            var query = new GetTrainingPlansBySearchQuery(searchTerm, pageNumber, pageSize);
            var trainingPlans = await _mediator.Send(query);

            return View("Index", trainingPlans);
        }
    }
}
