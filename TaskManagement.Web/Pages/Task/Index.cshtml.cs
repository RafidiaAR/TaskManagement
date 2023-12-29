using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Web.Model.Task;
using TaskManagement.Web.Model.User;

namespace TaskManagement.Web.Pages.Task
{
    public class IndexModel : PageModel
    {
        public List<GetTaskResponse> TaskData { get; set; }
        public void OnGet()
        {
            TaskData = new List<GetTaskResponse>();
            var addData = new GetTaskResponse
            {
                TaskDescription = "Rafid",
                TaskName = "Rafidia Aqil"
            };
            TaskData.Add(addData);
        }
    }
}
