using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.Client;
using MediatR;

namespace CoachBuddy.Application.ClientTraining.Commands
{
    public class CreateClientTrainingCommandHandler : IRequestHandler<CreateClientTrainingCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IClientRepository _clientRepository;
        private readonly IClientTrainingRepository _clientTrainingRepository;

        public CreateClientTrainingCommandHandler(IUserContext userContext, IClientRepository clientRepository,
            IClientTrainingRepository clientTrainingRepository)
        {
            _userContext = userContext;
            _clientRepository = clientRepository;
            _clientTrainingRepository = clientTrainingRepository;
        }
        public async Task<Unit> Handle(CreateClientTrainingCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByEncodedName(request.ClientEncodedName!);

            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && ( user.IsInRole("Admin"));

            if (!isEditable)
            {
                return Unit.Value;
            }

            var clientTraining = new Domain.Entities.Client.ClientTraining()
            {
                Date = request.Date,
                Description = request.Description,
                ClientId = client.Id
            };

            await _clientTrainingRepository.Create(clientTraining);

            return Unit.Value;
        }
    }
}
