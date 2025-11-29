using AutoMapper;
using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.TrainingPlan;
using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Commands.DeleteTrainingPlan
{
    public class DeleteTrainingPlanCommandHandler : IRequestHandler<DeleteTrainingPlanCommand>
    {
        private readonly IMapper _mapper;
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IUserContext _userContext;
        public async Task<Unit> Handle(DeleteTrainingPlanCommand request, CancellationToken cancellationToken)
        {
            var trainingPlan = await _trainingPlanRepository.GetByIdAsync(request.Id);

            var user = _userContext.GetCurrentUser();

            var isEditable = user != null && user.IsInRole("Admin");

            if (!isEditable)
            {
                return Unit.Value;
            }

            if(trainingPlan == null)
            {
                throw new KeyNotFoundException($"Training plan with ID {request.Id} not found");
            }

            await _trainingPlanRepository.DeleteAsync(trainingPlan);
            return Unit.Value;
        }
    }
}
