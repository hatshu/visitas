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
    public class DetailsModel : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public DetailsModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
        }

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
    }
}
