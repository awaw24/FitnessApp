using CoachBuddy.Application.Group;
using MediatR;

namespace CoachBuddy.Application.Group.Queries.GetGroupByEncodedName
{
    public class GetGroupByEncodedNameQuery : IRequest<GroupDto>
    {
        public string EncodedName { get; set; }
        public GetGroupByEncodedNameQuery(string endcodedName)
        {
            EncodedName = endcodedName;
        }
    }
}
