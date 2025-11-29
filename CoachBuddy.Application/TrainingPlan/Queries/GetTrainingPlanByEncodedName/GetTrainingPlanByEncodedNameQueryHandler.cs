using AutoMapper;
using CoachBuddy.Domain.Interfaces.TrainingPlan;
using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Queries.GetTrainingPlanByEncodedName
{
    public class GetTrainingPlanByEncodedNameQueryHandler : IRequestHandler<GetTrainingPlanByEncodedNameQuery, TrainingPlanDto>
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly IMapper _mapper;
        public GetTrainingPlanByEncodedNameQueryHandler(ITrainingPlanRepository trainingPlanRepository, IMapper mapper)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _mapper = mapper;
        }
        public async Task<TrainingPlanDto> Handle(GetTrainingPlanByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var trainingPlan = await _trainingPlanRepository.GetByEncodedName(request.EncodedName);

            var dto = _mapper.Map<TrainingPlanDto>(trainingPlan);

            return dto;
        }
    }
}
