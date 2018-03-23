using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CatecVisitas.Models;

namespace CatecVisitas.Pages.EnlaceVisitasPersonas
{
    public class CreateModel : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public CreateModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EnlaceVisitaPersona EnlaceVisitaPersona { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.EnlaceVisitaPersona.Add(EnlaceVisitaPersona);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}