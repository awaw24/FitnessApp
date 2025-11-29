using AutoMapper;
using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.Group;
using MediatR;

namespace CoachBuddy.Application.Group.Commands.DeleteGroup
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public DeleteGroupCommandHandler(IGroupRepository groupRepository, IMapper mapper, IUserContext userContext)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.Id);
            
            var user = _userContext.GetCurrentUser();

            var isEditable = user != null &&  user.IsInRole("Admin");

            if (!isEditable)
            {
                return Unit.Value;
            }
            if(group == null)
            {
                throw new KeyNotFoundException($"Group with ID {request.Id} not found.");
            }

            await _groupRepository.DeleteAsync(group);
            return Unit.Value;
        }
    }
}
