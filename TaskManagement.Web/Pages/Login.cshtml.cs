using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace TaskManagement.Web.Pages
{
    public class LoginModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Rafid"),
               
            };

            var identity = new ClaimsIdentity(claims, "cookie");

            await HttpContext.SignInAsync("cookie", new ClaimsPrincipal(identity));

            return RedirectToPage("/User/Index");
        }
    }
}
