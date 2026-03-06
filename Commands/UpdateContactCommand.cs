using ContactManagementAPI.Dto;
using ContactManagementAPI.Services;
using MediatR;

namespace ContactManagementAPI.Commands;

public record UpdateContactCommand(ContactDto ContactDto) : IRequest<ContactDto>;

public class UpdateContactCommandHandler(IContactManagmentService contactService)
    : IRequestHandler<UpdateContactCommand, ContactDto?>
{
    private readonly IContactManagmentService _contactService = contactService;

    public Task<ContactDto?> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        => _contactService.UpdateContactAsync(request.ContactDto, cancellationToken);
}
