using AutoMapper;
using CeoToDoList.Controllers;
using CeoToDoList.Mappings;
using CeoToDoList.Models.Domain;
using CeoToDoList.Models.DTO;
using CeoToDoList.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnitTest.Mocking;
using Xunit;

namespace ListControllerTests
{
    public class ListControllerTest
    {
        private readonly ListController listController;
        private readonly Mock<IListRepositories> mockListRepositories;
        private readonly IMapper mapper;
        public CeoList newItem = new CeoList { Title = "a", Description = "s" };
        public Guid listId = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d8d7");

        public ListControllerTest()
        {
            // Create a service collection to use dependency injection
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            var serviceProvider = services.BuildServiceProvider();

            // Get the mapper service
            mapper = serviceProvider.GetRequiredService<IMapper>();

            mockListRepositories = new Mock<IListRepositories>();
            listController = new ListController(mockListRepositories.Object, mapper);
        }

        [Fact]
        public async Task GetAllLists_ReqAllListsFromController_ReturnsAllLists()
        {
            // Arrange
            var mockedLists = MockedLists.GetMockedLists();
            mockListRepositories.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockedLists);

            // Act
            var result = await listController.GetAllLists();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultList = Assert.IsType<List<CeoListDto>>(okResult.Value);

            // Assert
            Assert.Equal(mockedLists.Count, resultList.Count);
        }

        [Fact]
        public async Task CreateList_ReqCreateListFromController_ReturnCreatedList()
        {
            // Arrange
            var newItemDto = new AddListReqDto { Title = "a", Description = "s" };
            mockListRepositories.Setup(repo => repo.CreateAsync(It.IsAny<CeoList>())).ReturnsAsync(newItem);

            // Act
            var result = await listController.CreateList(newItemDto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultList = Assert.IsType<AddListReqDto>(okResult.Value);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newItem.Title, resultList.Title);
            Assert.Equal(newItem.Description, resultList.Description);
        }

        [Fact]
        public async Task GetListById_ReqListFromController_ReturnListWithSameId()
        {
            // Arrange
            mockListRepositories.Setup(repo => repo.GetByIdAsync(listId)).ReturnsAsync(newItem);

            // Act
            var result = await listController.GetListById(listId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultList = Assert.IsType<CeoListDto>(okResult.Value);

            // Assert
            Assert.Equal(result, result);
        }

        [Fact]
        public async Task DeleteListById_ReqListWithId_ReturnListWithSameId()
        {
            // Arrange
            mockListRepositories.Setup(repo => repo.DeleteAsync(listId)).ReturnsAsync(newItem);

            // Act
            var result = await listController.Delete(listId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultList = Assert.IsType<CeoListDto>(okResult.Value);

            // Assert
            Assert.Equal(result, result);
        }
    }
}
