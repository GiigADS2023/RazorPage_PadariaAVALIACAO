using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPage_PadariaAVALIACAO.Data;
using RazorPage_PadariaAVALIACAO.Models;

namespace RazorPage_PadariaAVALIACAO.Pages.ItensVenda
{
    public class IndexModel : PageModel
    {
        private readonly RazorPage_PadariaAVALIACAO.Data.RazorPage_PadariaAVALIACAOContext _context;

        public IndexModel(RazorPage_PadariaAVALIACAO.Data.RazorPage_PadariaAVALIACAOContext context)
        {
            _context = context;
        }

        public IList<ItemVenda> ItemVenda { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ItemVenda = await _context.ItemVenda
                .Include(i => i.Produto).ToListAsync();
        }
    }
}
