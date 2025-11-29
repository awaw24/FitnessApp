using AutoMapper;
using CoachBuddy.Application.Common;
using CoachBuddy.Domain.Interfaces.Exercise;
using MediatR;
using System.Numerics;

namespace CoachBuddy.Application.Exercise.Queries.GetExercisesBySearch
{
    public class GetExercisesBySearchQueryHandler : IRequestHandler<GetExercisesBySearchQuery, PaginatedResult<ExerciseDto>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;
        public GetExercisesBySearchQueryHandler(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<ExerciseDto>> Handle(GetExercisesBySearchQuery request, CancellationToken cancellationToken)
        {
            var exercises = await _exerciseRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                exercises = exercises.Where(e =>
                    e.Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var totalExercises = exercises.Count();

            var paginatedExercises = exercises
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var dtos = _mapper.Map<List<ExerciseDto>>(paginatedExercises);

            return new PaginatedResult<ExerciseDto>{
                Items = dtos,
                TotalCount = totalExercises,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
