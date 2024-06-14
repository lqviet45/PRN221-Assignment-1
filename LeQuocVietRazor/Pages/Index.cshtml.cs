using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.abstraction;

namespace LeQuocVietRazor.Pages;

public class IndexModel : PageModel
{
    private readonly IAccountServices _accountServices;
    
    [BindProperty]
    public string Username { get; set; }
    
    [BindProperty]
    public string Password { get; set; }

    public IndexModel(IAccountServices accountServices)
    {
        _accountServices = accountServices;
    }
    
    public async Task OnPostAsync()
    {
        try
        {
            var account = await _accountServices.LoginRazor(Username, Password);
        
            HttpContext.Session.SetString("AccountId", account.AccountId.ToString());
            HttpContext.Session.SetString("AccountEmail", account.AccountEmail!);
            HttpContext.Session.SetString("Role", account.AccountRole.ToString()!);
            // Response.Cookies.Append("AccountEmail", account.AccountEmail);
            // Response.Cookies.Append("AccountName", account.AccountName);
            // Response.Cookies.Append("AccountRole", account.AccountRole.ToString());
            
            switch (account.AccountRole)
            {
                case 3:
                    Response.Redirect("/user/index");
                    break;
                case 1:
                    Response.Redirect("/category/index");
                    break;
                case 2:
                    Response.Redirect("/news/index");
                    break;
            }
            
        }
        catch (Exception e)
        {
            if (e.Message.Contains("Username or password"))
                ModelState.AddModelError("LoginFailed", e.Message);
            else
                ModelState.AddModelError("LoginFailed", "An error occurred while logging in");
        }
    }
}