using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatecVisitas.Models
{
    public class PersonContext :DbContext
    {
      public PersonContext(DbContextOptions<PersonContext> options)
        : base(options)
      {

      }
      public DbSet<Person> Person { get; set; }
      public DbSet<Visita> Visita { get; set; }
      public DbSet<EnlaceVisitaPersona> EnlaceVisitaPersona { get; set; }
   }
}
