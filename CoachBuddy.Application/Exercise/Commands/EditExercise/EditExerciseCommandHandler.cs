using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.Client;
using CoachBuddy.Domain.Interfaces.Exercise;
using MediatR;

namespace CoachBuddy.Application.Exercise.Commands.EditExercise
{
    public class EditExerciseCommandHandler : IRequestHandler<EditExerciseCommand>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserContext _userContext;
        public EditExerciseCommandHandler(IExerciseRepository exerciseRepository, IUserContext userContext)
        {
            _exerciseRepository = exerciseRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditExerciseCommand request, CancellationToken cancellationToken)
        {
            var exercise = await _exerciseRepository.GetByEncodedName(request.EncodedName!);

            var user = _userContext.GetCurrentUser();

            var isEditable = user != null && user.IsInRole("Admin");

            if (!isEditable)
            {
                return Unit.Value;
            }
            exercise.Description = request.Description;
            exercise.MuscleGroup = request.MuscleGroup;

            await _exerciseRepository.Commit();

            return Unit.Value;
        }
    }
}
