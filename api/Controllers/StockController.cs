using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext dbcontext;

        public StockController(ApplicationDBContext context)
        {
            dbcontext = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = dbcontext.Stocks.ToList()
            .Select(p => p.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById([FromRoute] int id)
        {
            var stock = dbcontext.Stocks.Find(id);

            if (stock is null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult AddStock([FromBody] AddStockDto addStockDto)
        {
            var stock = addStockDto.ToAddStockDto();
            dbcontext.Stocks.Add(stock);
            dbcontext.SaveChanges();
            return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStock([FromRoute] int id, [FromBody] AddStockDto addStockDto)
        {
            var stock = dbcontext.Stocks.Find(id);

            if (stock is null)
            {
                return NotFound();
            }

            stock.Symbol = addStockDto.Symbol;
            stock.CompanyName = addStockDto.CompanyName;
            stock.Purchase = addStockDto.Purchase;
            stock.Lastdiv = addStockDto.Lastdiv;
            stock.Industry = addStockDto.Industry;
            stock.MarketCap = addStockDto.MarketCap;


            dbcontext.SaveChanges();
            return Ok(stock);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStock([FromRoute] int id)
        {
            var stock = dbcontext.Stocks.Find(id);
            if (stock is null)
            {
                return NotFound();
            }

            dbcontext.Stocks.Remove(stock);
            dbcontext.SaveChanges();
            return NoContent();
        }
    }
}