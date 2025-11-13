namespace NominaMAD
{
    partial class P_ConceptosDP
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_Monto = new System.Windows.Forms.Label();
            this.labelMonto = new System.Windows.Forms.Label();
            this.labelPorcentaje = new System.Windows.Forms.Label();
            this.CmBox_Concepto_ConceptosDP = new System.Windows.Forms.ComboBox();
            this.CmBox_Tipo_ConceptosDP = new System.Windows.Forms.ComboBox();
            this.txt_NombreCon_ConceptosDP = new System.Windows.Forms.TextBox();
            this.txt_Monto_ConceptosDP = new System.Windows.Forms.TextBox();
            this.txt_Porcentaje_ConceptosDP = new System.Windows.Forms.TextBox();
            this.dtgv_ConceptosDP = new System.Windows.Forms.DataGridView();
            this.btn_Agregar_ConceptosDP = new System.Windows.Forms.Button();
            this.btn_Aceptar_ConceptosDP = new System.Windows.Forms.Button();
            this.btn_Cancelar_ConceptosDP = new System.Windows.Forms.Button();
            this.btn_Regresar_ConceptosDP = new System.Windows.Forms.Button();
            this.btn_Modificar_ConceptosDP = new System.Windows.Forms.Button();
            this.btn_Eliminar_ConceptosDP = new System.Windows.Forms.Button();
            this.btn_ModAceptar_ConceptosDP = new System.Windows.Forms.Button();
            this.btn_ModCancelar_ConceptosDP = new System.Windows.Forms.Button();
            this.cadames = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_ConceptosDP)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Concepto:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(190, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tipo:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombre Concepto:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label_Monto
            // 
            this.label_Monto.AutoSize = true;
            this.label_Monto.Location = new System.Drawing.Point(44, 230);
            this.label_Monto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Monto.Name = "label_Monto";
            this.label_Monto.Size = new System.Drawing.Size(0, 13);
            this.label_Monto.TabIndex = 3;
            // 
            // labelMonto
            // 
            this.labelMonto.AutoSize = true;
            this.labelMonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMonto.Location = new System.Drawing.Point(14, 93);
            this.labelMonto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMonto.Name = "labelMonto";
            this.labelMonto.Size = new System.Drawing.Size(46, 13);
            this.labelMonto.TabIndex = 4;
            this.labelMonto.Text = "Monto:";
            this.labelMonto.Click += new System.EventHandler(this.labelMonto_Click);
            // 
            // labelPorcentaje
            // 
            this.labelPorcentaje.AutoSize = true;
            this.labelPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPorcentaje.Location = new System.Drawing.Point(11, 122);
            this.labelPorcentaje.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPorcentaje.Name = "labelPorcentaje";
            this.labelPorcentaje.Size = new System.Drawing.Size(72, 13);
            this.labelPorcentaje.TabIndex = 5;
            this.labelPorcentaje.Text = "Porcentaje:";
            this.labelPorcentaje.Click += new System.EventHandler(this.labelPorcentaje_Click);
            // 
            // CmBox_Concepto_ConceptosDP
            // 
            this.CmBox_Concepto_ConceptosDP.AllowDrop = true;
            this.CmBox_Concepto_ConceptosDP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmBox_Concepto_ConceptosDP.FormattingEnabled = true;
            this.CmBox_Concepto_ConceptosDP.Location = new System.Drawing.Point(87, 20);
            this.CmBox_Concepto_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CmBox_Concepto_ConceptosDP.Name = "CmBox_Concepto_ConceptosDP";
            this.CmBox_Concepto_ConceptosDP.Size = new System.Drawing.Size(84, 21);
            this.CmBox_Concepto_ConceptosDP.TabIndex = 6;
            this.CmBox_Concepto_ConceptosDP.SelectedIndexChanged += new System.EventHandler(this.CmBox_Concepto_ConceptosDP_SelectedIndexChanged);
            // 
            // CmBox_Tipo_ConceptosDP
            // 
            this.CmBox_Tipo_ConceptosDP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmBox_Tipo_ConceptosDP.FormattingEnabled = true;
            this.CmBox_Tipo_ConceptosDP.Location = new System.Drawing.Point(227, 24);
            this.CmBox_Tipo_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CmBox_Tipo_ConceptosDP.Name = "CmBox_Tipo_ConceptosDP";
            this.CmBox_Tipo_ConceptosDP.Size = new System.Drawing.Size(76, 21);
            this.CmBox_Tipo_ConceptosDP.TabIndex = 7;
            this.CmBox_Tipo_ConceptosDP.SelectedIndexChanged += new System.EventHandler(this.CmBox_Tipo_ConceptosDP_SelectedIndexChanged);
            // 
            // txt_NombreCon_ConceptosDP
            // 
            this.txt_NombreCon_ConceptosDP.Location = new System.Drawing.Point(118, 58);
            this.txt_NombreCon_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_NombreCon_ConceptosDP.Name = "txt_NombreCon_ConceptosDP";
            this.txt_NombreCon_ConceptosDP.Size = new System.Drawing.Size(106, 20);
            this.txt_NombreCon_ConceptosDP.TabIndex = 8;
            this.txt_NombreCon_ConceptosDP.TextChanged += new System.EventHandler(this.txt_NombreCon_ConceptosDP_TextChanged);
            // 
            // txt_Monto_ConceptosDP
            // 
            this.txt_Monto_ConceptosDP.Location = new System.Drawing.Point(82, 90);
            this.txt_Monto_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Monto_ConceptosDP.Name = "txt_Monto_ConceptosDP";
            this.txt_Monto_ConceptosDP.Size = new System.Drawing.Size(88, 20);
            this.txt_Monto_ConceptosDP.TabIndex = 9;
            this.txt_Monto_ConceptosDP.TextChanged += new System.EventHandler(this.txt_Monto_ConceptosDP_TextChanged);
            // 
            // txt_Porcentaje_ConceptosDP
            // 
            this.txt_Porcentaje_ConceptosDP.Location = new System.Drawing.Point(94, 117);
            this.txt_Porcentaje_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Porcentaje_ConceptosDP.Name = "txt_Porcentaje_ConceptosDP";
            this.txt_Porcentaje_ConceptosDP.Size = new System.Drawing.Size(77, 20);
            this.txt_Porcentaje_ConceptosDP.TabIndex = 10;
            this.txt_Porcentaje_ConceptosDP.TextChanged += new System.EventHandler(this.txt_Porcentaje_ConceptosDP_TextChanged);
            // 
            // dtgv_ConceptosDP
            // 
            this.dtgv_ConceptosDP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_ConceptosDP.Location = new System.Drawing.Point(336, 20);
            this.dtgv_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtgv_ConceptosDP.Name = "dtgv_ConceptosDP";
            this.dtgv_ConceptosDP.ReadOnly = true;
            this.dtgv_ConceptosDP.RowHeadersWidth = 51;
            this.dtgv_ConceptosDP.RowTemplate.Height = 24;
            this.dtgv_ConceptosDP.Size = new System.Drawing.Size(333, 254);
            this.dtgv_ConceptosDP.TabIndex = 11;
            this.dtgv_ConceptosDP.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgv_ConceptosDP_CellClick);
            this.dtgv_ConceptosDP.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgv_ConceptosDP_CellContentClick);
            // 
            // btn_Agregar_ConceptosDP
            // 
            this.btn_Agregar_ConceptosDP.Location = new System.Drawing.Point(205, 90);
            this.btn_Agregar_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Agregar_ConceptosDP.Name = "btn_Agregar_ConceptosDP";
            this.btn_Agregar_ConceptosDP.Size = new System.Drawing.Size(64, 27);
            this.btn_Agregar_ConceptosDP.TabIndex = 12;
            this.btn_Agregar_ConceptosDP.Text = "Agregar";
            this.btn_Agregar_ConceptosDP.UseVisualStyleBackColor = true;
            this.btn_Agregar_ConceptosDP.Click += new System.EventHandler(this.btn_Agregar_ConceptosDP_Click);
            // 
            // btn_Aceptar_ConceptosDP
            // 
            this.btn_Aceptar_ConceptosDP.Location = new System.Drawing.Point(9, 168);
            this.btn_Aceptar_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Aceptar_ConceptosDP.Name = "btn_Aceptar_ConceptosDP";
            this.btn_Aceptar_ConceptosDP.Size = new System.Drawing.Size(72, 19);
            this.btn_Aceptar_ConceptosDP.TabIndex = 13;
            this.btn_Aceptar_ConceptosDP.Text = "Aceptar";
            this.btn_Aceptar_ConceptosDP.UseVisualStyleBackColor = true;
            this.btn_Aceptar_ConceptosDP.Click += new System.EventHandler(this.btn_Aceptar_ConceptosDP_Click);
            // 
            // btn_Cancelar_ConceptosDP
            // 
            this.btn_Cancelar_ConceptosDP.Location = new System.Drawing.Point(107, 168);
            this.btn_Cancelar_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Cancelar_ConceptosDP.Name = "btn_Cancelar_ConceptosDP";
            this.btn_Cancelar_ConceptosDP.Size = new System.Drawing.Size(81, 19);
            this.btn_Cancelar_ConceptosDP.TabIndex = 14;
            this.btn_Cancelar_ConceptosDP.Text = "Cancelar";
            this.btn_Cancelar_ConceptosDP.UseVisualStyleBackColor = true;
            this.btn_Cancelar_ConceptosDP.Click += new System.EventHandler(this.btn_Cancelar_ConceptosDP_Click);
            // 
            // btn_Regresar_ConceptosDP
            // 
            this.btn_Regresar_ConceptosDP.Location = new System.Drawing.Point(336, 300);
            this.btn_Regresar_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Regresar_ConceptosDP.Name = "btn_Regresar_ConceptosDP";
            this.btn_Regresar_ConceptosDP.Size = new System.Drawing.Size(64, 27);
            this.btn_Regresar_ConceptosDP.TabIndex = 15;
            this.btn_Regresar_ConceptosDP.Text = "Regresar";
            this.btn_Regresar_ConceptosDP.UseVisualStyleBackColor = true;
            this.btn_Regresar_ConceptosDP.Click += new System.EventHandler(this.btn_Regresar_ConceptosDP_Click);
            // 
            // btn_Modificar_ConceptosDP
            // 
            this.btn_Modificar_ConceptosDP.Location = new System.Drawing.Point(65, 203);
            this.btn_Modificar_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Modificar_ConceptosDP.Name = "btn_Modificar_ConceptosDP";
            this.btn_Modificar_ConceptosDP.Size = new System.Drawing.Size(68, 23);
            this.btn_Modificar_ConceptosDP.TabIndex = 16;
            this.btn_Modificar_ConceptosDP.Text = "Modificar";
            this.btn_Modificar_ConceptosDP.UseVisualStyleBackColor = true;
            this.btn_Modificar_ConceptosDP.Click += new System.EventHandler(this.btn_Modificar_ConceptosDP_Click);
            // 
            // btn_Eliminar_ConceptosDP
            // 
            this.btn_Eliminar_ConceptosDP.Location = new System.Drawing.Point(161, 203);
            this.btn_Eliminar_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Eliminar_ConceptosDP.Name = "btn_Eliminar_ConceptosDP";
            this.btn_Eliminar_ConceptosDP.Size = new System.Drawing.Size(68, 23);
            this.btn_Eliminar_ConceptosDP.TabIndex = 17;
            this.btn_Eliminar_ConceptosDP.Text = "Eliminar";
            this.btn_Eliminar_ConceptosDP.UseVisualStyleBackColor = true;
            this.btn_Eliminar_ConceptosDP.Click += new System.EventHandler(this.btn_Eliminar_ConceptosDP_Click);
            // 
            // btn_ModAceptar_ConceptosDP
            // 
            this.btn_ModAceptar_ConceptosDP.Location = new System.Drawing.Point(18, 251);
            this.btn_ModAceptar_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_ModAceptar_ConceptosDP.Name = "btn_ModAceptar_ConceptosDP";
            this.btn_ModAceptar_ConceptosDP.Size = new System.Drawing.Size(59, 20);
            this.btn_ModAceptar_ConceptosDP.TabIndex = 18;
            this.btn_ModAceptar_ConceptosDP.Text = "Aceptar";
            this.btn_ModAceptar_ConceptosDP.UseVisualStyleBackColor = true;
            this.btn_ModAceptar_ConceptosDP.Click += new System.EventHandler(this.btn_ModAceptar_ConceptosDP_Click);
            // 
            // btn_ModCancelar_ConceptosDP
            // 
            this.btn_ModCancelar_ConceptosDP.Location = new System.Drawing.Point(82, 251);
            this.btn_ModCancelar_ConceptosDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_ModCancelar_ConceptosDP.Name = "btn_ModCancelar_ConceptosDP";
            this.btn_ModCancelar_ConceptosDP.Size = new System.Drawing.Size(64, 20);
            this.btn_ModCancelar_ConceptosDP.TabIndex = 19;
            this.btn_ModCancelar_ConceptosDP.Text = "Cancelar";
            this.btn_ModCancelar_ConceptosDP.UseVisualStyleBackColor = true;
            this.btn_ModCancelar_ConceptosDP.Click += new System.EventHandler(this.btn_ModCancelar_ConceptosDP_Click);
            // 
            // cadames
            // 
            this.cadames.AutoSize = true;
            this.cadames.Location = new System.Drawing.Point(227, 144);
            this.cadames.Name = "cadames";
            this.cadames.Size = new System.Drawing.Size(73, 17);
            this.cadames.TabIndex = 20;
            this.cadames.Text = "Cada mes";
            this.cadames.UseVisualStyleBackColor = true;
            // 
            // P_ConceptosDP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 350);
            this.ControlBox = false;
            this.Controls.Add(this.cadames);
            this.Controls.Add(this.btn_ModCancelar_ConceptosDP);
            this.Controls.Add(this.btn_ModAceptar_ConceptosDP);
            this.Controls.Add(this.btn_Eliminar_ConceptosDP);
            this.Controls.Add(this.btn_Modificar_ConceptosDP);
            this.Controls.Add(this.btn_Regresar_ConceptosDP);
            this.Controls.Add(this.btn_Cancelar_ConceptosDP);
            this.Controls.Add(this.btn_Aceptar_ConceptosDP);
            this.Controls.Add(this.btn_Agregar_ConceptosDP);
            this.Controls.Add(this.dtgv_ConceptosDP);
            this.Controls.Add(this.txt_Porcentaje_ConceptosDP);
            this.Controls.Add(this.txt_Monto_ConceptosDP);
            this.Controls.Add(this.txt_NombreCon_ConceptosDP);
            this.Controls.Add(this.CmBox_Tipo_ConceptosDP);
            this.Controls.Add(this.CmBox_Concepto_ConceptosDP);
            this.Controls.Add(this.labelPorcentaje);
            this.Controls.Add(this.labelMonto);
            this.Controls.Add(this.label_Monto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "P_ConceptosDP";
            this.Text = "Conceptos Deducciones Percepciones";
            this.Load += new System.EventHandler(this.P_ConceptosDP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_ConceptosDP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_Monto;
        private System.Windows.Forms.Label labelMonto;
        private System.Windows.Forms.Label labelPorcentaje;
        private System.Windows.Forms.ComboBox CmBox_Concepto_ConceptosDP;
        private System.Windows.Forms.ComboBox CmBox_Tipo_ConceptosDP;
        private System.Windows.Forms.TextBox txt_NombreCon_ConceptosDP;
        private System.Windows.Forms.TextBox txt_Monto_ConceptosDP;
        private System.Windows.Forms.TextBox txt_Porcentaje_ConceptosDP;
        private System.Windows.Forms.DataGridView dtgv_ConceptosDP;
        private System.Windows.Forms.Button btn_Agregar_ConceptosDP;
        private System.Windows.Forms.Button btn_Aceptar_ConceptosDP;
        private System.Windows.Forms.Button btn_Cancelar_ConceptosDP;
        private System.Windows.Forms.Button btn_Regresar_ConceptosDP;
        private System.Windows.Forms.Button btn_Modificar_ConceptosDP;
        private System.Windows.Forms.Button btn_Eliminar_ConceptosDP;
        private System.Windows.Forms.Button btn_ModAceptar_ConceptosDP;
        private System.Windows.Forms.Button btn_ModCancelar_ConceptosDP;
        private System.Windows.Forms.CheckBox cadames;
    }
}