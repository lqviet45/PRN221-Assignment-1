using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public static class CategoryDao
{
    public static IQueryable<Category> GetAll()
    {
        var context = new FunewsManagementDbContext();

        return context.Categories.AsQueryable().AsNoTracking();
    }

    public static async Task<Category?> GetByIdAsync(short id)
    {
        var context = new FunewsManagementDbContext();

        return await context.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);
    }

    public static async Task<bool> AddAsync(Category category)
    {
        var context = new FunewsManagementDbContext();
        context.Add(category);

        return await context.SaveChangesAsync() > 0;
    }
    
    public static async Task<bool> UpdateAsync(Category category)
    {
        var context = new FunewsManagementDbContext();
        context.Entry(category)
            .State = EntityState.Modified;

        return await context.SaveChangesAsync() > 0;
    }
    
    public static async Task<bool> DeleteAsync(Category category)
    {
        var context = new FunewsManagementDbContext();
        context.Remove(category);

        return await context.SaveChangesAsync() > 0;
    }
}