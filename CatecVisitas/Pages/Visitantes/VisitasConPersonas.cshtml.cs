using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatecVisitas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CatecVisitas.Pages.Visitantes
{
    public class VisitasConPersonasModel : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public VisitasConPersonasModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
        }

        public IList<Visita> Visita { get; set; }

        public IList<Person> Persona { get; set; }

        public IList<EnlaceVisitaPersona> EnlaceVisitasPersonas { get; set; }


        public async Task OnGetAsync()
        {
            Visita = await _context.Visita.ToListAsync();
            Persona = await _context.Person.ToListAsync();
            EnlaceVisitasPersonas = await _context.EnlaceVisitaPersona.ToListAsync();
        }
    }
}