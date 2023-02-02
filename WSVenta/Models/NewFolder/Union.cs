using System.ComponentModel.DataAnnotations;

namespace WSVenta.Models.NewFolder
{
    public class Union
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Fechaformat { get; set; }
        public int IdCliente { get; set; }
        public decimal Total { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IdProducto { get; set; }
        public string NombreCliente { get; set; }
        public string NombreProducto { get; set; }



    }
}
