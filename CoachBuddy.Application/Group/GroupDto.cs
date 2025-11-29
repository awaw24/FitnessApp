using CoachBuddy.Application.Client;
using CoachBuddy.Application.ClientGroup;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoachBuddy.Application.Group
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? EncodedName { get; set; }
        public string? CreatedById { get; set; }
        public bool IsEditable { get; set; }
    }
}
