using ProjectTaskManager.Domain.Entities;

namespace ProjectTaskManager.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task UpdateAsync(Project project, CancellationToken cancellationToken = default);
        Task<Project> AddAsync(Project project, CancellationToken cancellationToken = default);
        Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Project project, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Project?> GetByIdWithTasksAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
