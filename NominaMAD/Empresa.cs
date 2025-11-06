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
    public partial class P_Empresa : Form
    {
        string Conexion = "Data Source=LUISMTZ\\SQLEXPRESS;Initial Catalog=Nomina;Integrated Security=True";
        public P_Empresa()
        {
            InitializeComponent();
        }

        private void P_Empresa_Load(object sender, EventArgs e)
        {
            txt_Nombre_Empresa.Enabled = false;
            txt_RazonSocial_Empresa.Enabled=false;
            txt_DomFiscal_Empresa.Enabled =false;
            txt_Telelfono_Empresa.Enabled = false;
            txt_RegistroPeatronal_Empresa.Enabled= false;
            txt_RFC_Empresa.Enabled = false;
            dtp_FechaInOpera_Empresa.Enabled = false;
            CargarDatosEmpresa();
        }


        private void CargarDatosEmpresa()
        {
            using (SqlConnection cn = new SqlConnection(Conexion)) // Reemplaza "your_connection_string" con la cadena de conexión real
            {
                try
                {
                    cn.Open();
                    string query = "SELECT TOP 1 Nombre, RazonFiscal, DomicilioFiscal, Telefono, RegistroPatronal, RFC, FechaInOperaciones FROM Empresa";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Asignar los datos obtenidos a los campos de texto
                        txt_Nombre_Empresa.Text = reader["Nombre"].ToString();
                        txt_RazonSocial_Empresa.Text = reader["RazonFiscal"].ToString();
                        txt_DomFiscal_Empresa.Text = reader["DomicilioFiscal"].ToString();
                        txt_Telelfono_Empresa.Text = reader["Telefono"].ToString();
                        txt_RegistroPeatronal_Empresa.Text = reader["RegistroPatronal"].ToString();
                        txt_RFC_Empresa.Text = reader["RFC"].ToString();
                        dtp_FechaInOpera_Empresa.Value = Convert.ToDateTime(reader["FechaInOperaciones"]);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos de la empresa: " + ex.Message);
                }
            }
        }

        private void btn_Regresar_Empresa_Click(object sender, EventArgs e)
        {
            if (P_Inicio.MMenuAoE == 1)
            {
                P_Menu1 p_Menu1 = new P_Menu1();
                // Ocultar el formulario actual (Form1)
                this.Hide();

                // Mostrar el nuevo formulario
                p_Menu1.ShowDialog();

            }
            else
            {
                if (P_Inicio.MMenuAoE == 2)
                {
                    P_Menu2 p_Menu2 = new P_Menu2();
                    // Ocultar el formulario actual (Form1)
                    this.Hide();

                    // Mostrar el nuevo formulario
                    p_Menu2.ShowDialog();

                }
            }
        }

        private void dtp_FechaInOpera_Empresa_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
