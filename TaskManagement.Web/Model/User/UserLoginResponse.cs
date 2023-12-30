namespace TaskManagement.Web.Model.User
{
    public class UserLoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public UserData UserData { get; set; }
    }
    public class UserData
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
    }
}
