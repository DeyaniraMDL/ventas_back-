using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVenta.Models;
using WSVenta.Models.NewFolder;
using WSVenta.Models.ViewModels;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        
        [EnableCors("MiCors")]
        [HttpGet]
        public IActionResult Get() {

            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {

                using (VentaRealContext db = new VentaRealContext())
                {
                    var lst = db.Clientes.OrderByDescending(d=>d.Id).ToList();
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

        [EnableCors("MiCors")]
        [HttpPost]
        public IActionResult Add(ClienteRequest oModel) {
            Respuesta oRespuesta = new Respuesta();
         
            try
            {
                using (VentaRealContext db = new VentaRealContext()) {
                    Cliente oCliente = new Cliente();
                    oCliente.Nombre = oModel.Nombre;
                    oCliente.Correo = oModel.Correo;
                    oCliente.Rfc = oModel.Rfc;
                    db.Clientes.Add(oCliente);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
                
            }
            catch (Exception ex) {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [EnableCors("MiCors")]
        [HttpPut]
        public IActionResult Edit(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente oCliente = db.Clientes.Find(oModel.Id);
                    oCliente.Nombre = oModel.Nombre;
                    oCliente.Correo = oModel.Correo;
                    oCliente.Rfc = oModel.Rfc;
                    db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;                   
                }

            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }


        [EnableCors("MiCors")]
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente oCliente = db.Clientes.Find(Id);
                    db.Remove(oCliente);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }

            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }//Final delete
    }
}
