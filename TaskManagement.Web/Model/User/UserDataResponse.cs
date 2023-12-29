namespace TaskManagement.Web.Model.User
{
    public class UserDataResponse
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Division { get; set; }
        public DateTime LastLogin { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
