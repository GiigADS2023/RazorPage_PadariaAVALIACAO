using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPage_PadariaAVALIACAO.Data;
using RazorPage_PadariaAVALIACAO.Models;

namespace RazorPage_PadariaAVALIACAO.Pages.Produtos
{
    public class CreateModel : PageModel
    {
        private readonly RazorPage_PadariaAVALIACAO.Data.RazorPage_PadariaAVALIACAOContext _context;

        public CreateModel(RazorPage_PadariaAVALIACAO.Data.RazorPage_PadariaAVALIACAOContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Produto Produto { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Produto.Add(Produto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
