using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LeQuocVietRazor.Pages;

public class IndexModel : PageModel
{
    private readonly DataAccess.FunewsManagementDbContext _context;

    public IndexModel(DataAccess.FunewsManagementDbContext context)
    {
        _context = context;
    }

    public IList<NewsArticle> NewsArticle { get;set; } = default!;

    public async Task OnGetAsync()
    {
        NewsArticle = await _context.NewsArticles
            .Include(n => n.Category)
            .Include(n => n.CreatedBy).ToListAsync();
    }
}