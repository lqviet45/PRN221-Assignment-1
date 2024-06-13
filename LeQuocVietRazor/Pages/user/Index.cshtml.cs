using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Services.abstraction;

namespace LeQuocVietRazor.Pages.user
{
    public class IndexModel : PageModel
    {
        private readonly IAccountServices _accountServices;

        public IndexModel(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        public IList<SystemAccount> SystemAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SystemAccount = (await _accountServices.GetAllAccount()).ToList();
        }
    }
}
