using Microsoft.AspNetCore.Routing;

namespace Mastery.Common.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}