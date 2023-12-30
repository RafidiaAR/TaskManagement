    using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TaskManagement.Web.Model.Task;
using TaskManagement.Web.Model.User;
using TaskManagement.Web.Pages.User;
using System.Threading.Tasks;

namespace TaskManagement.Web.Pages.Task
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private string UrlAPI;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
            UrlAPI = _configuration["UrlBackendAPI"];
        }

        public List<GetTaskResponse> TaskData { get; set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {

            var apiUrl = $"{UrlAPI}Task/FindAll";

            TaskData = new List<GetTaskResponse>();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<GetTaskResponse>>(result);

                    TaskData.AddRange(data);
                }
            }

        }
    }
}
