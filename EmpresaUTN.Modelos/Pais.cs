using System.ComponentModel.DataAnnotations;

namespace EmpresaUTN.Modelos
{
    public class Pais
    {
        [Key]
        public int CodigoPais { get; set; } // pk, sugerencia de tipo int
        public string Nombre { get; set; }
        public int Poblacion { get; set; }
        public string CodigoISO { get; set; }
        public string Moneda { get; set; } 
        public string Capital { get; set; }
        public string Idioma { get; set; }

        //relacion con provincia
        public List<Provincia>? Provincias { get; set; }

    }
}