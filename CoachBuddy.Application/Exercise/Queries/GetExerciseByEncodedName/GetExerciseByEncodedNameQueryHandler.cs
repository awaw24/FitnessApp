using AutoMapper;
using CoachBuddy.Domain.Interfaces.Exercise;
using MediatR;

namespace CoachBuddy.Application.Exercise.Queries.GetExerciseByEncodedName
{
    public class GetExerciseByEncodedNameQueryHandler : IRequestHandler<GetExerciseByEncodedNameQuery, ExerciseDto>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public GetExerciseByEncodedNameQueryHandler(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }
        public async Task<ExerciseDto> Handle(GetExerciseByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var exercise = await _exerciseRepository.GetByEncodedName(request.EncodedName);

            var dto  = _mapper.Map<ExerciseDto>(exercise);

            return dto;
        }
    }
}
