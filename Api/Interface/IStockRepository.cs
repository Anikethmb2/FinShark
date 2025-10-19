using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO.Stock;
using Api.Models;
using Api.Data;
using Microsoft.EntityFrameworkCore;
namespace Api.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync();
        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock> CreateStockAsync(Stock stock);
        Task<Stock?> UpdateStockAsync(int id, DTO.Stock.UpdateStockRequestDto updateDto);
        Task<Stock?> DeleteStockAsync(int id);

        Task<bool> isStockExist(int id);
    }
}