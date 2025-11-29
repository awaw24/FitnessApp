namespace CoachBuddy.Application.ClientTraining
{
    public class ClientTrainingDto
    {
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;
    }
}
