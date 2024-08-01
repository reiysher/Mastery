using Microsoft.AspNetCore.Routing;

namespace Mastery.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
