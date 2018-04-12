using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatecVisitas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CatecVisitas.Pages.Visitantes
{
    public class Create2Model : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public Create2Model(CatecVisitas.Models.PersonContext context)
        {
            _context = context;

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Person Person { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Person.FechaAlta = DateTime.Today.Date;       
            _context.Person.Add(Person);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index2");

        }

        

        private IActionResult RedirectToPage(object pageName, object p)
        {
            throw new NotImplementedException();
        }
    }
}
