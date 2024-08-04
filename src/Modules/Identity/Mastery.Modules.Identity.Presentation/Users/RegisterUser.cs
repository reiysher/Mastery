using Mastery.Common.Domain;
using Mastery.Common.Presentation.Endpoints;
using Mastery.Common.Presentation.Results;
using Mastery.Modules.Identity.Application.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Mastery.Modules.Identity.Presentation.Users;

internal sealed class RegisterUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/register", async (
                [FromServices] ISender sender,
                [FromBody] RegisterUserRequest request,
                CancellationToken cancellationToken) =>
            {
                RegisterUserCommand command = request.ToCommand();
                Result<Guid> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .AllowAnonymous()
            .WithTags("Users")
            .WithOpenApi();
    }
}

public sealed record RegisterUserRequest(
    string Email,
    string FirstName,
    string LastName,
    string Password)
{
    public RegisterUserCommand ToCommand()
    {
        return new RegisterUserCommand(Email, FirstName, LastName, Password);
    }
}
