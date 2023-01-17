using System.Security.Claims;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoList.API.Controllers;
using ToDoList.API.Core.Commands.InsertTodoItem;
using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Interfaces.Services;

namespace ToDoList.API.Tests.API.Controllers;

public class TodoItemsControllerTests : IDisposable
{
    private readonly Mock<IHttpContextAccessor> httpContextAccessorMock;
    private readonly Mock<ITodoItemsService> todoItemsServiceMock;
    private readonly Fixture fixture;
    
    public TodoItemsControllerTests()
    {
        httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        todoItemsServiceMock = new Mock<ITodoItemsService>();
        fixture = new Fixture();
    }
    
    public void Dispose()
    {
        httpContextAccessorMock.VerifyAll();
        todoItemsServiceMock.VerifyAll();
    }

    // [Fact]
    // public async Task GetTodoItems_ReturnsOk()
    // {
    //     // Arrange
    //     var mediator = new Mock<IMediator>();
    //
    //     var command = new InsertTodoItemCommand();
    //     var handler = new InsertTodoItemCommandHandler();
    //     string userId = fixture.Create<string>();
    //     var todos = fixture.Create<IEnumerable<ToDoItemResponse>>();
    //     todoItemsServiceMock.Setup(m => m.GetAllAsync(userId))
    //         .ReturnsAsync(todos);
    //     
    //     var claims = new List<Claim>
    //     { 
    //         new (ClaimTypes.NameIdentifier, userId)
    //     };
    //
    //     httpContextAccessorMock.SetupGet(m => m.HttpContext.User)
    //         .Returns(new ClaimsPrincipal(new ClaimsIdentity(claims)));
    //
    //     var controller = new TodoItemsController(httpContextAccessorMock.Object, todoItemsServiceMock.Object);
    //
    //     // Act
    //     var result = await controller.GetTodoItemsAsync();
    //     
    //     // Assert
    //     result.Should().BeOfType<OkObjectResult>();
    // }
}