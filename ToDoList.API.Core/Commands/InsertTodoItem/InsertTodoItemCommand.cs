using System.Text.Json.Serialization;
using MediatR;
using ToDoList.API.Core.Entities;

namespace ToDoList.API.Core.Commands.InsertTodoItem;

public class InsertTodoItemCommand : IRequest<OperationResult<ToDo>>
{
    public string Name { get; init; }
    
    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
}