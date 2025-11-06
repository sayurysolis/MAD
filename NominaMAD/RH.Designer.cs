namespace NominaMAD
{
    partial class P_RH
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
            this.dgv_RH = new System.Windows.Forms.DataGridView();
            this.txt_Usuario_RH = new System.Windows.Forms.TextBox();
            this.txt_Contra_RH = new System.Windows.Forms.TextBox();
            this.btn_AcepartAgre_RH = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Modificar_RH = new System.Windows.Forms.Button();
            this.btn_AcepatarMOD_RH = new System.Windows.Forms.Button();
            this.btn_CancelarMOD_RH = new System.Windows.Forms.Button();
            this.btn_Agre_RH = new System.Windows.Forms.Button();
            this.btn_Regresar_RHbutton1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_RH)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_RH
            // 
            this.dgv_RH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_RH.Location = new System.Drawing.Point(12, 171);
            this.dgv_RH.Name = "dgv_RH";
            this.dgv_RH.RowHeadersWidth = 51;
            this.dgv_RH.RowTemplate.Height = 24;
            this.dgv_RH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_RH.Size = new System.Drawing.Size(469, 247);
            this.dgv_RH.TabIndex = 0;
            this.dgv_RH.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RH_CellClick);
            this.dgv_RH.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RH_CellContentClick);
            // 
            // txt_Usuario_RH
            // 
            this.txt_Usuario_RH.Location = new System.Drawing.Point(110, 21);
            this.txt_Usuario_RH.Name = "txt_Usuario_RH";
            this.txt_Usuario_RH.Size = new System.Drawing.Size(151, 22);
            this.txt_Usuario_RH.TabIndex = 1;
            // 
            // txt_Contra_RH
            // 
            this.txt_Contra_RH.Location = new System.Drawing.Point(401, 21);
            this.txt_Contra_RH.Name = "txt_Contra_RH";
            this.txt_Contra_RH.Size = new System.Drawing.Size(158, 22);
            this.txt_Contra_RH.TabIndex = 2;
            // 
            // btn_AcepartAgre_RH
            // 
            this.btn_AcepartAgre_RH.Location = new System.Drawing.Point(139, 72);
            this.btn_AcepartAgre_RH.Name = "btn_AcepartAgre_RH";
            this.btn_AcepartAgre_RH.Size = new System.Drawing.Size(113, 32);
            this.btn_AcepartAgre_RH.TabIndex = 3;
            this.btn_AcepartAgre_RH.Text = "Aceptar";
            this.btn_AcepartAgre_RH.UseVisualStyleBackColor = true;
            this.btn_AcepartAgre_RH.Click += new System.EventHandler(this.btn_Agregar_RH_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Usuario:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Contraseña:";
            // 
            // btn_Modificar_RH
            // 
            this.btn_Modificar_RH.Location = new System.Drawing.Point(282, 72);
            this.btn_Modificar_RH.Name = "btn_Modificar_RH";
            this.btn_Modificar_RH.Size = new System.Drawing.Size(119, 32);
            this.btn_Modificar_RH.TabIndex = 6;
            this.btn_Modificar_RH.Text = "Modificar";
            this.btn_Modificar_RH.UseVisualStyleBackColor = true;
            this.btn_Modificar_RH.Click += new System.EventHandler(this.btn_Modificar_RH_Click);
            // 
            // btn_AcepatarMOD_RH
            // 
            this.btn_AcepatarMOD_RH.Location = new System.Drawing.Point(208, 127);
            this.btn_AcepatarMOD_RH.Name = "btn_AcepatarMOD_RH";
            this.btn_AcepatarMOD_RH.Size = new System.Drawing.Size(152, 26);
            this.btn_AcepatarMOD_RH.TabIndex = 7;
            this.btn_AcepatarMOD_RH.Text = "Aceptar Modicacion";
            this.btn_AcepatarMOD_RH.UseVisualStyleBackColor = true;
            this.btn_AcepatarMOD_RH.Click += new System.EventHandler(this.btn_AcepatarMOD_RH_Click);
            // 
            // btn_CancelarMOD_RH
            // 
            this.btn_CancelarMOD_RH.Location = new System.Drawing.Point(16, 126);
            this.btn_CancelarMOD_RH.Name = "btn_CancelarMOD_RH";
            this.btn_CancelarMOD_RH.Size = new System.Drawing.Size(162, 27);
            this.btn_CancelarMOD_RH.TabIndex = 8;
            this.btn_CancelarMOD_RH.Text = "Cancelar";
            this.btn_CancelarMOD_RH.UseVisualStyleBackColor = true;
            this.btn_CancelarMOD_RH.Click += new System.EventHandler(this.btn_CancelarMOD_RH_Click);
            // 
            // btn_Agre_RH
            // 
            this.btn_Agre_RH.Location = new System.Drawing.Point(16, 72);
            this.btn_Agre_RH.Name = "btn_Agre_RH";
            this.btn_Agre_RH.Size = new System.Drawing.Size(95, 32);
            this.btn_Agre_RH.TabIndex = 9;
            this.btn_Agre_RH.Text = "Agregar";
            this.btn_Agre_RH.UseVisualStyleBackColor = true;
            this.btn_Agre_RH.Click += new System.EventHandler(this.btn_Agre_RH_Click);
            // 
            // btn_Regresar_RHbutton1
            // 
            this.btn_Regresar_RHbutton1.Location = new System.Drawing.Point(208, 439);
            this.btn_Regresar_RHbutton1.Name = "btn_Regresar_RHbutton1";
            this.btn_Regresar_RHbutton1.Size = new System.Drawing.Size(98, 36);
            this.btn_Regresar_RHbutton1.TabIndex = 10;
            this.btn_Regresar_RHbutton1.Text = "Regresar";
            this.btn_Regresar_RHbutton1.UseVisualStyleBackColor = true;
            this.btn_Regresar_RHbutton1.Click += new System.EventHandler(this.btn_Regresar_RHbutton1_Click);
            // 
            // P_RH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 489);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Regresar_RHbutton1);
            this.Controls.Add(this.btn_Agre_RH);
            this.Controls.Add(this.btn_CancelarMOD_RH);
            this.Controls.Add(this.btn_AcepatarMOD_RH);
            this.Controls.Add(this.btn_Modificar_RH);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_AcepartAgre_RH);
            this.Controls.Add(this.txt_Contra_RH);
            this.Controls.Add(this.txt_Usuario_RH);
            this.Controls.Add(this.dgv_RH);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "P_RH";
            this.Text = "RH";
            this.Load += new System.EventHandler(this.P_RH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_RH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_RH;
        private System.Windows.Forms.TextBox txt_Usuario_RH;
        private System.Windows.Forms.TextBox txt_Contra_RH;
        private System.Windows.Forms.Button btn_AcepartAgre_RH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Modificar_RH;
        private System.Windows.Forms.Button btn_AcepatarMOD_RH;
        private System.Windows.Forms.Button btn_CancelarMOD_RH;
        private System.Windows.Forms.Button btn_Agre_RH;
        private System.Windows.Forms.Button btn_Regresar_RHbutton1;
    }
}