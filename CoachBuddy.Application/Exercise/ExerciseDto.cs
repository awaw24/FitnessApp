namespace CoachBuddy.Application.Exercise
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? MuscleGroup { get; set; }
        public bool IsEditable { get; set; }
        public string? EncodedName { get; set; }
    }
}
