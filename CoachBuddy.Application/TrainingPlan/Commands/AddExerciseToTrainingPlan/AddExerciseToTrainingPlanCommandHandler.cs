using AutoMapper;
using CoachBuddy.Domain.Interfaces.Exercise;
using CoachBuddy.Domain.Interfaces.TrainingPlan;
using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Commands.AddExerciseToTrainingPlan
{
    public class AddExerciseToTrainingPlanCommandHandler : IRequestHandler<AddExerciseToTrainingPlanCommand>
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;
        public AddExerciseToTrainingPlanCommandHandler(ITrainingPlanRepository trainingPlanRepository, IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(AddExerciseToTrainingPlanCommand request, CancellationToken cancellationToken)
        {
            var trainingPlan = await _trainingPlanRepository.GetByIdAsync(request.TrainingPlanId);

            if (trainingPlan == null)
            {
                throw new KeyNotFoundException($"Training plan with ID {request.TrainingPlanId} not found");
            }

            var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

            if (exercise == null)
            {
                throw new KeyNotFoundException($"Exercise with ID {request.ExerciseId} not found");
            }

            if(trainingPlan.TrainingPlanExercises.Any(tpe=>tpe.ExerciseId == request.ExerciseId))
            {
                throw new InvalidOperationException($"Exercise with ID {request.ExerciseId} is already assigned to this training plan.");
            }
            var trainingPlanExercise = new Domain.Entities.TrainingPlan.TrainingPlanExercise
            {
                TrainingPlanId = request.TrainingPlanId,
                ExerciseId = request.ExerciseId
            };

            trainingPlan.TrainingPlanExercises.Add(trainingPlanExercise);

            await _trainingPlanRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
