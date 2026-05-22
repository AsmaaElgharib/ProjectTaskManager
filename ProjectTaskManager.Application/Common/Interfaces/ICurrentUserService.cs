namespace ProjectTaskManager.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string? UserRole { get; }
        bool IsAuthenticated { get; }
    }
}
