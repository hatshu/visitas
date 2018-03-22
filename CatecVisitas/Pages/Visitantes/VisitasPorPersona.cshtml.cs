using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatecVisitas.Models;

namespace CatecVisitas.Pages.Visitantes
{
    public class VisitasPorPersonaModel : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public VisitasPorPersonaModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
        }

        public IList<Visita> Visita { get;set; }

        public IList<Person> Persona { get; set; }

        public async Task OnGetAsync()
        {
            Visita = await _context.Visita.ToListAsync();
            Persona = await _context.Person.ToListAsync();
        }
    }
}
