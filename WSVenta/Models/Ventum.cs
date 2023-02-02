using System;
using System.Collections.Generic;

namespace WSVenta.Models
{
    public partial class Ventum
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public decimal Total { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Concepto Concepto { get; set; } = null!;
    }
}
