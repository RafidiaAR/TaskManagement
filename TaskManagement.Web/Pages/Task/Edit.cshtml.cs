using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TaskManagement.Web.Model.Task;
using TaskManagement.Web.Model.User;

namespace TaskManagement.Web.Pages.Task
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private string UrlAPI;

        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
            UrlAPI = _configuration["UrlBackendAPI"];
        }


        [BindProperty]
        public CreateTaskModel CreateTaskModel { get; set; }

        public async System.Threading.Tasks.Task OnGetAsync(string id)
        {
            var apiUrl = $"{UrlAPI}Task/FindById/{id}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    CreateTaskModel = JsonConvert.DeserializeObject<CreateTaskModel>(result);
                }
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                TempData["IsShow"] = "1";
                TempData["IsSuccess"] = "0";
                TempData["AlertMessage"] = "Please fix the validation errors before submitting the form.";

                return Page();
            }

            var apiUrl = $"{UrlAPI}Task/Update";

            var createModel = new
            {
                TaskId = id,
                Title = CreateTaskModel.Title,
                Description = CreateTaskModel.Description,
                Assignee = CreateTaskModel.Assignee,
                Priority = CreateTaskModel.Priority,
                Status = CreateTaskModel.Status,
                DueDate = CreateTaskModel.DueDate,
                Progress = CreateTaskModel.Progress,
                UpdatedBy = User.Identity.Name
            };

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync(apiUrl, createModel);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<UserLoginResponse>(result);

                    if (data.IsSuccess)
                    {
                        TempData["IsShow"] = "1";
                        TempData["IsSuccess"] = "1";
                        TempData["AlertMessage"] = "Success Update Task";

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
