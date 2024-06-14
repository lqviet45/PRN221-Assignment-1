using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeQuocVietRazor.Pages.user;

public class Logout : PageModel
{
    public void OnPost()
    {
        HttpContext.Session.Clear();
        Response.Redirect("/user/login");
    }
}