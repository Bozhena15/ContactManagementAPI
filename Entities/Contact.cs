namespace ContactManagementAPI.Entities;

public class Contact
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; } = string.Empty;
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    public string? CustomFields { get; set; }
}
