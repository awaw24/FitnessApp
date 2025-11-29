using MediatR;

namespace CoachBuddy.Application.TrainingPlan.Queries.GetTrainingPlanByEncodedName
{
    public class GetTrainingPlanByEncodedNameQuery:IRequest<TrainingPlanDto>
    {
        public string EncodedName { get; set; }
        public GetTrainingPlanByEncodedNameQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
