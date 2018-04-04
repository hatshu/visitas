using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatecVisitas.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CatecVisitas.Pages.Visitantes
{
    public class IndexModel : PageModel
    {

        public string Visitita;

        public string[] VisitaArray;

        public string Visitita2;

        public string[] VisitaArray2;

        public string SearchStream;

        public string PersonaID;

        public string VisitaID;

        private const string PageName = "../Visitantes/Index";

        private readonly CatecVisitas.Models.PersonContext _context;

        public IList<Person> Person { get; set; }

        public IList<Visita> Visita { get; set; }


        public IndexModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
            Person = _context.Person.ToList();
            Visita = _context.Visita.ToList();

        }

        [BindProperty]
        public EnlaceVisitaPersona EnlaceVisitaPersona { get; set; }
        public IList<EnlaceVisitaPersona> EnlaceVisitaPersonaList { get; set; }

        public async Task OnGetAsync(string searchString, string idVisita)
        {
            var persona = from p in _context.Person
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                persona = persona.Where(s => s.Empresa.Contains(searchString) || s.DNI.Equals(searchString));
                Person = await persona.ToListAsync();
                var visit = Request.QueryString.ToString();
                //TODO: trabajar con visit para sacar todas las variables
                if (visit != null)
                {
                    VisitaArray = visit.Split('=', '&');
                }

                if (VisitaArray.Length > 0 || VisitaArray.Length <=4)
                {
                    Visitita = VisitaArray[3];

                }
                SearchStream = searchString;
            }
            else
            {
                //persona = persona.Where(s => s.Empresa.Contains(searchString) || s.DNI.Equals(searchString));
                //Person = await persona.ToListAsync();
                //var visit = Request.QueryString.ToString();

                Person = await persona.ToListAsync();
                var visitSinBuscador = Request.QueryString.ToString();
                VisitaArray = visitSinBuscador.Split('=');
                if (VisitaArray.Length> 0 && !visitSinBuscador.Equals(""))
                {
                    Visitita = VisitaArray[1]; 
                }
            }

            VisitaID = Visitita;
           

        }

        public async Task<IActionResult> OnPostAsync(string save)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

                var visitSinBuscador = Request.QueryString.ToString();
                VisitaArray = visitSinBuscador.Split('=','&');
                if (VisitaArray.Length > 0 && !visitSinBuscador.Equals(""))
                {

                if (VisitaArray.Length==4)
                {
                    Visitita = VisitaArray[3];
                }
                else
                {
                    Visitita = VisitaArray[1];  
                }
                EnlaceVisitaPersona.VisitaID = Convert.ToInt32(Visitita);
                foreach (var item in Request.Form)
                {
                    if (item.Key.Equals("bookId"))
                    {
                        EnlaceVisitaPersona.PersonaID = Convert.ToInt32(item.Value);
                        //PersonaID = EnlaceVisitaPersona.PersonaID.ToString();
                        break;
                    }
                }
            }

            _context.EnlaceVisitaPersona.Add(EnlaceVisitaPersona);


            await _context.SaveChangesAsync();
            //return RedirectToPage("./Index");
            return this.RedirectToPage
            (PageName, new { idVisita = Visitita });


        }

    }
}
