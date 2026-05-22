using ProjectTaskManager.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManager.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public Guid UserId => throw new NotImplementedException();

        public string? UserRole => throw new NotImplementedException();

        public bool IsAuthenticated => throw new NotImplementedException();
    }
}
