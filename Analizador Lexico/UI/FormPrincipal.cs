using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalizadorLexico.Engine;

namespace AnalizadorLexico.UI
{
    public partial class FormPrincipal : Form
    {
        private Engine.AnalizadorLexico motorLexico;

        public FormPrincipal()
        {
            InitializeComponent();
            // inicializar grids inmediatamente para evitar que falten columnas
            InicializarGrids();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            // inicializar tablas de datos
            InicializarGrids();
        }

        private void InicializarGrids()
        {
            // Tokens grid
            dgvTokens.Columns.Clear();
            dgvTokens.Columns.Add("Linea", "Linea");
            dgvTokens.Columns.Add("Columna", "Columna");
            dgvTokens.Columns.Add("Tipo", "Tipo");
            dgvTokens.Columns.Add("Lexema", "Lexema");

            // Errores grid
            dgvErrores.Columns.Clear();
            dgvErrores.Columns.Add("Linea", "Linea");
            dgvErrores.Columns.Add("Columna", "Columna");
            dgvErrores.Columns.Add("Descripcion", "Descripcion");
            dgvErrores.Columns.Add("Texto", "Texto");

            // Simbolos grid
            dgvSimbolos.Columns.Clear();
            dgvSimbolos.Columns.Add("Id", "Id");
            dgvSimbolos.Columns.Add("Nombre", "Nombre");
            dgvSimbolos.Columns.Add("TipoToken", "Tipo");
            dgvSimbolos.Columns.Add("Linea", "Linea");
            dgvSimbolos.Columns.Add("Columna", "Columna");
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ruta = openFileDialog1.FileName;
                try
                {
                    txtCodigo.Text = System.IO.File.ReadAllText(ruta);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al leer archivo: " + ex.Message);
                }
            }
        }

        private void btnEscanear_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = txtCodigo.Text ?? string.Empty;
                motorLexico = new Engine.AnalizadorLexico(codigo);
                motorLexico.Escanear();

                // Mostrar tokens
                dgvTokens.Rows.Clear();
                var tokens = motorLexico.ObtenerTokens();
                foreach (var t in tokens)
                {
                    dgvTokens.Rows.Add(t.Linea, t.Columna, t.Tipo, t.Lexema);
                }

                // Mostrar errores
                dgvErrores.Rows.Clear();
                var errores = motorLexico.ObtenerErrores();
                foreach (var err in errores)
                {
                    dgvErrores.Rows.Add(err.Linea, err.Columna, err.Descripcion, err.CaracterOTexto);
                }

            // Mostrar tabla de símbolos
            dgvSimbolos.Rows.Clear();
            var simbolos = motorLexico.TablaSimbolos.ObtenerSimbolos();
            foreach (var s in simbolos)
            {
                dgvSimbolos.Rows.Add(s.Id, s.Nombre, s.TipoToken, s.PrimeraLinea, s.PrimeraColumna);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error durante el análisis: " + ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
