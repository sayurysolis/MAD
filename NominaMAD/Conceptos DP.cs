using NominaMAD.Entidad;
using NominaMAD.Resources;
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
        
        string modificarOpcion;

        private void P_ConceptosDP_Load(object sender, EventArgs e)
        {
            txt_NombreCon_ConceptosDP.MaxLength = 30;

            CmBox_Concepto_ConceptosDP.Items.Add("Deducción");
            CmBox_Concepto_ConceptosDP.Items.Add("Percepción");

            CmBox_Tipo_ConceptosDP.Items.Add("Monto");
            CmBox_Tipo_ConceptosDP.Items.Add("Porcentaje");

            CmBox_Tipo_ConceptosDP.SelectedIndexChanged += CmBox_Tipo_ConceptosDP_SelectedIndexChanged;
        }
        private void CmBox_Tipo_ConceptosDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ocultar y deshabilitar ambos al inicio
            labelMonto.Visible = false;
            txt_Monto_ConceptosDP.Visible = false;
            txt_Monto_ConceptosDP.Enabled = false;

            labelPorcentaje.Visible = false;
            txt_Porcentaje_ConceptosDP.Visible = false;
            txt_Porcentaje_ConceptosDP.Enabled = false;

            int index = CmBox_Tipo_ConceptosDP.SelectedIndex;
            if (index == 0) // Monto
            {
                labelMonto.Visible = true;
                txt_Monto_ConceptosDP.Visible = true;
                txt_Monto_ConceptosDP.Enabled = true;
                txt_Monto_ConceptosDP.Focus();
            }
            else if (index == 1) // Porcentaje
            {
                labelPorcentaje.Visible = true;
                txt_Porcentaje_ConceptosDP.Visible = true;
                txt_Porcentaje_ConceptosDP.Enabled = true;
                txt_Porcentaje_ConceptosDP.Focus();
            }
        }
        private void CmBox_Concepto_ConceptosDP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void HabilitarCampos(bool habilitar)
        {
            label1.Enabled = habilitar;
            CmBox_Concepto_ConceptosDP.Enabled = habilitar;
            label2.Enabled = habilitar;
            CmBox_Tipo_ConceptosDP.Enabled = habilitar;
            label3.Enabled = habilitar;
            txt_NombreCon_ConceptosDP.Enabled = habilitar;

            labelMonto.Enabled = habilitar;
            txt_Monto_ConceptosDP.Enabled = habilitar;

            labelPorcentaje.Enabled = habilitar;
            txt_Porcentaje_ConceptosDP.Enabled = habilitar;
        }

        private bool ValidarCampos()
        {
            //Validar que se haya seleccionado un departamento y un puesto
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
            


       

        private void btn_Agregar_ConceptosDP_Click(object sender, EventArgs e)
        {
            HabilitarCampos(true);

            btn_Agregar_ConceptosDP.Visible = false;
            btn_Aceptar_ConceptosDP.Visible = true;
            btn_Cancelar_ConceptosDP.Visible = true;

            if (CmBox_Tipo_ConceptosDP.SelectedIndex != -1)
                CmBox_Tipo_ConceptosDP_SelectedIndexChanged(null, null);


        }
        private void btn_Aceptar_ConceptosDP_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            decimal valor = 0;
            if (CmBox_Tipo_ConceptosDP.SelectedIndex == 0) // Monto
                valor = decimal.Parse(txt_Monto_ConceptosDP.Text);
            else // Porcentaje
                valor = decimal.Parse(txt_Porcentaje_ConceptosDP.Text);

            Concepto concepto = new Concepto
            {
                Tipo = CmBox_Concepto_ConceptosDP.SelectedIndex == 1, // Percepción = true
                Nombre = txt_NombreCon_ConceptosDP.Text,
                EsPorcentaje = CmBox_Tipo_ConceptosDP.SelectedIndex == 1,
                Valor = valor,
                General = cadames.Checked
            };

            try
            {
                ConceptoDAO dao = new ConceptoDAO();
                // dao.AgregarConcepto(concepto);

                MessageBox.Show("Concepto agregado correctamente.");
                LimpiarCampos();
                // mostrarTablaDP();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar concepto: " + ex.Message);
            }
        }
        private void btn_Cancelar_ConceptosDP_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }


        //Variable para almacenar el ID del concepto seleccionado
        private int idConceptoSeleccionado = -1;
    

        private void btn_Modificar_ConceptosDP_Click(object sender, EventArgs e)
        {
            if (idConceptoSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un concepto para modificar.");
                return;
            }

            HabilitarCampos(true);

            btn_Modificar_ConceptosDP.Visible = false;
            btn_ModAceptar_ConceptosDP.Visible = true;
            btn_ModCancelar_ConceptosDP.Visible = true;

            if (CmBox_Tipo_ConceptosDP.SelectedIndex != -1)
                CmBox_Tipo_ConceptosDP_SelectedIndexChanged(null, null);

        }
        private void btn_ModAceptar_ConceptosDP_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            decimal valor = 0;
            if (CmBox_Tipo_ConceptosDP.SelectedIndex == 0) // Monto
                valor = decimal.Parse(txt_Monto_ConceptosDP.Text);
            else // Porcentaje
                valor = decimal.Parse(txt_Porcentaje_ConceptosDP.Text);

            Concepto concepto = new Concepto
            {
                ID_Conceptos = idConceptoSeleccionado,
                Tipo = CmBox_Concepto_ConceptosDP.SelectedIndex == 1,
                Nombre = txt_NombreCon_ConceptosDP.Text,
                EsPorcentaje = CmBox_Tipo_ConceptosDP.SelectedIndex == 1,
                Valor = valor,
                General = cadames.Checked
            };

            try
            {
                ConceptoDAO.EditarConcepto(concepto);


                MessageBox.Show("Concepto modificado correctamente.");
                LimpiarCampos();
                // mostrarTablaDP();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar concepto: " + ex.Message);
            }
        }
        private void btn_ModCancelar_ConceptosDP_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btn_Eliminar_ConceptosDP_Click(object sender, EventArgs e)
        {
            if (idConceptoSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un concepto para eliminar.");
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de eliminar este concepto?", "Confirmar", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    ConceptoDAO dao = new ConceptoDAO();
                     dao.BajaConcepto (idConceptoSeleccionado);

                    MessageBox.Show("Concepto eliminado correctamente.");
                    LimpiarCampos();
                    // mostrarTablaDP();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar concepto: " + ex.Message);
                }
            }
        
        }

        private void btn_Regresar_ConceptosDP_Click(object sender, EventArgs e)
        {
            if (P_Inicio.MMenuAoE == 1)
            {
                P_Menu1 p_Menu1 = new P_Menu1();
                this.Hide();
                p_Menu1.ShowDialog();

            }
            else
            {
                if (P_Inicio.MMenuAoE == 2)
                {
                    P_Menu2 p_Menu2 = new P_Menu2();
                    this.Hide();
                    p_Menu2.ShowDialog();

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

        private void mostrarTablaDP()
        {
            ConceptoDAO dao = new ConceptoDAO();
            dtgv_ConceptosDP.DataSource = dao.ObtenerConceptos();
        }
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
                idConceptoSeleccionado = Convert.ToInt32(row.Cells["ID_Conceptos"].Value);


                CmBox_Concepto_ConceptosDP.SelectedItem = row.Cells["Tipo"].Value.ToString() == "P" ? "Percepción" : "Deducción";
                txt_NombreCon_ConceptosDP.Text = row.Cells["nombre"].Value.ToString();

                bool esPorcentaje = Convert.ToBoolean(row.Cells["EsPorcentaje"].Value);

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

        private void cadames_CheckedChanged(object sender, EventArgs e)
        {
            // Si está marcado, el concepto es general (aplica cada mes)
            if (cadames.Checked)
            {
                // Aquí activas/desactivas campos si quieres
                txt_Monto_ConceptosDP.Enabled = true;
                txt_Porcentaje_ConceptosDP.Enabled = true;
            }
            else
            {
                // Si no está marcado, solo permites campos según tipo
                CmBox_Tipo_ConceptosDP_SelectedIndexChanged(sender, e);
            }
        }
    }
}
