using System;
using System.Collections.Generic;

namespace WSVenta.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Ventum>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Correo { get; set; }
        public string? Rfc { get; set; }

        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
