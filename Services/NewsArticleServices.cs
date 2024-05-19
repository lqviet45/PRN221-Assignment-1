using BusinessObject;
using Repositories.abstraction;
using Services.abstraction;

namespace Services;

public class NewsArticleServices : INewsArticleServices
{
    private readonly INewsArticleRepository _articleRepository;
    private readonly IAccountServices _accountServices;

    public NewsArticleServices(INewsArticleRepository articleRepository, IAccountServices accountServices)
    {
        _articleRepository = articleRepository;
        _accountServices = accountServices;
    }

    public async Task<IEnumerable<NewsArticle>> GetAllArticle()
    {
        return await _articleRepository.GetAllArticle();
    }

    public async Task<NewsArticle?> GetArticleById(string id)
    {
        return await _articleRepository.GetArticleById(id);
    }

    public async Task<NewsArticle?> AddArticle(NewsArticle article)
    {
        var result = await _articleRepository.AddArticle(article);
        
        return result ? article : null;
    }

    public async Task<NewsArticle?> UpdateArticle(NewsArticle article)
    {
        var existArticle = await _articleRepository.GetArticleById(article.NewsArticleId);
        if (existArticle is null)
        {
            throw new ArgumentException("This article doesn't exist!!");
        }
        
        existArticle.ModifiedDate = DateTime.Now;
        existArticle.NewsContent = article.NewsContent;
        existArticle.CategoryId = article.CategoryId;
        existArticle.Tags = article.Tags;
        existArticle.CreatedById = article.CreatedById;
        existArticle.NewsTitle = article.NewsTitle;
        existArticle.NewsContent = article.NewsContent;
        existArticle.NewsStatus = article.NewsStatus;
        
        var result = await _articleRepository.UpdateArticle(existArticle);
        
        return result ? existArticle : null;
    }
    
    public async Task UpdateArticlesWhenDeleteAccount(List<NewsArticle> article)
    {
        var account = (await _accountServices.GetAllAccount()).First();
        if (article.Count <= 0)
        {
            return;
        }

        foreach (var vArticle in article)
        {
            vArticle.ModifiedDate = DateTime.Now;
            vArticle.CreatedById = account.AccountId;
            await _articleRepository.UpdateArticle(vArticle);
        }
    }

    public async Task<bool> DeleteArticle(string id)
    {
        var existArticle = await _articleRepository.GetArticleById(id);
        if (existArticle is null)
        {
            throw new ArgumentException("This article doesn't exist!!");
        }
        
        var result = await _articleRepository.DeleteArticle(existArticle);
        
        return result;
    }
}