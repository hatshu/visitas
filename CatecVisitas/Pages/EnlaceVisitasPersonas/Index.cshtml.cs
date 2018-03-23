using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatecVisitas.Models;

namespace CatecVisitas.Pages.EnlaceVisitasPersonas
{
    public class IndexModel : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public IndexModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
        }

        public IList<EnlaceVisitaPersona> EnlaceVisitaPersona { get;set; }

        public async Task OnGetAsync()
        {
            EnlaceVisitaPersona = await _context.EnlaceVisitaPersona.ToListAsync();
        }
    }
}