using ContactManagementAPI.Dto;
using ContactManagementAPI.Services;
using MediatR;

namespace ContactManagementAPI.Commands;

public record CreateContactCommand(
    ContactDto ContactDto) : IRequest<ContactDto>;

public class CreateContactCommandHandler(IContactManagmentService contactService)
    : IRequestHandler<CreateContactCommand, ContactDto>
{
    private readonly IContactManagmentService _contactService = contactService;

    public Task<ContactDto> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        => _contactService.CreateContactAsync(request.ContactDto, cancellationToken);
}
