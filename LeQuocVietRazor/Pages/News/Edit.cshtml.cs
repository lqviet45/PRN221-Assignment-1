using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess;
using Services.abstraction;

namespace LeQuocVietRazor.Pages.News
{
    public class EditModel : PageModel
    {
        private readonly INewsArticleServices _newsArticleServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IAccountServices _accountServices;

        public EditModel(INewsArticleServices newsArticleServices, ICategoryServices categoryServices, IAccountServices accountServices)
        {
            _newsArticleServices = newsArticleServices;
            _categoryServices = categoryServices;
            _accountServices = accountServices;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsarticle =  await _newsArticleServices.GetArticleById(id);
            if (newsarticle == null)
            {
                return NotFound();
            }
            NewsArticle = newsarticle;
            ViewData["CategoryId"] = new SelectList(await _categoryServices.GetAllCategory(), "CategoryId", "CategoryName");
            ViewData["CreatedById"] = new SelectList(await _accountServices.GetAllAccount(), "AccountId", "AccountName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                await _newsArticleServices.UpdateArticle(NewsArticle);
            }
            catch (Exception e)
            {
                if (!await NewsArticleExists(NewsArticle.NewsArticleId))
                {
                    return NotFound();
                }
                ModelState.AddModelError("Error", e.Message);
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> NewsArticleExists(string id)
        {
            return await _newsArticleServices.GetArticleById(id) != null;
        }
    }
}
