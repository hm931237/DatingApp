using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //Validate Request
            userForRegisterDto.Username=userForRegisterDto.Username.ToLower();
            if(await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");

            var UserToCreate=new User{
                UserName=userForRegisterDto.Username
            };
            var CreatedUser=_repo.Register(UserToCreate,userForRegisterDto.Password);
            return StatusCode(201);
        }
    }
}