using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.TrainingPlan;
using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Commands.EditTrainingPlan
{
    public class EditTrainingPlanCommandHandler : IRequestHandler<EditTrainingPlanCommand>
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IUserContext _userContext;
        public EditTrainingPlanCommandHandler(ITrainingPlanRepository trainingPlanRepository,IUserContext userContext)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditTrainingPlanCommand request, CancellationToken cancellationToken)
        {
            var trainingPlan = await _trainingPlanRepository.GetByEncodedName(request.EncodedName!);

            var user = _userContext.GetCurrentUser();

            var isEditable = user != null && user.IsInRole("Admin");
            if (!isEditable)
            {
                return Unit.Value;
            }
            trainingPlan.Description = request.Description;

            await _trainingPlanRepository.Commit();

            return Unit.Value;
        }

    }
}
