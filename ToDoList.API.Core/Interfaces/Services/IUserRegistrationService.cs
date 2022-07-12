using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Entities;

namespace ToDoList.API.Core.Interfaces.Services;

public interface IUserRegistrationService
{
    Task<OperationResult> RegisterUserAsync(UserRegistrationRequest userRegistration);
}