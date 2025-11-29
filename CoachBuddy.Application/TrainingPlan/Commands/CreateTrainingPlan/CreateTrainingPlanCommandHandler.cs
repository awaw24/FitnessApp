using AutoMapper;
using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.TrainingPlan;
using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Commands.CreateTrainingPlan
{
    public class CreateTrainingPlanCommandHandler : IRequestHandler<CreateTrainingPlanCommand>
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        public CreateTrainingPlanCommandHandler(ITrainingPlanRepository trainingPlanRepository, IMapper mapper, IUserContext userContext)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(CreateTrainingPlanCommand request, CancellationToken cancellationToken)
        {
            var trainingPlan = _mapper.Map<Domain.Entities.TrainingPlan.TrainingPlan>(request);

            var currentUser = _userContext.GetCurrentUser();

            if (currentUser == null || !currentUser.IsInRole("Admin"))
            {
                return Unit.Value;
            }

            trainingPlan.EncodeName();

            await _trainingPlanRepository.Create(trainingPlan);
            return Unit.Value;
        }
    }
}
