using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public static class NewsArticleDao
{
    public static IQueryable<NewsArticle> GetAll()
    {
        var context = new FunewsManagementDbContext();

        return context.NewsArticles.AsQueryable().AsNoTracking();
    }

    public static async Task<NewsArticle?> GetByIdAsync(string id)
    {
        var context = new FunewsManagementDbContext();

        return await context.NewsArticles
            .Include(a => a.Tags)
            .Include(a => a.Category)
            .Include(a => a.CreatedBy)
            .SingleOrDefaultAsync(c => c.NewsArticleId == id);
    }

    public static async Task<bool> AddAsync(NewsArticle newsArticle)
    {
        var context = new FunewsManagementDbContext();
        var max = await context.NewsArticles.MaxAsync(n => n.NewsArticleId);
        newsArticle.NewsArticleId = (int.Parse(max) + 1).ToString();
        
        context.Add(newsArticle);

        return await context.SaveChangesAsync() > 0;
    }
    
    public static async Task<bool> UpdateAsync(NewsArticle newsArticle)
    {
        var context = new FunewsManagementDbContext();
        
        context.Entry(newsArticle)
            .State = EntityState.Modified;
        

        return await context.SaveChangesAsync() > 0;
    }
    
    public static async Task<bool> DeleteAsync(NewsArticle newsArticle)
    {
        var context = new FunewsManagementDbContext();
        context.NewsArticles.Remove(newsArticle);
       
        return await context.SaveChangesAsync() > 0;
    }
}