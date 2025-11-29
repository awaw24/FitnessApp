using CoachBuddy.Application.Common;
using MediatR;

namespace CoachBuddy.Application.Group.Queries.GetGroupsBySearch
{
    public class GetGroupsBySearchQuery:IRequest<PaginatedResult<GroupDto>>
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetGroupsBySearchQuery(string searchTerm, int pageNumber = 1, int pageSize = 7)
        {
            SearchTerm = searchTerm;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
