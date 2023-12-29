using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Web.Model.User;

namespace TaskManagement.Web.Pages.User
{
    [Authorize]
    public class UserModel : PageModel
    {
        public List<UserDataResponse> UserData { get; set; }
        public void OnGet()
        {
            UserData = new List<UserDataResponse>();
            var addData = new UserDataResponse
            {
                UserID = Guid.NewGuid().ToString(),
                Username = "Username",
                FullName = "Fullname",
                Email = "email@gmail.com",
                Division = "IT",
                LastLogin = DateTime.Now,
            };
            UserData.Add(addData);
        }
    }
}
