namespace CoachBuddy.Domain.Entities.Client
{
    public class ClientTraining
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;

        public int ClientId { get; set; } = default!;
        public Client Client { get; set; } = default!;

    }
}
