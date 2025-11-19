namespace NominaMAD
{
    partial class P_HeadCounter
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
            this.btn_ReportesDepartamento_HC = new System.Windows.Forms.Button();
            this.btn_ReportesPuesto_HC = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Regresar_HC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ReportesDepartamento_HC
            // 
            this.btn_ReportesDepartamento_HC.Location = new System.Drawing.Point(104, 148);
            this.btn_ReportesDepartamento_HC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_ReportesDepartamento_HC.Name = "btn_ReportesDepartamento_HC";
            this.btn_ReportesDepartamento_HC.Size = new System.Drawing.Size(125, 34);
            this.btn_ReportesDepartamento_HC.TabIndex = 0;
            this.btn_ReportesDepartamento_HC.Text = "Departamentos";
            this.btn_ReportesDepartamento_HC.UseVisualStyleBackColor = true;
            this.btn_ReportesDepartamento_HC.Click += new System.EventHandler(this.btn_ReportesDepartamento_HC_Click);
            // 
            // btn_ReportesPuesto_HC
            // 
            this.btn_ReportesPuesto_HC.Location = new System.Drawing.Point(260, 148);
            this.btn_ReportesPuesto_HC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_ReportesPuesto_HC.Name = "btn_ReportesPuesto_HC";
            this.btn_ReportesPuesto_HC.Size = new System.Drawing.Size(120, 34);
            this.btn_ReportesPuesto_HC.TabIndex = 1;
            this.btn_ReportesPuesto_HC.Text = "Puestos";
            this.btn_ReportesPuesto_HC.UseVisualStyleBackColor = true;
            this.btn_ReportesPuesto_HC.Click += new System.EventHandler(this.btn_ReportesPuesto_HC_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(154, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 37);
            this.label1.TabIndex = 3;
            this.label1.Text = "REPORTES";
            // 
            // btn_Regresar_HC
            // 
            this.btn_Regresar_HC.Location = new System.Drawing.Point(423, 253);
            this.btn_Regresar_HC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Regresar_HC.Name = "btn_Regresar_HC";
            this.btn_Regresar_HC.Size = new System.Drawing.Size(70, 32);
            this.btn_Regresar_HC.TabIndex = 4;
            this.btn_Regresar_HC.Text = "Regresar";
            this.btn_Regresar_HC.UseVisualStyleBackColor = true;
            this.btn_Regresar_HC.Click += new System.EventHandler(this.btn_Regresar_HC_Click);
            // 
            // P_HeadCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 303);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Regresar_HC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ReportesPuesto_HC);
            this.Controls.Add(this.btn_ReportesDepartamento_HC);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "P_HeadCounter";
            this.Text = "Head Counter";
            this.Load += new System.EventHandler(this.P_HeadCounter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ReportesDepartamento_HC;
        private System.Windows.Forms.Button btn_ReportesPuesto_HC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Regresar_HC;
    }
}