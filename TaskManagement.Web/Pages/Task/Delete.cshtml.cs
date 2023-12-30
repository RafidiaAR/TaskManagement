using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TaskManagement.Web.Model.Task;
using TaskManagement.Web.Model.User;

namespace TaskManagement.Web.Pages.Task
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private string UrlAPI;

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
            UrlAPI = _configuration["UrlBackendAPI"];
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var apiUrl = $"{UrlAPI}Task/Delete/{id}";


            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(apiUrl);

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
