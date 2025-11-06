namespace NominaMAD
{
    partial class P_RepGenNomina
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
            this.btn_Buscar_RepGenNomina = new System.Windows.Forms.Button();
            this.txt_Ano_RepGenNomina = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_Imprimir_RepGenNomina = new System.Windows.Forms.Button();
            this.btn_Regresar_RepGenNomina = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Buscar_RepGenNomina
            // 
            this.btn_Buscar_RepGenNomina.Location = new System.Drawing.Point(12, 120);
            this.btn_Buscar_RepGenNomina.Name = "btn_Buscar_RepGenNomina";
            this.btn_Buscar_RepGenNomina.Size = new System.Drawing.Size(104, 30);
            this.btn_Buscar_RepGenNomina.TabIndex = 118;
            this.btn_Buscar_RepGenNomina.Text = "Buscar";
            this.btn_Buscar_RepGenNomina.UseVisualStyleBackColor = true;
            this.btn_Buscar_RepGenNomina.Click += new System.EventHandler(this.btn_Buscar_RepGenNomina_Click);
            // 
            // txt_Ano_RepGenNomina
            // 
            this.txt_Ano_RepGenNomina.Location = new System.Drawing.Point(162, 50);
            this.txt_Ano_RepGenNomina.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Ano_RepGenNomina.MaxLength = 4;
            this.txt_Ano_RepGenNomina.Name = "txt_Ano_RepGenNomina";
            this.txt_Ano_RepGenNomina.Size = new System.Drawing.Size(177, 22);
            this.txt_Ano_RepGenNomina.TabIndex = 113;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(107, 50);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 20);
            this.label10.TabIndex = 116;
            this.label10.Text = "Año:";
            // 
            // btn_Imprimir_RepGenNomina
            // 
            this.btn_Imprimir_RepGenNomina.Location = new System.Drawing.Point(136, 120);
            this.btn_Imprimir_RepGenNomina.Name = "btn_Imprimir_RepGenNomina";
            this.btn_Imprimir_RepGenNomina.Size = new System.Drawing.Size(122, 30);
            this.btn_Imprimir_RepGenNomina.TabIndex = 119;
            this.btn_Imprimir_RepGenNomina.Text = "Imprimir";
            this.btn_Imprimir_RepGenNomina.UseVisualStyleBackColor = true;
            this.btn_Imprimir_RepGenNomina.Click += new System.EventHandler(this.btn_Imprimir_RepGenNomina_Click);
            // 
            // btn_Regresar_RepGenNomina
            // 
            this.btn_Regresar_RepGenNomina.Location = new System.Drawing.Point(286, 119);
            this.btn_Regresar_RepGenNomina.Name = "btn_Regresar_RepGenNomina";
            this.btn_Regresar_RepGenNomina.Size = new System.Drawing.Size(102, 31);
            this.btn_Regresar_RepGenNomina.TabIndex = 120;
            this.btn_Regresar_RepGenNomina.Text = "Regresar";
            this.btn_Regresar_RepGenNomina.UseVisualStyleBackColor = true;
            this.btn_Regresar_RepGenNomina.Click += new System.EventHandler(this.btn_Regresar_RepGenNomina_Click);
            // 
            // P_RepGenNomina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 198);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Regresar_RepGenNomina);
            this.Controls.Add(this.btn_Imprimir_RepGenNomina);
            this.Controls.Add(this.btn_Buscar_RepGenNomina);
            this.Controls.Add(this.txt_Ano_RepGenNomina);
            this.Controls.Add(this.label10);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "P_RepGenNomina";
            this.Text = "Reporte General Nomina";
            this.Load += new System.EventHandler(this.P_RepGenNomina_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Buscar_RepGenNomina;
        private System.Windows.Forms.TextBox txt_Ano_RepGenNomina;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_Imprimir_RepGenNomina;
        private System.Windows.Forms.Button btn_Regresar_RepGenNomina;
    }
}