using CoachBuddy.Domain.Entities.Exercise;
using CoachBuddy.Domain.Interfaces.Exercise;
using CoachBuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoachBuddy.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly CoachBuddyDbContext _dbContext;
        public ExerciseRepository(CoachBuddyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Exercise exercise)
        {
            await _dbContext.Exercises.AddAsync(exercise);
            await _dbContext.SaveChangesAsync();
        }

        public Task Commit()
        => _dbContext.SaveChangesAsync();

        public async Task Create(Exercise exercise)
        {
            _dbContext.Add(exercise);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Exercise exercise)
        {
             _dbContext.Exercises.Remove(exercise);
             await _dbContext.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await _dbContext.Exercises.ToListAsync();
        }

        public async Task<Exercise> GetByEncodedName(string encodedName)
            => await _dbContext.Exercises.FirstAsync(e=>e.EncodedName ==encodedName);

        public async Task<Exercise> GetByIdAsync(int id)
        {
            return await _dbContext.Exercises.FindAsync(id);
        }

        public async Task<int> GetExerciseCountAsync()
        {
            return await _dbContext.Exercises.CountAsync();
        }

        public async Task UpdateAsync(Exercise exercise)
        {
            _dbContext.Exercises.Update(exercise);
            await _dbContext.SaveChangesAsync();
        }
    }
}
