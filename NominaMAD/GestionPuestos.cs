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

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using System.Data.SqlClient;
using NominaMAD.Entidad;
using NominaMAD.DAO;
using NominaMAD.Resources;

namespace NominaMAD
{
    public partial class P_GestionPuestos : Form
    {
        //public P_GestionPuestos()
        //{
        //    InitializeComponent();
        //}
        private int ColumnaSeleccionada = 0;
        public P_GestionPuestos()
        {
            InitializeComponent();
           // P_GestionPuestos_Load(); // Cargar datos al iniciar
            cmBox_Departamento_GestionPuestos.SelectedIndexChanged += cmBox_Departamento_GestionPuestos_SelectedIndexChanged; // Asignar el evento
        }

        string Conexion = "Data Source=RAGE-PC\\SQLEXPRESS;Initial Catalog=DSB_topografia;Integrated Security=True";
        string modificarOpcion;
        private void P_GestionPuestos_Load(object sender, EventArgs e)
        {
            txtPuesto_GestionPuestos.MaxLength = 40;

            // mostrarTablaPuestos();
            MostrarComboBoxDep();
            //OCULTAR BOTONES
            btn_Guardar_GestionPuestos.Visible = false;
            btn_Limpiar_GestionPuestos.Visible = false;
            btn_Modifcar_GestionPuestos.Visible = false;
            btn_AceptarMOD_GestionPuestos.Visible = false;
            btn_CancelarMOD_GestionPuestos.Visible = false;

            //habilitar txts
            txtPuesto_GestionPuestos.Enabled = false;
            //txt_SalarioDiario_GestionPuestos.Enabled = false;
            txt_DescripcionPuesto_GestionPuestos.Enabled=false;
        }

