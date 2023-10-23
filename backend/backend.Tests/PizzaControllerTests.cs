using backend.Controllers;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using backend.Tests.Mock;

namespace backend.Tests
{
    public class PizzaControllerTests
    {
        private readonly PizzaDbContext _context;
        private readonly PizzaController _controller;

        public PizzaControllerTests()
        {
            _context = PizzaDbContextMock.GetDatabaseMock().Object;
            _controller = new PizzaController(_context);
        }

        [Fact]
        public async void GetAllPizzaSizes_ReturnsCorrectSizes()
        {
            var expectedSizes = MockData.GetPizzaSizes();

            var actionResult = await _controller.GetAllPizzaSizes();
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            Assert.Equal(200, okResult.StatusCode);

            var sizes = okResult.Value as IEnumerable<PizzaSize>;
            Assert.NotNull(sizes);
            Assert.Equal(expectedSizes.Count, sizes.Count());

            var expectedSize = expectedSizes.First();
            var actualSize = sizes.First();
            Assert.Equal(expectedSize.Id, actualSize.Id);
            Assert.Equal(expectedSize.Name, actualSize.Name);
            Assert.Equal(expectedSize.Price, actualSize.Price);
        }

        [Fact]
        public async void GetAllPizzaToppings_ReturnsCorrectToppings()
        {
            var expectedToppings = MockData.GetPizzaToppings();

            var actionResult = await _controller.GetAllPizzaToppings();
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            Assert.Equal(200, okResult.StatusCode);

            var toppings = okResult.Value as IEnumerable<PizzaTopping>;
            Assert.NotNull(toppings);
            Assert.Equal(expectedToppings.Count, toppings.Count());

            var expectedTopping = expectedToppings.First();
            var actualTopping = toppings.First();
            Assert.Equal(expectedTopping.Id, actualTopping.Id);
            Assert.Equal(expectedTopping.Name, actualTopping.Name);
            Assert.Equal(expectedTopping.Price, actualTopping.Price);
        }
    }


}
