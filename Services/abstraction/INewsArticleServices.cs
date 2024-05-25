using BusinessObject;

namespace Services.abstraction;

public interface INewsArticleServices
{
    public Task<IEnumerable<NewsArticle>> GetAllArticle();

    public Task<IEnumerable<NewsArticle>> GetArticlesByTitle(string title);
    
    public Task<IEnumerable<NewsArticle>> GetArticlesByUserId(short id);

    public Task<NewsArticle?> GetArticleById(string id);

    public Task<NewsArticle?> AddArticle(NewsArticle article);

    public Task<NewsArticle?> UpdateArticle(NewsArticle article);

    public Task<bool> DeleteArticle(string article);

    public Task UpdateArticlesWhenDeleteAccount(List<NewsArticle> article);
}