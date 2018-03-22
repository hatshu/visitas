using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatecVisitas.Models;

namespace CatecVisitas.Pages.Visitas
{
    public class DetailsModel : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public DetailsModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
        }

        public Visita Visita { get; set; }

        public async Task<IActionResult> OnGetAsync(int? IdVisita)
        {
            if (IdVisita == null)
            {
                return NotFound();
            }

            Visita = await _context.Visita.SingleOrDefaultAsync(m => m.IdVisita == IdVisita);

            if (Visita == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
