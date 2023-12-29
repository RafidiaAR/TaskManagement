namespace TaskManagement.Web.Model.Task
{
    public class GetTaskResponse
    {
        public string TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public DateTime DueDate { get; set; }
        public decimal ProgressPercentage { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
