using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DockerVenta.Models;

namespace DockerVenta.Data
{
    public class VentaDb : DbContext
    {
        public VentaDb(DbContextOptions<VentaDb> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaItem> VentaItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().Property(p => p.Precio).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Venta>().Property(v => v.Total).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<VentaItem>().Property(vi => vi.PrecioUnitario).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<VentaItem>().Property(vi => vi.SubTotal).HasColumnType("decimal(18,2)");
        }
    }
}
