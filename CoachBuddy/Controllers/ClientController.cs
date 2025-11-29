using AutoMapper;
using CoachBuddy.Application.Client;
using CoachBuddy.Application.Client.Commands.CreateClient;
using CoachBuddy.Application.Client.Commands.DeleteClient;
using CoachBuddy.Application.Client.Commands.EditClient;
using CoachBuddy.Application.Client.Queries.GetAllClients;
using CoachBuddy.Application.Client.Queries.GetClientByEncodedName;
using CoachBuddy.Application.ClientTraining.Commands;
using CoachBuddy.Application.ClientTraining.Queries.GetClientTrainings;
using CoachBuddy.Application.Client.Queries.GetClientsBySearch;
using CoachBuddy.Infrastructure.Persistence;
using CoachBuddy.MVC.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoachBuddy.MVC.Controllers
{
    public class ClientController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly CoachBuddyDbContext _context;

        public ClientController(IMediator mediator, IMapper mapper,CoachBuddyDbContext context)
        {
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1,int pageSize = 10)
        {
            var query = new GetAllClientsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginatedResult = await _mediator.Send(query);
            return View(paginatedResult);
        }

        [Route("Client/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await _mediator.Send(new GetClientByEncodedNameQuery(encodedName));
            return View(dto);
        }

        [Route("Client/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetClientByEncodedNameQuery(encodedName));

            if (!dto.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            EditClientCommand model = _mapper.Map<EditClientCommand>(dto);

            return View(model);
        }

        [HttpPost]
        [Route("Client/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditClientCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Group '{command.Name}' has been updated.");
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateClientCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);

            this.SetNotification("success", $"Added new client: {command.Name} {command.LastName}");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Client/ClientTraining")]
        public async Task<IActionResult> CreateClientTraining(CreateClientTrainingCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("Client/{encodedName}/ClientTraining")]
        public async Task<IActionResult> GetClientTrainings(string encodedName)
        {
            var data = await _mediator.Send(new GetClientTrainingsQuery() { EncodedName = encodedName });
            return Ok(data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }
            var clientDto = _mapper.Map<ClientDto>(client);

            return View(clientDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [Route("Client/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }
            var command = new DeleteClientCommand { Id = id };

            await _mediator.Send(command);

            this.SetNotification("success", $"Client {client.Name} {client.LastName} was successfully deleted.");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm,int pageNumber = 1,int pageSize = 10)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction(nameof(Index));
            }
            var query = new GetClientsBySearchQuery(searchTerm, pageNumber, pageSize);

            var clients = await _mediator.Send(query);

            return View("Index", clients);
        }
    }
}