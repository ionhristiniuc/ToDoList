using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Core.Entities;
using ToDoList.API.Core.Interfaces;
using ToDoList.API.Infrastructure.Data.Entities;

namespace ToDoList.API.Infrastructure.Data.EntityFramework.Repositories
{
    public class TodoItemsRepository : ITodoItemsRepository
    {
        private readonly ToDoItemsContext context;
        private readonly IMapper mapper;

        public TodoItemsRepository(ToDoItemsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IList<ToDo>> GetAll(string userId)
        {
            var items = await context.TodoItems
                .Where(i => i.UserId == userId)
                .ToListAsync();

            return mapper.Map<IList<ToDo>>(items);
        }

        public async Task<OperationResult<ToDo>> Get(string userId, Guid toDoId)
        {
            var todo = await context.TodoItems
                .FirstOrDefaultAsync(i => i.Id == toDoId && i.UserId == userId);

            return todo != null
                ? new OperationResult<ToDo>(mapper.Map<ToDo>(todo))
                : new OperationResult<ToDo>(ErrorType.ResourceNotFound);
        }

        public async Task<ToDo> Create(ToDo toDo)
        {
            var todo = mapper.Map<TodoItem>(toDo);

            context.TodoItems.Add(todo);
            await context.SaveChangesAsync();

            return mapper.Map<ToDo>(todo);
        }

        public async Task<OperationResult<ToDo>> Update(ToDo toDo)
        {
            bool todoItemExists = await context.TodoItems.AnyAsync(e => e.UserId == toDo.UserId && e.Id == toDo.Id);

            if (!todoItemExists)
            {
                return new OperationResult<ToDo>(ErrorType.ResourceNotFound);
            }

            var todoItem = mapper.Map<TodoItem>(toDo);

            context.Entry(todoItem).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return new OperationResult<ToDo>(mapper.Map<ToDo>(todoItem));
        }

        public async Task<OperationResult> Delete(string userId, Guid toDoId)
        {
            var todo = await context.TodoItems
                .FirstOrDefaultAsync(i => i.Id == toDoId && i.UserId == userId);

            if (todo == null)
            {
                return new OperationResult(ErrorType.ResourceNotFound);
            }

            context.TodoItems.Remove(todo);
            await context.SaveChangesAsync();

            return new OperationResult();
        }
    }
}
