using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public static class TagDao
{
    public static IQueryable<Tag> GetAll()
    {
        var context = new FunewsManagementDbContext();

        return context.Tags.AsQueryable().AsNoTracking();
    }

    public static async Task<Tag?> GetByIdAsync(short id)
    {
        var context = new FunewsManagementDbContext();

        return await context.Tags.SingleOrDefaultAsync(c => c.TagId == id);
    }

    public static async Task<bool> AddAsync(Tag tag)
    {
        var context = new FunewsManagementDbContext();
        context.Add(tag);

        return await context.SaveChangesAsync() > 0;
    }
    
    public static async Task<bool> UpdateAsync(Tag tag)
    {
        var context = new FunewsManagementDbContext();
        context.Entry(tag)
            .State = EntityState.Modified;

        return await context.SaveChangesAsync() > 0;
    }
    
    public static async Task<bool> DeleteAsync(Tag tag)
    {
        var context = new FunewsManagementDbContext();
        context.Remove(tag);

        return await context.SaveChangesAsync() > 0;
    }
}