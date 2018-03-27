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

        public string SearchStream;

        private readonly CatecVisitas.Models.PersonContext _context;

        public IndexModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
           
        }

        public IList<Person> Person { get; set; }
        public IList<Visita> Visita { get; set; }


      


        public async Task OnGetAsync(string searchString, string id , string idVisita)
        {
            var persona = from p in _context.Person
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                persona = persona.Where(s => s.Empresa.Equals(searchString) || s.DNI.Equals(searchString));
                Person = await persona.ToListAsync();
                var visit = Request.QueryString.ToString();
                if (visit!=null)
                {
                    VisitaArray = visit.Split('=');
                }
               
                if (VisitaArray.Length > 0)
                {
                    Visitita = VisitaArray[2];

                }
                SearchStream = searchString;
            }
            else
            {
                Person = await persona.ToListAsync();
                var visitSinBuscador = Request.QueryString.ToString();
                VisitaArray = visitSinBuscador.Split('=');
                if (VisitaArray.Length>0)
                {
                    Visitita = VisitaArray[1];

                }
            }


           

        }



    }
}
