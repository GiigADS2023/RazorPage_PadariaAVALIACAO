using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPage_PadariaAVALIACAO.Models;

namespace RazorPage_PadariaAVALIACAO.Data
{
    public class RazorPage_PadariaAVALIACAOContext : DbContext
    {
        public RazorPage_PadariaAVALIACAOContext (DbContextOptions<RazorPage_PadariaAVALIACAOContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPage_PadariaAVALIACAO.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<RazorPage_PadariaAVALIACAO.Models.ItemVenda> ItemVenda { get; set; } = default!;
        public DbSet<RazorPage_PadariaAVALIACAO.Models.Produto> Produto { get; set; } = default!;
        public DbSet<RazorPage_PadariaAVALIACAO.Models.Venda> Venda { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>()
                .ToTable("Pessoa");

            modelBuilder.Entity<Cliente>()
                .ToTable("Cliente")
                .HasBaseType<Pessoa>();
        }
    }
}
