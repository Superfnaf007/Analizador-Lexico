using System;
using AnalizadorLexico.Models;

namespace AnalizadorLexico.Engine
{
    public class AnalizadorLexico
    {
        private string codigoFuente;
        private int posicion;
        private int lineaActual;
        private int columnaActual;

        private Token[] tokens;
        private int contadorTokens;

        private ErrorLexico[] errores;
        private int contadorErrores;

        public TablaDeSimbolos TablaSimbolos { get; private set; }

        public AnalizadorLexico(string codigoFuente)
        {
            this.codigoFuente = codigoFuente ?? string.Empty;
            posicion = 0;
            lineaActual = 1;
            columnaActual = 1;

            tokens = new Token[2000];
            contadorTokens = 0;

            errores = new ErrorLexico[500];
            contadorErrores = 0;

            TablaSimbolos = new TablaDeSimbolos();
        }

        public Token[] ObtenerTokens()
        {
            Token[] resultado = new Token[contadorTokens];
            Array.Copy(tokens, resultado, contadorTokens);
            return resultado;
        }

        public ErrorLexico[] ObtenerErrores()
        {
            ErrorLexico[] resultado = new ErrorLexico[contadorErrores];
            Array.Copy(errores, resultado, contadorErrores);
            return resultado;
        }


