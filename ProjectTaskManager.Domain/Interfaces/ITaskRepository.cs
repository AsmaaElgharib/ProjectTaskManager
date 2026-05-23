using ProjectTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<ProjectTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
        Task<ProjectTask> AddAsync(ProjectTask task, CancellationToken cancellationToken = default);
        Task UpdateAsync(ProjectTask task, CancellationToken cancellationToken = default);
        Task DeleteAsync(ProjectTask task, CancellationToken cancellationToken = default);
    }

}
