namespace Mastery.Common.Application.Security;

public interface ICurrentUserContext
{
    Guid UserId { get; }
}
