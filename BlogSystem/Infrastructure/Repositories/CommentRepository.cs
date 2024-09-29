using BlogSystem.Areas.Identity.Data;
using BlogSystem.Core.Interfaces.Repositories;
using BlogSystem.Core.Models;
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
        try
        {
            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();

            return comment;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<bool> checkIfExists(int? id)
    {
        try
        {
            return await _dbContext.Comments.AnyAsync(c => c.Id == id);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<IEnumerable<Comment>> GetAllComments()
    {
        try
        {
            return await _dbContext.Comments.ToListAsync();
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<Comment> GetById(int? id)
    {
        try
        {
            return await _dbContext.Comments.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

}

