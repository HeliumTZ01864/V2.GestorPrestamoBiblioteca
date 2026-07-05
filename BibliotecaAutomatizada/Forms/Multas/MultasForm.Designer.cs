namespace BibliotecaAutomatizada.Forms.Multas
{
    partial class MultasForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvMultas = new System.Windows.Forms.DataGridView();
            this.lblPrestamoDetalleId = new System.Windows.Forms.Label();
            this.txtPrestamoDetalleId = new System.Windows.Forms.TextBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.chkPagada = new System.Windows.Forms.CheckBox();
            this.BtRegistrar = new System.Windows.Forms.Button();
            this.BtPagar = new System.Windows.Forms.Button();
            this.BtEliminar = new System.Windows.Forms.Button();
            this.BtLimpiar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMultas
            // 
            this.dgvMultas.AllowUserToAddRows = false;
            this.dgvMultas.AllowUserToDeleteRows = false;
            this.dgvMultas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMultas.Location = new System.Drawing.Point(12, 185);
            this.dgvMultas.Name = "dgvMultas";
            this.dgvMultas.ReadOnly = true;
            this.dgvMultas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMultas.Size = new System.Drawing.Size(760, 260);
            this.dgvMultas.TabIndex = 10;
            this.dgvMultas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMultas_CellContentClick);
            // 
            // lblPrestamoDetalleId
            // 
            this.lblPrestamoDetalleId.AutoSize = true;
            this.lblPrestamoDetalleId.Location = new System.Drawing.Point(12, 50);
            this.lblPrestamoDetalleId.Name = "lblPrestamoDetalleId";
            this.lblPrestamoDetalleId.Size = new System.Drawing.Size(104, 13);
            this.lblPrestamoDetalleId.TabIndex = 1;
            this.lblPrestamoDetalleId.Text = "ID Préstamo Detalle:";
            // 
            // txtPrestamoDetalleId
            // 
            this.txtPrestamoDetalleId.Location = new System.Drawing.Point(160, 47);
            this.txtPrestamoDetalleId.Name = "txtPrestamoDetalleId";
            this.txtPrestamoDetalleId.Size = new System.Drawing.Size(120, 20);
            this.txtPrestamoDetalleId.TabIndex = 2;
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new System.Drawing.Point(12, 80);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(40, 13);
            this.lblMonto.TabIndex = 3;
            this.lblMonto.Text = "Monto:";
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(160, 77);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(120, 20);
            this.txtMonto.TabIndex = 4;
            // 
            // chkPagada
            // 
            this.chkPagada.AutoSize = true;
            this.chkPagada.Location = new System.Drawing.Point(160, 107);
            this.chkPagada.Name = "chkPagada";
            this.chkPagada.Size = new System.Drawing.Size(63, 17);
            this.chkPagada.TabIndex = 5;
            this.chkPagada.Text = "Pagada";
            // 
            // BtRegistrar
            // 
            this.BtRegistrar.Location = new System.Drawing.Point(12, 140);
            this.BtRegistrar.Name = "BtRegistrar";
            this.BtRegistrar.Size = new System.Drawing.Size(85, 30);
            this.BtRegistrar.TabIndex = 6;
            this.BtRegistrar.Text = "Registrar";
            this.BtRegistrar.UseVisualStyleBackColor = true;
            // 
            // BtPagar
            // 
            this.BtPagar.Location = new System.Drawing.Point(105, 140);
            this.BtPagar.Name = "BtPagar";
            this.BtPagar.Size = new System.Drawing.Size(85, 30);
            this.BtPagar.TabIndex = 7;
            this.BtPagar.Text = "Marcar Pagada";
            this.BtPagar.UseVisualStyleBackColor = true;
            // 
            // BtEliminar
            // 
            this.BtEliminar.Location = new System.Drawing.Point(198, 140);
            this.BtEliminar.Name = "BtEliminar";
            this.BtEliminar.Size = new System.Drawing.Size(85, 30);
            this.BtEliminar.TabIndex = 8;
            this.BtEliminar.Text = "Eliminar";
            this.BtEliminar.UseVisualStyleBackColor = true;
            // 
            // BtLimpiar
            // 
            this.BtLimpiar.Location = new System.Drawing.Point(291, 140);
            this.BtLimpiar.Name = "BtLimpiar";
            this.BtLimpiar.Size = new System.Drawing.Size(85, 30);
            this.BtLimpiar.TabIndex = 9;
            this.BtLimpiar.Text = "Limpiar";
            this.BtLimpiar.UseVisualStyleBackColor = true;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(177, 24);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de Multas";
            // 
            // MultasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 470);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblPrestamoDetalleId);
            this.Controls.Add(this.txtPrestamoDetalleId);
            this.Controls.Add(this.lblMonto);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.chkPagada);
            this.Controls.Add(this.BtRegistrar);
            this.Controls.Add(this.BtPagar);
            this.Controls.Add(this.BtEliminar);
            this.Controls.Add(this.BtLimpiar);
            this.Controls.Add(this.dgvMultas);
            this.Name = "MultasForm";
            this.Text = "Módulo de Multas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dgvMultas;
        private System.Windows.Forms.Label lblPrestamoDetalleId;
        private System.Windows.Forms.TextBox txtPrestamoDetalleId;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.CheckBox chkPagada;
        private System.Windows.Forms.Button BtRegistrar;
        private System.Windows.Forms.Button BtPagar;
        private System.Windows.Forms.Button BtEliminar;
        private System.Windows.Forms.Button BtLimpiar;
        private System.Windows.Forms.Label lblTitulo;
    }
}
