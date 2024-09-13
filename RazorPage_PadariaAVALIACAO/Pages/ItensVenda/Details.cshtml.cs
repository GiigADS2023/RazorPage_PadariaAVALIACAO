﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly RazorPage_PadariaAVALIACAO.Data.RazorPage_PadariaAVALIACAOContext _context;

        public DetailsModel(RazorPage_PadariaAVALIACAO.Data.RazorPage_PadariaAVALIACAOContext context)
        {
            _context = context;
        }

        public ItemVenda ItemVenda { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemvenda = await _context.ItemVenda.FirstOrDefaultAsync(m => m.Id == id);
            if (itemvenda == null)
            {
                return NotFound();
            }
            else
            {
                ItemVenda = itemvenda;
            }
            return Page();
        }
    }
}
