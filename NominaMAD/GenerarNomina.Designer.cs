namespace NominaMAD
{
    partial class P_GenerarNomina
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
            this.btn_Regresar_GenerarNomina = new System.Windows.Forms.Button();
            this.btn_Eliminar_GenerarNomina = new System.Windows.Forms.Button();
            this.btn_GenerarNominaInd_GenerarNomina = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_CierrePeriodo_GenerarNomina = new System.Windows.Forms.Button();
            this.dtgv_Matriz_GenerarNomina = new System.Windows.Forms.DataGridView();
            this.DateTime_Periodo = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Matriz_GenerarNomina)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Regresar_GenerarNomina
            // 
            this.btn_Regresar_GenerarNomina.Location = new System.Drawing.Point(953, 542);
            this.btn_Regresar_GenerarNomina.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Regresar_GenerarNomina.Name = "btn_Regresar_GenerarNomina";
            this.btn_Regresar_GenerarNomina.Size = new System.Drawing.Size(96, 33);
            this.btn_Regresar_GenerarNomina.TabIndex = 2;
            this.btn_Regresar_GenerarNomina.Text = "Regresar";
            this.btn_Regresar_GenerarNomina.UseVisualStyleBackColor = true;
            this.btn_Regresar_GenerarNomina.Click += new System.EventHandler(this.btn_Regresar_GenerarNomina_Click);
            // 
            // btn_Eliminar_GenerarNomina
            // 
            this.btn_Eliminar_GenerarNomina.Location = new System.Drawing.Point(139, 693);
            this.btn_Eliminar_GenerarNomina.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Eliminar_GenerarNomina.Name = "btn_Eliminar_GenerarNomina";
            this.btn_Eliminar_GenerarNomina.Size = new System.Drawing.Size(94, 27);
            this.btn_Eliminar_GenerarNomina.TabIndex = 5;
            this.btn_Eliminar_GenerarNomina.Text = "Eliminar";
            this.btn_Eliminar_GenerarNomina.UseVisualStyleBackColor = true;
            // 
            // btn_GenerarNominaInd_GenerarNomina
            // 
            this.btn_GenerarNominaInd_GenerarNomina.Location = new System.Drawing.Point(564, 508);
            this.btn_GenerarNominaInd_GenerarNomina.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GenerarNominaInd_GenerarNomina.Name = "btn_GenerarNominaInd_GenerarNomina";
            this.btn_GenerarNominaInd_GenerarNomina.Size = new System.Drawing.Size(116, 54);
            this.btn_GenerarNominaInd_GenerarNomina.TabIndex = 6;
            this.btn_GenerarNominaInd_GenerarNomina.Text = "Generar Nomina";
            this.btn_GenerarNominaInd_GenerarNomina.UseVisualStyleBackColor = true;
            this.btn_GenerarNominaInd_GenerarNomina.Click += new System.EventHandler(this.btn_GenerarNominaInd_GenerarNomina_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 494);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Periodo Actual";
            // 
            // btn_CierrePeriodo_GenerarNomina
            // 
            this.btn_CierrePeriodo_GenerarNomina.Location = new System.Drawing.Point(433, 508);
            this.btn_CierrePeriodo_GenerarNomina.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CierrePeriodo_GenerarNomina.Name = "btn_CierrePeriodo_GenerarNomina";
            this.btn_CierrePeriodo_GenerarNomina.Size = new System.Drawing.Size(106, 54);
            this.btn_CierrePeriodo_GenerarNomina.TabIndex = 10;
            this.btn_CierrePeriodo_GenerarNomina.Text = "Cierra De Periodo";
            this.btn_CierrePeriodo_GenerarNomina.UseVisualStyleBackColor = true;
            this.btn_CierrePeriodo_GenerarNomina.Click += new System.EventHandler(this.btn_CierrePeriodo_GenerarNomina_Click);
            // 
            // dtgv_Matriz_GenerarNomina
            // 
            this.dtgv_Matriz_GenerarNomina.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_Matriz_GenerarNomina.Location = new System.Drawing.Point(11, 28);
            this.dtgv_Matriz_GenerarNomina.Margin = new System.Windows.Forms.Padding(2);
            this.dtgv_Matriz_GenerarNomina.Name = "dtgv_Matriz_GenerarNomina";
            this.dtgv_Matriz_GenerarNomina.RowHeadersWidth = 51;
            this.dtgv_Matriz_GenerarNomina.RowTemplate.Height = 24;
            this.dtgv_Matriz_GenerarNomina.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgv_Matriz_GenerarNomina.Size = new System.Drawing.Size(1047, 381);
            this.dtgv_Matriz_GenerarNomina.TabIndex = 11;
            this.dtgv_Matriz_GenerarNomina.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgv_Matriz_GenerarNomina_CellContentClick);
            // 
            // DateTime_Periodo
            // 
            this.DateTime_Periodo.Location = new System.Drawing.Point(127, 524);
            this.DateTime_Periodo.Name = "DateTime_Periodo";
            this.DateTime_Periodo.Size = new System.Drawing.Size(200, 20);
            this.DateTime_Periodo.TabIndex = 13;
            this.DateTime_Periodo.ValueChanged += new System.EventHandler(this.DateTime_Periodo_ValueChanged);
            // 
            // P_GenerarNomina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 596);
            this.ControlBox = false;
            this.Controls.Add(this.DateTime_Periodo);
            this.Controls.Add(this.dtgv_Matriz_GenerarNomina);
            this.Controls.Add(this.btn_CierrePeriodo_GenerarNomina);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_GenerarNominaInd_GenerarNomina);
            this.Controls.Add(this.btn_Eliminar_GenerarNomina);
            this.Controls.Add(this.btn_Regresar_GenerarNomina);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "P_GenerarNomina";
            this.Text = "Generar Nomina";
            this.Load += new System.EventHandler(this.P_GenerarNomina_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_Matriz_GenerarNomina)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Regresar_GenerarNomina;
        private System.Windows.Forms.Button btn_Eliminar_GenerarNomina;
        private System.Windows.Forms.Button btn_GenerarNominaInd_GenerarNomina;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_CierrePeriodo_GenerarNomina;
        private System.Windows.Forms.DataGridView dtgv_Matriz_GenerarNomina;
        private System.Windows.Forms.DateTimePicker DateTime_Periodo;
    }
}