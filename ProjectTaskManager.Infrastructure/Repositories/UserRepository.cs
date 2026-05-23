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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) => _context = context;

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Users.FindAsync([id], cancellationToken);

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
            => await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }
}
