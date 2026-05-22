namespace ProjectTaskManager.Application.Features.Projects.Dtos
{
    public record ProjectDetailDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IEnumerable<ProjectTaskSummaryDto> Tasks);
}
