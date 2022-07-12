using ToDoList.API.Core.Entities;

namespace ToDoList.API.Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<OperationResult> Create(AppUser domainUser, string userRegistrationPassword);
    Task<OperationResult<AppUser>> FindByUserNameAsync(string username);

    Task<OperationResult> CheckPasswordAsync(AppUser domainUser, string password);
}