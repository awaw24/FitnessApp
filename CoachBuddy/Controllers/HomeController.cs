using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoachBuddy.Models;
using MediatR;
using CoachBuddy.Application.Client.Queries.GetClientCount;
using CoachBuddy.MVC.Models;
using CoachBuddy.Application.Group.Queries.GetGroupCount;
using CoachBuddy.Application.Exercise.Queries.GetExerciseById;
using CoachBuddy.Application.Exercise.Queries.GetExerciseCount;

namespace CoachBuddy.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMediator _mediator;

    public HomeController(ILogger<HomeController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var clientCount = await _mediator.Send(new GetClientCountQuery());
        var groupCount = await _mediator.Send(new GetGroupCountQuery());
        var exerciseCount = await _mediator.Send(new GetExerciseCountQuery());
        var viewModel = new HomeViewModel
        {
            ClientCount = clientCount,
            GroupCount = groupCount,
            ExerciseCount = exerciseCount
        };
        return View(viewModel);
    }
    public IActionResult NoAccess()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
