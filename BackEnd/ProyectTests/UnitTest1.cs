using FinalAPI.Controllers;
using FinalAPI.Models;
using FinalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace ProyectTests
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IDataService> _dataServiceMock;
        private DataController _controller;

        public UnitTest1()
        {
            _dataServiceMock = new Mock<IDataService>();
            _controller = new DataController(_dataServiceMock.Object);
        }

        [TestMethod]
        public async Task GetAllData_ReturnsOkObjectResult_WithData()
        {
            // Arrange
            var testData = new List<Car> { new Car(), new Car() };
            _dataServiceMock.Setup(service => service.GetAllData())
                            .ReturnsAsync(testData);

            // Act
            var result = await _controller.GetAllData() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testData, result.Value);
        }

        [TestMethod]
        public async Task GetAllCarBrands_ReturnsOkObjectResult_WithBrandList()
        {
            // Arrange
            var testBrands = new List<string> { "Toyota", "Honda", "Ford" };
            _dataServiceMock.Setup(service => service.GetAllCarBrands())
                            .ReturnsAsync(testBrands);

            // Act
            var result = await _controller.GetAllCarBrands() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testBrands, result.Value);
        }
    }
}