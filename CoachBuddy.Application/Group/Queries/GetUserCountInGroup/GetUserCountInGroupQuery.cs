using MediatR;

namespace CoachBuddy.Application.Group.Queries.GetUserCountInGroup
{
    public class GetUserCountInGroupQuery:IRequest<int>
    {
        public int GroupId { get; set; }
    }
}
