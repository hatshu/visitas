using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CatecVisitas.Models;

namespace CatecVisitas.Pages.Visitantes
{
    public class CreateModel : PageModel
    {
        private const string PageName = "Index";

        public string Query;

        public string[] QueryArray;

     

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
        public Person Person { get; set; }
        [BindProperty]
        public EnlaceVisitaPersona EnlaceVisitaPersona { get; set; }

        public IList<EnlaceVisitaPersona> EnlaceVisitaPersonaList { get; set; }

        //public IList<Person> PersonList { get; set; }

        public async Task<IActionResult> OnPostAsync(string DNI)
        {

         IQueryable<Person> PersonIQ = from s in _context.Person
                                          select s;

            if (!String.IsNullOrEmpty(DNI))
            {
                PersonIQ = PersonIQ.Where(s => s.DNI.Equals(DNI));
                if ((PersonIQ != null) && (!PersonIQ.Any()))
                {
                    ModelState.AddModelError("NewEmp", "EL DNI esta duplicado");
                    return Page();
                }


            }




            if (!ModelState.IsValid)
            {
                return Page();
            }
         Person.FechaAlta = DateTime.Today.Date;
            Query = Request.QueryString.ToString();
            QueryArray= Query.Split('=');
            _context.Person.Add(Person);

            await _context.SaveChangesAsync();


            EnlaceVisitaPersona.PersonaID = Person.Id;
            EnlaceVisitaPersona.VisitaID = Convert.ToInt32(QueryArray[1]);
            _context.EnlaceVisitaPersona.Add(EnlaceVisitaPersona);


            await _context.SaveChangesAsync();


            //return RedirectToPage("./Index");
            return this.RedirectToPage
            (PageName, new { idVisita = QueryArray[1] });
        }
    }
}