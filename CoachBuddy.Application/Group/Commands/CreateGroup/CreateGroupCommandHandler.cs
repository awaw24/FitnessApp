using AutoMapper;
using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.Client;
using CoachBuddy.Domain.Interfaces.Group;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoachBuddy.Application.Group.Commands.CreateGroup
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        public CreateGroupCommandHandler(IGroupRepository groupRepository, IMapper mapper, IUserContext userContext)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public IUserContext UserContext { get; }
        public async Task<Unit> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            if (currentUser == null || !currentUser.IsInRole("Admin"))
                {
                return Unit.Value;
            }

            var group = _mapper.Map<Domain.Entities.Group.Group>(request);

            group.EncodeName();

            group.CreatedById = currentUser.Id;

            group.CreatedAt = DateTime.UtcNow;

            await _groupRepository.Create(group);

            return Unit.Value;
        }
    }
}
