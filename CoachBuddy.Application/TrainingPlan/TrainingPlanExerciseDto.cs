namespace CoachBuddy.Application.TrainingPlan
{
    public class TrainingPlanExerciseDto
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int? Sets { get; set; }
        public int? Repetitions { get; set; }
        public int? RestTime { get; set; }
        public string? Notes { get; set; }

        public bool IsEditable { get; set; }
        public string? EncodedName { get; set; }

    }
}
