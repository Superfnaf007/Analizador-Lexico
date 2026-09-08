using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico.Models
{
    public class ErrorLexico
    {
        public string CaracterOTexto { get; set; }
        public string Descripcion { get; set; }
        public int Linea { get; set; }
        public int Columna { get; set; }

        public ErrorLexico(string caracterOTexto, string descripcion, int linea, int columna)
        {
            CaracterOTexto = caracterOTexto;
            Descripcion = descripcion;
            Linea = linea;
            Columna = columna;
        }
    }
}