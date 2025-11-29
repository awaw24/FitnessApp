using CoachBuddy.Application.Common;
using CoachBuddy.Application.Group;
using MediatR;

namespace CoachBuddy.Application.Group.Queries.GetAllGroups
{
    public class GetAllGroupsQuery : IRequest<PaginatedResult<GroupDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
