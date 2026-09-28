using System;
using System.Linq;
using AnalizadorLexico.Models;

namespace AnalizadorLexico.Engine
{
    public class TablaDeSimbolos
    {
        private SimboloTabla[] arregloSimbolos;
        private int contadorSimbolos;
        private string[] palabrasReservadas;

        public TablaDeSimbolos()
        {
            arregloSimbolos = new SimboloTabla[1000];
            contadorSimbolos = 0;

            palabrasReservadas = new string[]
            {
                "using", "namespace", "class", "public", "private", "protected",
                "static", "void", "int", "double", "float", "string", "bool",
                "if", "else", "while", "for", "return", "new", "true", "false"
            };
        }

        public bool EsPalabraReservada(string lexema)
        {
            for (int i = 0; i < palabrasReservadas.Length; i++)
            {
                if (palabrasReservadas[i] == lexema)
                    return true;
            }
            return false;
        }

        public void AgregarOActualizar(string nombre, string tipoToken, int linea, int columna)
        {
            if (EsPalabraReservada(nombre))
            {
                return;
            }

            for (int i = 0; i < contadorSimbolos; i++)
            {
                if (arregloSimbolos[i].Nombre == nombre)
                {
                    return; 
                }
            }

           
            int nuevoId = contadorSimbolos + 1;
            SimboloTabla nuevoSimbolo = new SimboloTabla(nuevoId, nombre, tipoToken, linea, columna);
            arregloSimbolos[contadorSimbolos] = nuevoSimbolo;
            contadorSimbolos++;
        }

        public SimboloTabla[] ObtenerSimbolos()
        {
            SimboloTabla[] resultado = new SimboloTabla[contadorSimbolos];
            Array.Copy(arregloSimbolos, resultado, contadorSimbolos);
            return resultado;
        }

        public void Limpiar()
        {
            contadorSimbolos = 0;
            arregloSimbolos = new SimboloTabla[1000];
        }
    }
}
