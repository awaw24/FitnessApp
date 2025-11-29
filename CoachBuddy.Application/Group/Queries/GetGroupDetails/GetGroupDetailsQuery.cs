using MediatR;

namespace CoachBuddy.Application.Group.Queries.GetGroupDetails
{
    public class GetGroupDetailsQuery:IRequest<GroupDetailsDto>
    {
        public string EncodedName { get; set; } = default!;
    }
}

