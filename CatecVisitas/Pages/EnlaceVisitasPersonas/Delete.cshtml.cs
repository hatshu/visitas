using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatecVisitas.Models;

namespace CatecVisitas.Pages.EnlaceVisitasPersonas
{
    public class DeleteModel : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public DeleteModel(CatecVisitas.Models.PersonContext context)
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

            EnlaceVisitaPersona = await _context.EnlaceVisitaPersona.SingleOrDefaultAsync(m => m.IDEnlace == id);

            if (EnlaceVisitaPersona == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EnlaceVisitaPersona = await _context.EnlaceVisitaPersona.FindAsync(id);

            if (EnlaceVisitaPersona != null)
            {
                _context.EnlaceVisitaPersona.Remove(EnlaceVisitaPersona);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
