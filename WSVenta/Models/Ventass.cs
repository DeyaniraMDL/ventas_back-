using System;
using System.Collections.Generic;

namespace WSVenta.Models
{
    public partial class Ventass
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
        public int Cantidad { get; set; }
        public int Preciounitario { get; set; }
        public int IdProducto { get; set; }
        public int IdCliente { get; set; }
    }
}
