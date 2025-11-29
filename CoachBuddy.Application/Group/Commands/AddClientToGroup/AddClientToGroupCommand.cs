using MediatR;

namespace CoachBuddy.Application.Group.Commands.AddClientToGroup
{
    public class AddClientToGroupCommand:IRequest
    {
        public int GroupId { get; set; }
        public int ClientId { get; set; }
    }
}
