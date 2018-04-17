﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatecVisitas.Models
{
    public class PaginatedList<T>: List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize, int sourceFull)
        {


            var count = sourceFull;
            //var count = await source.CountAsync();
            //var items = await source
            //    .Skip((pageIndex - 1) * pageSize)
            //    .Take(pageSize).ToListAsync();
            ////var items = await source.ToListAsync();

            //return new PaginatedList<T>(items, count, pageIndex, pageSize);
            return new PaginatedList<T>(source.ToList(), count, pageIndex, pageSize);

        }

        internal IQueryable<Person> AsNoTracking()
        {
            throw new NotImplementedException();
        }

        internal IQueryable<Visita> NoTracking()
        {
            throw new NotImplementedException();
        }
        internal List<Visita> DoNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}
