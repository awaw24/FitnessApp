using AutoMapper;
using CoachBuddy.Application.ClientGroup;
using CoachBuddy.Domain.Interfaces.Client;
using CoachBuddy.Domain.Interfaces.Group;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoachBuddy.Application.Group.Queries.GetGroupDetails
{
    public class GetGroupDetailsQueryHandler : IRequestHandler<GetGroupDetailsQuery, GroupDetailsDto>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetGroupDetailsQueryHandler> _logger;

        public GetGroupDetailsQueryHandler(IGroupRepository groupRepository, IClientRepository clientRepository, IMapper mapper,ILogger<GetGroupDetailsQueryHandler> logger)
        {
            _groupRepository = groupRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GroupDetailsDto> Handle(GetGroupDetailsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Received encodedName: {request.EncodedName}");

            var group = await _groupRepository.GetByEncodedNameAsync(request.EncodedName);

            if (group == null)
            {
                throw new KeyNotFoundException($"Group with encoded name '{request.EncodedName}' not found.");
            }

            var availableClients = await _clientRepository.GetAvailableClientsForGroupAsync(group.Id);

            var groupDetailsDto = _mapper.Map<GroupDetailsDto>(group);
            groupDetailsDto.ClientGroups = group.ClientGroups.Select(cg => _mapper.Map<ClientGroupDto>(cg)).ToList();
            groupDetailsDto.AvailableClients = availableClients.Select(c => _mapper.Map<Client.AvailableClientDto>(c)).ToList();

            return groupDetailsDto;
        }
    }
}
