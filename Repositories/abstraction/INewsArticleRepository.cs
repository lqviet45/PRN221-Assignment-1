﻿using BusinessObject;

namespace Repositories.abstraction;

public interface INewsArticleRepository
{
    public Task<IEnumerable<NewsArticle>> GetAllArticle();
    
    public Task<NewsArticle?> GetArticleById(string id);

    public Task<bool> AddArticle(NewsArticle newsArticle);

    public Task<bool> UpdateArticle(NewsArticle newsArticle);

    public Task<bool> DeleteArticle(NewsArticle newsArticle);
}