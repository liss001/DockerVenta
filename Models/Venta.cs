using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerVenta.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public decimal Total { get; set; }
        public List<VentaItem> Items { get; set; } = new List<VentaItem>();

    }
}