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
    public class Index2Model : PageModel
    {
        private int pageIndex = 0;

        public string SearchString = "";

        private readonly CatecVisitas.Models.PersonContext _context;

        public Index2Model(CatecVisitas.Models.PersonContext context)
        {
            _context = context;
        }


        //public IList<Person> Person { get; set; }
        public PaginatedList<Person> Person { get; set; }
        public PaginatedList<Person> Personita { get; set; }
        //public IList<Visita> Visita { get; set; }


        public async Task OnGetAsync( string searchString, int? pageIndex)
        {

            //var persona = from p in _context.Person
            //                  select p;

            //    if (!String.IsNullOrEmpty(searchString))
            //    {
            //        persona = persona.Where(s => s.Empresa.Contains(searchString) || s.DNI.Equals(searchString));
            //    }
            //    Person = await persona.ToListAsync();

            //if (searchString != null)
            //{
            //    pageIndex = pageIndex;
            //}

            IQueryable<Person> PersonIQ = from s in _context.Person
                                            select s;

            var sourceFull = PersonIQ.Count();

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
                PersonIQ = PersonIQ.Where(s => s.Empresa.Contains(searchString) || s.DNI.Equals(searchString));
                //PersonIQ = PersonIQ.OrderBy(s => s.Apellidos);
                sourceFull = PersonIQ.Count();
                var person2008 = _context.Person.FromSql($"SELECT * FROM (SELECT [RANK] = ROW_NUMBER() OVER (ORDER BY Apellidos, Nombre),* FROM Person WHERE Empresa LIKE {searchCriteria} OR DNI = {searchString}) A WHERE A.[RANK] BETWEEN {MinPageRank} AND {MaxPageRank}").ToList();

                IQueryable<Person> Personita = from s in person2008.AsQueryable() select s;

                SearchString = searchString;
                Person = await PaginatedList<Person>.CreateAsync(Personita.AsNoTracking(), pageIndex ?? 1, pageSize, sourceFull);

            }
            else
            {
             PersonIQ = PersonIQ.OrderBy(s => s.Apellidos);
             int pageSize = 15;
                if (pageIndex == null)
                {
                    pageIndex = 1;
                }

                var MinPageRank = (pageIndex - 1) * pageSize + 1;
                var MaxPageRank = (pageIndex * pageSize);
                var person2008 = _context.Person.FromSql($"SELECT * FROM (SELECT [RANK] = ROW_NUMBER() OVER (ORDER BY Apellidos , Nombre),* FROM Person) A WHERE A.[RANK] BETWEEN {MinPageRank} AND {MaxPageRank}").ToList();

                IQueryable<Person> Personita = from s in person2008.AsQueryable() select s;
                Person = await PaginatedList<Person>.CreateAsync(Personita.AsNoTracking(), pageIndex ?? 1, pageSize, sourceFull);
            }
           

        }

    }
}
