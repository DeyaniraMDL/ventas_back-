using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WSVenta.Models.ViewModels;
using WSVenta.Models.NewFolder;
using WSVenta.Models;
using WSVenta.Services;
using Microsoft.AspNetCore.Cors;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConceptoController : ControllerBase
    {

        [EnableCors("MiCors")]
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;

            try {

                using (VentaRealContext db = new VentaRealContext()) 
                { 
                    var lst = db.Conceptos.OrderBy(x => x.Id).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }

            }
            catch (Exception ex) 
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);

        }

    }
}
