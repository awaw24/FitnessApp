using AutoMapper;
using CoachBuddy.Application.Common;
using CoachBuddy.Domain.Interfaces.Exercise;
using MediatR;

namespace CoachBuddy.Application.Exercise.Queries.GetAllExercises
{
    public class GetAllExercisesQueryHandler : IRequestHandler<GetAllExercisesQuery, PaginatedResult<ExerciseDto>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;
        public GetAllExercisesQueryHandler(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }
        public IMapper Mapper { get; }
        public async Task<PaginatedResult<ExerciseDto>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
        {
            var exercises = await _exerciseRepository.GetAllAsync();

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
