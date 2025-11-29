namespace CoachBuddy.Application.Client
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? About { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? EncodedName { get; set; }
        public bool IsEditable { get; set; }
    }
}
