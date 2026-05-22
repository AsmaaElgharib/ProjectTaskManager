namespace ProjectTaskManager.Application.Features.Projects.Dtos
{
    public record ProjectDto(Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int TaskCount);

}
