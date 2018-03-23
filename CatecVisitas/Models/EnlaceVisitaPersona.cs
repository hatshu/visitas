using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatecVisitas.Models
{
    public class EnlaceVisitaPersona
    {
        public int Id { get; set; }
        public int IDEnlace { get; set; }
        public string VisitaID { get; set; }
        public string PersonaID { get; set; }
    }
}
