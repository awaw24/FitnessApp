using AutoMapper;
using CoachBuddy.Domain.Interfaces.Client;
using CoachBuddy.Domain.Interfaces.Group;
using MediatR;
using System.Security.Cryptography.Xml;

namespace CoachBuddy.Application.Group.Commands.RemoveClientFromGroup
{
    public class RemoveClientFromGroupCommandHandler : IRequestHandler<RemoveClientFromGroupCommand>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public RemoveClientFromGroupCommandHandler(IGroupRepository groupRepository, IClientRepository clientRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(RemoveClientFromGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByEncodedNameAsync(request.EncodedName);

            if(group == null)
            {
                throw new KeyNotFoundException($"Group with ID {request.EncodedName} not found."); 
            }

            var client = await _clientRepository.GetByIdAsync(request.ClientId);

            if(client == null)
            {
                throw new KeyNotFoundException($"Client with ID {request.ClientId} not found.");
            }

            var clientGroup = group.ClientGroups.FirstOrDefault(cg => cg.ClientId == request.ClientId);

            if(clientGroup == null)
            {
                throw new InvalidOperationException($"Client with ID {request.ClientId} is not assigned to this group");
            }

            group.ClientGroups.Remove(clientGroup);

            await _groupRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
