using Api_ComminityCares.Models;
using Api_ComminityCares.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_ComminityCares.Controllers
{
    //original
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) =>
            _userService = userService;

        [HttpGet]
        public async Task<List<User>> Get() =>
            await _userService.GetAsync();

        [HttpGet("login")]
        public async Task<ActionResult<User>> Login(string email, string password)
        {
            var user = await _userService.LoginAsync(email, password);

            if (!user)
            {
                return Unauthorized(); // Las credenciales son inválidas
            }

            // Aquí puedes generar un token de autenticación o realizar cualquier otra lógica relacionada con el inicio de sesión exitoso

            return Ok(); // Inicio de sesión exitoso
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            await _userService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(int id, User updatedUSer)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedUSer.Id = user.Id;

            await _userService.UpdateAsync(id, updatedUSer);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(id);

            return NoContent();
        }
    }
}
