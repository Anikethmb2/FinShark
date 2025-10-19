using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Mapper;
using Api.DTO.Stock;
using Microsoft.EntityFrameworkCore;
using Api.Interface;

namespace Api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IStockRepository _stockRepository;

        public StockController(Data.ApplicationDbContext context,IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var stocks = await _context.Stocks.ToListAsync();
            var stocks = await _stockRepository.GetAllStocksAsync();
            //var stockDto = stocks.Select(s => s.ToStockDto());
            var stockDto = stocks.Select(s => s.ToStockDto()).ToList(); 
            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var stock = await _stockRepository.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DTO.Stock.CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockDtoFromCreateDto();

            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateStockRequestDto UpdateDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }

            stockModel.Symbol = UpdateDto.Symbol;
            stockModel.CompanyName = UpdateDto.CompanyName;
            stockModel.Purchase = UpdateDto.Purchase;
            stockModel.LastDiv = UpdateDto.LastDiv;
            stockModel.Industry = UpdateDto.Industry;
            stockModel.Market = UpdateDto.Market;

            _context.Stocks.Update(stockModel);
            await _context.SaveChangesAsync();
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}