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
        [StringLength(50, ErrorMessage = "El campo motivo no puede tener mas de 100 caracteres")]
        [Required(ErrorMessage = "El motivo es obligatorio")]
        public string Motivo { get; set; }
        [StringLength(50, ErrorMessage = "El campo duracion no puede tener mas de 50 caracteres")]
        public string Duracion { get; set; }
        [StringLength(50, ErrorMessage = "El campo responsable no puede tener mas de 50 caracteres")]
        [Required(ErrorMessage = "El responsable es obligatorio")]
        public string ResponsableCatec { get; set; }

    }
}
