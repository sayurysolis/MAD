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
            dtgv_GestionDepar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        bool existe = false;
        private void ValidarExistencia()
        {
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
                //btn_eliminar.Visible = true;
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
            
            modificarOpcion = txt_Departamento_GestDepar.Text;
            txt_Departamento_GestDepar.Enabled = true;
            btn_Agregar_GestionDepar.Visible = false;
            btn_AceptarMod_GestionDepar.Visible = true;
            btn_CancelarMod_GestionDepar.Visible = true;
            btn_Modificar_GestionDepar.Visible = false;
          
        }
        private void btn_AceptarMod_GestionDepar_Click(object sender, EventArgs e)
        {
          
                int idDepa = Convert.ToInt32(dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[0].Value);
                string nuevoNombre = txt_Departamento_GestDepar.Text.Trim();
                DEPARTAMENTO depa = new DEPARTAMENTO
                {
                    ID_Departamento = idDepa,
                    nombre = nuevoNombre
                };

                DepartamentoDAO.EditarDepartamento(depa); 

                mostrarTablaDepart();

                txt_Departamento_GestDepar.Text = "";
                txt_Departamento_GestDepar.Enabled = false;
                btn_Guardar_GestionDepar.Visible = false;
                btn_Modificar_GestionDepar.Visible = false;
                btn_AceptarMod_GestionDepar.Visible = false;
                btn_CancelarMod_GestionDepar.Visible = false;
                btn_Agregar_GestionDepar.Visible = true;
                   
        }
        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (ColumnaSeleccionada != -1)
            {
                int idSeleccionado = Convert.ToInt32(dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[0].Value);
                DepartamentoDAO.EliminarDep(idSeleccionado);
                mostrarTablaDepart();
            }
            else
            {
                MessageBox.Show("Selecciona un departamento antes de eliminar.");
            }

        }
        private void btn_CancelarMod_GestionDepar_Click(object sender, EventArgs e)
        {
            txt_Departamento_GestDepar.Text = "";
            txt_Departamento_GestDepar.Enabled = false;
            //oculta botones
            btn_Guardar_GestionDepar.Visible = false;
            btn_Modificar_GestionDepar.Visible = false;
            btn_AceptarMod_GestionDepar.Visible = false;
            btn_CancelarMod_GestionDepar.Visible = false;
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
                txt_Departamento_GestDepar.Text = dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[1].Value.ToString();
                //desabilita txt
                txt_Departamento_GestDepar.Enabled = false;

                btn_Guardar_GestionDepar.Visible = false;
                btn_Agregar_GestionDepar.Visible = true;
                btn_Modificar_GestionDepar.Visible = true;
                btn_AceptarMod_GestionDepar.Visible = false;
                btn_CancelarMod_GestionDepar.Visible = false;
            }
        }
        
     
    }
}
