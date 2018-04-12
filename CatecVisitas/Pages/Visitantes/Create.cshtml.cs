using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CatecVisitas.Models;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CatecVisitas.Pages.Visitantes
{
    public class CreateModel : PageModel
    {
        private const string PageName = "Index";

        public string Query;

        public string[] QueryArray;

        public bool validationError = false;

        private readonly CatecVisitas.Models.PersonContext _context;

        public CreateModel(CatecVisitas.Models.PersonContext context)
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
        [BindProperty]
        public EnlaceVisitaPersona EnlaceVisitaPersona { get; set; }

        public IList<EnlaceVisitaPersona> EnlaceVisitaPersonaList { get; set; }

        public IList<Person> PersonList { get; set; }

      


        public async Task<IActionResult> OnPostAsync(string DNI)
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