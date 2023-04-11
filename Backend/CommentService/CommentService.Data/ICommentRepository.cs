using CommentService.DomainModels;

namespace CommentService.Data;

public interface ICommentRepository
{
    Task<Data.Models.Comment> GetByIdAsync(Guid id);
    IQueryable<Data.Models.Comment> GetByUser(Guid id);
    IQueryable<Data.Models.Comment> GetByTweet(Guid id);
    Task DeleteByIdAsync(Guid id);
    IQueryable<Data.Models.Comment> GetAll();

    Task<Data.Models.Comment> CreateAsync(DomainModels.Comment comment, Guid creatorId);
}