namespace CoachBuddy.Domain.Interfaces.TrainingPlan
{
    public interface ITrainingPlanRepository
    {
        Task Create(Entities.TrainingPlan.TrainingPlan trainingPlan);
        Task<IEnumerable<Entities.TrainingPlan.TrainingPlan>> GetAllAsync();
        Task<Entities.TrainingPlan.TrainingPlan> GetByIdAsync(int id);
        Task AddAsync(Entities.TrainingPlan.TrainingPlan trainingPlan);
        Task UpdateAsync(Entities.TrainingPlan.TrainingPlan trainingPlan);
        Task DeleteAsync(Entities.TrainingPlan.TrainingPlan id);
        Task Commit();
        Task SaveAsync();
        Task<int> GetTrainingPlanCountAsync();
        Task<Entities.TrainingPlan.TrainingPlan> GetByEncodedName(string encodedName);
    }
}
