using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaskManagement.Web.Pages.User
{
    public class EditModel : PageModel
    {
        public IActionResult OnGet(string id)
        {
            return Page();
        }
    }
}
