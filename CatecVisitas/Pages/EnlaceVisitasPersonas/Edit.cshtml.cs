using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatecVisitas.Models;

namespace CatecVisitas.Pages.EnlaceVisitasPersonas
{
    public class EditModel : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public EditModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EnlaceVisitaPersona EnlaceVisitaPersona { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EnlaceVisitaPersona = await _context.EnlaceVisitaPersona.SingleOrDefaultAsync(m => m.Id == id);

            if (EnlaceVisitaPersona == null)
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

            _context.Attach(EnlaceVisitaPersona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnlaceVisitaPersonaExists(EnlaceVisitaPersona.Id))
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

        private bool EnlaceVisitaPersonaExists(int id)
        {
            return _context.EnlaceVisitaPersona.Any(e => e.Id == id);
        }
    }
}
