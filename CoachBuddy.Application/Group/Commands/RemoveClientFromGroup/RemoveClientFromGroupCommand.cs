using MediatR;

namespace CoachBuddy.Application.Group.Commands.RemoveClientFromGroup
{
    public class RemoveClientFromGroupCommand:IRequest
    {
        public string EncodedName { get; set; }
        public int ClientId { get; set; }
    }
}
