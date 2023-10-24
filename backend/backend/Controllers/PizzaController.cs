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
                return Ok(pizzaSizes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("toppings")]
        public async Task<ActionResult<IEnumerable<PizzaTopping>>> GetAllPizzaToppings()
        {
            try
            {
                var pizzaToppings = await _dbContext.PizzaToppings.ToListAsync();
                return Ok(pizzaToppings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    };
}