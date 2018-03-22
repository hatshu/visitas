using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatecVisitas.Models;

namespace CatecVisitas.Pages.Visitas
{
    public class EditModel : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public EditModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Visita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitaExists(Visita.IdVisita))
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

        private bool VisitaExists(int IdVisita)
        {
            return _context.Visita.Any(e => e.IdVisita == IdVisita);
        }
    }
}
