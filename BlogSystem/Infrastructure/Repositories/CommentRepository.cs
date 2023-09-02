using BlogSystem.Areas.Identity.Data;
using BlogSystem.Core.Models;
using BlogSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommentRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Comment> Add(Comment comment)
    {
        _dbContext.Comments.Add(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task<bool> checkIfExists(int? id)
    {
          return await _dbContext.Comments.AnyAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Comment>> GetAllComments()
    {
        return await _dbContext.Comments.ToListAsync();
    }

    public async Task<Comment> GetById(int? id)
    {
        return await _dbContext.Comments.FindAsync(id);
    }

}

