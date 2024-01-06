using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaUTN.Modelos
{
    public class Provincia
    {
       
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Area { get; set; }
        public string? ActividadEconomica { get; set; }

        //relacion con pais
        public int PaisCodigoPais { get; set; } //fk

        public Pais? Pais { get; set; }

        //relacion con canton
        public List<Canton>? Cantones { get; set; }

    }
}
