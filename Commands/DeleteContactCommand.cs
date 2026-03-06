using ContactManagementAPI.Services;
using MediatR;

namespace ContactManagementAPI.Commands;

public record DeleteContactCommand(Guid Id) : IRequest;

public class DeleteContactCommandHandler(IContactManagmentService contactService)
    : IRequestHandler<DeleteContactCommand>
{
    private readonly IContactManagmentService _contactService = contactService;

    public Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        => _contactService.DeleteContactAsync(request.Id, cancellationToken);
}
