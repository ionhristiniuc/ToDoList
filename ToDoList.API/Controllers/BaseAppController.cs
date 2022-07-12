using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Extensions;

namespace ToDoList.API.Controllers
{
    [Authorize]
    public class BaseAuthorizedController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public BaseAuthorizedController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected string LoggedInUserId => httpContextAccessor.HttpContext.User.GetLoggedInUserId();
    }
}
