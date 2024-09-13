using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPage_PadariaAVALIACAO.Data;
using RazorPage_PadariaAVALIACAO.Models;

namespace RazorPage_PadariaAVALIACAO.Pages.ItensVenda
{
    public class EditModel : PageModel
    {
        private readonly RazorPage_PadariaAVALIACAO.Data.RazorPage_PadariaAVALIACAOContext _context;

        public EditModel(RazorPage_PadariaAVALIACAO.Data.RazorPage_PadariaAVALIACAOContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ItemVenda ItemVenda { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemvenda =  await _context.ItemVenda.FirstOrDefaultAsync(m => m.Id == id);
            if (itemvenda == null)
            {
                return NotFound();
            }
            ItemVenda = itemvenda;
           ViewData["ProdutoId"] = new SelectList(_context.Set<Produto>(), "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ItemVenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemVendaExists(ItemVenda.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ItemVendaExists(int id)
        {
            return _context.ItemVenda.Any(e => e.Id == id);
        }
    }
}
