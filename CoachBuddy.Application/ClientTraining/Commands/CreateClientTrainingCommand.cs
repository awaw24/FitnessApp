using MediatR;

namespace CoachBuddy.Application.ClientTraining.Commands
{
    public class CreateClientTrainingCommand:ClientTrainingDto,IRequest
    {
        public string ClientEncodedName { get; set; } = default!;
    }
}
