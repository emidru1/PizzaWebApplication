using AutoMapper;
using backend.Controllers;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace backend.Tests
{
    public class OrderControllerTests : IClassFixture<InMemoryDbContextSetup>
    {
        private readonly IMapper _mapper;
        private InMemoryDbContextSetup _dbSetup;

        public OrderControllerTests()
        {
            _dbSetup = new InMemoryDbContextSetup();
            _mapper = CreateTestMapper();
        }

        private static IMapper CreateTestMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DTOs.Profiles.MappingProfile());
            });

            return config.CreateMapper();
        }

        [Fact]
        public async Task GetAllOrdersAsync_ReturnsAllOrders()
        {
            // Arrange
            var context = _dbSetup.CreateNewContext();
            var controller = new OrderController(context, _mapper);

            // Act
            var result = await controller.GetAllOrdersAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<OrderDTO>>(okResult.Value);
            //Since new database context is created, it should return empty order list
            Assert.Equal(0, returnValue.Count);
        }


        [Fact]
        public async Task CalculateOrderTotal_WithValidInput_ReturnsCorrectTotal()
        {
            // Arrange
            var context = _dbSetup.CreateNewContext();
            var controller = new OrderController(context, _mapper);
            var orderRequest = new CreateOrderRequestDTO
            {
                SizeName = "Small",
                ToppingNames = new List<string> { "Tomato sauce", "Cheese" }
            };

            // Act
            var result = await controller.CalculateOrderTotal(orderRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var orderTotalProperty = okResult.Value.GetType().GetProperty("OrderTotal");
            if (orderTotalProperty != null)
            {
                double orderTotal = (double)orderTotalProperty.GetValue(okResult.Value);
                Assert.Equal(10.00, orderTotal);
            }
            else
            {   
                //Fail the test on purpose if orderTotalProperty is not found(debugging purposes)
                Assert.True(false, "OrderTotal property not found");
            }
            
        }
        [Fact]
        public async Task CalculateOrderTotal_WithInvalidSizeName_ReturnsBadRequest()
        {
            // Arrange
            var context = _dbSetup.CreateNewContext();
            var controller = new OrderController(context, _mapper);
            var orderRequest = new CreateOrderRequestDTO
            {
                SizeName = "ExtraLarge", //Does not exist
                ToppingNames = new List<string> { "Tomato sauce", "Cheese" }
            };

            // Act
            var result = await controller.CalculateOrderTotal(orderRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task CalculateOrderTotal_WithInvalidToppingNames_ReturnsBadRequest()
        {
            // Arrange
            var context = _dbSetup.CreateNewContext();
            var controller = new OrderController(context, _mapper);
            var orderRequest = new CreateOrderRequestDTO
            {
                SizeName = "Small",
                ToppingNames = new List<string> { "Tomato sauce", "DoesNotExist" } //One of the toppings does not exist
            };

            // Act
            var result = await controller.CalculateOrderTotal(orderRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task CreateOrderAsync_WithValidInput_CreatesOrderSuccessfully()
        {
            // Arrange
            var context = _dbSetup.CreateNewContext();
            var controller = new OrderController(context, _mapper);
            var orderRequest = new CreateOrderRequestDTO
            {
                SizeName = "Small",
                ToppingNames = new List<string> { "Tomato sauce", "Cheese" }
            };

            // Act
            var actionResult = await controller.CreateOrderAsync(orderRequest);

            // Assert
            var okResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okResult);
            var orderDto = okResult.Value as OrderDTO;
            double expectedTotal = 8.00 + 1.00 + 1.00; // Small size and two toppings
            Assert.Equal(expectedTotal, orderDto.OrderTotal);
        }

        [Fact]
        public async Task CreateOrderAsync_WithInvalidSizeName_ReturnsBadRequest()
        {
            // Arrange
            var context = _dbSetup.CreateNewContext();
            var controller = new OrderController(context, _mapper);
            var orderRequest = new CreateOrderRequestDTO
            {
                SizeName = "ExtraLarge", //ExtraLarge does not exist (Sizes: Small, Medium, Large)
                ToppingNames = new List<string> { "Tomato sauce", "Cheese" }
            };

            // Act
            var actionResult = await controller.CreateOrderAsync(orderRequest);

            // Assert
            var badRequestResult = actionResult.Result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
        }

        [Fact]
        public async Task CreateOrderAsync_WithMoreThanThreeToppings_AppliesDiscount()
        {
            // Arrange
            var context = _dbSetup.CreateNewContext();
            var controller = new OrderController(context, _mapper);
            var orderRequest = new CreateOrderRequestDTO
            {
                SizeName = "Small",
                ToppingNames = new List<string> { "Tomato sauce", "Cheese", "Pepperoni", "Chicken" }
            };

            // Act
            var actionResult = await controller.CreateOrderAsync(orderRequest);

            // Assert
            var okResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okResult);
            var orderDto = okResult.Value as OrderDTO;
            double expectedTotal = (8.00 + 1.00 + 1.00 + 1.00 + 1.00) * 0.9; // 10% discount
            Assert.Equal(expectedTotal, orderDto.OrderTotal);
        }
    }
}
