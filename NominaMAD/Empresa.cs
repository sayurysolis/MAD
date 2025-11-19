using NominaMAD.DAO;
using NominaMAD.Entidad;
using NominaMAD.Resources;
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
        public P_Empresa()
        {
            InitializeComponent();
        }

        private void P_Empresa_Load(object sender, EventArgs e)
        {
            txt_Nombre_empresa.ReadOnly = true;
            txt_RazonSocial_Empresa.ReadOnly = true;
            txt_DomFiscal_Empresa.ReadOnly = true;
            txt_Telelfono_Empresa.ReadOnly = true;
            txt_RegistroPeatronal_Empresa.ReadOnly = true;
            txt_RFC_Empresa.ReadOnly = true;
            dtp_FechaInOpera_Empresa.Enabled = false;
            CargarDatosEmpresa();

            #region formatos
            txt_RazonSocial_Empresa.Multiline = true;
            txt_RazonSocial_Empresa.ScrollBars = ScrollBars.Horizontal;
            txt_RazonSocial_Empresa.WordWrap = false;

            txt_DomFiscal_Empresa.Multiline = true;
            txt_DomFiscal_Empresa.ScrollBars = ScrollBars.Horizontal;

            txt_DomFiscal_Empresa.WordWrap = false;
            dtp_FechaInOpera_Empresa.Format = DateTimePickerFormat.Custom;
            dtp_FechaInOpera_Empresa.CustomFormat = "dd-MMM-yyyy";
            #endregion
            #region botones
            Btn_AceptarMod.Visible= false;
            BTN_Cancelar.Visible = false;
            #endregion

        }
        private void CargarDatosEmpresa()
        {
            EMPRESAS empresa = EmpresaDAO.ObtenerEmpresas();
            if (empresa != null)
            {
                txt_Nombre_empresa.Text = empresa.nombre;
                txt_RazonSocial_Empresa.Text = empresa.RazonSocial;
                txt_DomFiscal_Empresa.Text = empresa.DomicilioFiscal;
                txt_Telelfono_Empresa.Text = empresa.contacto;
                txt_RegistroPeatronal_Empresa.Text = empresa.registroPatronal;
                txt_RFC_Empresa.Text = empresa.RFC;
                dtp_FechaInOpera_Empresa.Value = empresa.fechaInicio;
            }
            else
            {
                MessageBox.Show("No se encontró la informacón de la empresa");
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
        private void BTN_Modificar_Click(object sender, EventArgs e)
        {
            txt_Nombre_empresa.ReadOnly = false;
            txt_RazonSocial_Empresa.ReadOnly = false;
            txt_DomFiscal_Empresa.ReadOnly = false;
            txt_Telelfono_Empresa.ReadOnly = false;
            txt_RegistroPeatronal_Empresa.ReadOnly = false;
            txt_RFC_Empresa.ReadOnly = false;
            //dtp_FechaInOpera_Empresa.Enabled = true;
            Btn_AceptarMod.Visible=true;
            BTN_Cancelar.Visible = true;

        }
        private void Btn_AceptarMod_Click(object sender, EventArgs e)
        {
            EMPRESAS empresa = new EMPRESAS
            {
                nombre = txt_Nombre_empresa.Text,
                RazonSocial = txt_RazonSocial_Empresa.Text,
                DomicilioFiscal = txt_DomFiscal_Empresa.Text,
                contacto = txt_Telelfono_Empresa.Text,
                registroPatronal = txt_RegistroPeatronal_Empresa.Text,
                RFC = txt_RFC_Empresa.Text
            };

            int result = EmpresaDAO.EditarEmpresa(empresa);
            if (result > 0)
            {
                MessageBox.Show("Empresa actualizada correctamente.");

                // Volver a modo solo lectura
                txt_Nombre_empresa.ReadOnly = true;
                txt_RazonSocial_Empresa.ReadOnly = true;
                txt_DomFiscal_Empresa.ReadOnly = true;
                txt_Telelfono_Empresa.ReadOnly = true;
                txt_RegistroPeatronal_Empresa.ReadOnly = true;
                txt_RFC_Empresa.ReadOnly = true;
                dtp_FechaInOpera_Empresa.Enabled = false;

                // Mostrar botón modificar y ocultar aceptar/cancelar
                BTN_Modificar.Visible = true;
                Btn_AceptarMod.Visible = false;
                BTN_Cancelar.Visible = false;
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la empresa.");
            }
        }
        private void BTN_Cancelar_Click(object sender, EventArgs e)
        {
            CargarDatosEmpresa();

            txt_Nombre_empresa.ReadOnly = true;
            txt_RazonSocial_Empresa.ReadOnly = true;
            txt_DomFiscal_Empresa.ReadOnly = true;
            txt_Telelfono_Empresa.ReadOnly = true;
            txt_RegistroPeatronal_Empresa.ReadOnly = true;
            txt_RFC_Empresa.ReadOnly = true;
            dtp_FechaInOpera_Empresa.Enabled = false;

            BTN_Modificar.Visible = true;
            Btn_AceptarMod.Visible = false;
            BTN_Cancelar.Visible = false;
        }
        
        
        private void dtp_FechaInOpera_Empresa_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}