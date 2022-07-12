using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Core.Entities;
using ToDoList.API.Core.Interfaces.Repositories;
using ToDoList.API.Infrastructure.Data.Entities;

namespace ToDoList.API.Infrastructure.Data.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly ToDoItemsContext dbContext;

        public UserRepository(UserManager<User> userManager, IMapper mapper, ToDoItemsContext dbContext)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<OperationResult> Create(AppUser domainUser, string userRegistrationPassword)
        {
            var user = mapper.Map<User>(domainUser);
            var result = await userManager.CreateAsync(user, userRegistrationPassword);
            return result.Succeeded
                ? new OperationResult()
                : new OperationResult(new Error(ErrorType.Unknown,
                    result.Errors.Select(e => e.Description).FirstOrDefault()));
        }

        public async Task<OperationResult<AppUser>> FindByUserNameAsync(string username)
        {
            var dbUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == username);

            if (dbUser == null)
            {
                return new OperationResult<AppUser>(ErrorType.ResourceNotFound);
            }
            else
            {
                return new OperationResult<AppUser>(mapper.Map<AppUser>(dbUser));
            }
        }

        public async Task<OperationResult> CheckPasswordAsync(AppUser domainUser, string password)
        {
            bool passwordsMatch = await userManager.CheckPasswordAsync(mapper.Map<User>(domainUser), password);
            return passwordsMatch ? new OperationResult() : new OperationResult(ErrorType.WrongPassword);
        }
    }
}
