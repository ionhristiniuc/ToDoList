using AutoMapper;
using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Entities;
using ToDoList.API.Core.Interfaces;
using ToDoList.API.Core.Interfaces.Services;

namespace ToDoList.API.Core.Services;

public class TodoItemsService : ITodoItemsService
{
    private readonly ITodoItemsRepository repository;
    private readonly IMapper mapper;

    public TodoItemsService(ITodoItemsRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<ToDoItemResponse>> GetAllAsync(string userId)
    {
        var todos = await repository.GetAll(userId);
        return mapper.Map<IEnumerable<ToDoItemResponse>>(todos);
    }
    
    public async Task<OperationResult<ToDoItemResponse>> GetSingleAsync(string userId, Guid id)
    {
        var getTodoResult = await repository.Get(userId, id);
        if (getTodoResult.Success)
        {
            return new OperationResult<ToDoItemResponse>(mapper.Map<ToDoItemResponse>(getTodoResult.Value));
        }
        else
        {
            return new OperationResult<ToDoItemResponse>(getTodoResult.Error);
        }
    }

    public async Task<OperationResult<ToDo>> CreateAsync(CreateToDoRequest createToDoRequest, string userId)
    {
        var todo = mapper.Map<ToDo>(createToDoRequest);
        todo.UserId = userId;
        var createdTodo = await repository.Create(todo);

        return new OperationResult<ToDo>(createdTodo);
    }
    
    public async Task<OperationResult<ToDo>> UpdateAsync(UpdateToDoRequest request, string userId, Guid todoId)
    {
        var todo = mapper.Map<ToDo>(request);
        todo.UserId = userId;
        todo.Id = todoId;
        
        var updatedTodoResult = await repository.Update(todo);

        return updatedTodoResult;
    }

    public async Task<OperationResult> DeleteAsync(string userId, Guid todoId)
    {
        return await repository.Delete(userId, todoId);
    }
}