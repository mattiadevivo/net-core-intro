using System;
using System.Collections.Generic;
using introduction_api.Models;
using introduction_api.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Moq;

namespace introduction_api.Tests.UnitTests
{
    public class TodoItemControllerTests
    {
        [Fact]
        public async Task Get() {
            // Arrange
            var mockDbContext = new Mock<TodoContext>();
            mockDbContext.Setup(_ => _.TodoItems).Returns(new Mock<DbSet<TodoItem>>().Object);
            var controller = new TodoItemsController(new Mock<ILogger<TodoItemsController>>().Object, mockDbContext.Object);
            // Act
            var result = await controller.Get();
            // Assert
            var items = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(result);
            items.Should().Equal(GetTestTodoItems());
        }

        #region snippet_GetTestTodoItems
        /// <summary>
        /// Return mocked data
        /// </summary>
        /// <returns>List<TodoItem> data for test</returns>
        private static List<TodoItem> GetTestTodoItems() {
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