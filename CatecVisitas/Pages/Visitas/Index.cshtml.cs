using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CatecVisitas.Models;
using System.Data.SqlClient;
using System.Globalization;

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
                var searchCriteria = '%' + searchString + '%';
                DateTime dateSearch;
                var date = "";
                bool onlyDigits = false , formatDate = false;
                onlyDigits = searchString.All(c => c >= '0' && c <= '9' || c == '/' || c == '-');
                //TODO: forzar a que la fecha sea correcta
                formatDate = formatoCorrecto(searchString);
                if (searchString.Contains("-") || searchString.Contains("/") &&  onlyDigits && searchString.Count() > 6 && searchString.Count() <= 10 && formatDate)
                {
                    //dateSearch = Convert.ToDateTime(searchString);
                    dateSearch=Convert.ToDateTime(searchString);
                    var dia = dateSearch.Day;
                    var mes = dateSearch.Month;
                    var anno = dateSearch.Year;
                    if ( dia <= 9 && mes <= 9)
                    {
                        date = anno + "-0" + mes + "-0" + dia;
                    }
                    else if (dia <= 9)
                    {
                        date = anno + "-" + mes + "-0" + dia;
                    }
                    else if (mes <=9)
                    {
                        date = anno + "-0" + mes + "-" + dia;
                    }
                    else
                    {
                        date = anno + "-" + mes + "-" + dia;
                    }


                }
                if (date.Equals(""))
                {
                    visitaIQFecha = visitaIQFecha.Where(s => s.FechaVisita.ToShortDateString().Equals(searchString.Trim()) || s.Motivo.ToLower().Contains(searchString.ToLower()) || s.ResponsableCatec.ToLower().Equals(searchString.ToLower()));
                }
               else
                {
                    var dateAux = Convert.ToDateTime(date);
                    visitaIQFecha = visitaIQFecha.Where(s => s.FechaVisita.ToShortDateString().Equals(dateAux.ToShortDateString()));
                }
                sourceFull = visitaIQFecha.Count();
                var visit = _context.Visita.FromSql($"SELECT * FROM (SELECT [RANK] = ROW_NUMBER() OVER (ORDER BY FechaVisita DESC, HORA DESC),* FROM Visita WHERE Motivo LIKE {searchCriteria} OR FechaVisita = {date} OR ResponsableCatec = {searchString} ) A WHERE A.[RANK] BETWEEN {MinPageRank} AND {MaxPageRank}").ToList();

                IQueryable<Visita> visitita = from s in visit.AsQueryable() select s;

                //sourceFull = visitita.Count();
                
                SearchString = searchString;


                Visita = await PaginatedList<Visita>.CreateAsync(visitita.AsNoTracking(), pageIndex ?? 1, pageSize, sourceFull);
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
                var visit = _context.Visita.FromSql($"SELECT * FROM (SELECT [RANK] = ROW_NUMBER() OVER (ORDER BY FechaVisita DESC , Hora DESC),* FROM Visita) A WHERE A.[RANK] BETWEEN {MinPageRank} AND {MaxPageRank}").ToList();

                IQueryable<Visita> visitita = from s in visit.AsQueryable() select s;


                Visita = await PaginatedList<Visita>.CreateAsync(visitita.AsNoTracking(), pageIndex ?? 1, pageSize, sourceFull);

                //Visita = await PaginatedList<Visita>.CreateAsync(visitaIQFecha.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
           

        }

        private bool formatoCorrecto(string searchString)
        {
            if (searchString.Contains("-"))
            {
                var searchArray = searchString.Split('-');
                if (searchArray.Count()==3 && searchArray[0].Length <= 2 && searchArray[1].Length <= 2 && searchArray[2].Length <= 4 && Convert.ToInt32(searchArray[1]) <= 12)
                {
                    return true;
                }
            }


            if (searchString.Contains('/'))
            {
                var searchArray = searchString.Split('/');
                if (searchArray.Count() == 3 && searchArray[0].Length <= 2 && searchArray[1].Length <= 2 && searchArray[2].Length <= 4  && Convert.ToInt32(searchArray[1]) <= 12)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
