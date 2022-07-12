using ToDoList.API.Core.Entities;

namespace ToDoList.API.Core.Interfaces
{
    public interface ITodoItemsRepository
    {
        Task<IList<ToDo>> GetAll(string userId);
        Task<OperationResult<ToDo>> Get(string userId, Guid toDoId);
        Task<ToDo> Create(ToDo toDo);
        Task<OperationResult<ToDo>> Update(ToDo toDo);
        Task<OperationResult> Delete(string userId, Guid toDoId);
    }
}
