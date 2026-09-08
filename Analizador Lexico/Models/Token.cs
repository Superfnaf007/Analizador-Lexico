using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico.Models
{
    public class Token
    {
        public string Lexema { get; set; }
        public string Tipo { get; set; }
        public int Linea { get; set; }
        public int Columna { get; set; }

        public Token(string lexema, string tipo, int linea, int columna)
        {
            Lexema = lexema;
            Tipo = tipo;
            Linea = linea;
            Columna = columna;
        }

        public override string ToString()
        {
            return $"[{Linea}:{Columna}] {Tipo} -> '{Lexema}'";
        }
    }
}
