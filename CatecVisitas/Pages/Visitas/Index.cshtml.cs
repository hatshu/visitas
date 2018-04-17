using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatecVisitas.Models;
using System.Data.SqlClient;


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
        public PaginatedList<Visita> Visit { get; set; }


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

            var sourceFull = visitaIQFecha.Count();

            //var visita = from m in visitaIQFecha
            //             select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                int pageSize = 15;

                if (pageIndex == null)
                {
                    pageIndex = 1;
                }

                var MinPageRank = (pageIndex - 1) * pageSize + 1;
                var MaxPageRank = (pageIndex * pageSize);
                visitaIQFecha = visitaIQFecha.Where(s => s.FechaVisita.ToShortDateString().Equals(searchString.Trim()) || s.Motivo.ToLower().Contains(searchString.ToLower()) || s.ResponsableCatec.ToLower().Equals(searchString.ToLower()));
                //var visit = _context.Visita.FromSql($"SELECT * FROM (SELECT [RANK] = ROW_NUMBER() OVER (ORDER BY Hora DESC),* FROM Visita) A WHERE A.[RANK] BETWEEN {MinPageRank} AND {MaxPageRank}").ToList();

                IQueryable<Visita> visitita = from s in visitaIQFecha.AsQueryable() select s;

               
                
                SearchString = searchString;


                Visita = await PaginatedList<Visita>.CreateAsync(visitaIQFecha.AsNoTracking(), pageIndex ?? 1, pageSize, sourceFull);
            }
            else
            {
                int pageSize = 15;
                //Visita = await visita.ToListAsync();
                if (pageIndex==null)
                {
                    pageIndex = 1;
                }
                
                var MinPageRank =  (pageIndex - 1) * pageSize + 1; 
                var MaxPageRank = (pageIndex * pageSize);
                var visit = _context.Visita.FromSql($"SELECT * FROM (SELECT [RANK] = ROW_NUMBER() OVER (ORDER BY Hora DESC),* FROM Visita) A WHERE A.[RANK] BETWEEN {MinPageRank} AND {MaxPageRank}").ToList();

                IQueryable<Visita> visitita = from s in visit.AsQueryable() select s;


                Visita = await PaginatedList<Visita>.CreateAsync(visitita.AsNoTracking(), pageIndex ?? 1, pageSize, sourceFull);

                //Visita = await PaginatedList<Visita>.CreateAsync(visitaIQFecha.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
           

        }
    }
}
