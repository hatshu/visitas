using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatecVisitas.Models
{
    public class Visita
    {
       public int IDVisita { get; set; }
       public int ID { get; set; }
       [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
       public DateTime FechaVisita { get; set; }
    }
}