        public void Escanear()
        {
            posicion = 0;
            lineaActual = 1;
            columnaActual = 1;
            contadorTokens = 0;
            contadorErrores = 0;
            TablaSimbolos.Limpiar();

            while (posicion < codigoFuente.Length)
            {
                char caracterActual = codigoFuente[posicion];

                if (char.IsWhiteSpace(caracterActual))
                {
                    if (caracterActual == '\n')
                    {
                        lineaActual++;
                        columnaActual = 1;
                    }
                    else if (caracterActual == '\r')
                    {
                    }
                    else
                    {
                        columnaActual++;
                    }
                    posicion++;
                    continue;
                }

                if (caracterActual == '/' && Peek() == '/')
                {
                    int inicioCol = columnaActual;
                    int inicioPos = posicion;
                    posicion += 2;
                    columnaActual += 2;
                    while (posicion < codigoFuente.Length && codigoFuente[posicion] != '\n')
                    {
                        posicion++;
                        columnaActual++;
                    }
                    string lex = codigoFuente.Substring(inicioPos, posicion - inicioPos);
                    AgregarToken(lex, "COMENTARIO_LINEA", lineaActual, inicioCol);
                    continue;
                }

                if (caracterActual == '/' && Peek() == '*')
                {
                    int inicioCol = columnaActual;
                    int inicioLinea = lineaActual;
                    int inicioPos = posicion;
                    posicion += 2;
                    columnaActual += 2;
                    bool cerrado = false;
                    while (posicion < codigoFuente.Length)
                    {
                        if (codigoFuente[posicion] == '*' && Peek() == '/')
                        {
                            posicion += 2;
                            columnaActual += 2;
                            cerrado = true;
                            break;
                        }
                        if (codigoFuente[posicion] == '\n')
                        {
                            lineaActual++;
                            columnaActual = 1;
                            posicion++;
                        }
                        else
                        {
                            posicion++;
                            columnaActual++;
                        }
                    }
                    if (!cerrado)
                    {
                        AgregarError("/*", "Comentario de bloque sin cerrar", inicioLinea, inicioCol);
                    }
                    else
                    {
                        string lex = codigoFuente.Substring(inicioPos, posicion - inicioPos);
                        AgregarToken(lex, "COMENTARIO_BLOQUE", inicioLinea, inicioCol);
                    }
                    continue;
                }

                if (caracterActual == '"')
                {
                    int inicioCol = columnaActual;
                    int inicioLinea = lineaActual;
                    int inicioPos = posicion;
                    posicion++;
                    columnaActual++;
                    bool cerrado = false;
                    while (posicion < codigoFuente.Length)
                    {
                        char c = codigoFuente[posicion];
                        if (c == '\\')
                        {
                            posicion += 2;
                            columnaActual += 2;
                            continue;
                        }
                        if (c == '"')
                        {
                            posicion++;
                            columnaActual++;
                            cerrado = true;
                            break;
                        }
                        if (c == '\n')
                        {
                            lineaActual++;
                            columnaActual = 1;
                            posicion++;
                        }
                        else
                        {
                            posicion++;
                            columnaActual++;
                        }
                    }
                    if (!cerrado)
                    {
                        AgregarError(codigoFuente.Substring(inicioPos, Math.Min(10, codigoFuente.Length - inicioPos)), "Cadena sin cerrar", inicioLinea, inicioCol);
                    }
                    else
                    {
                        string lex = codigoFuente.Substring(inicioPos, posicion - inicioPos);
                        AgregarToken(lex, "CADENA", inicioLinea, inicioCol);
                    }
                    continue;
                }

                if (caracterActual == '\'')
                {
                    int inicioCol = columnaActual;
                    int inicioLinea = lineaActual;
                    int inicioPos = posicion;
                    posicion++;
                    columnaActual++;
                    bool cerrado = false;
                    if (posicion < codigoFuente.Length)
                    {
                        if (codigoFuente[posicion] == '\\')
                        {
                            posicion += 2; 
                            columnaActual += 2;
                        }
                        else
                        {
                            posicion++;
                            columnaActual++;
                        }
                    }
                    if (posicion < codigoFuente.Length && codigoFuente[posicion] == '\'')
                    {
                        posicion++;
                        columnaActual++;
                        cerrado = true;
                    }
                    if (!cerrado)
                    {
                        AgregarError("'", "Caracter literal sin cerrar", inicioLinea, inicioCol);
                    }
                    else
                    {
                        string lex = codigoFuente.Substring(inicioPos, posicion - inicioPos);
                        AgregarToken(lex, "CARACTER", inicioLinea, inicioCol);
                    }
                    continue;
                }

                if (EsLetra(caracterActual) || caracterActual == '_')
                {
                    int inicioCol = columnaActual;
                    int inicioPos = posicion;
                    while (posicion < codigoFuente.Length && (EsLetraODigito(codigoFuente[posicion]) || codigoFuente[posicion] == '_'))
                    {
                        posicion++;
                        columnaActual++;
                    }
                    string lex = codigoFuente.Substring(inicioPos, posicion - inicioPos);
                    if (TablaSimbolos.EsPalabraReservada(lex))
                    {
                        AgregarToken(lex, "PALABRA_RESERVADA", lineaActual, inicioCol);
                    }
                    else
                    {
                        AgregarToken(lex, "IDENTIFICADOR", lineaActual, inicioCol);
                        TablaSimbolos.AgregarOActualizar(lex, "IDENTIFICADOR", lineaActual, inicioCol);
                    }
                    continue;
                }

                if (char.IsDigit(caracterActual))
                {
                    int inicioCol = columnaActual;
                    int inicioPos = posicion;
                    bool esReal = false;
                    while (posicion < codigoFuente.Length && char.IsDigit(codigoFuente[posicion]))
                    {
                        posicion++;
                        columnaActual++;
                    }
                    if (posicion < codigoFuente.Length && (EsLetra(codigoFuente[posicion]) || codigoFuente[posicion] == '_'))
                    {
                        int fin = posicion + 1;
                        while (fin < codigoFuente.Length && (EsLetraODigito(codigoFuente[fin]) || codigoFuente[fin] == '_'))
                        {
                            fin++;
                        }
                        string mal = codigoFuente.Substring(inicioPos, fin - inicioPos);
                        AgregarError(mal, "Identificador inválido: empieza con dígito", lineaActual, inicioCol);
                        columnaActual += (fin - posicion);
                        posicion = fin;
                        continue;
                    }
                    if (posicion < codigoFuente.Length && codigoFuente[posicion] == '.')
                    {
                        esReal = true;
                        posicion++;
                        columnaActual++;
                        if (posicion < codigoFuente.Length && char.IsDigit(codigoFuente[posicion]))
                        {
                            while (posicion < codigoFuente.Length && char.IsDigit(codigoFuente[posicion]))
                            {
                                posicion++;
                                columnaActual++;
                            }
                        }
                        else
                        {
                            int largo = Math.Min(codigoFuente.Length - inicioPos, posicion - inicioPos + 1);
                            if (largo <= 0) largo = codigoFuente.Length - inicioPos;
                            AgregarError(codigoFuente.Substring(inicioPos, largo), "Número real mal formado", lineaActual, inicioCol);
                            continue;
                        }
                    }
                    string lex = codigoFuente.Substring(inicioPos, posicion - inicioPos);
                    AgregarToken(lex, esReal ? "REAL" : "ENTERO", lineaActual, inicioCol);
                    continue;
                }

                int colAntes = columnaActual;
                string two = posicion + 1 < codigoFuente.Length ? codigoFuente.Substring(posicion, 2) : null;
                switch (two)
                {
                    case "++": AgregarToken("++", "OPERADOR_INCREMENTO", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                    case "--": AgregarToken("--", "OPERADOR_DECREMENTO", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                    case "==": AgregarToken("==", "OPERADOR_IGUALDAD", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                    case "!=": AgregarToken("!=", "OPERADOR_DESIGUALDAD", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                    case "<=": AgregarToken("<=", "OPERADOR_MENOR_IGUAL", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                    case ">=": AgregarToken(">=", "OPERADOR_MAYOR_IGUAL", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                    case "&&": AgregarToken("&&", "OPERADOR_LOGICO_Y", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                    case "||": AgregarToken("||", "OPERADOR_LOGICO_O", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                    case "+=": AgregarToken("+=", "OPERADOR_ASIGNACION_COMPUESTA", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                    case "-=": AgregarToken("-=", "OPERADOR_ASIGNACION_COMPUESTA", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                    case "*=": AgregarToken("*=", "OPERADOR_ASIGNACION_COMPUESTA", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                    case "/=": AgregarToken("/=", "OPERADOR_ASIGNACION_COMPUESTA", lineaActual, colAntes); posicion += 2; columnaActual += 2; continue;
                }

                switch (caracterActual)
                {
                    case '+': AgregarToken("+", "OPERADOR_ARITMETICO", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '-': AgregarToken("-", "OPERADOR_ARITMETICO", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '*': AgregarToken("*", "OPERADOR_ARITMETICO", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '/': AgregarToken("/", "OPERADOR_ARITMETICO", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '%': AgregarToken("%", "OPERADOR_ARITMETICO", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '<': AgregarToken("<", "OPERADOR_RELACIONAL", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '>': AgregarToken(">", "OPERADOR_RELACIONAL", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '!': AgregarToken("!", "OPERADOR_LOGICO", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '=': AgregarToken("=", "OPERADOR_ASIGNACION", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '(': AgregarToken("(", "DELIMITADOR", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case ')': AgregarToken(")", "DELIMITADOR", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '{': AgregarToken("{", "DELIMITADOR", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '}': AgregarToken("}", "DELIMITADOR", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '[': AgregarToken("[", "DELIMITADOR", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case ']': AgregarToken("]", "DELIMITADOR", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case ';': AgregarToken(";", "PUNTO_Y_COMA", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case ',': AgregarToken(",", "COMA", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    case '.': AgregarToken(".", "PUNTO", lineaActual, columnaActual); posicion++; columnaActual++; break;
                    default:
                        AgregarError(caracterActual.ToString(), "Caracter no reconocido", lineaActual, columnaActual);
                        posicion++;
                        columnaActual++;
                        break;
                }
            }
        }

        private char Peek()
        {
            if (posicion + 1 >= codigoFuente.Length) return '\0';
            return codigoFuente[posicion + 1];
        }

        private bool EsLetra(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        private bool EsLetraODigito(char c)
        {
            return EsLetra(c) || char.IsDigit(c);
        }

        private void AgregarToken(string lexema, string tipo, int linea, int columna)
        {
            if (contadorTokens >= tokens.Length) return;
            tokens[contadorTokens++] = new Token(lexema, tipo, linea, columna);
        }

        private void AgregarError(string texto, string descripcion, int linea, int columna)
        {
            if (contadorErrores >= errores.Length) return;
            errores[contadorErrores++] = new ErrorLexico(texto, descripcion, linea, columna);
        }
    }
}
