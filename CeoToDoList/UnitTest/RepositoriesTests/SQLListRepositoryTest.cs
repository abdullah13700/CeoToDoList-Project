using CeoToDoList.Data;
using CeoToDoList.Models.Domain;
using CeoToDoList.Repositories;
using Microsoft.EntityFrameworkCore;
using UnitTest.Mocking;
using Xunit;

namespace UnitTest.RepositoriesTests
{
    public class SQLListRepositoryTest
    {
        private readonly SQLListRepository repository;
        private readonly CeoDbContext dbContext;
        private readonly List<CeoList> mockedLists;
        public Guid listId = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d8d7");
        public Guid invalidId = Guid.Parse("09f321b9-7a72-4301-a345-1f8a66b1d7d7");


        public SQLListRepositoryTest()
        {
            // Initialize mocked data
            mockedLists = MockedLists.GetMockedLists();

            // Setup in-memory database
            var options = new DbContextOptionsBuilder<CeoDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Create the context with the in-memory database
            dbContext = new CeoDbContext(options);

            // Seed the in-memory database with mocked data
            dbContext.Lists.AddRange(mockedLists);
            dbContext.SaveChanges();

            // Initialize repository
            repository = new SQLListRepository(dbContext);
        }

        [Fact]
        public async Task CreateListAsync_NewItem_ReturnsItem()
        {
            // Arrange
            var newItem = new CeoList { Title = "New List", Description = "New Description" };

            // Act
            var result = await repository.CreateAsync(newItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newItem.Title, result.Title);
            Assert.Equal(newItem.Description, result.Description);
        }

        [Fact]
        public async Task CreateListAsync_NewItemWithDuplicateTitle_ReturnsException()
        {
            // Arrange
            var newItem = new CeoList { Title = "List 1", Description = "New Description" };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
               () => repository.CreateAsync(newItem));

            // Assert
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async Task CreateListAsync_NewItemWithEmptyName_ReturnsException()
        {
            // Arrange
            var newItem = new CeoList { Title = "", Description = "New Description" };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
               () => repository.CreateAsync(newItem));

            // Assert
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async Task GetAllLists_RequestAllItems_ReturnsAllItems()
        {
            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task DeleteListById_DeleteListById_DeleteOneItem()
        {
            // Arrange
            var DbBeforeDelete = await repository.GetAllAsync();

            // Act
            var result = await repository.DeleteAsync(listId);

            var DbAfterDelete = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(DbAfterDelete.Count, DbBeforeDelete.Count - 1);
        }
        [Fact]
        public async Task DeleteListById_GettingInvalidId_ReturnException()
        {
            // Arrange
            // Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => repository.GetByIdAsync(invalidId));

            // Assert

            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async Task GetListById_GetingListById_ReturnOneItem()
        {
            // Arrange
            var listMustReturn = mockedLists[0];
            // Act
            var result = await repository.GetByIdAsync(listId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, listMustReturn.Id);
        }

        [Fact]
        public async Task GetListById_GettingInvalidId_ReturnException()
        {
            // Arrange
            // Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => repository.GetByIdAsync(invalidId));

            // Assert

            Assert.IsType<InvalidOperationException>(exception);
        }
    }
}