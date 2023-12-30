using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using TaskManagement.Web.Model.User;
using TaskManagement.Web.Pages.User;

namespace TaskManagement.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private string UrlAPI;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
            UrlAPI = _configuration["UrlBackendAPI"];
        }

        [BindProperty]
        public UserLoginModel UserLoginModel { get; set; }
        public IActionResult OnGet(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string ReturnUrl)
        {
            var apiUrl = $"{UrlAPI}user/Login";

            var loginModel = new
            {
                UserName = UserLoginModel.UserName,
                Password = UserLoginModel.Password
            };

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(apiUrl, loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<UserLoginResponse>(result);

                    if (data.IsSuccess)
                    {
                        var claims = new[]
                        {
                        new Claim(ClaimTypes.Name, data.UserData.Username),
                        new Claim("Fullname", data.UserData.Fullname)
                        };

                        var identity = new ClaimsIdentity(claims, "cookie");

                        await HttpContext.SignInAsync("cookie", new ClaimsPrincipal(identity));

                        return RedirectToPage("/User/Index");
                    }
                    else 
                    {
                        TempData["IsShow"] = "1";
                        TempData["IsSuccess"] = "0";
                        TempData["AlertMessage"] = data.Message;
                        return Page();
                    }
                }
                else
                {
                    TempData["IsShow"] = "1";
                    TempData["IsSuccess"] = "0";
                    TempData["AlertMessage"] = "Please Check Your Password";
                    return Page();
                }
            }
        }
    }
}
