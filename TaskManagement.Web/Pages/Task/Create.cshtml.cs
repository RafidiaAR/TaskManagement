using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using TaskManagement.Web.Model.Task;
using TaskManagement.Web.Model.User;

namespace TaskManagement.Web.Pages.Task
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private string UrlAPI;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
            UrlAPI = _configuration["UrlBackendAPI"];
        }

        [BindProperty]
        public CreateTaskModel CreateTaskModel { get; set; }

        public void OnGet()
        {
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["IsShow"] = "1";
                TempData["IsSuccess"] = "0";
                TempData["AlertMessage"] = "Please fix the validation errors before submitting the form.";

                return Page();
            }

            var apiUrl = $"{UrlAPI}Task/Create";

            var createModel = new
            {
                Title = CreateTaskModel.Title,
                Description = CreateTaskModel.Description,
                Assignee = CreateTaskModel.Assignee,
                Priority = CreateTaskModel.Priority,
                Status = CreateTaskModel.Status,
                DueDate = CreateTaskModel.DueDate,
                CreatedBy = User.Identity.Name
            };

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(apiUrl, createModel);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<UserLoginResponse>(result);

                    if (data.IsSuccess)
                    {
                        TempData["IsShow"] = "1";
                        TempData["IsSuccess"] = "1";
                        TempData["AlertMessage"] = "Success Create Task";

                        return RedirectToPage("/Task/Index");
                    }
                    else
                    {
                        TempData["IsShow"] = "1";
                        TempData["IsSuccess"] = "0";
                        TempData["AlertMessage"] = data.Message;
                        return Page();
                    }
                }
            }

            TempData["IsShow"] = "1";
            TempData["IsSuccess"] = "0";
            TempData["AlertMessage"] = "Something went wrong";
            return Page();
        }
    }
}
