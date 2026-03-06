using ContactManagementAPI.Data;
using ContactManagementAPI.Dto;
using ContactManagementAPI.Entities;
using ContactManagementAPI.Enums;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementAPI.Services;

public class ContactManagmentService : IContactManagmentService
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public ContactManagmentService(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<ContactDto> CreateContactAsync(ContactDto contactDto, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var contact = MapToEntity(contactDto);
        contact.Id = Guid.NewGuid();
        contact.CreateTime = DateTime.UtcNow;

        dbContext.Contacts.Add(contact);
        await dbContext.SaveChangesAsync(cancellationToken);

        return MapToDto(contact);
    }

    public async Task<ContactDto?> UpdateContactAsync(ContactDto contactDto, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var contact = await dbContext.Contacts
            .FirstOrDefaultAsync(c => c.Id == contactDto.Id, cancellationToken);

        if (contact is null)
            return null;

        contact.Name = contactDto.Name;
        contact.Email = contactDto.Email;
        contact.Phone = contactDto.Phone;
        contact.CustomFields = contactDto.CustomFields;

        dbContext.Contacts.Update(contact);
        await dbContext.SaveChangesAsync(cancellationToken);

        return MapToDto(contact);
    }

    public async Task DeleteContactAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var contact = await dbContext.Contacts
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (contact is not null)
        {
            dbContext.Contacts.Remove(contact);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<PagedResultDto<ContactDto>> GetContactsAsync(ContactQueryDto query, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var queryable = dbContext.Contacts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SearchText))
        {
            var search = query.SearchText.Trim().ToLower();
            queryable = queryable.Where(c =>
                (c.Name != null && c.Name.ToLower().Contains(search)) ||
                (c.Email != null && c.Email.ToLower().Contains(search)) ||
                (c.Phone != null && c.Phone.ToLower().Contains(search)) ||
                (c.CustomFields != null && c.CustomFields.ToLower().Contains(search)));
        }

        queryable = query.SortBy switch
        {
            ContactSortField.Name => query.Ascending
                ? queryable.OrderBy(c => c.Name)
                : queryable.OrderByDescending(c => c.Name),
            ContactSortField.Email => query.Ascending
                ? queryable.OrderBy(c => c.Email)
                : queryable.OrderByDescending(c => c.Email),
            ContactSortField.Phone => query.Ascending
                ? queryable.OrderBy(c => c.Phone)
                : queryable.OrderByDescending(c => c.Phone),
            _ => query.Ascending
                ? queryable.OrderBy(c => c.CreateTime)
                : queryable.OrderByDescending(c => c.CreateTime)
        };

        var totalCount = await queryable.CountAsync(cancellationToken);

        var items = await queryable
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResultDto<ContactDto>
        {
            Items = items.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }

    public async Task<ContactDto?> GetContactByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var contact = await dbContext.Contacts
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return contact is not null ? MapToDto(contact) : null;
    }

    private static ContactDto MapToDto(Contact contact) => new()
    {
        Id = contact.Id,
        Name = contact.Name,
        Email = contact.Email,
        Phone = contact.Phone,
        CustomFields = contact.CustomFields
    };

    private static Contact MapToEntity(ContactDto dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name,
        Email = dto.Email,
        Phone = dto.Phone,
        CustomFields = dto.CustomFields
    };
}
