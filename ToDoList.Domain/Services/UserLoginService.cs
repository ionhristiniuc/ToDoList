using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Entities;
using ToDoList.API.Core.Interfaces.Repositories;
using ToDoList.API.Core.Interfaces.Services;

namespace ToDoList.API.Core.Services;

public class UserLoginService : IUserLoginService
{
    private readonly IUserRepository userRepository;
    private readonly IJwtHandler jwtHandler;

    public UserLoginService(IUserRepository userRepository, IJwtHandler jwtHandler)
    {
        this.userRepository = userRepository;
        this.jwtHandler = jwtHandler;
    }

    public async Task<OperationResult<string>> LoginAsync(LoginRequest loginRequest)
    {
        var findUserResult = await userRepository.FindByUserNameAsync(loginRequest.Email);
        if (!findUserResult.Success
            || !(await userRepository.CheckPasswordAsync(findUserResult.Value, loginRequest.Password)).Success)
        {
            return new OperationResult<string>(ErrorType.ResourceNotFound);
        }

        var jwt = await jwtHandler.GetTokenAsync(findUserResult.Value);
        return new OperationResult<string>(jwt);
    }
}