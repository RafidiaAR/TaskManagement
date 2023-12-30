namespace TaskManagement.API.Domain.User.Dto
{
    public class CreateUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Division { get; set; }
        public string CreatedBy { get; set; }
    }
}
