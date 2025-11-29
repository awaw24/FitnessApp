namespace CoachBuddy.Domain.Interfaces.Exercise
{
    public interface IExerciseRepository
    {
        Task Create(Entities.Exercise.Exercise exercise);
        Task<IEnumerable<Entities.Exercise.Exercise>> GetAllAsync();
        Task<Entities.Exercise.Exercise> GetByIdAsync(int id);
        Task AddAsync(Entities.Exercise.Exercise exercise);
        Task UpdateAsync(Entities.Exercise.Exercise exercise);
        Task DeleteAsync(Entities.Exercise.Exercise id);
        Task Commit();
        Task<int> GetExerciseCountAsync();
        Task<Entities.Exercise.Exercise> GetByEncodedName(string encodedName);
    }
}
