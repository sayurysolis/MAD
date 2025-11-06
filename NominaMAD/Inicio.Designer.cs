namespace NominaMAD
{
    partial class P_Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.btn_INGRESAR_ACEPTAR = new System.Windows.Forms.Button();
            this.txt_Contra_Inicio = new System.Windows.Forms.TextBox();
            this.txt_NomUsua_Inicio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_INGRESAR_ACEPTAR
            // 
            this.btn_INGRESAR_ACEPTAR.Location = new System.Drawing.Point(428, 195);
            this.btn_INGRESAR_ACEPTAR.Name = "btn_INGRESAR_ACEPTAR";
            this.btn_INGRESAR_ACEPTAR.Size = new System.Drawing.Size(103, 35);
            this.btn_INGRESAR_ACEPTAR.TabIndex = 19;
            this.btn_INGRESAR_ACEPTAR.Text = "Ingresar";
            this.btn_INGRESAR_ACEPTAR.UseVisualStyleBackColor = true;
            this.btn_INGRESAR_ACEPTAR.Click += new System.EventHandler(this.btn_INGRESAR_ACEPTAR_Click);
            // 
            // txt_Contra_Inicio
            // 
            this.txt_Contra_Inicio.Location = new System.Drawing.Point(235, 146);
            this.txt_Contra_Inicio.Name = "txt_Contra_Inicio";
            this.txt_Contra_Inicio.PasswordChar = '*';
            this.txt_Contra_Inicio.Size = new System.Drawing.Size(164, 22);
            this.txt_Contra_Inicio.TabIndex = 18;
            // 
            // txt_NomUsua_Inicio
            // 
            this.txt_NomUsua_Inicio.Location = new System.Drawing.Point(235, 101);
            this.txt_NomUsua_Inicio.Name = "txt_NomUsua_Inicio";
            this.txt_NomUsua_Inicio.Size = new System.Drawing.Size(164, 22);
            this.txt_NomUsua_Inicio.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Contraseña:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Nombre de Usuario:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(511, 38);
            this.label1.TabIndex = 20;
            this.label1.Text = "!Bienvenidos a DSB Topografia!";
            // 
            // P_Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 284);
            this.Controls.Add(this.btn_INGRESAR_ACEPTAR);
            this.Controls.Add(this.txt_Contra_Inicio);
            this.Controls.Add(this.txt_NomUsua_Inicio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "P_Inicio";
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_INGRESAR_ACEPTAR;
        private System.Windows.Forms.TextBox txt_Contra_Inicio;
        private System.Windows.Forms.TextBox txt_NomUsua_Inicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

