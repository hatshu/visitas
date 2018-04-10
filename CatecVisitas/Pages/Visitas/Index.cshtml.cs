using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatecVisitas.Models;

namespace CatecVisitas.Pages.Visitas
{
    public class IndexModel : PageModel
    {
        private int pageIndex = 0;
        public string SearchString = "";
        private readonly CatecVisitas.Models.PersonContext _context;
        public List<string> ListaNombres = new List<string>();
        public string DateSort { get; set; }
        public IndexModel(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
        }

        //public IList<Visita> Visita { get;set; }
        public PaginatedList<Visita> Visita { get; set; }


        public async Task OnGetAsync(string searchString, int? pageIndex)
        {

            //if (searchString != null)
            //{
            //    pageIndex = 1 + pageIndex;
            //}

            IQueryable<Visita> visitaIQFecha = from s in _context.Visita
                                            select s;


            visitaIQFecha = visitaIQFecha.OrderByDescending(s => s.FechaVisita.Date)
                                         .ThenByDescending(s => s.Hora);


            //var visita = from m in visitaIQFecha
            //             select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                int pageSize = 15;
                visitaIQFecha = visitaIQFecha.Where(s => s.FechaVisita.ToShortDateString().Equals(searchString) || s.Motivo.Contains(searchString));
                SearchString = searchString;
                Visita = await PaginatedList<Visita>.CreateAsync(visitaIQFecha.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
            else
            {
                int pageSize = 15;
                //Visita = await visita.ToListAsync();
                Visita = await PaginatedList<Visita>.CreateAsync(visitaIQFecha.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
           

        }
    }
}
