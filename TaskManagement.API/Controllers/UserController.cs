using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Domain.User.Dto;
using TaskManagement.API.Domain.User.Entities;
using TaskManagement.API.Domain.User.Repositories;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            bool valid = false;

            var result = new UserLoginResponseDTO();
            var user = await _userRepository.GetByUsername(request.Username);

            if (user != null)
            {
                valid = _userRepository.ValidateLogin(request.Password, user.Password, user.Salt);

                if (valid)
                {
                    result.IsSuccess = valid;
                    result.Message = "Login Successfully";
                    result.UserData = new UserDataDTO
                    {
                        Username = user.Username,
                        Fullname = user.FullName
                    };
                    return Ok(result);
                }
                else 
                {
                    result.Message = "Check again your password";
                }
            }
            else 
            {
                result.Message = "User Not Found";

            }
            result.IsSuccess = valid;
            return Unauthorized(result);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(CreateUserDTO request)
        {
            var data = new UserEntity
            {
                Username = request.Username,
                FullName = request.FullName,
                Password = request.Password,
                Email = request.Email,
                Division = request.Division,
                CreatedBy = request.CreatedBy
            };
            _userRepository.CreateUser(data);
            return Ok();
        }
    }
}
