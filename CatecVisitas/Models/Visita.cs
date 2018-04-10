using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CatecVisitas.Models
{
    public class Visita
    {
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        //public int ID { get; set; }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdVisita { get; set; }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int IdPerson { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaVisita { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime Hora { get; set; }
        public string Motivo { get; set; }
        public string Duracion { get; set; }
        public string ResponsableCatec { get; set; }

    }
}
