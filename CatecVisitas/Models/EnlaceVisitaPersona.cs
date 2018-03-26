using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CatecVisitas.Models
{
    public class EnlaceVisitaPersona
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IDEnlace { get; set; }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int VisitaID { get; set; }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int PersonaID { get; set; }
    }
}
