using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.abstraction;

namespace LeQuocVietRazor.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryServices _categoryServices;

        public IndexModel(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public IList<BusinessObject.Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = (await _categoryServices.GetAllCategory()).ToList();
        }
    }
}
