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
                TaskID = Guid.NewGuid().ToString(),
                Title = "API - Modify Response",
                Description = "Detailed",
                Assignee = "rramadhan3",
                DueDate = DateTime.Now.AddDays(2),
                ProgressPercentage = (decimal)78.5,
                Priority = "High",
                Status = "In Progress"
            };
            TaskData.Add(addData);
        }
    }
}
