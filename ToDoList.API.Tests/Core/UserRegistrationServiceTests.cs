using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Entities;
using ToDoList.API.Core.Interfaces.Repositories;
using ToDoList.API.Core.Services;
using ToDoList.API.Tests.Utils;

namespace ToDoList.API.Tests.Core;

public class UserRegistrationServiceTests : IDisposable
{
    private static IMapper mapper;
    private readonly Fixture fixture;
    private readonly Mock<IUserRepository> userRepositoryMock;

    public UserRegistrationServiceTests()
    {
        fixture = new Fixture();
        mapper = MapperBuilder.Create();
        
        userRepositoryMock = new Mock<IUserRepository>();
    }
    
    public void Dispose()
    {
        userRepositoryMock.VerifyAll();
    }

    [Fact]
    public async Task RegisterUserAsync_UserCreatedSuccessfully_ReturnsSuccessResult()
    {
        // Arrange
        var request = fixture.Create<UserRegistrationRequest>();
        var userCreatedOperationResult = new OperationResult();
        
        userRepositoryMock.Setup(m => m.Create(
                It.Is<AppUser>(user => user.Id != null), request.Password))
            .ReturnsAsync(userCreatedOperationResult);

        var sut = new UserRegistrationService(userRepositoryMock.Object, mapper);

        // Act
        var result = await sut.RegisterUserAsync(request);

        // Assert
        result.Should().BeEquivalentTo(userCreatedOperationResult);
    }
    
    [Fact]
    public async Task RegisterUserAsync_UserCreationFailed_ReturnsFailedResult()
    {
        // Arrange
        var request = fixture.Create<UserRegistrationRequest>();
        var operationResult = new OperationResult(ErrorType.Unknown);
        
        userRepositoryMock.Setup(m => m.Create(
                It.Is<AppUser>(user => user.Id != null), request.Password))
            .ReturnsAsync(operationResult);
        
        var sut = new UserRegistrationService(userRepositoryMock.Object, mapper);

        // Act
        var result = await sut.RegisterUserAsync(request);

        // Assert
        result.Should().BeEquivalentTo(operationResult);
    }
}