using AutoMapper;
using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.Exercise;
using MediatR;

namespace CoachBuddy.Application.Exercise.Commands.DeleteExercise
{
    public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserContext _userContext;
        public DeleteExerciseCommandHandler(IExerciseRepository exerciseRepository, IMapper mapper, IUserContext userContext)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(request.Id);

            var user = _userContext.GetCurrentUser();

            var isEditable = user != null && user.IsInRole("Admin");

            if (!isEditable)
            {
                return Unit.Value;
            }

            if (exercise == null)
            {
                throw new KeyNotFoundException($"Exercise with ID {request.Id} not found.");
            }

            await _exerciseRepository.DeleteAsync(exercise);
            return Unit.Value;
        }
    }
}
