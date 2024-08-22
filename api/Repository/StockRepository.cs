using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext dbcontext;
        public StockRepository(ApplicationDBContext context)
        {
            dbcontext = context;
        }

        public async Task<Stock> AddStockAsync(Stock stock)
        {
            await dbcontext.Stocks.AddAsync(stock);
            await dbcontext.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var stock = await dbcontext.Stocks.FindAsync(id);

            if (stock is null) return null;
            dbcontext.Stocks.Remove(stock);
            await dbcontext.SaveChangesAsync();
            return stock;
        }

        public Task<List<Stock>> GetAllAsync()
        {
            return dbcontext.Stocks.ToListAsync();
        }

        public Task<Stock> GetStockByIdAsync(int Id)
        {
            return dbcontext.Stocks.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Stock?> UpdateStockAsync(int id, AddStockDto addStockDto)
        {
            var stock = await dbcontext.Stocks.FirstOrDefaultAsync(p => p.Id == id);

            if (stock == null) return null;

            stock.Symbol = addStockDto.Symbol;
            stock.CompanyName = addStockDto.CompanyName;
            stock.Purchase = addStockDto.Purchase;
            stock.Lastdiv = addStockDto.Lastdiv;
            stock.Industry = addStockDto.Industry;
            stock.MarketCap = addStockDto.MarketCap;

            await dbcontext.SaveChangesAsync();
            return stock;
        }
    }
}