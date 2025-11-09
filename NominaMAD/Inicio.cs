using NominaMAD.DAO;
using NominaMAD.Entidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NominaMAD
{
    public partial class P_Inicio : Form
    {       
        //public int MMenuAoE;
        public static int MMenuAoE;  // Variable estática                             

        public string NombreRh;
        public static string NombUsuario { get; set; }
        public string Contra;

        //public int idPeriodoActual { get; set; }
        //public string MesActual { get; set; }
        //public int AnoActual { get; set; }

        public P_Inicio()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // ObtenerPeriodoActual();
        }

        private void btn_INGRESAR_ACEPTAR_Click(object sender, EventArgs e)
        {
            NombUsuario = txt_NomUsua_Inicio.Text;
            Contra = txt_Contra_Inicio.Text;


            if (NombUsuario == "fer" && Contra == "123")
            {           
                MessageBox.Show("Bienvenido Admin.");
                MMenuAoE = 1;
                // Crear una instancia del nuevo formulario
                P_Menu1 p_Menu1 = new P_Menu1();
                this.Hide();

                // Mostrar el nuevo formulario
                p_Menu1.ShowDialog();
            }
           

        }
                
      


    }

}
