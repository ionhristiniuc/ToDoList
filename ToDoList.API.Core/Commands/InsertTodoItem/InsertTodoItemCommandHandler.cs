using AutoMapper;
using MediatR;
using ToDoList.API.Core.Entities;
using ToDoList.API.Core.Interfaces;

namespace ToDoList.API.Core.Commands.InsertTodoItem;

public class InsertTodoItemCommandHandler : IRequestHandler<InsertTodoItemCommand, OperationResult<ToDo>>
{
    private readonly ITodoItemsRepository repository;
    private readonly IMapper mapper;

    public InsertTodoItemCommandHandler(IMapper mapper, ITodoItemsRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<OperationResult<ToDo>> Handle(InsertTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todo = mapper.Map<ToDo>(request);
        var createdTodo = await repository.Create(todo);

        return new OperationResult<ToDo>(createdTodo);
    }
}