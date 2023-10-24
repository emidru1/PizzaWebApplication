using backend.Controllers;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Tests
{
    public class PizzaControllerTests : IClassFixture<InMemoryDbContextSetup>
    {
        private readonly InMemoryDbContextSetup _dbSetup;

        public PizzaControllerTests(InMemoryDbContextSetup dbSetup)
        {
            _dbSetup = new InMemoryDbContextSetup();
        }

        [Fact]
        public async Task GetAllPizzaSizes_ReturnsAllSizes()
        {
            // Arrange
            var context = _dbSetup.CreateNewContext();

            var controller = new PizzaController(context);

            // Act
            var result = await controller.GetAllPizzaSizes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<PizzaSize>>(okResult.Value);
            Assert.Equal(3, returnValue.Count); 
        }

        [Fact]
        public async Task GetAllPizzaToppings_ReturnsAllPizzaSizes()
        {
            // Arrange
            var context = _dbSetup.CreateNewContext();
            var controller = new PizzaController(context);

            // Act
            var result = await controller.GetAllPizzaToppings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<PizzaTopping>>(okResult.Value);
            Assert.Equal(7, returnValue.Count);
        }
    }
}
