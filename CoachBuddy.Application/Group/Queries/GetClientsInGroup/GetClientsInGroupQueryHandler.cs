using CoachBuddy.Domain.Interfaces.Group;
using MediatR;

namespace CoachBuddy.Application.Group.Queries.GetClientsInGroup
{
    public class GetClientsInGroupQueryHandler : IRequestHandler<GetClientsInGroupQuery, List<Domain.Entities.Client.Client>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetClientsInGroupQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<List<Domain.Entities.Client.Client>> Handle(GetClientsInGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupWithClientsAsync(request.GroupId);

            if (group == null)
            {
                throw new ArgumentException("Group not found.");
            }

            return group.ClientGroups.Select(cg => cg.Client).ToList();
        }
    }
}
