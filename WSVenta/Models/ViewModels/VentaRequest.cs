using System.ComponentModel.DataAnnotations;

namespace WSVenta.Models.ViewModels
{
    public class VentaRequest
    {
        [Required]
        [Range(1, Double.MaxValue, ErrorMessage = "El valor del idCliente debe ser mayor a 0")]
        [ExisteCliente(ErrorMessage = "El cliente no existe")]

        public int IdCliente { get; set; }
        public int Id { get; set; }
        public int Total { get; set; }
        public int Cantidad { get; set; }
        public int PrecioUnitario { get; set; }
        public int IdProducto { get; set; }

    }

    #region Validaciones
    public class ExisteClienteAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int idCliente = (int)value;

            using (var db = new Models.VentaRealContext())
            {
                if (db.Clientes.Find(idCliente) == null) return false;
            }
            return true;
        }
    }
    #endregion
}
