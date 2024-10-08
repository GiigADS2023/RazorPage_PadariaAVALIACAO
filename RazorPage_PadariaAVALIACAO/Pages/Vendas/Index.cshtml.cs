﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPage_PadariaAVALIACAO.Models;
using RazorPage_PadariaAVALIACAO.Services;
using RazorPage_PadariaAVALIACAO.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPage_PadariaAVALIACAO.Pages.Vendas
{
    public class IndexModel : PageModel
    {
        private readonly RazorPage_PadariaAVALIACAOContext _context;
        private readonly VendaService _vendaService;

        public IndexModel(RazorPage_PadariaAVALIACAOContext context, VendaService vendaService)
        {
            _context = context;
            _vendaService = vendaService;
        }

        [BindProperty]
        public List<Produto> ProdutosDisponiveis { get; set; }
        [BindProperty]
        public List<ItemVenda> ItensVenda { get; set; } = new List<ItemVenda>();
        [BindProperty]
        public string FormaPagamento { get; set; }
        [BindProperty]
        public string NomeCliente { get; set; }
        public Cliente Cliente { get; set; }

        public decimal Total { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ProdutosDisponiveis = await _context.Produto.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int[] selectedProducts, int[] quantidades, string formaPagamento, string nomeCliente)
        {
            // Check if products and payment method are selected
            if (selectedProducts.Length == 0 || string.IsNullOrEmpty(formaPagamento) || selectedProducts.Length != quantidades.Length)
            {
                ModelState.AddModelError(string.Empty, "Selecione produtos e informe a forma de pagamento. As quantidades devem corresponder aos produtos.");
                ProdutosDisponiveis = await _context.Produto.ToListAsync();
                return Page();
            }

            ItensVenda = new List<ItemVenda>();

            for (int i = 0; i < selectedProducts.Length; i++)
            {
                var produtoId = selectedProducts[i];
                var quantidade = quantidades[i];


                var produto = await _context.Produto.FindAsync(produtoId);
                if (produto != null && quantidade > 0)
                {

                    var itemVenda = new ItemVenda(produto, quantidade);
                    ItensVenda.Add(itemVenda);
                }
            }

            Cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Nome == nomeCliente);

            /*
            if (Cliente == null)
            {
                ModelState.AddModelError(string.Empty, "Usuário não encontrado. Você será redirecionado para a tela de cadastro.");
                return RedirectToPage("/Clientes/Create", new { nome = nomeCliente });
            }
            */

            var venda = new Venda(ItensVenda, formaPagamento, Cliente);
            Total = venda.Total;


            var novaVenda = new Models.Venda
            {
                FormaPagamento = formaPagamento,
                Cliente = Cliente,
                ItensVenda = ItensVenda
            };

            _context.Venda.Add(novaVenda);
            await _context.SaveChangesAsync();


            _vendaService.AtualizarPontosFidelidade(Cliente, ItensVenda);

            return RedirectToPage("Confirmacao", new { vendaId = novaVenda.Id });
        }


    }
}