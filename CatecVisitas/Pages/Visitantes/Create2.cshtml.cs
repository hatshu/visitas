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

        public bool validationError = false;

        public IList<Person> PersonList { get; set; }

        private readonly CatecVisitas.Models.PersonContext _context;

        public Create2Model(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
            PersonList = _context.Person.ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Person Person { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            IQueryable<Person> PersonIQ = from s in _context.Person
                                          select s;

            var DNIID = Person.DNI;
            if (!String.IsNullOrEmpty(DNIID))
            {
                PersonIQ = PersonIQ.Where(s => s.DNI.Equals(DNIID));
                if (PersonIQ.Count() > 0)
                {
                    ModelState.AddModelError("DNIerror", "EL DNI esta duplicado");
                    validationError = true;
                }


            }
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
