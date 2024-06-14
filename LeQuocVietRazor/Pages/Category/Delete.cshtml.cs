using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.abstraction;

namespace LeQuocVietRazor.Pages.Category
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryServices _categoryServices;

        public DeleteModel(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [BindProperty]
        public BusinessObject.Category Category { get; set; } = default!;
        
        public string Message { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryServices.GetCategoryById(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryServices.GetCategoryById(id.Value);
            if (category == null) return RedirectToPage("./Index");

            if (category.NewsArticles.Count > 0)
            {
                //ModelState.AddModelError(string.Empty, "Category has news articles, can't delete!!");
                Message = "Category has news articles, can't delete!!";
                return Page();
            }
            
            Category = category;
            await _categoryServices.DeleteCategory(Category.CategoryId);
            Message = "Delete successfully!!";

            return RedirectToPage("./Index");
        }
    }
}
