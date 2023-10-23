using backend.Controllers;
using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using backend.Tests.Mock;

namespace backend.Tests
{
    public class OrderControllerTests
    {
        private readonly PizzaDbContext _context;
        private readonly OrderController _controller;
        private readonly Mock<IMapper> _mockMapper;

        public OrderControllerTests()
        {
            _context = PizzaDbContextMock.GetDatabaseMock().Object;
            _mockMapper = new Mock<IMapper>();
            _controller = new OrderController(_context, _mockMapper.Object);
        }

        [Fact]
        public async void GetAllOrdersAsync_ShouldReturnAllOrders()
        {

            var orders = MockData.GetOrders();
            Console.WriteLine(orders.Count());
            // Act
            var result = await _controller.GetAllOrdersAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var actionResult = result.Result as OkObjectResult;
            Assert.IsType<List<OrderDTO>>(actionResult.Value);
        }

        [Fact]
        public async void GetOrderByIdAsync_ValidId_ShouldReturnOrder()
        {

        }



    }
}
