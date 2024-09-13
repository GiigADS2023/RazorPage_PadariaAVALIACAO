using RazorPage_PadariaAVALIACAO.Models;
using RazorPage_PadariaAVALIACAO.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorPage_PadariaAVALIACAO.Services
{
    public class VendaService
    {
        private readonly RazorPage_PadariaAVALIACAOContext _context;

        public VendaService(RazorPage_PadariaAVALIACAOContext context)
        {
            _context = context;
        }

        public void AddVenda(Venda venda)
        {
            if (venda.Total <= 0)
            {
                throw new InvalidOperationException("O valor total deve ser maior do que zero.");
            }

            _context.Venda.Add(venda);
            _context.SaveChanges();
        }

        public void AtualizarPontosFidelidade(Cliente cliente, List<ItemVenda> itensVenda)
        {
            if (cliente != null)
            {
                int pontosGanhosPorItem = itensVenda.Count; // Example: 1 point per item sold
                cliente.PontosFidelidade += pontosGanhosPorItem;

                _context.Cliente.Update(cliente);
                _context.SaveChanges();
            }
        }
    }
}