using MediatR;

namespace CoachBuddy.Application.Client.Commands.DeleteClient
{
    public class DeleteClientCommand : ClientDto, IRequest
    {
        public int Id { get; set; }
    }
}
