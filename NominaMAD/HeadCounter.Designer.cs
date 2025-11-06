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
            this.btn_ReportesTurnos_HC = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Regresar_HC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ReportesDepartamento_HC
            // 
            this.btn_ReportesDepartamento_HC.Location = new System.Drawing.Point(35, 148);
            this.btn_ReportesDepartamento_HC.Name = "btn_ReportesDepartamento_HC";
            this.btn_ReportesDepartamento_HC.Size = new System.Drawing.Size(167, 42);
            this.btn_ReportesDepartamento_HC.TabIndex = 0;
            this.btn_ReportesDepartamento_HC.Text = "Departamentos";
            this.btn_ReportesDepartamento_HC.UseVisualStyleBackColor = true;
            this.btn_ReportesDepartamento_HC.Click += new System.EventHandler(this.btn_ReportesDepartamento_HC_Click);
            // 
            // btn_ReportesPuesto_HC
            // 
            this.btn_ReportesPuesto_HC.Location = new System.Drawing.Point(242, 148);
            this.btn_ReportesPuesto_HC.Name = "btn_ReportesPuesto_HC";
            this.btn_ReportesPuesto_HC.Size = new System.Drawing.Size(160, 42);
            this.btn_ReportesPuesto_HC.TabIndex = 1;
            this.btn_ReportesPuesto_HC.Text = "Puestos";
            this.btn_ReportesPuesto_HC.UseVisualStyleBackColor = true;
            this.btn_ReportesPuesto_HC.Click += new System.EventHandler(this.btn_ReportesPuesto_HC_Click);
            // 
            // btn_ReportesTurnos_HC
            // 
            this.btn_ReportesTurnos_HC.Location = new System.Drawing.Point(437, 148);
            this.btn_ReportesTurnos_HC.Name = "btn_ReportesTurnos_HC";
            this.btn_ReportesTurnos_HC.Size = new System.Drawing.Size(160, 42);
            this.btn_ReportesTurnos_HC.TabIndex = 2;
            this.btn_ReportesTurnos_HC.Text = " Turnos";
            this.btn_ReportesTurnos_HC.UseVisualStyleBackColor = true;
            this.btn_ReportesTurnos_HC.Click += new System.EventHandler(this.btn_ReportesTurnos_HC_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(206, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 46);
            this.label1.TabIndex = 3;
            this.label1.Text = "REPORTES";
            // 
            // btn_Regresar_HC
            // 
            this.btn_Regresar_HC.Location = new System.Drawing.Point(564, 311);
            this.btn_Regresar_HC.Name = "btn_Regresar_HC";
            this.btn_Regresar_HC.Size = new System.Drawing.Size(93, 40);
            this.btn_Regresar_HC.TabIndex = 4;
            this.btn_Regresar_HC.Text = "Regresar";
            this.btn_Regresar_HC.UseVisualStyleBackColor = true;
            this.btn_Regresar_HC.Click += new System.EventHandler(this.btn_Regresar_HC_Click);
            // 
            // P_HeadCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 373);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Regresar_HC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ReportesTurnos_HC);
            this.Controls.Add(this.btn_ReportesPuesto_HC);
            this.Controls.Add(this.btn_ReportesDepartamento_HC);
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
        private System.Windows.Forms.Button btn_ReportesTurnos_HC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Regresar_HC;
    }
}