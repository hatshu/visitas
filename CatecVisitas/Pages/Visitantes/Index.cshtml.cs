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

        public bool duplicateITEM = false;

        private const string PageName = "../Visitantes/Index";

        private readonly CatecVisitas.Models.PersonContext _context;

        public IList<Person> Person { get; set; }

        public IList<Person> PersonaListaFija { get; set; }

        public IList<Visita> Visita { get; set; }

        public IList<EnlaceVisitaPersona> EnlaceVisitaPersonaList { get; set; }

        public IndexModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
            Person = _context.Person.ToList();
            PersonaListaFija = _context.Person.ToList();
            Visita = _context.Visita.ToList();
            EnlaceVisitaPersonaList = _context.EnlaceVisitaPersona.ToList();

        }

        [BindProperty]
        public EnlaceVisitaPersona EnlaceVisitaPersona { get; set; }

        public async Task OnGetAsync(string searchString, string idVisita)
        {


            IQueryable<Person> personaIQNombreApellido = from s in _context.Person
                                                select s;

            personaIQNombreApellido = personaIQNombreApellido.OrderBy(s => s.Apellidos)
                                           .ThenBy(s => s.Nombre);            

            if (!String.IsNullOrEmpty(searchString))
            {

                var persona = personaIQNombreApellido.Where(s => s.Empresa.Contains(searchString) || s.DNI.Equals(searchString));
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
                var persona = personaIQNombreApellido.Where(s => s.Empresa.Contains("") && s.DNI.Equals(""));
                Person = await persona.ToListAsync();
                //Person = await personaIQNombreApellido.ToListAsync();
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



                //TODO: COMPROBAR QUE LA PERSONA NO ESTA YA EN LA LISTA

            foreach (var item in EnlaceVisitaPersonaList)
            {
                if (EnlaceVisitaPersona.VisitaID.Equals(item.VisitaID) && EnlaceVisitaPersona.PersonaID.Equals(item.PersonaID))
                {
                    duplicateITEM = true;
                    break;
                }
               
            }

            if (!duplicateITEM)
            {
                _context.EnlaceVisitaPersona.Add(EnlaceVisitaPersona);
                await _context.SaveChangesAsync();
            }
            else
            {
                return Page();
            }
           
           
           
            //return RedirectToPage("./Index");
            return this.RedirectToPage
            (PageName, new { idVisita = Visitita });


        }

    }
}
