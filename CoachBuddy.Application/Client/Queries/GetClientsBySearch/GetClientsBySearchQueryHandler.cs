using AutoMapper;
using CoachBuddy.Application.Common;
using CoachBuddy.Domain.Interfaces.Client;
using MediatR;

namespace CoachBuddy.Application.Client.Queries.GetClientsBySearch
{
    public class GetClientsBySearchQueryHandler : IRequestHandler<GetClientsBySearchQuery, PaginatedResult<ClientDto>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientsBySearchQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ClientDto>> Handle(GetClientsBySearchQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                clients = clients.Where(c =>
                    c.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    c.LastName.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var totalClients = clients.Count();

            var paginatedClients = clients
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtos = _mapper.Map<List<ClientDto>>(paginatedClients);

            return new PaginatedResult<ClientDto>
            {
                Items = dtos,
                TotalCount = totalClients,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
