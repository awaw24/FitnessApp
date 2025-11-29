using CoachBuddy.Domain.Entities.TrainingPlan;
using CoachBuddy.Domain.Interfaces.TrainingPlan;
using CoachBuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoachBuddy.Infrastructure.Repositories
{
    public class TrainingPlanRepository : ITrainingPlanRepository
    {
        private readonly CoachBuddyDbContext _dbContext;
        public TrainingPlanRepository(CoachBuddyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TrainingPlan trainingPlan)
        {
            await _dbContext.TrainingPlans.AddAsync(trainingPlan);
            await _dbContext.SaveChangesAsync();
        }

        public Task Commit()
            =>_dbContext.SaveChangesAsync();

        public async Task Create(TrainingPlan trainingPlan)
        {
            _dbContext.Add(trainingPlan);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TrainingPlan trainingPlan)
        {
            _dbContext.TrainingPlans.Remove(trainingPlan);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingPlan>> GetAllAsync()
        {
            return await _dbContext.TrainingPlans.ToListAsync();
        }

        public async Task<TrainingPlan> GetByEncodedName(string encodedName)
            => await _dbContext.TrainingPlans.FirstAsync(e => e.EncodedName == encodedName);
        public async Task<TrainingPlan> GetByIdAsync(int id)
        {
            var trainingPlan = await _dbContext.TrainingPlans
               .Include(tp => tp.TrainingPlanExercises)
               .Include(tp => tp.Groups)
               .FirstOrDefaultAsync(tp => tp.Id == id);

            return trainingPlan;
        }

        public async Task<int> GetTrainingPlanCountAsync()
        {
            return await _dbContext.TrainingPlans.CountAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TrainingPlan trainingPlan)
        {
            _dbContext.TrainingPlans.Update(trainingPlan);
            await _dbContext.SaveChangesAsync();
        }
    }
}
