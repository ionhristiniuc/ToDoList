using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Entities;

namespace ToDoList.API.Core.Interfaces.Services;

public interface IUserLoginService
{
    Task<OperationResult<string>> LoginAsync(LoginRequest loginRequest);
}