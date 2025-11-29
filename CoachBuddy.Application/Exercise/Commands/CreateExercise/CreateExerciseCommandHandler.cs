using AutoMapper;
using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.Exercise;
using MediatR;
using System.ComponentModel;

namespace CoachBuddy.Application.Exercise.Commands.CreateExercise
{
    public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        public CreateExerciseCommandHandler(IExerciseRepository exerciseRepository, IMapper mapper, IUserContext userContext)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            var exercise = _mapper.Map<Domain.Entities.Exercise.Exercise>(request);

            var currentUser = _userContext.GetCurrentUser();

            if (currentUser == null || !currentUser.IsInRole("Admin"))
            {
                return Unit.Value;
            }
            exercise.EncodeName();

            await _exerciseRepository.Create(exercise);
            return Unit.Value;
        }
    }
}
