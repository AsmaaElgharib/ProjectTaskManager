namespace ProjectTaskManager.Application.Features.Projects.Dtos
{
    public record ProjectTaskSummaryDto(
    Guid Id,
    string Title,
    string Status,
    string Priority,
    DateTime? DueDate);
}
