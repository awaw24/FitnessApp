using AutoMapper;
using CoachBuddy.Domain.Interfaces.Client;
using MediatR;

namespace CoachBuddy.Application.ClientTraining.Queries.GetClientTrainings
{
    public class GetClientTrainingsQueryHandler : IRequestHandler<GetClientTrainingsQuery, IEnumerable<ClientTrainingDto>>
    {
        private readonly IClientTrainingRepository _clientTrainingRepository;
        private readonly IMapper _mapper;

        public GetClientTrainingsQueryHandler(IClientTrainingRepository clientTrainingRepository, IMapper mapper)
        {
            _clientTrainingRepository = clientTrainingRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ClientTrainingDto>> Handle(GetClientTrainingsQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientTrainingRepository.GetAllByEncodedName(request.EncodedName);

            var dtos= _mapper.Map<IEnumerable<ClientTrainingDto>>(result);

            return dtos;
        }
    }
}
