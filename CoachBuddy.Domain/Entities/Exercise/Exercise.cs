using CoachBuddy.Domain.Entities.TrainingPlan;

namespace CoachBuddy.Domain.Entities.Exercise
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? MuscleGroup { get; set; }
        public string EncodedName { get; private set; } = default!;

        public List<TrainingPlanExercise> TrainingPlanExercises { get; set; } = new();

        public void EncodeName() => EncodedName = $"{Name.ToLower().Replace(" ", "-")}";
    }
}
