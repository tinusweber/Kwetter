using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommentService.Data.Context;
using CommentService.Data.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Comment = CommentService.DomainModels.Comment;

namespace CommentService.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommentContext _commentContext;

        public CommentRepository(CommentContext commentContext)
        {
            _commentContext = commentContext;
        }

        // get comment by id
        public async Task<Data.Models.Comment> GetByIdAsync(Guid id)
        {
            return await _commentContext.Comments.SingleAsync(x => x.Id == id);
        }
        // get all from user
        public IQueryable<Data.Models.Comment> GetAll()
        {
            return _commentContext.Comments;
        }
        // get all from user
        public IQueryable<Data.Models.Comment> GetByUser(Guid id)
        {
            return _commentContext.Comments.Where(x => x.CommenterId == id);
        }

        // get all from tweet
        public IQueryable<Data.Models.Comment> GetByTweet(Guid id)
        {
            return _commentContext.Comments.Where(x => x.TweetId == id);
        }

        // delete comment by id
        public async Task DeleteByIdAsync(Guid id)
        {
            this._commentContext.Comments.Remove(await _commentContext.Comments.SingleAsync(x => x.Id == id));
            await this._commentContext.SaveChangesAsync();
        }

        public async Task<Data.Models.Comment> CreateAsync(Comment comment, Guid creatorId)
        {
            var res = await this._commentContext.Comments.AddAsync(new Models.Comment()
            {
                CommenterId = creatorId,
                TweetId = comment.TweetId,
                Body = comment.Body
            });
            
            await this._commentContext.SaveChangesAsync();

            return res.Entity;
        }
    }
}