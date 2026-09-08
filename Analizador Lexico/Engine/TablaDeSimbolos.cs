using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalizadorLexico.Models;

namespace AnalizadorLexico.Engine
{
    public class TablaDeSimbolos
    {
        private List<SimboloTabla> listaSimbolos;
        private HashSet<string> palabrasReservadas;

        public TablaDeSimbolos()
        {
            listaSimbolos = new List<SimboloTabla>();
            palabrasReservadas = new HashSet<string>
            {
                "using", "namespace", "class", "public", "private", "protected",
                "static", "void", "int", "double", "float", "string", "bool",
                "if", "else", "while", "for", "return", "new", "true", "false"
            };
        }

        public bool EsPalabraReservada(string lexema)
        {
            return palabrasReservadas.Contains(lexema);
        }

        public void AgregarOActualizar(string nombre, string tipoToken, int linea, int columna)
        {
            
        }

        public List<SimboloTabla> ObtenerSimbolos()
        {
            return listaSimbolos;
        }
    }
}
