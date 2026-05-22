using Microsoft.EntityFrameworkCore;
using ProjectTaskManager.Domain.Entities;
using ProjectTaskManager.Domain.Interfaces;
using ProjectTaskManager.Infrastructure.Data;

namespace ProjectTaskManager.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context) => _context = context;

        public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Projects.FindAsync([id], cancellationToken);

        public async Task<Project?> GetByIdWithTasksAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<IEnumerable<Project>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
            => await _context.Projects
                .Include(p => p.Tasks)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);

        public async Task<Project> AddAsync(Project project, CancellationToken cancellationToken = default)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync(cancellationToken);
            return project;
        }

        public async Task UpdateAsync(Project project, CancellationToken cancellationToken = default)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Project project, CancellationToken cancellationToken = default)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
            => await _context.Projects.AnyAsync(p => p.Id == id && p.UserId == userId, cancellationToken);

    }
}
