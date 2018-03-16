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
    public class IndexModel : PageModel
    {
        private readonly CatecVisitas.Models.PersonContext _context;

        public IndexModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
        }

        public IList<Person> Person { get;set; }

       public async Task OnGetAsync(string searchString)
       {
          var persona = from p in _context.Person
             select p;

          if (!String.IsNullOrEmpty(searchString))
          {
             persona = persona.Where(s =>  s.Empresa.Equals(searchString) || s.DNI.Equals(searchString));
          }

          Person = await persona.ToListAsync();
       }
   }
}
