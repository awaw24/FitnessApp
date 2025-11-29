namespace CoachBuddy.Application.ClientGroup
{
    public class ClientGroupDto
    {
        public int ClientId { get; set; }
        public string FullName { get; set; } = default!;
        public DateTime AssignedAt { get; set; }
    }
}
