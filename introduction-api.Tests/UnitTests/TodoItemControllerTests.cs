using System;
using System.Collections.Generic;
using introduction_api.Models;
using introduction_api.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Data.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace introduction_api.Tests.UnitTests
{
    public class TodoItemControllerTests
    {
        /// <summary>
        /// Test TodoItemController.Get()
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get()
        {
            // Arrange
            DbContextOptions<TodoContext> options = await Fixture();
            var dbContext = new TodoContext(options);
            var controller = new TodoItemsController(
                new Mock<ILogger<TodoItemsController>>().Object, dbContext);

            // Act
            var result = await controller.Get();

            // Assert
            var items = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(result.Value);
            items.Should().BeEquivalentTo(GetTestTodoItems());

            // Clean
            await dbContext.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            DbContextOptions<TodoContext> options = await Fixture();
            var dbContext = new TodoContext(options);
            var controller = new TodoItemsController(
                new Mock<ILogger<TodoItemsController>>().Object, dbContext);

            // Act
            var result = await controller.GetById(4);

            // Assert
            var item = Assert.IsAssignableFrom<TodoItem>(result.Value);
            var itemWithIdx4 = GetTestTodoItems().First(i => i.Id == 4);
            item.Should().Match<TodoItem>(i => i.Id == itemWithIdx4.Id &&
                i.Name == itemWithIdx4.Name &&
                i.IsComplete == itemWithIdx4.IsComplete
                , "because they have the same id");

            // Clean
            await dbContext.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Put()
        {
            // Arrange
            DbContextOptions<TodoContext> options = await Fixture();
            var dbContext = new TodoContext(options);
            var controller = new TodoItemsController(
                new Mock<ILogger<TodoItemsController>>().Object, dbContext);

            // Act
            TodoItem toBeModified = GetTestTodoItems().First(i => i.Id == 3);
            toBeModified.IsComplete = true;
            toBeModified.Name = "Third Item (not completed)";
            var result = await controller.Put(toBeModified.Id, toBeModified);

            // Assert
            var modified = await dbContext.TodoItems.FindAsync(toBeModified.Id);
            modified.Should().Match<TodoItem>(i => i.IsComplete == toBeModified.IsComplete &&
            i.Name == toBeModified.Name
                , "because they have the same id");

            // Clean
            await dbContext.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async Task Post() {
            // Arrange
            DbContextOptions<TodoContext> options = await Fixture();
            var dbContext = new TodoContext(options);
            var controller = new TodoItemsController(
                new Mock<ILogger<TodoItemsController>>().Object, dbContext);

            // Act
            TodoItem itemToAdd = new TodoItem("Fifth item (completed)", true);
            var result = await controller.Post(itemToAdd);

            // Assert
            var added = await dbContext.TodoItems.FindAsync(itemToAdd.Id);
            added.Should().Match<TodoItem>(a => a.Name == itemToAdd.Name &&
                a.IsComplete == itemToAdd.IsComplete, "because item has just be inserted into db"
            );

            // Clean
            await dbContext.Database.EnsureDeletedAsync();

        }

        [Fact]
        public async Task Delete() {
            // Arrange
            DbContextOptions<TodoContext> options = await Fixture();
            var dbContext = new TodoContext(options);
            var controller = new TodoItemsController(
                new Mock<ILogger<TodoItemsController>>().Object, dbContext);

            // Act
            const long deletedIdx = 1;
            var result = await controller.Delete(deletedIdx);

            // Assert
            var modified = await dbContext.TodoItems.FindAsync(deletedIdx);
            modified.Should().BeNull("because it's been deleted");

            // Clean
            await dbContext.Database.EnsureDeletedAsync();
        }

        #region fixture_function
        private async static Task<DbContextOptions<TodoContext>> Fixture()
        {
            /* // In-memory Database
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("TodoDatabase")
                .EnableSensitiveDataLogging().
                UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            */
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseSqlite(connection)
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var dbContext = new TodoContext(options);
            await dbContext.Database.EnsureCreatedAsync();
            await dbContext.AddRangeAsync(GetTestTodoItems());
            await dbContext.SaveChangesAsync();
            await dbContext.DisposeAsync();
            return options;
        }
        #endregion

        #region snippet_GetTestTodoItems
        /// <summary>
        /// Return mocked data
        /// </summary>
        /// <returns>List<TodoItem> data for test</returns>
        private static List<TodoItem> GetTestTodoItems()
        {
            return new List<TodoItem>{
                new TodoItem(1, "First Item (not completed)", false),
                new TodoItem(2, "Second Item (completed)", true),
                new TodoItem(3, "Third Item (not completed)", false),
                new TodoItem(4, "Fourth Item (not completed)", false)
            };
        }
        #endregion
    }
}