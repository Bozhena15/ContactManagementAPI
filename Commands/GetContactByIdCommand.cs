using ContactManagementAPI.Dto;
using ContactManagementAPI.Services;
using MediatR;

namespace ContactManagementAPI.Commands;

public record GetContactByIdCommand(Guid Id) : IRequest<ContactDto>;

public class GetContactByIdCommandHandler(IContactManagmentService contactService)
    : IRequestHandler<GetContactByIdCommand, ContactDto?>
{
    private readonly IContactManagmentService _contactService = contactService;

    public Task<ContactDto?> Handle(GetContactByIdCommand request, CancellationToken cancellationToken)
        => _contactService.GetContactByIdAsync(request.Id, cancellationToken);
}
