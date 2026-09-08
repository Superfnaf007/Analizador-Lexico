using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico.Models
{
    public class SimboloTabla
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoToken { get; set; }
        public int PrimeraLinea { get; set; }
        public int PrimeraColumna { get; set; }

        public SimboloTabla(int id, string nombre, string tipoToken, int primeraLinea, int primeraColumna)
        {
            Id = id;
            Nombre = nombre;
            TipoToken = tipoToken;
            PrimeraLinea = primeraLinea;
            PrimeraColumna = primeraColumna;
        }
    }
}
