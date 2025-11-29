using CoachBuddy.Domain.Interfaces.Client;
using MediatR;

namespace CoachBuddy.Application.Client.Queries.GetClientCount
{
    public class GetClientCountQueryHandler : IRequestHandler<GetClientCountQuery, int>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientCountQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<int> Handle(GetClientCountQuery request, CancellationToken cancellationToken)
        {
            return await _clientRepository.GetClientCountAsync();
        }
    }
}
