namespace ToDoList.Infrastructure.DataAccessLayer.Handlers;

internal static class MapperExtension
{
    public static CategoryDto AsDto(this Category entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Description = entity.Description
    };

    public static TaskDto AsDto(this Tasks entity) => new()
    {
        TaskId = entity.Id,
        TaskName = entity.TaskName,
        IsCompleted = entity.IsCompleted,
        Category = entity.Category.Name.Value,
        CreatedAt = entity.CreatedAt
    };
}