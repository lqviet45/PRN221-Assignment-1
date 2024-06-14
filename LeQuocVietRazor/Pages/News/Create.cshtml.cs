using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Services.abstraction;

namespace LeQuocVietRazor.Pages.News
{
    public class CreateModel : PageModel
    {
        private readonly INewsArticleServices _newsArticleServices;
        private readonly IAccountServices _accountServices;
        private readonly ICategoryServices _categoryServices;

        public CreateModel(INewsArticleServices newsArticleServices, ICategoryServices categoryServices, IAccountServices accountServices)
        {
            _newsArticleServices = newsArticleServices;
            _categoryServices = categoryServices;
            _accountServices = accountServices;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryServices.GetAllCategory(), "CategoryId", "CategoryName");
            ViewData["CreatedById"] = new SelectList(await _accountServices.GetAllAccount(), "AccountId", "AccountName");
            return Page();
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                await _newsArticleServices.AddArticle(NewsArticle);

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
