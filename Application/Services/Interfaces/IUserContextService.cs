namespace Application.Services.Interfaces;

public interface IUserContextService
{
    Guid? GetCurrentUserId();
}