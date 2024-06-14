using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess;
using Services.abstraction;

namespace LeQuocVietRazor.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly INewsArticleServices _newsArticleServices;

        public IndexModel(INewsArticleServices newsArticleServices)
        {
            _newsArticleServices = newsArticleServices;
        }

        public IList<NewsArticle> NewsArticle { get;set; } = default!;

        public async Task OnGetAsync()
        {
            NewsArticle = (await _newsArticleServices.GetAllArticle()).ToList();
        }
    }
}