        private void mostrarTablaPuestos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Puesto", cn);
                da.SelectCommand.CommandType = CommandType.Text;
                da.Fill(dt);
                dtgv_GestionPustos.DataSource = dt;
            }
        }

        bool existe = false;
        private void ValidarExistencia()
        {
            //bool existe = false;

            // Recorrer cada fila en el DataGridView
            foreach (DataGridViewRow fila in dtgv_GestionPustos.Rows)
            {
                // Comparar el valor de la columna que deseas verificar (NombreDepartamento en este caso)
                if (fila.Cells["Nombre"].Value != null &&
                    fila.Cells["Nombre"].Value.ToString().ToLower() == txtPuesto_GestionPuestos.Text.ToLower())
                {
                    existe = true;
                    break;
                }
            }

        }
        private void btn_Guardar_GestionPuestos_Click(object sender, EventArgs e)
        {
            if (txtPuesto_GestionPuestos.Text == "" || txt_DescripcionPuesto_GestionPuestos.Text == "")
            {
                MessageBox.Show("Algun Dato Vacio");
            }
            else
            {
                int idDepartamentoSeleccionado = ((ComboBoxItem)cmBox_Departamento_GestionPuestos.SelectedItem).Value;

                using (SqlConnection cn = BD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Puesto (Nombre, Descripcion, EmpresaID, DepartamentoID) VALUES (@Nombre, @Descripcion, @EmpresaID, @DepartamentoID)", cn);

                    cmd.Parameters.AddWithValue("@Nombre", txtPuesto_GestionPuestos.Text);
                    cmd.Parameters.AddWithValue("@Descripcion", txt_DescripcionPuesto_GestionPuestos.Text);
                    cmd.Parameters.AddWithValue("@EmpresaID", 1); 
                    cmd.Parameters.AddWithValue("@DepartamentoID", idDepartamentoSeleccionado);

                    cmd.ExecuteNonQuery();
                    CargarPuestos(idDepartamentoSeleccionado);
                }



            }
        }

        private void MostrarComboBoxDep()
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                try
                {
                    
                    SqlCommand cmd = new SqlCommand("SELECT ID_Departamento, nombre FROM Departamento", cn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpiar el ComboBox antes de llenarlo
                    cmBox_Departamento_GestionPuestos.Items.Clear();

                    // Agregar cada departamento al ComboBox
                    while (reader.Read())
                    {
                        // Crear un nuevo item en el ComboBox con el nombre y el ID del departamento
                        cmBox_Departamento_GestionPuestos.Items.Add(new ComboBoxItem
                        {
                            Text = reader["nombre"].ToString(),
                            Value = (int)reader["ID_Departamento"]
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los departamentos: " + ex.Message);
                }
            }
        }


        private void cmBox_Departamento_GestionPuestos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el id_Departamento del item seleccionado
            int idDepartamentoSeleccionado = ((ComboBoxItem)cmBox_Departamento_GestionPuestos.SelectedItem).Value;
            CargarPuestos(idDepartamentoSeleccionado);
        }

        private void CargarPuestos(int idDepartamento)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                
                SqlDataAdapter da = new SqlDataAdapter("SELECT Nombre, Descripcion FROM Puesto WHERE DepartamentoID = @id_Departamento", cn);
                da.SelectCommand.Parameters.AddWithValue("@ID_Departamento", idDepartamento);
                cn.Open();
                da.Fill(dt);

                // Mostrar los puestos en el DataGridView
                dtgv_GestionPustos.DataSource = dt;
            }
        }

        private void dtgv_GestionPustos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ColumnaSeleccionada = e.RowIndex;

            if (ColumnaSeleccionada != -1)
            {
                // limpiar
                txtPuesto_GestionPuestos.Text = "";
                //txt_SalarioDiario_GestionPuestos.Text = "";
                txt_DescripcionPuesto_GestionPuestos.Text = ""; // Añadir aquí el campo de descripción

                // ingresa datos en los txt
                txtPuesto_GestionPuestos.Text = dtgv_GestionPustos.Rows[ColumnaSeleccionada].Cells["Nombre"].Value.ToString();
               txt_DescripcionPuesto_GestionPuestos.Text = dtgv_GestionPustos.Rows[ColumnaSeleccionada].Cells["Descripcion"].Value.ToString(); 

                // deshabilitar txts
                txtPuesto_GestionPuestos.Enabled = false;
                //txt_SalarioDiario_GestionPuestos.Enabled = false;
                txt_DescripcionPuesto_GestionPuestos.Enabled = false; // Deshabilitar si es necesario

                // Mostrar los botones según tu lógica
                btn_Agregar_GestionPuestos.Visible = true;
                btn_Guardar_GestionPuestos.Visible = false;
                btn_Limpiar_GestionPuestos.Visible = false;
                btn_Modifcar_GestionPuestos.Visible = true;
                btn_AceptarMOD_GestionPuestos.Visible = false;
                btn_CancelarMOD_GestionPuestos.Visible = false;
            }
        }


        private void btn_Regresar_GestionPuestos_Click(object sender, EventArgs e)
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

        private void btn_Agregar_GestionPuestos_Click(object sender, EventArgs e)
        {
            // limpiar
            txtPuesto_GestionPuestos.Text = "";
           // txt_SalarioDiario_GestionPuestos.Text = "";
            txt_DescripcionPuesto_GestionPuestos.Text = "";

            btn_Agregar_GestionPuestos.Visible = false;
            btn_Guardar_GestionPuestos.Visible = true;
            btn_Limpiar_GestionPuestos.Visible = true;
            btn_Modifcar_GestionPuestos.Visible = false;
            btn_AceptarMOD_GestionPuestos.Visible = false;
            btn_CancelarMOD_GestionPuestos.Visible = false;

            //habilitar txts
            txtPuesto_GestionPuestos.Enabled = true;
           // txt_SalarioDiario_GestionPuestos.Enabled = true;
            txt_DescripcionPuesto_GestionPuestos.Enabled = true;
        }

        private void btn_Limpiar_GestionPuestos_Click(object sender, EventArgs e)
        {
            //OCULTAR BOTONES
            btn_Agregar_GestionPuestos.Visible = true;
            btn_Guardar_GestionPuestos.Visible = false;
            btn_Limpiar_GestionPuestos.Visible = false;
            btn_Modifcar_GestionPuestos.Visible = false;
            btn_AceptarMOD_GestionPuestos.Visible = false;
            btn_CancelarMOD_GestionPuestos.Visible = false;

            //habilitar txts
            txtPuesto_GestionPuestos.Enabled = false;
           // txt_SalarioDiario_GestionPuestos.Enabled = false;
            txt_DescripcionPuesto_GestionPuestos.Enabled=false;

            // limpiar
            txtPuesto_GestionPuestos.Text = "";
           // txt_SalarioDiario_GestionPuestos.Text = "";
            txt_DescripcionPuesto_GestionPuestos.Text = "";
        }

        private void btn_Modifcar_GestionPuestos_Click(object sender, EventArgs e)
        {
           
            modificarOpcion = txtPuesto_GestionPuestos.Text;
            txtPuesto_GestionPuestos.Enabled = true;
          //  txt_SalarioDiario_GestionPuestos.Enabled = true;
            txt_DescripcionPuesto_GestionPuestos.Enabled=true;

            btn_Agregar_GestionPuestos.Visible = false;
            btn_Guardar_GestionPuestos.Visible = false;
            btn_Limpiar_GestionPuestos.Visible = false;
            btn_Modifcar_GestionPuestos.Visible = false;
            btn_AceptarMOD_GestionPuestos.Visible = true;
            btn_CancelarMOD_GestionPuestos.Visible = true;

            //habilitar txts
            txtPuesto_GestionPuestos.Enabled = true;
          //  txt_SalarioDiario_GestionPuestos.Enabled = true;
            txt_DescripcionPuesto_GestionPuestos.Enabled= true;
        }

        private void btn_AceptarMOD_GestionPuestos_Click(object sender, EventArgs e)
        {
            int idDepartamentoSeleccionado = ((ComboBoxItem)cmBox_Departamento_GestionPuestos.SelectedItem).Value;
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Puesto SET Nombre='" + txtPuesto_GestionPuestos.Text + "', Descripcion='" + txt_DescripcionPuesto_GestionPuestos.Text + "' WHERE Nombre='" + modificarOpcion + "'", cn);

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                CargarPuestos(idDepartamentoSeleccionado);
                //mostrarTablaDepart();
            }

            // limpiar
            txtPuesto_GestionPuestos.Text = "";
            //txt_SalarioDiario_GestionPuestos.Text = "";
            txt_DescripcionPuesto_GestionPuestos.Text = "";
            txtPuesto_GestionPuestos.Enabled = false;
            //txt_SalarioDiario_GestionPuestos.Enabled = false;
            txt_DescripcionPuesto_GestionPuestos.Enabled=false;

            ////oculta botones
            btn_Agregar_GestionPuestos.Visible = true;
            btn_Guardar_GestionPuestos.Visible = false;
            btn_Limpiar_GestionPuestos.Visible = false;
            btn_Modifcar_GestionPuestos.Visible = false;
            btn_AceptarMOD_GestionPuestos.Visible = false;
            btn_CancelarMOD_GestionPuestos.Visible = false;
            //mostrarTablaDepart();
        }

        private void btn_CancelarMOD_GestionPuestos_Click(object sender, EventArgs e)
        {
            btn_Agregar_GestionPuestos.Visible = true;
            btn_Guardar_GestionPuestos.Visible = false;
            btn_Limpiar_GestionPuestos.Visible = false;
            btn_Modifcar_GestionPuestos.Visible = false;
            btn_AceptarMOD_GestionPuestos.Visible = false;
            btn_CancelarMOD_GestionPuestos.Visible = false;
            // limpiar
            txtPuesto_GestionPuestos.Text = "";
           // txt_SalarioDiario_GestionPuestos.Text = "";
            txt_DescripcionPuesto_GestionPuestos.Text = "";

            //habilitar txts
            txtPuesto_GestionPuestos.Enabled = false;
            //txt_SalarioDiario_GestionPuestos.Enabled = false;
            txt_DescripcionPuesto_GestionPuestos.Enabled=false; 
        }

        private void cmBox_Departamento_GestionPuestos_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void txtPuesto_GestionPuestos_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
