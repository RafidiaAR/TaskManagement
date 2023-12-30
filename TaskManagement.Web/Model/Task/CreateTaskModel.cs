namespace TaskManagement.Web.Model.Task
{
    public class CreateTaskModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public int Progress { get; set; }

    }
}
