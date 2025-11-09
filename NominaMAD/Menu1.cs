using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NominaMAD
{
    public partial class P_Menu1 : Form
    {
        public P_Menu1()
        {
            InitializeComponent();
        }

        private void btn_Empresa_MENU1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            P_Empresa p_Empresa = new P_Empresa();
            // Ocultar el formulario actual (Form1)
            this.Hide();
            // Mostrar el nuevo formulario
            p_Empresa.ShowDialog();
        }
        private void btn_GestionEmpleados_MENU1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            P_GestionEmpleados p_GestionEmpleados = new P_GestionEmpleados();
            // Ocultar el formulario actual (Form1)
            this.Hide();
            // Mostrar el nuevo formulario
            p_GestionEmpleados.ShowDialog();
        }
        private void btn_GestionDepar_MENU1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            P_GestionDepar p_GestionDepar = new P_GestionDepar();


            // Ocultar el formulario actual (Form1)
            this.Hide();

            // Mostrar el nuevo formulario
            p_GestionDepar.ShowDialog();
        }
        private void btn_GestionPuestos_MENU1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            P_GestionPuestos p_GestionPuestos = new P_GestionPuestos();
            // Ocultar el formulario actual (Form1)
            this.Hide();
            // Mostrar el nuevo formulario
            p_GestionPuestos.ShowDialog();
        }
        private void btn_ConceptosDedPer_MENU1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            P_ConceptosDP p_ConceptosDP = new P_ConceptosDP();
            // Ocultar el formulario actual (Form1)
            this.Hide();
            // Mostrar el nuevo formulario
            p_ConceptosDP.ShowDialog();
        }
        private void btn_ReporteGenNomina_MENU1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            P_RepGenNomina p_RepGenNomina = new P_RepGenNomina();
            // Ocultar el formulario actual (Form1)
            this.Hide();
            // Mostrar el nuevo formulario
            p_RepGenNomina.ShowDialog();
        }
        private void btn_ReporteHeadcounter_MENU1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            P_HeadCounter p_HeadCounter = new P_HeadCounter();
            // Ocultar el formulario actual (Form1)
            this.Hide();
            // Mostrar el nuevo formulario
            p_HeadCounter.ShowDialog();
        }
        private void btn_ReciboEmpleado_MENU1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            P_ReciboEmpleado p_ReciboEmpleado = new P_ReciboEmpleado();
            // Ocultar el formulario actual (Form1)
            this.Hide();
            // Mostrar el nuevo formulario
            p_ReciboEmpleado.ShowDialog();
        }
        private void btn_GenerarNomina_MENU1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            P_GenerarNomina p_GenerarNomina= new P_GenerarNomina();
            // Ocultar el formulario actual (Form1)
            this.Hide();
            // Mostrar el nuevo formulario
            p_GenerarNomina.ShowDialog();
        }
        private void btn_RH_MENU1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del nuevo formulario
            P_RH p_RH = new P_RH();
            // Ocultar el formulario actual (Form1)
            this.Hide();
            // Mostrar el nuevo formulario
            p_RH.ShowDialog();
        }
        private void btn_Salir_MENU1_Click(object sender, EventArgs e)
        {
            P_Inicio p_Inicio = new P_Inicio();
            // Ocultar el formulario actual (Form1)
            this.Hide();

            // Mostrar el nuevo formulario
            p_Inicio.ShowDialog();
        }









        private void label2_Click(object sender, EventArgs e)
        {
            
        }
        private void checkBox_Admin_MENU1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
