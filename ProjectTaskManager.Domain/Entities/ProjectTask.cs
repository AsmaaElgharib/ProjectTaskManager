using ProjectTaskManager.Domain.Common;
using ProjectTaskManager.Domain.Enums;
using TaskStatus = ProjectTaskManager.Domain.Enums.TaskStatus;

namespace ProjectTaskManager.Domain.Entities
{
    public class ProjectTask : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Todo;
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }
}
