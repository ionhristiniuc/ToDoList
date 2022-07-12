using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Entities;

namespace ToDoList.API.Core.Interfaces.Services;

public interface ITodoItemsService
{
    Task<IEnumerable<ToDoItemResponse>> GetAllAsync(string userId);

    Task<OperationResult<ToDoItemResponse>> GetSingleAsync(string userId, Guid id);

    Task<OperationResult<ToDo>> CreateAsync(CreateToDoRequest createToDoRequest, string userId);

    Task<OperationResult<ToDo>> UpdateAsync(UpdateToDoRequest request, string userId, Guid todoId);

    Task<OperationResult> DeleteAsync(string userId, Guid todoId);
}