using AutoMapper;
using CoachBuddy.Application.Common;
using CoachBuddy.Domain.Interfaces.TrainingPlan;
using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Queries.GetAllTrainingPlans
{
    public class GetAllTrainingPlansQueryHandler : IRequestHandler<GetAllTrainingPlansQuery, PaginatedResult<TrainingPlanDto>>
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IMapper _mapper;
        public GetAllTrainingPlansQueryHandler(ITrainingPlanRepository trainingPlanRepository, IMapper mapper)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _mapper = mapper;
        }
        public IMapper Mapper { get; }
        public async Task<PaginatedResult<TrainingPlanDto>> Handle(GetAllTrainingPlansQuery request, CancellationToken cancellationToken)
        {
            var trainingPlans = await _trainingPlanRepository.GetAllAsync();

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
