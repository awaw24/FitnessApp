using AutoMapper;
using CoachBuddy.Application.ClientGroup;
using CoachBuddy.Domain.Interfaces.Client;
using CoachBuddy.Domain.Interfaces.Group;
using MediatR;

namespace CoachBuddy.Application.Group.Commands.AddClientToGroup
{
    public class AddClientToGroupCommandHandler : IRequestHandler<AddClientToGroupCommand>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public AddClientToGroupCommandHandler(IGroupRepository groupRepository,IClientRepository clientRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(AddClientToGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId);

            if (group == null)
            {
                throw new KeyNotFoundException($"Group with ID {request.GroupId} not found.");
            }

            var client = await _clientRepository.GetByIdAsync(request.ClientId);

            if (client == null)
            {
                throw new KeyNotFoundException($"Client with ID {request.ClientId} not found.");
            }

            if (group.ClientGroups.Any(cg => cg.ClientId == request.ClientId))
            {
                throw new InvalidOperationException($"Client with ID {request.ClientId} is already assigned to this group.");
            }

            var clientGroup = new Domain.Entities.Group.ClientGroup
            {
                GroupId = request.GroupId,
                ClientId = request.ClientId,
                AssignedAt = DateTime.UtcNow
            };

            group.ClientGroups.Add(clientGroup);

            await _groupRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
