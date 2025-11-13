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
        private int ColumnaSeleccionada = 0;
        private int ID_Puesto = 0;
        public P_GestionPuestos()
        {
            InitializeComponent();
            cmBox_Departamento_GestionPuestos.SelectedIndexChanged += cmBox_Departamento_GestionPuestos_SelectedIndexChanged; // Asignar el evento
        }
        private void P_GestionPuestos_Load(object sender, EventArgs e)
        {
            txtPuesto_GestionPuestos.MaxLength = 40;
            mostrarTablaPuestos();
            MostrarComboBoxDep();
            //OCULTAR BOTONES
            btn_Guardar_GestionPuestos.Visible = false;
            btn_Modifcar_GestionPuestos.Visible = false;
            btn_AceptarMOD_GestionPuestos.Visible = false;
            btn_CancelarMOD_GestionPuestos.Visible = false;

            //habilitar txts
            txtPuesto_GestionPuestos.Enabled = false;
            //txt_SalarioDiario_GestionPuestos.Enabled = false;
            txt_DescripcionPuesto_GestionPuestos.Enabled = false;
        }
        private void mostrarTablaPuestos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_GetPuestos", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                dtgv_GestionPustos.DataSource = dt;
            }
        }
        //COMBOS Y TABLAS
        private void CargarPuestos(int idDepartamento)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_GetPuestos", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@DepartamentoID", idDepartamento);
                da.Fill(dt);
                dtgv_GestionPustos.DataSource = dt;
            }
            DataView dv = new DataView(dt);
            dv.RowFilter = "Estatus = 'Activo'";
            dtgv_GestionPustos.DataSource = dv;
        }
        private void cmBox_Departamento_GestionPuestos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idDepartamentoSeleccionado = ((ComboBoxItem)cmBox_Departamento_GestionPuestos.SelectedItem).Value;
            CargarPuestos(idDepartamentoSeleccionado);
        }
        private void MostrarComboBoxDep()
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_GetDepartamento", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    cmBox_Departamento_GestionPuestos.Items.Clear();

                    cmBox_Departamento_GestionPuestos.Items.Add(new ComboBoxItem
                    {
                        Text = "Todos los departamentos",
                        Value = 0
                    });

                    while (reader.Read())
                    {
                        cmBox_Departamento_GestionPuestos.Items.Add(new ComboBoxItem
                        {
                            Text = reader["NombreDepartamento"].ToString(),
                            Value = (int)reader["ID_Departamento"]
                        });
                    }

                    reader.Close();
                    cmBox_Departamento_GestionPuestos.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los departamentos: " + ex.Message);
                }
            }
        }
        ////
        bool existe = false;
        private void ValidarExistencia()
        {

            foreach (DataGridViewRow fila in dtgv_GestionPustos.Rows)
            {
                // Comparar el valor de la columna que deseas verificar (NombreDepartamento en este caso)
                if (fila.Cells["Nombre"].Value != null &&
                    fila.Cells["Nombre"].Value.ToString().ToLower() == txtPuesto_GestionPuestos.Text.ToLower())
                {
                    existe = true;
                    MessageBox.Show("El puesto ya existe.");
                    break;
                }

            }

        }
        #region Habilitar_btn


        private void btn_Agregar_GestionPuestos_Click(object sender, EventArgs e)
        {
            // limpiar
            txtPuesto_GestionPuestos.Text = "";
            // txt_SalarioDiario_GestionPuestos.Text = "";
            txt_DescripcionPuesto_GestionPuestos.Text = "";

            btn_Agregar_GestionPuestos.Visible = false;
            btn_Guardar_GestionPuestos.Visible = true;
            btn_Modifcar_GestionPuestos.Visible = false;
            btn_AceptarMOD_GestionPuestos.Visible = false;
            btn_CancelarMOD_GestionPuestos.Visible = false;

            //habilitar txts
            txtPuesto_GestionPuestos.Enabled = true;
            // txt_SalarioDiario_GestionPuestos.Enabled = true;
            txt_DescripcionPuesto_GestionPuestos.Enabled = true;
        }
        private void btn_Modifcar_GestionPuestos_Click(object sender, EventArgs e)
        {

            //modificarOpcion = txtPuesto_GestionPuestos.Text;
            txtPuesto_GestionPuestos.Enabled = true;
            //  txt_SalarioDiario_GestionPuestos.Enabled = true;
            txt_DescripcionPuesto_GestionPuestos.Enabled = true;

            btn_Agregar_GestionPuestos.Visible = false;
            btn_Guardar_GestionPuestos.Visible = false;
            btn_Modifcar_GestionPuestos.Visible = false;
            btn_AceptarMOD_GestionPuestos.Visible = true;
            btn_CancelarMOD_GestionPuestos.Visible = true;

            //habilitar txts
            txtPuesto_GestionPuestos.Enabled = true;
            //  txt_SalarioDiario_GestionPuestos.Enabled = true;
            txt_DescripcionPuesto_GestionPuestos.Enabled = true;
        }
        #endregion

        private void btn_Guardar_GestionPuestos_Click(object sender, EventArgs e)
        {
            ValidarExistencia();
            int idDepartamentoSeleccionado = ((ComboBoxItem)cmBox_Departamento_GestionPuestos.SelectedItem).Value;
            if (idDepartamentoSeleccionado == 0)
            {
                MessageBox.Show("Por favor selecciona un departamento válido antes de guardar.");
                return;
            }

            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_AddPuesto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", txtPuesto_GestionPuestos.Text);
                cmd.Parameters.AddWithValue("@Descripcion", txt_DescripcionPuesto_GestionPuestos.Text);
                cmd.Parameters.AddWithValue("@DepartamentoID", idDepartamentoSeleccionado);

                cmd.ExecuteNonQuery();
                CargarPuestos(idDepartamentoSeleccionado);
            }
        }
        private void btn_AceptarMOD_GestionPuestos_Click(object sender, EventArgs e)
        {
            int idDepartamentoSeleccionado = ((ComboBoxItem)cmBox_Departamento_GestionPuestos.SelectedItem).Value;

            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EditarPuesto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros del procedimiento
                cmd.Parameters.AddWithValue("@ID_Puesto", ID_Puesto);
                cmd.Parameters.AddWithValue("@Nombre", txtPuesto_GestionPuestos.Text);
                cmd.Parameters.AddWithValue("@Descripcion", txt_DescripcionPuesto_GestionPuestos.Text);
                cmd.Parameters.AddWithValue("@estatus", 1);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Puesto actualizado correctamente.");


                CargarPuestos(idDepartamentoSeleccionado);
            }
        }
        private void btn_eliminar_Click_1(object sender, EventArgs e)
        {
            if (ColumnaSeleccionada < 0 || ColumnaSeleccionada >= dtgv_GestionPustos.Rows.Count)
            {
                MessageBox.Show("Por favor selecciona un puesto válido para eliminar.");
                return;
            }

            var valorCelda = dtgv_GestionPustos.Rows[ColumnaSeleccionada].Cells["ID_Puesto"].Value;
            if (valorCelda == null || valorCelda == DBNull.Value)
            {
                MessageBox.Show("El puesto seleccionado no es válido.");
                return;
            }

            int idPuestoSeleccionado = Convert.ToInt32(valorCelda);

            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_BajaPuesto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Puesto", idPuestoSeleccionado);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Puesto eliminado correctamente.");

            int idDepartamentoSeleccionado = ((ComboBoxItem)cmBox_Departamento_GestionPuestos.SelectedItem).Value;
            CargarPuestos(idDepartamentoSeleccionado);
        }
        private void btn_CancelarMOD_GestionPuestos_Click(object sender, EventArgs e)
        {
            btn_Agregar_GestionPuestos.Visible = true;
            btn_Guardar_GestionPuestos.Visible = false;
            btn_Modifcar_GestionPuestos.Visible = false;
            btn_AceptarMOD_GestionPuestos.Visible = false;
            btn_CancelarMOD_GestionPuestos.Visible = false;
            // limpiar
            txtPuesto_GestionPuestos.Text = "";
            txt_DescripcionPuesto_GestionPuestos.Text = "";

            //habilitar txts
            txtPuesto_GestionPuestos.Enabled = false;

            txt_DescripcionPuesto_GestionPuestos.Enabled = false;
        }


        private void dtgv_GestionPustos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ColumnaSeleccionada = e.RowIndex;

            if (ColumnaSeleccionada != -1)
            {
                // Guardar ID_Puesto de la fila seleccionada
                ID_Puesto = Convert.ToInt32(dtgv_GestionPustos.Rows[ColumnaSeleccionada].Cells["ID_Puesto"].Value);

                // llenar txt
                txtPuesto_GestionPuestos.Text = dtgv_GestionPustos.Rows[ColumnaSeleccionada].Cells["Nombre"].Value.ToString();
                txt_DescripcionPuesto_GestionPuestos.Text = dtgv_GestionPustos.Rows[ColumnaSeleccionada].Cells["Descripcion"].Value.ToString();

                // deshabilitar txts
                txtPuesto_GestionPuestos.Enabled = false;
                txt_DescripcionPuesto_GestionPuestos.Enabled = false;

                // botones
                btn_Agregar_GestionPuestos.Visible = true;
                btn_Modifcar_GestionPuestos.Visible = true;
                btn_Guardar_GestionPuestos.Visible = false;
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
    }

}
