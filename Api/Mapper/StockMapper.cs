using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO.Stock;
using Api.Models;


namespace Api.Mapper
{
    public static class StockMapper
    {
        public static DTO.Stock.StockDto ToStockDto(this Models.Stock stockModel)
        {
            return new DTO.Stock.StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                Market = stockModel.Market,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockDtoFromCreateDto(this CreateStockRequestDto stockModel)
        {
            return new Stock
            {
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                Market = stockModel.Market
               // Comments = new List<Comment>()
            };
        }
    }
}