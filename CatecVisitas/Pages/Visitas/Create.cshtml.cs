using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CatecVisitas.Models;

namespace CatecVisitas.Pages.Visitas
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
        public Visita Visita { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var queryString = Request.QueryString.ToString();
            var partOfQueryString = queryString.Split('=');
            Visita.IdPerson = Convert.ToInt32(partOfQueryString[1]);
            Visita.FechaVisita = DateTime.Today.Date;
            Visita.Hora = DateTime.Now.ToString("hh:mm:ss");
            
            _context.Visita.Add(Visita);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}