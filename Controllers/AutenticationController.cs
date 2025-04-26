using BlogsWebApi.Abstractions;
using BlogsWebApi.Models;
using BlogsWebApi.Models.DTO;
using BlogsWebApi.Security;
using BlogsWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;


namespace BlogsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticationController : ControllerBase
    {
        private readonly IServices _services;
        private readonly Auth1 _auth1;

        public AutenticationController(IServices services, Auth1 auth1)
        {
            _services = services;
            _auth1 = auth1;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Autenticate(string user, string password)
        {
            var result = await _services.ValidateUser(user, password);
            if (result.autenticated)
                return Ok(new { token = _auth1.GenerateJWT(result.user) });

            return BadRequest("Usuario o contraseña incorrecta");

        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            var user = Mappers.GetUser(userDTO);
            var result = await _services.AddUser(user);

            if (result != null)
                return Ok("Usuario creado con éxito");

            return BadRequest();
        }
    }
}
