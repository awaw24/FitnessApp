using CoachBuddy.Domain.Interfaces.Group;
using MediatR;

namespace CoachBuddy.Application.Group.Queries.GetUserCountInGroup
{
    public class GetUserCountInGroupQueryHandler : IRequestHandler<GetUserCountInGroupQuery, int>
    {
        private readonly IGroupRepository _groupRepository;

        public GetUserCountInGroupQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<int> Handle(GetUserCountInGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupWithClientsAsync(request.GroupId);

            if (group == null)
            {
                throw new ArgumentException("Group not found.");
            }

            return group.ClientGroups.Count;
        }
    }
}
