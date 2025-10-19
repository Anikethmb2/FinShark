using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Interface;
using Api.DTO.Comment;
using Api.Mapper; // Ensure this is present for the extension method

namespace Api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly Interface.ICommentRepository _commentRepository;
        private readonly IStockRepository _stockReporsitory;

        public CommentController(ICommentRepository commentRepository,IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockReporsitory = stockRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentDto = comments.Select(c => c.ToCommentDto()).ToList();
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            var commentDto = comment.ToCommentDto();
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, commentDto);
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreatComment([FromRoute] int stockId, [FromBody] CreateCommentDto createCommentDto)
        {

            if (!await _stockReporsitory.isStockExist(stockId))
            {
                return BadRequest("stock id does not exists");
            }

            var comment = createCommentDto.ToCommentModel(stockId);

            await _commentRepository.CreateComment(comment);

            return Ok(comment.ToCommentDto());

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
        {
            var comment = await _commentRepository.UpdateAsync(id, updateCommentRequestDto.ModelUpdateToCommentModel());

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteAsync(id);

            if (comment == null)
            {
                return NotFound("Comment is not found");
            }

            return Ok(comment.ToCommentDto());
        }
    }
}