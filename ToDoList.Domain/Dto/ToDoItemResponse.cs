namespace ToDoList.API.Core.Dto;

public class ToDoItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsCompleted { get; set; }
}