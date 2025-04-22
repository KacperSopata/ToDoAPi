using ToDoApi.Models;
using ToDoApi.Repositories;
using ToDoApi.Services;
using Moq;
using Xunit;

public class ToDoServiceTests
{
    private readonly Mock<IToDoRepository> _mockRepo;
    private readonly ToDoService _service;

    public ToDoServiceTests()
    {
        _mockRepo = new Mock<IToDoRepository>();
        _service = new ToDoService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsList()
    {
        // Arrange
        var todos = new List<ToDo> { new ToDo { Id = 1, Title = "Test" } };
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(todos);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Test", result.First().Title);
    }

    [Fact]
    public async Task CreateAsync_AddsTodo()
    {
        // Arrange
        var todo = new ToDo { Title = "Create test" };
        _mockRepo.Setup(r => r.AddAsync(todo)).Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateAsync(todo);

        // Assert
        Assert.Equal("Create test", result.Title);
        _mockRepo.Verify(r => r.AddAsync(todo), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Deletes_WhenExists()
    {
        // Arrange
        var todo = new ToDo { Id = 1 };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(todo);
        _mockRepo.Setup(r => r.DeleteAsync(todo)).Returns(Task.CompletedTask);

        // Act
        var result = await _service.DeleteAsync(1);

        // Assert
        Assert.True(result);
        _mockRepo.Verify(r => r.DeleteAsync(todo), Times.Once);
    }
    [Fact]
    public async Task UpdateAsync_Updates_WhenExists()
    {
        // Arrange
        var id = 1;
        var original = new ToDo { Id = id, Title = "Old" };
        var updated = new ToDo { Id = id, Title = "New", Description = "Updated", ExpiryDate = DateTime.Today, PercentComplete = 50, IsDone = true };

        _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(original);
        _mockRepo.Setup(r => r.UpdateAsync(original)).Returns(Task.CompletedTask);

        // Act
        var result = await _service.UpdateAsync(id, updated);

        // Assert
        Assert.True(result);
        Assert.Equal("New", original.Title);
        _mockRepo.Verify(r => r.UpdateAsync(original), Times.Once);
    }
    [Fact]
    public async Task SetPercentCompleteAsync_UpdatesPercent_WhenExists()
    {
        var todo = new ToDo { Id = 1, PercentComplete = 0 };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(todo);
        _mockRepo.Setup(r => r.UpdateAsync(todo)).Returns(Task.CompletedTask);

        var result = await _service.SetPercentCompleteAsync(1, 75);

        Assert.True(result);
        Assert.Equal(75, todo.PercentComplete);
    }

    [Fact]
    public async Task SetPercentCompleteAsync_ReturnsFalse_WhenNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((ToDo?)null);

        var result = await _service.SetPercentCompleteAsync(1, 75);

        Assert.False(result);
    }

    [Fact]
    public async Task MarkAsDoneAsync_SetsIsDoneTrue_WhenExists()
    {
        var todo = new ToDo { Id = 1, IsDone = false };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(todo);
        _mockRepo.Setup(r => r.UpdateAsync(todo)).Returns(Task.CompletedTask);

        var result = await _service.MarkAsDoneAsync(1);

        Assert.True(result);
        Assert.True(todo.IsDone);
    }

    [Fact]
    public async Task MarkAsDoneAsync_ReturnsFalse_WhenNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((ToDo?)null);

        var result = await _service.MarkAsDoneAsync(1);

        Assert.False(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsTodo_WhenExists()
    {
        var todo = new ToDo { Id = 1 };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(todo);

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((ToDo?)null);

        var result = await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetIncomingTodayAsync_CallsRepoWithCorrectDate()
    {
        var today = DateTime.Today;
        _mockRepo.Setup(r => r.GetByDueDateRangeAsync(today, today)).ReturnsAsync(new List<ToDo>());

        var result = await _service.GetIncomingTodayAsync();

        Assert.NotNull(result);
        _mockRepo.Verify(r => r.GetByDueDateRangeAsync(today, today), Times.Once);
    }

    [Fact]
    public async Task GetIncomingTomorrowAsync_CallsRepoWithCorrectDate()
    {
        var tomorrow = DateTime.Today.AddDays(1);
        _mockRepo.Setup(r => r.GetByDueDateRangeAsync(tomorrow, tomorrow)).ReturnsAsync(new List<ToDo>());

        var result = await _service.GetIncomingTomorrowAsync();

        Assert.NotNull(result);
        _mockRepo.Verify(r => r.GetByDueDateRangeAsync(tomorrow, tomorrow), Times.Once);
    }

    [Fact]
    public async Task GetIncomingThisWeekAsync_CallsRepoWithCorrectRange()
    {
        var today = DateTime.Today;
        var end = today.AddDays(7);
        _mockRepo.Setup(r => r.GetByDueDateRangeAsync(today, end)).ReturnsAsync(new List<ToDo>());

        var result = await _service.GetIncomingThisWeekAsync();

        Assert.NotNull(result);
        _mockRepo.Verify(r => r.GetByDueDateRangeAsync(today, end), Times.Once);
    }

}
