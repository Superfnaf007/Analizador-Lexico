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
            InicializarGrids();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            InicializarGrids();
        }

        private void InicializarGrids()
        {
            dgvTokens.Columns.Clear();
            dgvTokens.Columns.Add("Linea", "Linea");
            dgvTokens.Columns.Add("Columna", "Columna");
            dgvTokens.Columns.Add("Tipo", "Tipo");
            dgvTokens.Columns.Add("Lexema", "Lexema");

            dgvErrores.Columns.Clear();
            dgvErrores.Columns.Add("Linea", "Linea");
            dgvErrores.Columns.Add("Columna", "Columna");
            dgvErrores.Columns.Add("Descripcion", "Descripcion");
            dgvErrores.Columns.Add("Texto", "Texto");

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

                dgvTokens.Rows.Clear();
                var tokens = motorLexico.ObtenerTokens();
                foreach (var t in tokens)
                {
                    dgvTokens.Rows.Add(t.Linea, t.Columna, t.Tipo, t.Lexema);
                }

                dgvErrores.Rows.Clear();
                var errores = motorLexico.ObtenerErrores();
                foreach (var err in errores)
                {
                    dgvErrores.Rows.Add(err.Linea, err.Columna, err.Descripcion, err.CaracterOTexto);
                }

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
