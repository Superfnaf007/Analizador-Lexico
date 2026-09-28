namespace AnalizadorLexico.UI
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.Button btnEscanear;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.DataGridView dgvTokens;
        private System.Windows.Forms.DataGridView dgvErrores;
        private System.Windows.Forms.DataGridView dgvSimbolos;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.btnEscanear = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.dgvTokens = new System.Windows.Forms.DataGridView();
            this.dgvErrores = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTokens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(12, 12);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(100, 30);
            this.btnAbrir.TabIndex = 0;
            this.btnAbrir.Text = "Abrir archivo";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // btnEscanear
            // 
            this.btnEscanear.Location = new System.Drawing.Point(118, 12);
            this.btnEscanear.Name = "btnEscanear";
            this.btnEscanear.Size = new System.Drawing.Size(100, 30);
            this.btnEscanear.TabIndex = 1;
            this.btnEscanear.Text = "Escanear";
            this.btnEscanear.UseVisualStyleBackColor = true;
            this.btnEscanear.Click += new System.EventHandler(this.btnEscanear_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(12, 48);
            this.txtCodigo.Multiline = true;
            this.txtCodigo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCodigo.WordWrap = false;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(700, 550);
            this.txtCodigo.TabIndex = 2;
            // 
            // dgvTokens
            // 
            this.dgvTokens.AllowUserToAddRows = false;
            this.dgvTokens.AllowUserToDeleteRows = false;
            this.dgvTokens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTokens.Location = new System.Drawing.Point(718, 48);
            this.dgvTokens.Name = "dgvTokens";
            this.dgvTokens.ReadOnly = true;
            this.dgvTokens.Size = new System.Drawing.Size(430, 300);
            this.dgvTokens.TabIndex = 3;
            // 
            // dgvSimbolos
            // 
            this.dgvSimbolos = new System.Windows.Forms.DataGridView();
            this.dgvSimbolos.AllowUserToAddRows = false;
            this.dgvSimbolos.AllowUserToDeleteRows = false;
            this.dgvSimbolos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSimbolos.Location = new System.Drawing.Point(1158, 48);
            this.dgvSimbolos.Name = "dgvSimbolos";
            this.dgvSimbolos.ReadOnly = true;
            this.dgvSimbolos.Size = new System.Drawing.Size(210, 300);
            this.dgvSimbolos.TabIndex = 4;

            // dgvErrores
            // 
            this.dgvErrores.AllowUserToAddRows = false;
            this.dgvErrores.AllowUserToDeleteRows = false;
            this.dgvErrores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErrores.Location = new System.Drawing.Point(718, 358);
            this.dgvErrores.Name = "dgvErrores";
            this.dgvErrores.ReadOnly = true;
            this.dgvErrores.Size = new System.Drawing.Size(650, 240);
            this.dgvErrores.TabIndex = 5;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Archivos de código|*.txt;*.cs|Todos los archivos|*.*";
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 639);
            this.Controls.Add(this.dgvErrores);
            this.Controls.Add(this.dgvSimbolos);
            this.Controls.Add(this.dgvTokens);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.btnEscanear);
            this.Controls.Add(this.btnAbrir);
            this.Name = "FormPrincipal";
            this.Text = "Analizador Léxico";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTokens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}

