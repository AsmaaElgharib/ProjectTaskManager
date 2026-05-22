using ProjectTaskManager.Domain.Common;

namespace ProjectTaskManager.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public IEnumerable<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
