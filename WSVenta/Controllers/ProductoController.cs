using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using WSVenta.Models;
using WSVenta.Models.NewFolder;
using WSVenta.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProductoController : ControllerBase
    {
        // GET: api/<ProductoController>
        [EnableCors("MiCors")]
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try 
            {
                using (VentaRealContext db = new VentaRealContext()) 
                { 
                    var lst = db.Productos.OrderByDescending(x => x.Id).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch(Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [EnableCors("MiCors")]
        [HttpPost]
        public IActionResult Add(ProductoRequest oModel) {
            Respuesta oRespuesta = new Respuesta();
            try 
            {
                using (VentaRealContext db = new VentaRealContext()) 
                { 
                    Producto oProducto = new Producto();
                    oProducto.Id = oModel.Id;
                    oProducto.Nombre = oModel.Nombre;
                    oProducto.PrecioUnitario = oModel.PrecioUnitario;
                    oProducto.Costo = oModel.Costo;
                    db.Productos.Add(oProducto);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }

            }
            catch(Exception ex) 
            { 
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [EnableCors("MiCors")]
        [HttpPut]
        public IActionResult Edit(ProductoRequest oModel) 
        {
            Respuesta oRespuesta = new Respuesta();
            try 
            {
                using (VentaRealContext db = new VentaRealContext()) 
                { 
                    Producto oProducto = db.Productos.Find(oModel.Id);
                    oProducto.Nombre = oModel.Nombre;
                    oProducto.PrecioUnitario = oModel.PrecioUnitario;
                    oProducto.Costo = oModel.Costo;
                    db.Entry(oProducto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                    Producto oProducto = db.Productos.Find(Id);
                    db.Remove(oProducto);
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
    }
}
