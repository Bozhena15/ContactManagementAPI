using ContactManagementAPI.Dto;
using ContactManagementAPI.Services;
using MediatR;

namespace ContactManagementAPI.Commands;

public record GetContactsCommand(ContactQueryDto Query) : IRequest<PagedResultDto<ContactDto>>;

public class GetContactsQueryHandler(IContactManagmentService contactService)
    : IRequestHandler<GetContactsCommand, PagedResultDto<ContactDto>>
{
    private readonly IContactManagmentService _contactService = contactService;

    public Task<PagedResultDto<ContactDto>> Handle(GetContactsCommand request, CancellationToken cancellationToken)
        => _contactService.GetContactsAsync(request.Query, cancellationToken);
}
