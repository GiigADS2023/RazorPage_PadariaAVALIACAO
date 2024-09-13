using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPage_PadariaAVALIACAO.Models;
using RazorPage_PadariaAVALIACAO.Data;
using System.Threading.Tasks;

namespace RazorPage_PadariaAVALIACAO.Pages.Vendas
{
    public class ConfirmacaoModel : PageModel
    {
        private readonly RazorPage_PadariaAVALIACAOContext _context;

        public ConfirmacaoModel(RazorPage_PadariaAVALIACAOContext context)
        {
            _context = context;
        }

        public Venda Venda { get; set; }

        public async Task<IActionResult> OnGetAsync(int vendaId)
        {
            Venda = await _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.ItensVenda)
                    .ThenInclude(iv => iv.Produto)
                .FirstOrDefaultAsync(v => v.Id == vendaId);

            if (Venda == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}