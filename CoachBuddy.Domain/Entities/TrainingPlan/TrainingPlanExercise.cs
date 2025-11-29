namespace CoachBuddy.Domain.Entities.TrainingPlan
{
    public class TrainingPlanExercise
    {
        public int Id { get; set; }
        public int TrainingPlanId { get; set; }
        public TrainingPlan? TrainingPlan { get; set; } = default!;

        public int ExerciseId { get; set; }
        public Exercise.Exercise? Exercise { get; set; } = default!;

        public int? Sets { get; set; }
        public int? Repetitions { get; set; }
        public int? RestTime { get; set; }
        public string? Notes { get; set; }
    }
}
