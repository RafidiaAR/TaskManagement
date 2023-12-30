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
        public IActionResult Login(UserLoginDTO request)
        {
            _userRepository.Validate(request.Username, request.Password);
            return Ok();
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
