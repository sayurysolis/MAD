using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
    public partial class P_GestionDepar : Form
    {
        private int ColumnaSeleccionada = 0;
        public P_GestionDepar()
        {
            InitializeComponent();
        }
        
        string modificarOpcion;
        private void P_GestionDepar_Load(object sender, EventArgs e)
        {
            txt_Departamento_GestDepar.MaxLength = 20;
            //Que no se vea
            btn_Guardar_GestionDepar.Visible = false;
            btn_Modificar_GestionDepar.Visible = false;
            btn_limpiar_GestionDepar.Visible = false;
            btn_AceptarMod_GestionDepar.Visible = false;
            btn_CancelarMod_GestionDepar.Visible = false;

            btn_Agregar_GestionDepar.Visible = true;
            //Que no pueda escribir
            txt_Departamento_GestDepar.Enabled = false;
            mostrarTablaDepart();
        }
        private void mostrarTablaDepart()
        {
            dtgv_GestionDepar.DataSource = DepartamentoDAO.Get_Departamento();
        }
        bool existe = false;
        private void ValidarExistencia()
        {
            //bool existe = false;

            // Recorrer cada fila en el DataGridView
            foreach (DataGridViewRow fila in dtgv_GestionDepar.Rows)
            {
                // Comparar el valor de la columna que deseas verificar (NombreDepartamento en este caso)
                if (fila.Cells["nombre"].Value != null &&
                    fila.Cells["nombre"].Value.ToString().ToLower() == txt_Departamento_GestDepar.Text.ToLower())
                {
                    existe = true;
                    break;
                }
            }

        }
        private void btn_Agregar_GestionDepar_Click(object sender, EventArgs e)
        {
    
            txt_Departamento_GestDepar.Text = "";
                //mostrar botones
                btn_Guardar_GestionDepar.Visible = true;
                btn_Modificar_GestionDepar.Visible = false;
                btn_limpiar_GestionDepar.Visible = true;
                btn_Agregar_GestionDepar.Visible = false;
                btn_AceptarMod_GestionDepar.Visible = false;
                btn_CancelarMod_GestionDepar.Visible = false;

                //habilitar txts
                txt_Departamento_GestDepar.Enabled = true;
               
           
        }

        private void btn_Guardar_GestionDepar_Click(object sender, EventArgs e)                   
        {
            if (txt_Departamento_GestDepar.Text == "")
            {
                MessageBox.Show("Algun Dato Vacio");
            }
            else
            {
                ValidarExistencia();
                if (existe == false)
                {
                    DEPARTAMENTO depa = new DEPARTAMENTO();
                    depa.nombre = txt_Departamento_GestDepar.Text;
                    DepartamentoDAO.Add_Departamento(depa);
                    txt_Departamento_GestDepar.Text = "";
                    mostrarTablaDepart();


                }
                else { MessageBox.Show("Este departamento ya existe. Por favor, ingresa un departamento diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); existe = false; }
            }
        }

        private void btn_Modificar_GestionDepar_Click(object sender, EventArgs e)
        {
            // update Departamento set NombreDepartamento='zxc', SueldoBase = 123 where NombreDepartamento='vxcv'
            modificarOpcion = txt_Departamento_GestDepar.Text;
            txt_Departamento_GestDepar.Enabled = true;
           // txt_SueldoBase_GestionDepar.Enabled = true;
            //txt_Empleados_GestionDepar.Enabled = true;

            btn_Agregar_GestionDepar.Visible = false;

            btn_AceptarMod_GestionDepar.Visible = true;
            btn_CancelarMod_GestionDepar.Visible = true;
            btn_Modificar_GestionDepar.Visible = false;
        }

        private void btn_limpiar_GestionDepar_Click(object sender, EventArgs e)
        {
            txt_Departamento_GestDepar.Text = "";
           // txt_SueldoBase_GestionDepar.Text = "";
            //txt_Empleados_GestionDepar.Text = "";
            txt_Departamento_GestDepar.Enabled = false;
           // txt_SueldoBase_GestionDepar.Enabled = false;
          //  txt_Empleados_GestionDepar.Enabled = false;

            //oculta botones
            btn_Guardar_GestionDepar.Visible = false;
            btn_Modificar_GestionDepar.Visible = false;
            btn_AceptarMod_GestionDepar.Visible = false;
            btn_CancelarMod_GestionDepar.Visible = false;
            btn_limpiar_GestionDepar.Visible = false;
            btn_Agregar_GestionDepar.Visible = true;
        }
        private void eliminar()
        {

            // delete from Departamento where NombreDepartamento = 'asd'
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("delete from Departamento where NombreDepartamento = 'asd'", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                cmd.ExecuteNonQuery();
                mostrarTablaDepart();
            }
        }

        private void btn_AceptarMod_GestionDepar_Click(object sender, EventArgs e)
        {

            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                //SqlCommand cmd = new SqlCommand("update Departamento set NombreDepartamento='" + txt_Departamento_GestDepar.Text + "', SueldoBase =" + txt_SueldoBase_GestionDepar.Text + " where NombreDepartamento='" + modificarOpcion + "'", cn);
                SqlCommand cmd = new SqlCommand("UPDATE Departamento SET NombreDepartamento = '" + txt_Departamento_GestDepar.Text + "' WHERE NombreDepartamento = '" + modificarOpcion + "'", cn);

                cmd.CommandType = CommandType.Text;
                cn.Open();
                cmd.ExecuteNonQuery();
                mostrarTablaDepart();
            }

            txt_Departamento_GestDepar.Text = "";
            //txt_SueldoBase_GestionDepar.Text = "";
           // txt_Empleados_GestionDepar.Text = "";
            txt_Departamento_GestDepar.Enabled = false;
           // txt_SueldoBase_GestionDepar.Enabled = false;
            //txt_Empleados_GestionDepar.Enabled = false;

            //oculta botones
            btn_Guardar_GestionDepar.Visible = false;
            btn_Modificar_GestionDepar.Visible = false;
            btn_AceptarMod_GestionDepar.Visible = false;
            btn_CancelarMod_GestionDepar.Visible = false;
            btn_limpiar_GestionDepar.Visible = false;
            btn_Agregar_GestionDepar.Visible = true;
            mostrarTablaDepart();
        }

        private void btn_CancelarMod_GestionDepar_Click(object sender, EventArgs e)
        {
            txt_Departamento_GestDepar.Text = "";
            //txt_SueldoBase_GestionDepar.Text = "";
            //txt_Empleados_GestionDepar.Text = "";
            txt_Departamento_GestDepar.Enabled = false;
            //txt_SueldoBase_GestionDepar.Enabled = false;
            ///txt_Empleados_GestionDepar.Enabled = false;

            //oculta botones
            btn_Guardar_GestionDepar.Visible = false;
            btn_Modificar_GestionDepar.Visible = false;
            btn_AceptarMod_GestionDepar.Visible = false;
            btn_CancelarMod_GestionDepar.Visible = false;
            btn_limpiar_GestionDepar.Visible = false;
            btn_Agregar_GestionDepar.Visible = true;
        }

        private void btn_Regresar_GestionDepar_Click(object sender, EventArgs e)
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

        private void dtgv_GestionDepar_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            ColumnaSeleccionada = e.RowIndex;

            if (ColumnaSeleccionada != -1)
            {
                //limpa txt
                txt_Departamento_GestDepar.Text = "";
               //txt_SueldoBase_GestionDepar.Text = "";
                //txt_Empleados_GestionDepar.Text = "";
                //ingresa datos en los txt
                //txt_Departamento_GestDepar.Text = (string)dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[0].Value;
                //txt_SueldoBase_GestionDepar.Text = (string)dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[1].Value;
                //txt_Empleados_GestionDepar.Text = (string)dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[2].Value;

                txt_Departamento_GestDepar.Text = dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[1].Value.ToString();
               // txt_SueldoBase_GestionDepar.Text = dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[2].Value.ToString();
               // txt_Empleados_GestionDepar.Text = dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[2].Value.ToString();


                //desabilita txt
                txt_Departamento_GestDepar.Enabled = false;
                //txt_SueldoBase_GestionDepar.Enabled = false;
                //txt_Empleados_GestionDepar.Enabled = false;
                //muestra boton modificar y oculta los demas
                btn_Guardar_GestionDepar.Visible = false;
                btn_limpiar_GestionDepar.Visible = false;
                btn_Agregar_GestionDepar.Visible = true;
                btn_Modificar_GestionDepar.Visible = true;
                btn_AceptarMod_GestionDepar.Visible = false;
                btn_CancelarMod_GestionDepar.Visible = false;
            }
        }

        private void txt_Departamento_GestDepar_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtgv_GestionDepar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
