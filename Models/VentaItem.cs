using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DockerVenta.Models
{
    public class VentaItem
    {
        public int Id { get; set; }
        public int VentaId { get; set; }

        [JsonIgnore] // Evita ciclo al serializar
        public Venta? Venta { get; set; }

        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}
