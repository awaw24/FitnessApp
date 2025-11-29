using CoachBuddy.Domain.Entities.Client;

namespace CoachBuddy.Domain.Interfaces.Client
{
    public interface IClientTrainingRepository
    {
        Task Create(ClientTraining clientTraining);
        Task<IEnumerable<ClientTraining>> GetAllByEncodedName(string encodedName);
    }
}
