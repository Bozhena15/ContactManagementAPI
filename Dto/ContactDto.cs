namespace ContactManagementAPI.Dto;

public class ContactDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? CustomFields { get; set; }
}
