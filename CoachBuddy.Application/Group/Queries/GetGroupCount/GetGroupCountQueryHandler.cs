using CoachBuddy.Domain.Interfaces.Group;
using MediatR;

namespace CoachBuddy.Application.Group.Queries.GetGroupCount
{
    public class GetGroupCountQueryHandler : IRequestHandler<GetGroupCountQuery, int>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupCountQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<int> Handle(GetGroupCountQuery request, CancellationToken cancellationToken)
        {
            return await _groupRepository.GetGroupCountAsync();
        }
    }
}
