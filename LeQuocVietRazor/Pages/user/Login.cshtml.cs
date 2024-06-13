using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.abstraction;

namespace LeQuocVietRazor.Pages.user;

public record LoginModel(string Username, string Password);

public class Login : PageModel
{
    private readonly IAccountServices _accountServices;
    
    [BindProperty]
    public string Username { get; set; }
    
    [BindProperty]
    public string Password { get; set; }

    public Login(IAccountServices accountServices)
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
            
            Response.Redirect("/user/index");
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