using AutoMapper;
using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.Client;
using MediatR;

namespace CoachBuddy.Application.Client.Commands.DeleteClient
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        public DeleteClientCommandHandler(IClientRepository clientRepository, IMapper mapper, IUserContext userContext)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.Id);

            var user = _userContext.GetCurrentUser();

            var isEditable = user != null && user.IsInRole("Admin");

            if (!isEditable)
            {
                return Unit.Value;
            }

            if (client == null)
            {
                throw new KeyNotFoundException($"Client with ID {request.Id} not found.");
            }

            await _clientRepository.DeleteAsync(client);
            return Unit.Value;
        }
    }
}
