using Microsoft.EntityFrameworkCore;
using ProjectTaskManager.Domain.Entities;
using ProjectTaskManager.Domain.Interfaces;
using ProjectTaskManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context) => _context = context;

        public async Task<ProjectTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Tasks.FindAsync([id], cancellationToken);

        public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
            => await _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(cancellationToken);

        public async Task<ProjectTask> AddAsync(ProjectTask task, CancellationToken cancellationToken = default)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync(cancellationToken);
            return task;
        }

        public async Task UpdateAsync(ProjectTask task, CancellationToken cancellationToken = default)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ProjectTask task, CancellationToken cancellationToken = default)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
