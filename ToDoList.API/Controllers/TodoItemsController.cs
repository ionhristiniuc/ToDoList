using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Entities;
using ToDoList.API.Core.Interfaces.Services;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : BaseAuthorizedController
    {
        private readonly ITodoItemsService todoItemsService;

        public TodoItemsController(IHttpContextAccessor httpContextAccessor,
            ITodoItemsService todoItemsService)
            : base(httpContextAccessor)
        {
            this.todoItemsService = todoItemsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoItemsAsync()
        {
            var userId = base.LoggedInUserId;

            return Ok(await todoItemsService.GetAllAsync(userId));
        }

        [HttpPost]
        public async Task<IActionResult> PostTodoItemAsync(CreateToDoRequest request)
        {
            var createdToDoItemResult = await todoItemsService.CreateAsync(request, base.LoggedInUserId);
            return CreatedAtAction("GetTodoItem", new { id = createdToDoItemResult.Value.Id }, createdToDoItemResult.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItemAsync(Guid id)
        {
            var getResult = await todoItemsService.GetSingleAsync(base.LoggedInUserId, id);

            if (getResult.Success)
            {
                return Ok(getResult.Value);
            }
            else
            {
                if (getResult.Error.Type == ErrorType.ResourceNotFound)
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItemAsync(Guid id, UpdateToDoRequest request)
        {
            var updateResult = await todoItemsService.UpdateAsync(request, LoggedInUserId, id);

            if (updateResult.Success)
            {
                return Ok(updateResult.Value);
            }
            else
            {
                if (updateResult.Error.Type == ErrorType.ResourceNotFound)
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItemAsync(Guid id)
        {
            var deleteResult = await todoItemsService.DeleteAsync(base.LoggedInUserId, id);
            
            if (!deleteResult.Success && deleteResult.Error.Type == ErrorType.ResourceNotFound)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }
    }
}
