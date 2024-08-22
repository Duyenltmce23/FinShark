using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetStockByIdAsync(int Id);
        Task<Stock> AddStockAsync(Stock stock);
        Task<Stock?> UpdateStockAsync(int id, AddStockDto addStockDto);
        Task<Stock?> DeleteStockAsync(int id);

    }
}