using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO.Stock;
using Api.Interface;
using Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly Data.ApplicationDbContext _context;
        public StockRepository(Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllStocksAsync()
        {
            var simple = await _context.Stocks.Include(c=>c.Comments).ToListAsync();
            return simple;
        }
        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
           var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto updateDto)
        {
            var ExistingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (ExistingStock == null)
            {
                return null;
            }

            ExistingStock.Symbol = updateDto.Symbol;
            ExistingStock.CompanyName = updateDto.CompanyName;
            ExistingStock.Purchase = updateDto.Purchase;
            ExistingStock.LastDiv = updateDto.LastDiv;
            ExistingStock.Industry = updateDto.Industry;
            ExistingStock.Market = updateDto.Market;

            await _context.SaveChangesAsync();
            return ExistingStock;
        }

        public async Task<bool> isStockExist(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}