using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext dbcontext;
        private readonly IStockRepository stockRepo;

        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            dbcontext = context;
            stockRepo = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await stockRepo.GetAllAsync();
            var stockDto = stocks.Select(p => p.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await stockRepo.GetStockByIdAsync(id);

            if (stock is null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody] AddStockDto addStockDto)
        {
            var stockAdd = addStockDto.ToAddStockDto();
            var stock = await stockRepo.AddStockAsync(stockAdd);
            return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] AddStockDto addStockDto)
        {
            var stock = await stockRepo.UpdateStockAsync(id, addStockDto);

            if (stock is null)
            {
                return NotFound();
            }

            return Ok(stock);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = stockRepo.DeleteStockAsync(id);
            if (stock is null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}