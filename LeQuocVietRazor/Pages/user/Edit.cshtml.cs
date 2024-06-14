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

namespace LeQuocVietRazor.Pages.user
{
    public class EditModel : PageModel
    {
        private readonly IAccountServices _accountServices;

        public EditModel(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemaccount =  await _accountServices.GetAccountById(id.Value);
            if (systemaccount == null)
            {
                return NotFound();
            }
            SystemAccount = systemaccount;
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
                await _accountServices.UpdateAccount(SystemAccount);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SystemAccountExists(SystemAccount.AccountId))
                {
                    return NotFound();
                }
                else
                {
                    ModelState.AddModelError("Error", "This account doesn't exist!!");
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> SystemAccountExists(short id)
        {
            return await _accountServices.GetAccountById(id) != null;
        }
    }
}
