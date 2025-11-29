using AutoMapper;
using CoachBuddy.Application.Exercise;
using CoachBuddy.Domain.Interfaces.Exercise;
using CoachBuddy.Domain.Interfaces.TrainingPlan;
using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Queries.GetTrainingPlanById
{
    public class GetTrainingPlanByIdQueryHandler : IRequestHandler<GetTrainingPlanByIdQuery, TrainingPlanDto>
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;
        public GetTrainingPlanByIdQueryHandler(ITrainingPlanRepository trainingPlanRepository, IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }
        public async Task<TrainingPlanDto> Handle(GetTrainingPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var trainingPlan = await _trainingPlanRepository.GetByIdAsync(request.Id);

            var dto = _mapper.Map<TrainingPlanDto>(trainingPlan);

            var allExercises = await _exerciseRepository.GetAllAsync();

            var availableExercises = allExercises
                .Where(e => !trainingPlan.TrainingPlanExercises!
                    .Any(tpe => tpe.ExerciseId == e.Id))
                .ToList();

            dto.AvailableExercises = _mapper.Map<List<ExerciseDto>>(availableExercises);

            return dto;
        }
    }
}
