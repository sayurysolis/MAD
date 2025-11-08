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
        string Conexion = "Data Source=RAGE-PC\\SQLEXPRESS;Initial Catalog=DSB_topografia;Integrated Security=True";
        //public int MMenuAoE;
        public static int MMenuAoE;  // Variable estática
                                     // public static string NombUsuario;

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
        {//string NombUsuario= txt_NomUsua_Inicio.Text;
            //string Contra = txt_Contra_Inicio.Text;

            NombUsuario = txt_NomUsua_Inicio.Text;
            Contra = txt_Contra_Inicio.Text;




            if (NombUsuario == "fer" && Contra == "123")
            {
                MessageBox.Show("Bienvenido Admin.");
                MMenuAoE = 1;
                // Crear una instancia del nuevo formulario
                P_Menu1 p_Menu1 = new P_Menu1();

                // p_Menu1.NombreUsuario = NombUsuario;

                // Ocultar el formulario actual (Form1)
                this.Hide();

                // Mostrar el nuevo formulario
                p_Menu1.ShowDialog();



            }
            else
            {
                //MMenuAoE = 2;
                //// Crear una instancia del nuevo formulario
                //P_Menu2 p_Menu2 = new P_Menu2();

                //// p_Menu2.NombreUsuario= NombUsuario;



                //// Ocultar el formulario actual (Form1)
                //this.Hide();

                //// Mostrar el nuevo formulario
                //p_Menu2.ShowDialog();

                // Consultar la base de datos para verificar si el usuario existe en UsuariosRH
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    string query = "SELECT COUNT(1) FROM UsuariosRH WHERE Usuario = @Usuario AND contrase = @Contra";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Usuario", NombUsuario);
                    cmd.Parameters.AddWithValue("@Contra", Contra);

                    cn.Open();
                    int userExists = Convert.ToInt32(cmd.ExecuteScalar());

                    if (userExists > 0)
                    {
                        NombreRh = txt_NomUsua_Inicio.Text;
                        // Si el usuario existe en UsuariosRH, asignar el menú 2 y abrir el formulario correspondiente
                        MMenuAoE = 2;

                        P_Menu2 p_Menu2 = new P_Menu2();
                        this.Hide();
                        p_Menu2.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        // Si el usuario no existe, mostrar un mensaje de error
                        MessageBox.Show("Usuario o contraseña incorrectos. Por favor, intente de nuevo.");
                    }
                }

            }

        }

        

        //void ObtenerPeriodoActual()
        //{
        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        string query = "SELECT id_Periodo, Mes, Ano FROM Periodo";
        //        SqlCommand cmd = new SqlCommand(query, cn);

        //        cn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        // Avanza hasta el último registro
        //        while (reader.Read())
        //        {
        //            idPeriodoActual = reader.GetInt32(0);  // id_Periodo
        //            MesActual = reader.GetString(1);       // Mes
        //            AnoActual = reader.GetInt32(2);        // Ano
        //        }

        //        reader.Close();
        //    }
        //}


    }

}
