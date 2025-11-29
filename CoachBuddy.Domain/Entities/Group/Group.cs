using Microsoft.AspNetCore.Identity;

namespace CoachBuddy.Domain.Entities.Group
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? CreatedById { get; set; }
        public IdentityUser? CreatedBy { get; set; }

        public string EncodedName { get; private set; } = default!;

        public List<ClientGroup> ClientGroups { get; set; } = new();
        public List<TrainingPlan.TrainingPlan> TrainingPlans { get; set; } = new();

        public void EncodeName() => EncodedName = $"{Name.ToLower().Replace(" ", "-")}";
    }
}
