using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                Lastdiv = stock.Lastdiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap


            };
        }

        public static Stock ToAddStockDto(this AddStockDto addStockDto)
        {
            return new Stock
            {
                Symbol = addStockDto.Symbol,
                CompanyName = addStockDto.CompanyName,
                Purchase = addStockDto.Purchase,
                Lastdiv = addStockDto.Lastdiv,
                Industry = addStockDto.Industry,
                MarketCap = addStockDto.MarketCap
            };
        }
    }
}