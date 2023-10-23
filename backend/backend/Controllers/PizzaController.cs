using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly ILogger<PizzaController> _logger;
        private readonly PizzaDbContext _dbContext;
        public PizzaController(PizzaDbContext context)
        {
            this._dbContext = context;
        }
        [HttpGet("sizes")]
        public async Task<ActionResult<IEnumerable<PizzaSize>>> GetAllPizzaSizes()
        {
            try
            {
                var pizzaSizes = await _dbContext.PizzaSizes.ToListAsync();
                return new OkObjectResult(pizzaSizes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // or use a logger if you have one set up
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("toppings")]
        public async Task<ActionResult<IEnumerable<PizzaTopping>>> GetAllPizzaToppings()
        {
            try
            {
                var pizzaToppings = await _dbContext.PizzaToppings.ToListAsync();
                return new OkObjectResult(pizzaToppings);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // or use a logger if you have one set up
                return StatusCode(500, "Internal server error");
            }
        }
    };
}