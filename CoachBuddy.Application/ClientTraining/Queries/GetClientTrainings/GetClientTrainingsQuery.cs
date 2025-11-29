using MediatR;

namespace CoachBuddy.Application.ClientTraining.Queries.GetClientTrainings
{
    public class GetClientTrainingsQuery:IRequest<IEnumerable<ClientTrainingDto>>
    {
        public string EncodedName { get; set; } = default!;
    }
}
