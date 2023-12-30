namespace TaskManagement.API.Domain.User.Dto
{
    public class UserLoginResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public UserDataDTO UserData { get; set; }
    }
    public class UserDataDTO 
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
    }
}
