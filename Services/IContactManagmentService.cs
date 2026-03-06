using ContactManagementAPI.Dto;

namespace ContactManagementAPI.Services;

public interface IContactManagmentService
{
    Task<ContactDto> CreateContactAsync(ContactDto contactDto, CancellationToken cancellationToken = default);
    Task<ContactDto?> UpdateContactAsync(ContactDto contactDto, CancellationToken cancellationToken = default);
    Task DeleteContactAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResultDto<ContactDto>> GetContactsAsync(ContactQueryDto query, CancellationToken cancellationToken = default);
    Task<ContactDto?> GetContactByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
