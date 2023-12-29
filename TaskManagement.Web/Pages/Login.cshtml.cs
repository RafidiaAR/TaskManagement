using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TaskManagement.Web.Model.User;
using TaskManagement.Web.Pages.User;

namespace TaskManagement.Web.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UserLoginModel UserLoginModel { get; set; }
        public IActionResult OnGet(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string ReturnUrl)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, UserLoginModel.UserName),
               
            };

            var identity = new ClaimsIdentity(claims, "cookie");

            await HttpContext.SignInAsync("cookie", new ClaimsPrincipal(identity));

            return RedirectToPage("/User/Index");
        }
    }
}
