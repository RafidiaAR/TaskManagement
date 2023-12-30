namespace TaskManagement.API.Domain.Task.DTO
{
    public class CreateTaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
