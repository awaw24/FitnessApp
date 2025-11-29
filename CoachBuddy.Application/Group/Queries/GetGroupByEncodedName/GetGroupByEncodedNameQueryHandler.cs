using AutoMapper;
using CoachBuddy.Application.Group;
using CoachBuddy.Domain.Interfaces.Group;
using MediatR;

namespace CoachBuddy.Application.Group.Queries.GetGroupByEncodedName
{
    public class GetGroupByEncodedNameQueryHandler : IRequestHandler<GetGroupByEncodedNameQuery, GroupDto>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GetGroupByEncodedNameQueryHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }
        public async Task<GroupDto> Handle(GetGroupByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByEncodedName(request.EncodedName);

            var dto = _mapper.Map<GroupDto>(group);

            return dto;
        }
    }
}
