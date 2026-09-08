using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalizadorLexico.Models;

namespace AnalizadorLexico.Engine
{
    public class AnalizadorLexico
    {
        private string codigoFuente;
        public List<Token> TokensReconocidos { get; private set; }
        public List<ErrorLexico> ErroresDetectados { get; private set; }
        public TablaDeSimbolos TablaSimbolos { get; private set; }

        public AnalizadorLexico(string codigoFuente)
        {
            this.codigoFuente = codigoFuente;
            TokensReconocidos = new List<Token>();
            ErroresDetectados = new List<ErrorLexico>();
            TablaSimbolos = new TablaDeSimbolos();
        }

        public void Escanear()
        {
           
        }
    }
}
