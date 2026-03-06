using ContactManagementAPI.Enums;

namespace ContactManagementAPI.Dto;

public class ContactQueryDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public ContactSortField SortBy { get; set; } = ContactSortField.CreateTime;
    public bool Ascending { get; set; } = false;
    public string? SearchText { get; set; }
}
