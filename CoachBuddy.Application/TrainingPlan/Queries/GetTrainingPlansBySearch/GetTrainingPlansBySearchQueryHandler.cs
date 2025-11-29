using AutoMapper;
using CoachBuddy.Application.Common;
using CoachBuddy.Application.Exercise.Queries.GetExercisesBySearch;
using CoachBuddy.Domain.Entities.Exercise;
using CoachBuddy.Domain.Interfaces.TrainingPlan;
using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Queries.GetTrainingPlansBySearch
{
    public class GetTrainingPlansBySearchQueryHandler : IRequestHandler<GetTrainingPlansBySearchQuery, PaginatedResult<TrainingPlanDto>>
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IMapper _mapper;
        public GetTrainingPlansBySearchQueryHandler(ITrainingPlanRepository trainingPlanRepository, IMapper mapper)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<TrainingPlanDto>> Handle(GetTrainingPlansBySearchQuery request, CancellationToken cancellationToken)
        {
            var trainingPlans = await _trainingPlanRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                trainingPlans = trainingPlans.Where(e =>
                    e.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var totalTrainingPlans = trainingPlans.Count();

            var paginatedTrainingPlans = trainingPlans
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtos = _mapper.Map<List<TrainingPlanDto>>(paginatedTrainingPlans);

            return new PaginatedResult<TrainingPlanDto>
            {
                Items = dtos,
                TotalCount = totalTrainingPlans,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
