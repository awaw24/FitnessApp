using CoachBuddy.Application.ApplicationUser;
using CoachBuddy.Domain.Interfaces.Client;
using MediatR;

namespace CoachBuddy.Application.Client.Commands.EditClient
{
    public class EditClientCommandHandler : IRequestHandler<EditClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserContext _userContext;

        public EditClientCommandHandler(IClientRepository clientRepository, IUserContext userContext)
        {
            _clientRepository = clientRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByEncodedName(request.EncodedName!);

            var user = _userContext.GetCurrentUser();

            var isEditable = user != null &&  user.IsInRole("Admin");

            if (!isEditable)
            {
                return Unit.Value;
            }

            client.Name = request.Name;
            client.LastName = request.LastName;
            client.Email = request.Email;
            client.Description=request.Description;
            client.About=request.About;

            client.ContactDetails.City = request.City;
            client.ContactDetails.PhoneNumber = request.PhoneNumber;
            client.ContactDetails.PostalCode = request.PostalCode;
            client.ContactDetails.Street = request.Street;

            await _clientRepository.Commit();

            return Unit.Value;
        }   
    }
}
