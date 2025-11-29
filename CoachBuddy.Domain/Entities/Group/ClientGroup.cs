namespace CoachBuddy.Domain.Entities.Group
{
    public class ClientGroup
    {
        public int ClientId { get; set; }
        public Client.Client Client { get; set; } = default!;
        public int GroupId { get; set; }
        public Group Group { get; set; } = default!;
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
