using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatecVisitas.Models
{
    public class Person
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellidos { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "El DNI es obligatorio")]
        public string DNI { get; set; }
        [MaxLength(75)]
        public string Empresa { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaAlta { get; set; }
    }
}
