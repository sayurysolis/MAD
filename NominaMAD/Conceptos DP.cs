using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace NominaMAD
{
    public partial class P_ConceptosDP : Form
    {
        private int ColumnaSeleccionada = 0;
        public P_ConceptosDP()
        {
            InitializeComponent();
            mostrarTablaDP();

            label1.Enabled = false;
            CmBox_Concepto_ConceptosDP.Enabled = false;
            label2.Enabled = false;
            CmBox_Tipo_ConceptosDP.Enabled = false;
            label3.Enabled = false;
            txt_NombreCon_ConceptosDP.Enabled = false;

            labelMonto.Visible = false;
            txt_Monto_ConceptosDP.Visible = false;
            labelPorcentaje.Visible = false;
            txt_Porcentaje_ConceptosDP.Visible = false;

            btn_Agregar_ConceptosDP.Visible=true;
            btn_Aceptar_ConceptosDP.Visible=false;
            btn_Cancelar_ConceptosDP.Visible=false;
            btn_Modificar_ConceptosDP.Visible = false;
            btn_ModAceptar_ConceptosDP.Visible = false;
            btn_ModCancelar_ConceptosDP.Visible = false;
            btn_Eliminar_ConceptosDP.Visible = false;
        }
        string Conexion = "Data Source=RAGE-PC\\SQLEXPRESS;Initial Catalog=DSB_topografia;Integrated Security=True";
        string modificarOpcion;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CmBox_Concepto_ConceptosDP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void P_ConceptosDP_Load(object sender, EventArgs e)
        {
            txt_NombreCon_ConceptosDP.MaxLength = 30;
            // Llena el ComboBox con "Deducción" y "Percepción"
            CmBox_Concepto_ConceptosDP.Items.Add("Deducción");
            CmBox_Concepto_ConceptosDP.Items.Add("Percepción");
            CmBox_Tipo_ConceptosDP.Items.Add("Monto");
            CmBox_Tipo_ConceptosDP.Items.Add("Porcentaje");
            // Suscribe el evento después de que el ComboBox esté lleno
            CmBox_Tipo_ConceptosDP.SelectedIndexChanged += CmBox_Tipo_ConceptosDP_SelectedIndexChanged;

        }

        private void mostrarTablaDP()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PercepcionesDeduccion", cn);
                da.SelectCommand.CommandType = CommandType.Text;
                
                da.Fill(dt);
                dtgv_ConceptosDP.DataSource = dt;

            }
        }

        private void CmBox_Tipo_ConceptosDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica si hay un elemento seleccionado antes de continuar
            if (CmBox_Tipo_ConceptosDP.SelectedItem == null)
            {
                return; // No hacer nada si no hay ningún elemento seleccionado
            }
            // Aquí va el código que deseas ejecutar cuando cambie la selección
            string seleccion = CmBox_Tipo_ConceptosDP.SelectedItem.ToString();

            if(seleccion == "Monto")
            {
                //labelMonto.Enabled = true;
                //txt_Monto_ConceptosDP.Enabled = true;
                //labelPorcentaje.Enabled = false;
                //txt_Porcentaje_ConceptosDP.Enabled=false;
                labelMonto.Visible = true;
                txt_Monto_ConceptosDP.Visible = true;
                labelPorcentaje.Visible = false;
                txt_Porcentaje_ConceptosDP.Visible = false;
            }
            else
            {
                if(seleccion == "Porcentaje")
                {
                    labelMonto.Visible = false;
                    txt_Monto_ConceptosDP.Visible = false;
                    labelPorcentaje.Visible = true;
                    txt_Porcentaje_ConceptosDP.Visible = true;
                }

            }

            //MessageBox.Show("Has seleccionado: " + seleccion);

        }

        private void btn_Agregar_ConceptosDP_Click(object sender, EventArgs e)
        {
            label1.Enabled = true;
            CmBox_Concepto_ConceptosDP.Enabled = true;
            label2.Enabled = true;
            CmBox_Tipo_ConceptosDP.Enabled = true;
            label3.Enabled = true;
            txt_NombreCon_ConceptosDP.Enabled = true;

            labelMonto.Visible = false;
            txt_Monto_ConceptosDP.Visible = false;
            labelPorcentaje.Visible = false;
            txt_Porcentaje_ConceptosDP.Visible = false;

            btn_Agregar_ConceptosDP.Visible = false;
            btn_Aceptar_ConceptosDP.Visible = true;
            btn_Cancelar_ConceptosDP.Visible = true;
            btn_Modificar_ConceptosDP.Visible = false;
            btn_ModAceptar_ConceptosDP.Visible = false;
            btn_ModCancelar_ConceptosDP.Visible = false;
            btn_Eliminar_ConceptosDP.Visible = false;

            //MessageBox.Show("Agregado Correctamente");
        }

        private bool ValidarCampos()
        {
            // Validar que se haya seleccionado un departamento y un puesto
            if (CmBox_Concepto_ConceptosDP.SelectedIndex == -1 ||
                CmBox_Tipo_ConceptosDP.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un Concepto y un Tipo.");
                return false;
            }
            else
            {
                // Validar campos obligatorios
                string seleccion = CmBox_Tipo_ConceptosDP.SelectedItem.ToString();
                if (seleccion == "Monto")
                {
                    if (string.IsNullOrWhiteSpace(txt_NombreCon_ConceptosDP.Text) ||
                    string.IsNullOrWhiteSpace(txt_Monto_ConceptosDP.Text))
                    {
                        MessageBox.Show("Por favor, complete todos los campos obligatorios.");
                        return false;
                    }
                }
                else
                {
                    if (seleccion == "Porcentaje")
                    {
                        if (string.IsNullOrWhiteSpace(txt_NombreCon_ConceptosDP.Text) ||
                            string.IsNullOrWhiteSpace(txt_Porcentaje_ConceptosDP.Text))
                        {
                            MessageBox.Show("Por favor, complete todos los campos obligatorios.");
                            return false;
                        }
                    }

                }
            }

           

            


            return true; // Todos los campos están completos
        }

        private void btn_Aceptar_ConceptosDP_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            string tipo = CmBox_Concepto_ConceptosDP.SelectedItem.ToString() == "Percepción" ? "P" : "D";
            string nombre = txt_NombreCon_ConceptosDP.Text;
            bool esPorcentaje = CmBox_Tipo_ConceptosDP.SelectedItem.ToString() == "Porcentaje";
            decimal valor;

            if (esPorcentaje)
            {
                if (!decimal.TryParse(txt_Porcentaje_ConceptosDP.Text, out valor))
                {
                    MessageBox.Show("Porcentaje inválido.");
                    return;
                }
            }
            else
            {
                if (!decimal.TryParse(txt_Monto_ConceptosDP.Text, out valor))
                {
                    MessageBox.Show("Monto inválido.");
                    return;
                }
            }

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                string query = "INSERT INTO PercepcionesDeduccion (Tipo, nombre, EsPorcetanje, Valor) VALUES (@Tipo, @Nombre, @EsPorcentaje, @Valor)";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@EsPorcentaje", esPorcentaje);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }

            MessageBox.Show("Concepto registrado correctamente.");
            LimpiarCampos();
            mostrarTablaDP();
        }


        // Función para limpiar los campos después de la inserción
        private void LimpiarCampos()
        {
            CmBox_Concepto_ConceptosDP.SelectedIndex = -1;
            CmBox_Tipo_ConceptosDP.SelectedIndex = -1;
            txt_NombreCon_ConceptosDP.Clear();
            txt_Monto_ConceptosDP.Clear();
            txt_Porcentaje_ConceptosDP.Clear();
            labelMonto.Visible = false;
            txt_Monto_ConceptosDP.Visible = false;
            labelPorcentaje.Visible = false;
            txt_Porcentaje_ConceptosDP.Visible = false;


            label1.Enabled = false;
            CmBox_Concepto_ConceptosDP.Enabled = false;
            label2.Enabled = false;
            CmBox_Tipo_ConceptosDP.Enabled = false;
            label3.Enabled = false;
            txt_NombreCon_ConceptosDP.Enabled = false;

            labelMonto.Visible = false;
            txt_Monto_ConceptosDP.Visible = false;
            labelPorcentaje.Visible = false;

            txt_Porcentaje_ConceptosDP.Visible = false;
            btn_Agregar_ConceptosDP.Visible = true;
            btn_Aceptar_ConceptosDP.Visible = false;
            btn_Cancelar_ConceptosDP.Visible = false;
            btn_Modificar_ConceptosDP.Visible = false;
            btn_ModAceptar_ConceptosDP.Visible = false;
            btn_ModCancelar_ConceptosDP.Visible = false;
            btn_Eliminar_ConceptosDP.Visible = false;
        }

        private void btn_Cancelar_ConceptosDP_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btn_Regresar_ConceptosDP_Click(object sender, EventArgs e)
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
        // Variable para almacenar el ID del concepto seleccionado
        private int idConceptoSeleccionado = -1;
        private void dtgv_ConceptosDP_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // Verifica que se haya seleccionado una fila válida
            if (e.RowIndex >= 0)
            {

                // limpa txt
                CmBox_Concepto_ConceptosDP.SelectedIndex = -1;//Es la manera correcta de dejar el ComboBox en un estado sin selección.
                CmBox_Tipo_ConceptosDP.SelectedIndex = -1;//Es la manera correcta de dejar el ComboBox en un estado sin selección.
                txt_NombreCon_ConceptosDP.Clear();
                txt_NombreCon_ConceptosDP.Clear();
                txt_NombreCon_ConceptosDP.Clear();

                label1.Enabled = false;
                CmBox_Concepto_ConceptosDP.Enabled = false;
                label2.Enabled = false;
                CmBox_Tipo_ConceptosDP.Enabled = false;
                label3.Enabled = false;
                txt_NombreCon_ConceptosDP.Enabled = false;
                txt_NombreCon_ConceptosDP.Enabled = false;

                labelMonto.Enabled = false;
                txt_Monto_ConceptosDP.Enabled = false;
                labelPorcentaje.Enabled = false;
                txt_Porcentaje_ConceptosDP.Enabled = false;

                btn_Agregar_ConceptosDP.Visible = true;
                btn_Aceptar_ConceptosDP.Visible = false;
                btn_Cancelar_ConceptosDP.Visible = false;
                btn_Modificar_ConceptosDP.Visible = true;
                btn_ModAceptar_ConceptosDP.Visible = false;
                btn_ModCancelar_ConceptosDP.Visible = false;
                btn_Eliminar_ConceptosDP.Visible = true;


                DataGridViewRow row = dtgv_ConceptosDP.Rows[e.RowIndex];
                // Almacenar el id_PD del concepto seleccionado
                idConceptoSeleccionado = Convert.ToInt32(row.Cells["ID_PercDed"].Value);


                CmBox_Concepto_ConceptosDP.SelectedItem = row.Cells["Tipo"].Value.ToString() == "P" ? "Percepción" : "Deducción";
                txt_NombreCon_ConceptosDP.Text = row.Cells["nombre"].Value.ToString();

                bool esPorcentaje = Convert.ToBoolean(row.Cells["EsPorcetanje"].Value);
                if (esPorcentaje)
                {
                    CmBox_Tipo_ConceptosDP.SelectedItem = "Porcentaje";
                    txt_Porcentaje_ConceptosDP.Text = row.Cells["Valor"].Value.ToString();
                    txt_Monto_ConceptosDP.Clear();
                    labelMonto.Visible = false;
                    txt_Monto_ConceptosDP.Visible = false;
                    labelPorcentaje.Visible = true;
                    txt_Porcentaje_ConceptosDP.Visible = true;
                }
                else
                {
                    CmBox_Tipo_ConceptosDP.SelectedItem = "Monto";
                    txt_Monto_ConceptosDP.Text = row.Cells["Valor"].Value.ToString();
                    txt_Porcentaje_ConceptosDP.Clear();
                    labelMonto.Visible = true;
                    txt_Monto_ConceptosDP.Visible = true;
                    labelPorcentaje.Visible = false;
                    txt_Porcentaje_ConceptosDP.Visible = false;
                }

            }
        }

        private void btn_Modificar_ConceptosDP_Click(object sender, EventArgs e)
        {

            label1.Enabled = true;
            CmBox_Concepto_ConceptosDP.Enabled = true;
            label2.Enabled = true;
            CmBox_Tipo_ConceptosDP.Enabled = true;
            label3.Enabled = true;
            txt_NombreCon_ConceptosDP.Enabled = true;
            labelMonto.Enabled = true;
            txt_Monto_ConceptosDP.Enabled = true;
            labelPorcentaje.Enabled = true;
            txt_Porcentaje_ConceptosDP.Enabled = true;

            

            btn_Agregar_ConceptosDP.Visible = false;
            btn_Aceptar_ConceptosDP.Visible =false;
            btn_Cancelar_ConceptosDP.Visible = false;
            btn_Modificar_ConceptosDP.Visible = false;
            btn_ModAceptar_ConceptosDP.Visible = true;
            btn_ModCancelar_ConceptosDP.Visible =true;
            btn_Eliminar_ConceptosDP.Visible = false;

        }

        private void btn_ModAceptar_ConceptosDP_Click(object sender, EventArgs e)
        {
            // Validar los campos antes de continuar
            if (!ValidarCampos())
            {
                return;
            }

            // Obtener valores desde los controles
            string concepto = CmBox_Concepto_ConceptosDP.SelectedItem.ToString(); // "Percepción" o "Deducción"
            string tipo = CmBox_Tipo_ConceptosDP.SelectedItem.ToString();         // "Monto" o "Porcentaje"
            string nombreConcepto = txt_NombreCon_ConceptosDP.Text;

            // Convertir tipo a 'P' o 'D'
            string tipoBD = concepto == "Percepción" ? "P" : "D";
            bool esPorcentaje = tipo == "Porcentaje";

            // Obtener valor numérico
            decimal valor;
            if (esPorcentaje)
            {
                if (!decimal.TryParse(txt_Porcentaje_ConceptosDP.Text, out valor))
                {
                    MessageBox.Show("Porcentaje inválido.");
                    return;
                }
            }
            else
            {
                if (!decimal.TryParse(txt_Monto_ConceptosDP.Text, out valor))
                {
                    MessageBox.Show("Monto inválido.");
                    return;
                }
            }

            // Actualizar en la base de datos
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                string query = @"UPDATE PercepcionesDeduccion 
                         SET Tipo = @Tipo, nombre = @Nombre, EsPorcetanje = @EsPorcentaje, Valor = @Valor 
                         WHERE ID_PercDed = @ID";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Tipo", tipoBD);
                cmd.Parameters.AddWithValue("@Nombre", nombreConcepto);
                cmd.Parameters.AddWithValue("@EsPorcentaje", esPorcentaje);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@ID", idConceptoSeleccionado);

                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Concepto modificado exitosamente.");
                    LimpiarCampos();
                    mostrarTablaDP(); // Refresca el DataGridView
                }
                else
                {
                    MessageBox.Show("Error al modificar el concepto.");
                }
            }
        }


        private void btn_ModCancelar_ConceptosDP_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btn_Eliminar_ConceptosDP_Click(object sender, EventArgs e)
        {
            // Verifica que se haya seleccionado un concepto
            if (idConceptoSeleccionado == -1)
            {
                MessageBox.Show("Por favor, selecciona un concepto para eliminar.");
                return;
            }

            // Muestra un mensaje de confirmación antes de eliminar
            DialogResult confirmacion = MessageBox.Show("¿Estás seguro de que deseas eliminar este concepto?",
                                                        "Confirmar Eliminación",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    // Comando SQL corregido para eliminar de la tabla correcta
                    string query = "DELETE FROM PercepcionesDeduccion WHERE ID_PercDed = @ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", idConceptoSeleccionado);

                    cn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Concepto eliminado exitosamente.");
                        LimpiarCampos();
                        mostrarTablaDP(); // Actualizar el DataGridView
                        idConceptoSeleccionado = -1; // Resetear el ID después de eliminar
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el concepto.");
                    }
                }
            }
        }


        private void dtgv_ConceptosDP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_Porcentaje_ConceptosDP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Monto_ConceptosDP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_NombreCon_ConceptosDP_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelPorcentaje_Click(object sender, EventArgs e)
        {

        }

        private void labelMonto_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
