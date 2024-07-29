using AutoMapper;
using CeoToDoList.Controllers;
using CeoToDoList.Mappings;
using CeoToDoList.Models.Domain;
using CeoToDoList.Models.DTO;
using CeoToDoList.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace UnitTest.ControllerTests
{
    public class TaskControllerTest
    {
        private readonly TaskController taskController;
        private readonly Mock<ITaskRepositories> mockTaskRepositories;
        private readonly IMapper mapper;
        public Guid taskId = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d8d7");
        public CeoTask newItem = new CeoTask { Title = "a", Description = "s" };


    public TaskControllerTest()
        {
            // Create a service collection to use dependency injection
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            var serviceProvider = services.BuildServiceProvider();

            // Get the mapper service
            mapper = serviceProvider.GetRequiredService<IMapper>();

            mockTaskRepositories = new Mock<ITaskRepositories>();
            taskController = new TaskController(mapper, mockTaskRepositories.Object);
        }

        [Fact]
        public async Task CreateTask_ReqCreateTaskFromController_ReturnCreatedTask()
        {
            // Arrange
            var newItemDto = new AddTaskReqDto { Title = "a", Description = "s" };

            mockTaskRepositories.Setup(repo => repo.CreateAsync(It.IsAny<CeoTask>())).ReturnsAsync(newItem);

            // Act
            var result = await taskController.CreateTask(newItemDto);


            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultTask = Assert.IsType<CeoTaskDto>(okResult.Value);

            Assert.NotNull(result);
            Assert.Equal(newItem.Title, resultTask.Title);
            Assert.Equal(newItem.Description, resultTask.Description);
        }

        [Fact]
        public async Task DeleteTaskById_ReqTaskWithId_ReturnTaskWithSameId()
        {
            // Arrange
            mockTaskRepositories.Setup(repo => repo.DeleteAsync(taskId)).ReturnsAsync(newItem);

            // Act
            var result = await taskController.DeleteTask(taskId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultTask = Assert.IsType<CeoTaskDto>(okResult.Value);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newItem.Id, resultTask.Id);
            Assert.Equal(newItem.Title, resultTask.Title);
            Assert.Equal(newItem.Description, resultTask.Description);
        }

        [Fact]
        public async Task UpdateTask_ReqUpdateTaskFromController_ReturnUpdatedTask()
        {
            // Arrange
            var newItemDto = new UpdateTaskReqDto();

            mockTaskRepositories.Setup(repo => repo.UpdateAsync(taskId, It.IsAny<CeoTask>())).ReturnsAsync(newItem);

            // Act
            var result = await taskController.UpdateTask(taskId,newItemDto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultTask = Assert.IsType<CeoTaskDto>(okResult.Value);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newItem.Title, resultTask.Title);
            Assert.Equal(newItem.Description, resultTask.Description);
        }

        [Fact]
        public async Task UpdateTaskComplete_ReqUpdateTaskCompleteFromController_ReturnCompleteTask()
        {
            // Arrange
            var newItemDto = new UpdateCompleteTaskDto();

            mockTaskRepositories.Setup(repo => repo.UpdateCompletedAsync(taskId, It.IsAny<CeoTask>())).ReturnsAsync(newItem);

            // Act
            var result = await taskController.UpdateTaskCompleted(taskId, newItemDto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultTask = Assert.IsType<CeoTaskDto>(okResult.Value);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newItem.Title, resultTask.Title);
            Assert.Equal(newItem.Description, resultTask.Description);
        }
    }
}
