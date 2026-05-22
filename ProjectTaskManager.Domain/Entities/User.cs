using ProjectTaskManager.Domain.Common;

namespace ProjectTaskManager.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public IEnumerable<Project> Projects { get; set; } = new List<Project>();
    }
}
