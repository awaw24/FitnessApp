using MediatR;

namespace CoachBuddy.Application.Group.Commands.DeleteGroup
{
    public class DeleteGroupCommand : GroupDto, IRequest
    {
        public int Id { get; set; }
    }
}
