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
    public class DeleteModel : PageModel
    {
        private readonly INewsArticleServices _newsArticleServices;

        public DeleteModel(INewsArticleServices newsArticleServices)
        {
            _newsArticleServices = newsArticleServices;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsarticle = await _newsArticleServices.GetArticleById(id);

            if (newsarticle == null)
            {
                return NotFound();
            }
            else
            {
                NewsArticle = newsarticle;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var newsarticle = await _newsArticleServices.GetArticleById(id);
                if (newsarticle != null)
                {
                    NewsArticle = newsarticle;
                    await _newsArticleServices.DeleteArticle(NewsArticle.NewsArticleId);
                }

                return RedirectToPage("./Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message);
                return Page();
            }
        }
    }
}
