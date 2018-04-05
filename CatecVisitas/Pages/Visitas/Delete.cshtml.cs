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
    public class DeleteModel : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public IList<EnlaceVisitaPersona> EnlaceVisitaPersonaList { get; set; }

        public DeleteModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
            EnlaceVisitaPersonaList = _context.EnlaceVisitaPersona.ToList();
        }

        [BindProperty]
        public Visita Visita { get; set; }

        [BindProperty]
        public EnlaceVisitaPersona EnlaceVisitaPersona { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? IdVisita)
        {
            if (IdVisita == null)
            {
                return NotFound();
            }

            Visita = await _context.Visita.FindAsync(IdVisita);


            foreach (var item in EnlaceVisitaPersonaList)
            {
                if (item.VisitaID == IdVisita)
                {
                    EnlaceVisitaPersona = item;
                    if (item != null)
                    {
                        _context.EnlaceVisitaPersona.Remove(EnlaceVisitaPersona);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (Visita != null)
            {
                _context.Visita.Remove(Visita);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
