using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using WSVenta.Models;
using WSVenta.Models.NewFolder;
using WSVenta.Models.ViewModels;
using Microsoft.Data.SqlClient;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {
        /*private IVentaService _venta;

        public VentaController( IVentaService venta) 
        { 
            this._venta = venta;
        }*/

        [EnableCors("MiCors")]
        [HttpPost]
        public IActionResult Add(VentaRequest model)
        {

            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    var venta = new Ventass();
                    venta.Fecha = DateTime.Now;
                    venta.IdCliente = model.IdCliente;
                    venta.IdProducto = model.IdProducto;
                    venta.Cantidad = model.Cantidad;
                    venta.Preciounitario = model.PrecioUnitario;
                    venta.Total = model.Cantidad * model.PrecioUnitario;
                    db.Ventasses.Add(venta);
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
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            List<Union> oUnion = new List<Union>();
            oRespuesta.Exito = 0;

            using (SqlConnection oConexion = new SqlConnection(@"server=LAPTOP-LBPM4VFA\SQLEXPRESS;Database=VentaReal; Trusted_Connection=True;")) 
            {
                SqlCommand cmd = new SqlCommand("select c.id, c.fecha, c.id_cliente, c.total, c.cantidad, c.precioUnitario, c.id_producto, m.nombre as nombre_producto, n.nombre as nombre_cliente from VentaReal.dbo.ventass as c inner join VentaReal.dbo.cliente as n on c.id_cliente = n.id inner join VentaReal.dbo.producto as m on c.id_producto = m.id Order by c.id desc;", oConexion);
                cmd.CommandType = CommandType.Text;

                try 
                { 
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) 
                    {
                        while (dr.Read()) 
                        {
                            oUnion.Add( new Union{ 

                              Id = Convert.ToInt32(dr["id"]),
                              Fecha = Convert.ToDateTime(dr["fecha"]),
                              Fechaformat = String.Format("{0:dd-MM-yyyy}", (dr["fecha"])),
                              IdCliente = Convert.ToInt32(dr["id_cliente"]),
                              Total = Convert.ToInt32(dr["total"]),
                              Cantidad = Convert.ToInt32(dr["cantidad"]),
                              PrecioUnitario = Convert.ToInt32(dr["precioUnitario"]),
                              NombreCliente = dr["nombre_cliente"].ToString(),
                              NombreProducto = dr["nombre_producto"].ToString(),
                              IdProducto = Convert.ToInt32(dr["id_producto"])

                            });
                        }
                        oRespuesta.Exito = 1;
                        oRespuesta.Data = oUnion;
                    }
                } 
                catch(Exception ex) 
                {
                    oRespuesta.Mensaje = ex.Message;
                } 
                return Ok(oRespuesta);

            }

        } //hasta aqui llega get()


        [EnableCors("MiCors")]
        [HttpPut]
        public IActionResult Edit(VentaRequest model)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Ventass venta = db.Ventasses.Find(model.Id);
                    venta.IdCliente = model.IdCliente;
                    venta.IdProducto = model.IdProducto;
                    venta.Cantidad = model.Cantidad;
                    venta.Preciounitario = model.PrecioUnitario;
                    venta.Total = model.Cantidad * model.PrecioUnitario;
                    db.Entry(venta).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }

            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }//Final edit


        [EnableCors("MiCors")]
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Ventass oVenta = db.Ventasses.Find(Id);
                    db.Remove(oVenta);
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
