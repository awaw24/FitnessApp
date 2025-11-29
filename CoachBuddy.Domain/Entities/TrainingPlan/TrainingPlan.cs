namespace CoachBuddy.Domain.Entities.TrainingPlan
{
    public class TrainingPlan
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string EncodedName { get; private set; } = default!;

        public List<TrainingPlanExercise>? TrainingPlanExercises { get; set; } = new List<TrainingPlanExercise>();
        public List<Group.Group>? Groups { get; set; } = new List<Group.Group>();

        public void EncodeName() => EncodedName = $"{Name.ToLower().Replace(" ","-")}";
    }
}
