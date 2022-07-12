using AutoMapper;
using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Entities;
using ToDoList.API.Core.Interfaces.Repositories;
using ToDoList.API.Core.Interfaces.Services;

namespace ToDoList.API.Core.Services;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UserRegistrationService(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<OperationResult> RegisterUserAsync(UserRegistrationRequest userRegistration)
    {
        var domainUser = mapper.Map<AppUser>(userRegistration);
        domainUser.Id = Guid.NewGuid().ToString();
        return await userRepository.Create(domainUser, userRegistration.Password);
    }
}