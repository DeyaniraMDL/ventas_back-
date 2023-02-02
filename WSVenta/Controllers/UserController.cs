using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVenta.Models.NewFolder;
using WSVenta.Models.ViewModels;
using WSVenta.Services;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [EnableCors("MiCors")]
        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model) 
        {
            Respuesta respuesta = new Respuesta();
            var userresponse = _userService.Auth(model);

            if (userresponse == null) 
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o Contraseña Incorrecta";
                return Ok(respuesta);
            }

            respuesta.Exito = 1;
            respuesta.Data = userresponse;

            return Ok(respuesta);
        }
    }
}
