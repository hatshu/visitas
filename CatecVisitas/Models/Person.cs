﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatecVisitas.Models
{
    public class Person
    {
      public int ID { get; set; }
      public string Nombre { get; set; }
      public string Apellidos { get; set; }
      public string DNI { get; set; }
      public string Empresa { get; set; }
      public DateTime FechaAlta { get; set; }
   }
}
