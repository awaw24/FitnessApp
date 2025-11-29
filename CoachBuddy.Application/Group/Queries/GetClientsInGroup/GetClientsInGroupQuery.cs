using MediatR;

namespace CoachBuddy.Application.Group.Queries.GetClientsInGroup
{
    public class GetClientsInGroupQuery : IRequest<List<Domain.Entities.Client.Client>>
    {
        public int GroupId { get; set; }
    }
}
