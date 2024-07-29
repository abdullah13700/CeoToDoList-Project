using CeoToDoList.Data;
using CeoToDoList.Models.Domain;
using CeoToDoList.Repositories;
using Microsoft.EntityFrameworkCore;
using UnitTest.Mocking;
using Xunit;

namespace UnitTest.RepositoriesTests
{
    public class SQLTaskRepositoryTest
    {
        private readonly SQLTaskRepository repository;
        private readonly CeoDbContext dbContext;
        private readonly List<CeoTask> mockedTasks;
        public Guid listId = Guid.Parse("88956C96-7385-43F3-E6B9-08DCAB092EB6");


        public SQLTaskRepositoryTest()
        {

            // Initialize mocked data
            mockedTasks = MockedTasks.GetMockedTasks();

            // Setup in-memory database
            var options = new DbContextOptionsBuilder<CeoDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Create the context with the in-memory database
            dbContext = new CeoDbContext(options);

            // Seed the in-memory database with mocked data
            dbContext.Tasks.AddRange(mockedTasks);
            dbContext.SaveChanges();

            // Initialize repository
            repository = new SQLTaskRepository(dbContext);
        }

        [Fact]
        public async Task CreateTaskAsync_NewItem_ReturnsItem()
        {
            // Arrange
            var newItem = new CeoTask { Title = "New task", Description = "New Description", Completed = false, ListId = listId };

            // Act
            var result = await repository.CreateAsync(newItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newItem.Title, result.Title);
            Assert.Equal(newItem.Description, result.Description);
        }

        [Fact]
        public async Task CreateTaskAsync_NewItemWithDuplicateName_ReturnsException()
        {
            // Arrange
            var newItem = new CeoTask { Title = "Duplicated Title", Description = "New Description", Completed = false, ListId = listId };

            // Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
               () => repository.CreateAsync(newItem));

            // Assert
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async Task GetTaskById_GetingTaskById_ReturnOneItem()
        {
            // Arrange
            var TaskIdMustReturn = mockedTasks[0].Id;

            // Act
            var result = await repository.GetTaskByIdAsync(TaskIdMustReturn);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, TaskIdMustReturn);
        }

        [Fact]
        public async Task GetTaskById_GettingInvalidId_ReturnException()
        {
            // Arrange
            var invalidId = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d7d7");

            // Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => repository.GetTaskByIdAsync(invalidId));

            // Assert

            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async Task DeleteTaskById_DeleteTaskById_DeleteOneItem()
        {
            // Arrange
            var taskToDelete = mockedTasks[0];

            // Act
            var deletedTask = await repository.DeleteAsync(taskToDelete.Id);

            // Assert
            Assert.Null(deletedTask.CeoList);
        }

        [Fact]
        public async Task DeleteTaskById_GettingInvalidId_ReturnException()
        {
            // Arrange
            var invalidId = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d7d7");

            // Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => repository.DeleteAsync(invalidId));

            // Assert
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async Task UpdateTaskById_SendingIdAndTask_ReturnsUpdatedTask()
        {
            // Arrange
            var taskId = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d8d0");
            var newTaskInfo = new CeoTask { Title = "Updated task", Description = "Updated Description", Completed = false, ListId = listId };

            // Act
            var updatedItem = await repository.UpdateAsync(taskId, newTaskInfo);

            // Assert
            Assert.NotNull(updatedItem);
            Assert.Equal(updatedItem.Title, newTaskInfo.Title);
            Assert.Equal(updatedItem.Description, newTaskInfo.Description);
        }

        [Fact]
        public async Task UpdateTaskById_SendingInvalidId_ReturnsException()
        {
            // Arrange
            var invalidId = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d7d7");
            var newTaskInfo = new CeoTask { Title = "Updated task", Description = "Updated Description", Completed = false, ListId = listId };

            // Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => repository.UpdateAsync(invalidId, newTaskInfo));

            // Assert
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async Task UpdateCompletedTaskById_SendingIdAndCompleted_ReturnsCompletedTask()
        {
            // Arrange
            var taskId = mockedTasks[0].Id;
            var CompletedBeforeUpdate = mockedTasks[0].Completed;
            var TaskCompleted = new CeoTask { Completed = true };

            // Act
            var CompletedTaskAfterUpdate = await repository.UpdateCompletedAsync(taskId, TaskCompleted);

            // Assert
            Assert.NotNull(CompletedTaskAfterUpdate);
            Assert.NotEqual(CompletedTaskAfterUpdate.Completed, CompletedBeforeUpdate);

        }

        [Fact]
        public async Task UpdateCompletedTaskById_SendingInvalidIdAndCompleted_ReturnsException()
        {
            // Arrange
            var invalidId = new Guid();
            var TaskCompleted = new CeoTask { Completed = true };

            // Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                   () => repository.UpdateAsync(invalidId, TaskCompleted));

            // Assert
            Assert.IsType<InvalidOperationException>(exception);

        }
    }
}

