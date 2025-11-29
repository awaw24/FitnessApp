using CoachBuddy.Domain.Entities.Client;
using CoachBuddy.Domain.Interfaces.Client;
using CoachBuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoachBuddy.Infrastructure.Repositories
{
    public class ClientTrainingRepository : IClientTrainingRepository
    {
        private readonly CoachBuddyDbContext _dbContext;
        public ClientTrainingRepository(CoachBuddyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(ClientTraining clientTraining)
        {
            _dbContext.Trainings.Add(clientTraining);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<ClientTraining>> GetAllByEncodedName(string encodedName)
        => await _dbContext.Trainings
            .Where(s => s.Client.EncodedName == encodedName)
            .ToListAsync();
    }
}
