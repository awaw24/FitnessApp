using CoachBuddy.Application.ClientGroup;

namespace CoachBuddy.Application.Group
{
    public class GroupDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? EncodedName { get; set; }
        public string? CreatedById { get; set; }
        public bool IsEditable { get; set; }
        public List<ClientGroupDto> ClientGroups { get; set; } = new();
        public List<Client.AvailableClientDto> AvailableClients { get; set; } = new();
    }
}
