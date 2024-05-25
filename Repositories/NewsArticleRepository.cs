using BusinessObject;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repositories.abstraction;

namespace Repositories;

public class NewsArticleRepository : INewsArticleRepository
{
    public async Task<IEnumerable<NewsArticle>> GetAllArticle()
        => await NewsArticleDao.GetAll()
            .Include(a => a.Tags)
            .Include(a => a.Category)
            .Include(a => a.CreatedBy)
            .ToListAsync();
    
    public async Task<IEnumerable<NewsArticle>> GetArticleByTitle(string title)
        => await NewsArticleDao.GetAll()
            .Where(n => n.NewsTitle != null && n.NewsTitle.Contains(title))
            .Include(a => a.Tags)
            .Include(a => a.Category)
            .Include(a => a.CreatedBy)
            .ToListAsync();

    public async Task<NewsArticle?> GetArticleById(string id)
        =>  await NewsArticleDao.GetByIdAsync(id);

    public async Task<bool> AddArticle(NewsArticle newsArticle)
        => await NewsArticleDao.AddAsync(newsArticle);

    public async Task<bool> UpdateArticle(NewsArticle newsArticle)
        => await NewsArticleDao.UpdateAsync(newsArticle);

    public async Task<bool> DeleteArticle(NewsArticle newsArticle)
        => await NewsArticleDao.DeleteAsync(newsArticle);
}