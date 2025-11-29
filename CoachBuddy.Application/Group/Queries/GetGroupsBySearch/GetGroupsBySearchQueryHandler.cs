using AutoMapper;
using CoachBuddy.Application.Client;
using CoachBuddy.Application.Common;
using CoachBuddy.Domain.Interfaces.Client;
using CoachBuddy.Domain.Interfaces.Group;
using MediatR;

namespace CoachBuddy.Application.Group.Queries.GetGroupsBySearch
{
    public class GetGroupsBySearchQueryHandler : IRequestHandler<GetGroupsBySearchQuery, PaginatedResult<GroupDto>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GetGroupsBySearchQueryHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<GroupDto>> Handle(GetGroupsBySearchQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                groups = groups.Where(c =>
                    c.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            var totalGroups = groups.Count();

            var paginatedGroups = groups
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtos = _mapper.Map<List<GroupDto>>(paginatedGroups);

            return new PaginatedResult<GroupDto>
            {
                Items = dtos,
                TotalCount = totalGroups,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
