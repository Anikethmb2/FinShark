using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO.Comment;
using Api.Models;

namespace Api.Mapper
{
    public static class CommentMapper
    {
        public static DTO.Comment.CommentDto ToCommentDto(this Models.Comment comment)
        {
            return new DTO.Comment.CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }
        public static Models.Comment ToCommentModel(this CreateCommentDto commentDto, int stockId)
        {
            return new Models.Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId,
            };
        }
        
        public static Comment ModelUpdateToCommentModel(this UpdateCommentRequestDto updateCommentRequestDto)
        {
            return new Comment
            {
                Title = updateCommentRequestDto.Title,
                Content = updateCommentRequestDto.Content,
            };
        }
    }
}