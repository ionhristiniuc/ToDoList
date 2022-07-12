using ToDoList.API.Core.Entities;

namespace ToDoList.API.Core.Interfaces.Services;

public interface IJwtHandler
{
    Task<string> GetTokenAsync(AppUser user);
}