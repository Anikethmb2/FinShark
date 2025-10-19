using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();

        Task<Comment?> GetCommentByIdAsync(int id);

        Task<Comment> CreateComment(Comment comment);

        Task<Comment?> UpdateAsync(int id, Comment comment);

        Task<Comment?> DeleteAsync(int id);
    }
}