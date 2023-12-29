using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Web.Model.User;

namespace TaskManagement.Web.Pages.User
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public UserDataResponse UserModel { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["IsShow"] = "1";
                TempData["IsSuccess"] = "0";
                TempData["AlertMessage"] = "Please fix the validation errors before submitting the form.";

                return Page();
            }
            TempData["IsShow"] = "1";
            TempData["IsSuccess"] = "1";
            TempData["AlertMessage"] = "Success Create User.";

            return RedirectToPage("/User/Index");
        }
    }
}
