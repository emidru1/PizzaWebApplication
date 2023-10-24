using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.DTOs;
using AutoMapper;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly PizzaDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderController(PizzaDbContext context, IMapper mapper)
        {
            this._dbContext = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await FetchOrdersAsync();
                var orderDtos = _mapper.Map<List<OrderDTO>>(orders);
                return Ok(orderDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        private async Task<List<Order>> FetchOrdersAsync()
        {
            return await _dbContext.Orders
                                  .Include(o => o.Size)
                                  .Include(o => o.OrderToppings)
                                  .ThenInclude(ot => ot.PizzaTopping)
                                  .ToListAsync();
        }

        [HttpPost("calculateTotal")]
        public async Task<IActionResult> CalculateOrderTotal([FromBody] CreateOrderRequestDTO orderRequest)
        {
            try
            {
                if (orderRequest == null || string.IsNullOrEmpty(orderRequest.SizeName) || orderRequest.ToppingNames == null)
                {
                    return BadRequest("Invalid order payload");
                }
                var size = await _dbContext.PizzaSizes.FirstOrDefaultAsync(s => s.Name == orderRequest.SizeName);
                if (size == null)
                {
                    return BadRequest("Size not found");
                }
                var toppings = await _dbContext.PizzaToppings.Where(t => orderRequest.ToppingNames.Contains(t.Name)).ToListAsync();
                if (toppings.Count != orderRequest.ToppingNames.Count)
                {
                    return BadRequest("Some toppings were not found.");
                }

                double total = size.Price + toppings.Sum(t => t.Price);
                if (toppings.Count > 3)
                {
                    total *= 0.9;
                }

                //Round order total to 2 decimals after comma
                total = Math.Round(total, 2);
                return Ok(new { OrderTotal = total });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrderAsync(CreateOrderRequestDTO orderRequest)
        {
            try
            {
                var size = await _dbContext.PizzaSizes.FirstOrDefaultAsync(s => s.Name == orderRequest.SizeName);
                if (size == null)
                {
                    return BadRequest("Size not found");
                }
                var toppingNames = orderRequest.ToppingNames;
                var toppings = await _dbContext.PizzaToppings.Where(t => toppingNames.Contains(t.Name)).ToListAsync();
                if (toppings.Count != toppingNames.Count)
                {
                    return BadRequest("Some toppings were not found.");
                }
                var order = new Order
                {
                    Size = size,
                    OrderTotal = size.Price + toppings.Sum(t => t.Price)
                };

                if (toppings.Count > 3)
                {
                    order.OrderTotal *= 0.9;
                }

                //Round order total to 2 decimals after comma
                order.OrderTotal = Math.Round(order.OrderTotal, 2);

                foreach (var topping in toppings)
                {
                    var orderTopping = new OrderTopping
                    {
                        PizzaToppingId = topping.Id,
                        Order = order
                    };
                    order.OrderToppings.Add(orderTopping);
                }
                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                var orderDto = _mapper.Map<OrderDTO>(order);
                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

