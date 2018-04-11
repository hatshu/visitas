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
        [StringLength(50, ErrorMessage = "El campo nombre no puede tener mas de 50 caracteres")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [StringLength(50, ErrorMessage = "El campo apellido no puede tener mas de 50 caracteres")]
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellidos { get; set; }
        [StringLength(50, ErrorMessage = "El campo DNI no puede tener mas de 50 caracteres")]
        [Required(ErrorMessage = "El DNI es obligatorio")]
        public string DNI { get; set; }
        [StringLength(75, ErrorMessage = "El campo empresa no puede tener mas de 50 caracteres")]
        public string Empresa { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaAlta { get; set; }
    }
}
