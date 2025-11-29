using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.Group;
using MediatR;

namespace CoachBuddy.Application.Group.Commands.EditGroup
{
    public class EditGroupCommandHandler : IRequestHandler<EditGroupCommand>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserContext _userContext;

        public EditGroupCommandHandler(IGroupRepository groupRepository, IUserContext userContext)
        {
            _groupRepository = groupRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByEncodedName(request.EncodedName!);  
       
            var user = _userContext.GetCurrentUser();

            var isEditable = user != null && user.IsInRole("Admin");
       
            if (!isEditable)
            {
                return Unit.Value;
            }

            group.Name = request.Name;
            group.Description = request.Description;

            await _groupRepository.Commit();

            return Unit.Value;


        }
    }
}
