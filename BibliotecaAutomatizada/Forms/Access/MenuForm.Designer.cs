namespace BibliotecaAutomatizada.Forms.Access
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtLibros = new System.Windows.Forms.Button();
            this.BtPrestamos = new System.Windows.Forms.Button();
            this.BtSalir = new System.Windows.Forms.Button();
            this.BtMultas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtLibros
            // 
            this.BtLibros.Location = new System.Drawing.Point(155, 76);
            this.BtLibros.Name = "BtLibros";
            this.BtLibros.Size = new System.Drawing.Size(75, 23);
            this.BtLibros.TabIndex = 0;
            this.BtLibros.Text = "Libros";
            this.BtLibros.UseVisualStyleBackColor = true;
            this.BtLibros.Click += new System.EventHandler(this.BtLibros_Click);
            // 
            // BtPrestamos
            // 
            this.BtPrestamos.Location = new System.Drawing.Point(155, 123);
            this.BtPrestamos.Name = "BtPrestamos";
            this.BtPrestamos.Size = new System.Drawing.Size(75, 23);
            this.BtPrestamos.TabIndex = 1;
            this.BtPrestamos.Text = "Prestamos";
            this.BtPrestamos.UseVisualStyleBackColor = true;
            this.BtPrestamos.Click += new System.EventHandler(this.BtPrestamos_Click);
            // 
            // BtSalir
            // 
            this.BtSalir.Location = new System.Drawing.Point(331, 273);
            this.BtSalir.Name = "BtSalir";
            this.BtSalir.Size = new System.Drawing.Size(75, 23);
            this.BtSalir.TabIndex = 2;
            this.BtSalir.Text = "Salir";
            this.BtSalir.UseVisualStyleBackColor = true;
            this.BtSalir.Click += new System.EventHandler(this.BtSalir_Click);
            // 
            // BtMultas
            // 
            this.BtMultas.Location = new System.Drawing.Point(155, 172);
            this.BtMultas.Name = "BtMultas";
            this.BtMultas.Size = new System.Drawing.Size(75, 23);
            this.BtMultas.TabIndex = 3;
            this.BtMultas.Text = "Multas";
            this.BtMultas.UseVisualStyleBackColor = true;
            this.BtMultas.Click += new System.EventHandler(this.BtMultas_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 308);
            this.Controls.Add(this.BtMultas);
            this.Controls.Add(this.BtSalir);
            this.Controls.Add(this.BtPrestamos);
            this.Controls.Add(this.BtLibros);
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtLibros;
        private System.Windows.Forms.Button BtPrestamos;
        private System.Windows.Forms.Button BtSalir;
        private System.Windows.Forms.Button BtMultas;
    }
}