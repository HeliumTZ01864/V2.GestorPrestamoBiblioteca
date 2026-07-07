namespace BibliotecaAutomatizada.Forms.Menus
{
    partial class MenuClienteForm
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
            this.pnlSidebarEmpleado = new System.Windows.Forms.Panel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnCobrarMultas = new System.Windows.Forms.Button();
            this.btnVerLibros = new System.Windows.Forms.Button();
            this.btnNuevaReserva = new System.Windows.Forms.Button();
            this.pnlContenedorEmpleado = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSidebarEmpleado.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebarEmpleado
            // 
            this.pnlSidebarEmpleado.Controls.Add(this.btnCerrarSesion);
            this.pnlSidebarEmpleado.Controls.Add(this.btnCobrarMultas);
            this.pnlSidebarEmpleado.Controls.Add(this.btnVerLibros);
            this.pnlSidebarEmpleado.Controls.Add(this.btnNuevaReserva);
            this.pnlSidebarEmpleado.Location = new System.Drawing.Point(10, 108);
            this.pnlSidebarEmpleado.Name = "pnlSidebarEmpleado";
            this.pnlSidebarEmpleado.Size = new System.Drawing.Size(250, 548);
            this.pnlSidebarEmpleado.TabIndex = 0;
            this.pnlSidebarEmpleado.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSidebarEmpleado_Paint);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(43, 483);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(108, 23);
            this.btnCerrarSesion.TabIndex = 3;
            this.btnCerrarSesion.Text = "Cerrar Sesion";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // btnCobrarMultas
            // 
            this.btnCobrarMultas.Location = new System.Drawing.Point(43, 321);
            this.btnCobrarMultas.Name = "btnCobrarMultas";
            this.btnCobrarMultas.Size = new System.Drawing.Size(108, 41);
            this.btnCobrarMultas.TabIndex = 2;
            this.btnCobrarMultas.Text = "Cobranza de Multas";
            this.btnCobrarMultas.UseVisualStyleBackColor = true;
            this.btnCobrarMultas.Click += new System.EventHandler(this.btnCobrarMultas_Click);
            // 
            // btnVerLibros
            // 
            this.btnVerLibros.Location = new System.Drawing.Point(43, 256);
            this.btnVerLibros.Name = "btnVerLibros";
            this.btnVerLibros.Size = new System.Drawing.Size(108, 43);
            this.btnVerLibros.TabIndex = 1;
            this.btnVerLibros.Text = "Consultar Catalogo";
            this.btnVerLibros.UseVisualStyleBackColor = true;
            this.btnVerLibros.Click += new System.EventHandler(this.btnVerLibros_Click);
            // 
            // btnNuevaReserva
            // 
            this.btnNuevaReserva.Location = new System.Drawing.Point(43, 197);
            this.btnNuevaReserva.Name = "btnNuevaReserva";
            this.btnNuevaReserva.Size = new System.Drawing.Size(108, 41);
            this.btnNuevaReserva.TabIndex = 0;
            this.btnNuevaReserva.Text = "Registrar Prestamo";
            this.btnNuevaReserva.UseVisualStyleBackColor = true;
            this.btnNuevaReserva.Click += new System.EventHandler(this.btnNuevaReserva_Click);
            // 
            // pnlContenedorEmpleado
            // 
            this.pnlContenedorEmpleado.Location = new System.Drawing.Point(266, 108);
            this.pnlContenedorEmpleado.Name = "pnlContenedorEmpleado";
            this.pnlContenedorEmpleado.Size = new System.Drawing.Size(806, 548);
            this.pnlContenedorEmpleado.TabIndex = 1;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Location = new System.Drawing.Point(10, 42);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1065, 60);
            this.pnlHeader.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(403, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "SISTEMA DE BIBLIOTECA - ROL CLIENTE";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // MenuEmpleadoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 661);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlContenedorEmpleado);
            this.Controls.Add(this.pnlSidebarEmpleado);
            this.Name = "MenuEmpleadoForm";
            this.Text = "MenuEmpleadoForm";
            this.Load += new System.EventHandler(this.MenuEmpleadoForm_Load);
            this.pnlSidebarEmpleado.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebarEmpleado;
        private System.Windows.Forms.Panel pnlContenedorEmpleado;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button btnCobrarMultas;
        private System.Windows.Forms.Button btnVerLibros;
        private System.Windows.Forms.Button btnNuevaReserva;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
    }
}