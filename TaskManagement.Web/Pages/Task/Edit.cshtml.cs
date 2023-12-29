using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaskManagement.Web.Pages.Task
{
    public class EditModel : PageModel
    {
        public IActionResult OnGet(string TaskId)
        {
            return Page();
        }
    }
}
