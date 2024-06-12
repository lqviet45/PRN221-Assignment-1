using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LeQuocVietRazor.Pages.Category
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.FunewsManagementDbContext _context;

        public DetailsModel(DataAccess.FunewsManagementDbContext context)
        {
            _context = context;
        }

        public BusinessObject.Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);
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
    }
}
