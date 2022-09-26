/*
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using WebApi.Helpers;
using WebApplicationDelta1.Model;
using WebApplicationDelta1.Support;
namespace UnitTestWebApplicationDelta1
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<TestDBContext> _testDBContext = new Mock<TestDBContext>();
        private Mock<ILogger> _logger = new Mock<ILogger>();
        [TestMethod]
        public void TestDelete()
        {
            var mockSet = new Mock<DbSet<Vehicle>>();
            _testDBContext.Setup(m => m.Vehicle).Returns(mockSet.Object);
            var vehicleHelper = new Mock<VehicleHelper>(_testDBContext.Object, _logger.Object);
            vehicleHelper.CallBase = true;
            Vehicle returnValue = default(Vehicle);
            vehicleHelper
                //.Setup<Vehicle>(nameof(VehicleHelper.GetVehicle), It.IsAny<string>()) // methodname followed by arguments
                .Setup(m => m.GetVehicle(It.IsAny<string>()))
                .Returns(returnValue);
            vehicleHelper.Object.deleteVehicle("AB13");
            mockSet
            .Verify(m => m.Remove(It.IsAny<Vehicle>()), Times.Once);
            _testDBContext
            .Verify(v => v.SaveChanges(), Times.Once);
        }
        [TestMethod]
        public void TestGetList()
        {
            List<Vehicle> list = new List<Vehicle>
            {
                new Vehicle("ABC123", "CAR"),
                new Vehicle("ABC234", "CAR")
            };
            TestDBContext testDBContext = new TestDBContext();
            testDBContext.Vehicle = GetQueryableMockDbSet(list);
            _testDBContext.Setup(m => m.Vehicle).Returns(testDBContext.Vehicle);
            var vehicleHelper = new VehicleHelper(_testDBContext.Object, _logger.Object);
            var result = vehicleHelper.GetVehicles();
            Assert.IsTrue(result.LongCount() == 2);
        }
        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            IQueryable<T> queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            return dbSet.Object;
        }
        [TestMethod]
        public void TestAdd()
        {
            var mockSet = new Mock<DbSet<Vehicle>>();
            Mock<TestDBContext> _testDBContext = new Mock<TestDBContext>();
            _testDBContext.Setup(m => m.Vehicle).Returns(mockSet.Object);
            var vehicleHelper = new VehicleHelper(_testDBContext.Object, _logger.Object);
            var result = vehicleHelper.AddVehicle(new Vehicle("ADDT2345", "CAR"));
            mockSet
            .Verify(m => m.Add(It.IsAny<Vehicle>()), Times.Once);
            _testDBContext
            .Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
// An other way is also to use inmemorydb and avoid mocking
/*
 * var options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: "FekaConnectionString")
                .Options;
    var context = new MyContext(options);
*/
//https://stackoverflow.com/questions/31349351/how-to-add-an-item-to-a-mock-dbset-using-moq
//https://learn.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking
//https://stackoverflow.com/questions/4769928/using-moq-to-mock-only-some-methods
