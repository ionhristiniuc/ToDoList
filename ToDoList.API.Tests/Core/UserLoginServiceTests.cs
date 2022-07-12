using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Entities;
using ToDoList.API.Core.Interfaces.Repositories;
using ToDoList.API.Core.Interfaces.Services;
using ToDoList.API.Core.Services;
using ToDoList.API.Tests.Utils;

namespace ToDoList.API.Tests.Core;

public class UserLoginServiceTests : IDisposable
{
    private static IMapper mapper;
    private readonly Fixture fixture;
    private readonly Mock<IUserRepository> userRepositoryMock;
    private readonly Mock<IJwtHandler> jwtHandlerMock;

    public UserLoginServiceTests()
    {
        fixture = new Fixture();
        mapper = MapperBuilder.Create();
        
        userRepositoryMock = new Mock<IUserRepository>();
        jwtHandlerMock = new Mock<IJwtHandler>();
    }
    
    public void Dispose()
    {
        userRepositoryMock.VerifyAll();
        jwtHandlerMock.VerifyAll();
    }

    [Fact]
    public async Task LoginAsync_UserDoesNotExist_ReturnsResourceNotFoundError()
    {
        // Arrange
        var request = fixture.Create<LoginRequest>();
        var findUserOperationResult = new OperationResult<AppUser>(ErrorType.ResourceNotFound);
        
        userRepositoryMock.Setup(m => m.FindByUserNameAsync(request.Email))
            .ReturnsAsync(findUserOperationResult);
        
        var sut = new UserLoginService(userRepositoryMock.Object, jwtHandlerMock.Object);
        
        // Act
        var result = await sut.LoginAsync(request);
        
        // Assert
        result.Success.Should().BeFalse();
        result.Error.Type.Should().Be(ErrorType.ResourceNotFound);
    }
    
    [Fact]
    public async Task LoginAsync_PasswordIsIncorrect_ReturnsResourceNotFoundError()
    {
        // Arrange
        var request = fixture.Create<LoginRequest>();
        var domainUser = fixture.Create<AppUser>();
        var findUserResult = new OperationResult<AppUser>(domainUser);
        var checkPasswordResult = new OperationResult(ErrorType.WrongPassword);
        
        userRepositoryMock.Setup(m => m.FindByUserNameAsync(request.Email))
            .ReturnsAsync(findUserResult);
        
        userRepositoryMock.Setup(m => m.CheckPasswordAsync(findUserResult.Value, request.Password))
            .ReturnsAsync(checkPasswordResult);
        
        var sut = new UserLoginService(userRepositoryMock.Object, jwtHandlerMock.Object);
        
        // Act
        var result = await sut.LoginAsync(request);
        
        // Assert
        result.Success.Should().BeFalse();
        result.Error.Type.Should().Be(ErrorType.ResourceNotFound);
    }
    
    [Fact]
    public async Task LoginAsync_CorrectPassword_ReturnsJwtToken()
    {
        // Arrange
        var request = fixture.Create<LoginRequest>();
        var domainUser = fixture.Create<AppUser>();
        var findUserResult = new OperationResult<AppUser>(domainUser);
        var checkPasswordResult = new OperationResult();
        var token = fixture.Create<string>();
        
        userRepositoryMock.Setup(m => m.FindByUserNameAsync(request.Email))
            .ReturnsAsync(findUserResult);
        
        userRepositoryMock.Setup(m => m.CheckPasswordAsync(findUserResult.Value, request.Password))
            .ReturnsAsync(checkPasswordResult);
        
        jwtHandlerMock.Setup(m => m.GetTokenAsync(findUserResult.Value))
            .ReturnsAsync(token);
        
        var sut = new UserLoginService(userRepositoryMock.Object, jwtHandlerMock.Object);
        
        // Act
        var result = await sut.LoginAsync(request);
        
        // Assert
        result.Success.Should().BeTrue();
        result.Value.Should().Be(token);
    }
}