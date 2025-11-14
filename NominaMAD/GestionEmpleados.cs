using NominaMAD.Resources;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NominaMAD
{
    public partial class P_GestionEmpleados : Form
    {
        public P_GestionEmpleados()
        {
            InitializeComponent();
            // Asignar el evento de solo números
            txt_NumEmplea_GestionEmpleados.KeyPress += SoloNumeros_KeyPress;
            txt_CodPost_Gestio.KeyPress += SoloNumeros_KeyPress;
            txt_Telefono_GestionEmpleados.KeyPress += SoloNumeros_KeyPress;
            txt_NSS_GestionEmpleados.KeyPress += SoloNumeros_KeyPress;
            txt_NumCuenta_GestionEmpleados.KeyPress += SoloNumeros_KeyPress;
            txt_SalarioDiario_GestionEmpleados.KeyPress += SoloNumeros_KeyPress;
            txt_SalarioDIntegrado_GestionEmpleados.KeyPress += SoloNumeros_KeyPress;
            txt_Nombres_GestionEmpleados.KeyPress += SoloLetras_KeyPress;
            //solo letras
            txt_ApellPaterno_GestionEmpleados.KeyPress += SoloLetras_KeyPress;
            txt_ApellMaterno_GestionEmpleados.KeyPress += SoloLetras_KeyPress;
            txt_Banco_GestionEmpleados.KeyPress += SoloLetras_KeyPress;

            desactivarCamposCaptura();       

            btn_Agregar_GestionEmpleados.Visible = true;
            btn_Modificar_GestionEmpleados.Visible = false;
            btn_Eliminar_GestionEmpleados.Visible = false;

            btn_AceptarMOD_GestionEmpleados.Visible = false;
            btn_CancelarMOD_GestionEmpleados.Visible=false;

            btn_AgregarAceptar_GestionEmpleados.Visible = false;
            btn_AgregarCancelar_GestionEmpleados.Visible = false;


            txt_Nombres_GestionEmpleados.MaxLength = 25;
            txt_ApellPaterno_GestionEmpleados.MaxLength = 20;
            txt_ApellMaterno_GestionEmpleados.MaxLength = 20;
            txt_Curp_GestionEmpleados.MaxLength = 18;
            txt_NSS_GestionEmpleados.MaxLength = 11;
            txt_RFC_GestionEmpleados.MaxLength = 13;
            txt_Banco_GestionEmpleados.MaxLength = 15;
            txt_NumCuenta_GestionEmpleados.MaxLength = 16;
            txt_Telefono_GestionEmpleados.MaxLength = 10;
            txt_SalarioDiario_GestionEmpleados.MaxLength = 15;
           
        }

        #region Formato
        private void desactivarCamposCaptura()
        {

            txt_MosNumEmplea_GestionEmpleados.Enabled = false;
            txt_Nombres_GestionEmpleados.Enabled = false;
            txt_ApellPaterno_GestionEmpleados.Enabled = false;
            txt_ApellMaterno_GestionEmpleados.Enabled = false;
            dateTimer_FechaNacim_GestionEmpleados.Enabled = false;

            txt_DomCompleto_GestionEmpleados.Enabled = false; //CALLE
            txt_numero_gestEmp.Enabled = false;
            txt_Colonia_Gestio.Enabled = false;
            txt_Municiipo_Gestio.Enabled = false;
            txt_Estado_Gestio.Enabled = false;
            txt_CodPost_Gestio.Enabled = false;
            txt_Email_GestionEmpleados.Enabled = false;
            txt_Telefono_GestionEmpleados.Enabled = false;
            txt_NSS_GestionEmpleados.Enabled = false;
            txt_Curp_GestionEmpleados.Enabled = false;
            txt_RFC_GestionEmpleados.Enabled = false;

            Cmbox_Departamento_GestionEmpleados.Enabled = false;
            CmBox_Puesto_GestionEmpleados.Enabled = false;


            txt_Banco_GestionEmpleados.Enabled = false;
            txt_NumCuenta_GestionEmpleados.Enabled = false;
            txt_SalarioDiario_GestionEmpleados.Enabled = false;
            txt_SalarioDIntegrado_GestionEmpleados.Enabled = false;
            dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = false;

        }
        private void activarCamposCaptura()
        {

            txt_MosNumEmplea_GestionEmpleados.Enabled = true;
            txt_Nombres_GestionEmpleados.Enabled = true;
            txt_ApellPaterno_GestionEmpleados.Enabled = true;
            txt_ApellMaterno_GestionEmpleados.Enabled = true;
            dateTimer_FechaNacim_GestionEmpleados.Enabled = true;

            txt_DomCompleto_GestionEmpleados.Enabled = true; //CALLE
            txt_numero_gestEmp.Enabled = true;
            txt_Colonia_Gestio.Enabled = true;
            txt_Municiipo_Gestio.Enabled = true;
            txt_Estado_Gestio.Enabled = true;
            txt_CodPost_Gestio.Enabled = true;
            txt_Email_GestionEmpleados.Enabled = true;
            txt_Telefono_GestionEmpleados.Enabled = true;
            txt_NSS_GestionEmpleados.Enabled = true;
            txt_Curp_GestionEmpleados.Enabled = true;
            txt_RFC_GestionEmpleados.Enabled = true;

            Cmbox_Departamento_GestionEmpleados.Enabled = true;
            CmBox_Puesto_GestionEmpleados.Enabled = true;


            txt_Banco_GestionEmpleados.Enabled = true;
            txt_NumCuenta_GestionEmpleados.Enabled = true;
            txt_SalarioDiario_GestionEmpleados.Enabled = true;
            txt_SalarioDIntegrado_GestionEmpleados.Enabled = true;
            dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = true;

        }
        #endregion



        string modificarOpcion;

        int idPeriodoActual;
        string MesPeriodo;
        int AnoPeriodo;
        private int anoSeleccionado;
        private string mes;


        private void P_GestionEmpleados_Load(object sender, EventArgs e)
        {
            // Llenar ComboBox de Departamento
            txt_NumEmplea_GestionEmpleados.MaxLength = 4;
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string queryDepartamento = "SELECT ID_Departamento, Nombre FROM Departamento";
                SqlDataAdapter daDepartamento = new SqlDataAdapter(queryDepartamento, cn);
                DataTable dtDepartamento = new DataTable();
                daDepartamento.Fill(dtDepartamento);

                Cmbox_Departamento_GestionEmpleados.DataSource = dtDepartamento;
                Cmbox_Departamento_GestionEmpleados.DisplayMember = "Nombre";
                Cmbox_Departamento_GestionEmpleados.ValueMember = "ID_Departamento";
            }

            // Llenar ComboBox de Puesto
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string queryPuesto = "SELECT ID_Puesto, Nombre FROM Puesto";
                SqlDataAdapter daPuesto = new SqlDataAdapter(queryPuesto, cn);
                DataTable dtPuesto = new DataTable();
                daPuesto.Fill(dtPuesto);

                CmBox_Puesto_GestionEmpleados.DataSource = dtPuesto;
                CmBox_Puesto_GestionEmpleados.DisplayMember = "Nombre";
                CmBox_Puesto_GestionEmpleados.ValueMember = "ID_Puesto";
            }
        }        

      
        
        private void btn_Buscar_GestionEmpleados_Click(object sender, EventArgs e)
        {

            string NumEmplBus = txt_NumEmplea_GestionEmpleados.Text;

            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Empleado WHERE ID_Empleado = @idEmpleado";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", NumEmplBus);


                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // Si el empleado existe
                {
                    txt_MosNumEmplea_GestionEmpleados.Text = reader["ID_Empleado"].ToString();
                    txt_Nombres_GestionEmpleados.Text = reader["Nombre"].ToString();
                    txt_ApellPaterno_GestionEmpleados.Text = reader["ApellidoPaterno"].ToString();
                    txt_ApellMaterno_GestionEmpleados.Text = reader["ApellidoMaterno"].ToString();

                    Cmbox_Departamento_GestionEmpleados.SelectedValue = reader["DepID"];
                    CmBox_Puesto_GestionEmpleados.SelectedValue = reader["PuestoID"];

                    dateTimer_FechaNacim_GestionEmpleados.Value = Convert.ToDateTime(reader["FechaNacimiento"]);
                    txt_Curp_GestionEmpleados.Text = reader["CURP"].ToString();
                    txt_NSS_GestionEmpleados.Text = reader["NSS"].ToString();
                    txt_RFC_GestionEmpleados.Text = reader["RFC"].ToString();
                    txt_DomCompleto_GestionEmpleados.Text = reader["Direccion"].ToString();
                    txt_Banco_GestionEmpleados.Text = reader["Banco"].ToString();
                    txt_NumCuenta_GestionEmpleados.Text = reader["NumeroCuenta"].ToString();
                    txt_Email_GestionEmpleados.Text = reader["Email"].ToString();
                    txt_Telefono_GestionEmpleados.Text = reader["Telefono"].ToString();
                    dateTimer_FechaIngreEmpr_GestionEmpleados.Value = Convert.ToDateTime(reader["FechaIngreso"]);

                    txt_SalarioDiario_GestionEmpleados.Text = reader["SalarioDiario"].ToString();
                    txt_SalarioDIntegrado_GestionEmpleados.Text = reader["SalarioDiarioIntegrado"].ToString();
                    bool activo = Convert.ToBoolean(reader["estatus"]);
                   

                    btn_Modificar_GestionEmpleados.Visible = true;
                    btn_Agregar_GestionEmpleados.Visible = false;
                    btn_Eliminar_GestionEmpleados.Visible = true;
                }


            }
            }

        private void btn_Modificar_GestionEmpleados_Click(object sender, EventArgs e)
        {
            txt_Nombres_GestionEmpleados.Enabled = true;
            txt_ApellPaterno_GestionEmpleados.Enabled = true;
            txt_ApellMaterno_GestionEmpleados.Enabled = true;
            Cmbox_Departamento_GestionEmpleados.Enabled = true;
            CmBox_Puesto_GestionEmpleados.Enabled = true;
            dateTimer_FechaNacim_GestionEmpleados.Enabled = true;
            txt_Curp_GestionEmpleados.Enabled = true;
            txt_NSS_GestionEmpleados.Enabled = true;
            txt_RFC_GestionEmpleados.Enabled = true;

            



            txt_Banco_GestionEmpleados.Enabled = true;
            txt_NumCuenta_GestionEmpleados.Enabled = true;
            txt_Email_GestionEmpleados.Enabled = true; ;
            txt_Telefono_GestionEmpleados.Enabled = true;
            dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = true;
            dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = false;
            // txt_NumEmplea_GestionEmpleados.Enabled = false;

            txt_SalarioDiario_GestionEmpleados.Enabled = true;
            txt_SalarioDIntegrado_GestionEmpleados.Enabled = false;
          

            btn_Modificar_GestionEmpleados.Visible = false;
            btn_Agregar_GestionEmpleados.Visible = false;
            btn_Eliminar_GestionEmpleados.Visible = false;
            btn_AceptarMOD_GestionEmpleados.Visible = true;
            btn_CancelarMOD_GestionEmpleados.Visible = true;
            btn_Buscar_GestionEmpleados.Visible = false;
        }

        private void btn_AceptarMOD_GestionEmpleados_Click(object sender, EventArgs e)
        {

            int idEmpleado = int.Parse(txt_MosNumEmplea_GestionEmpleados.Text);
          
            decimal salarioDiario = 0;
            decimal salarioDiarioIntegrado = 0;

            // Verificar si el salario diario ha cambiado y recalcular el salario diario integrado
            if (!string.IsNullOrEmpty(txt_SalarioDiario_GestionEmpleados.Text) &&
                decimal.TryParse(txt_SalarioDiario_GestionEmpleados.Text, out salarioDiario))
            {
                salarioDiarioIntegrado = salarioDiario * 1.0493m;
            }
            //2020, 01,10
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                // Crear el query SQL
                string query = @"UPDATE Empleado SET 
                Nombre = @Nombre, 
                ApellidoPaterno = @ApellidoPaterno, 
                ApellidoMaterno = @ApellidoMaterno, 
                DepID = @Departamento, 
                PuestoID = @Puesto, 
                FechaNacimiento = @FechaNacimiento, 
                CURP = @Curp, 
                NSS = @NSS, 
                RFC = @RFC, 
                Direccion = @Domicilio, 
                Banco = @Banco, 
                NumeroCuenta = @NumeroCuenta, 
                Email = @Email, 
                Telefono = @Telefono, 
                estatus = @NuevoEstatusActivo, 
                SalarioDiario = @SalarioDiario, 
                SalarioDiarioIntegrado = @SalarioDiarioIntegrado,
                FechaIngreso = CASE 
                WHEN estatus = 0 AND @NuevoEstatusActivo = 1 THEN @FechaActual 
                ELSE FechaIngreso 
                END 
                WHERE ID_Empleado = @idEmpleado;";

                SqlCommand cmd = new SqlCommand(query, cn);

                // Asignar valores a los parámetros
                cmd.Parameters.AddWithValue("@Nombre", txt_Nombres_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", txt_ApellPaterno_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", txt_ApellMaterno_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Departamento", Cmbox_Departamento_GestionEmpleados.SelectedValue);
                cmd.Parameters.AddWithValue("@Puesto", CmBox_Puesto_GestionEmpleados.SelectedValue);
                cmd.Parameters.AddWithValue("@FechaNacimiento", dateTimer_FechaNacim_GestionEmpleados.Value);
                cmd.Parameters.AddWithValue("@Curp", txt_Curp_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@NSS", txt_NSS_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@RFC", txt_RFC_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Domicilio", txt_DomCompleto_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Banco", txt_Banco_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@NumeroCuenta", txt_NumCuenta_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Email", txt_Email_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Telefono", txt_Telefono_GestionEmpleados.Text);
                
                cmd.Parameters.AddWithValue("@SalarioDiario", salarioDiario);
                cmd.Parameters.AddWithValue("@SalarioDiarioIntegrado", salarioDiarioIntegrado);
                // Fecha actual para reactivación
                cmd.Parameters.AddWithValue("@FechaActual", DateTime.Now);

                // ID del empleado
                cmd.Parameters.AddWithValue("@idEmpleado", int.Parse(txt_MosNumEmplea_GestionEmpleados.Text));

                
                int rowsAffected = cmd.ExecuteNonQuery();
                

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Empleado actualizado correctamente.");
                    CargarDatosEmpleado(int.Parse(txt_MosNumEmplea_GestionEmpleados.Text));
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el empleado.");
                }
            }

        }

        private void btn_CancelarMOD_GestionEmpleados_Click(object sender, EventArgs e)
        {
            int idEmpleado = int.Parse(txt_MosNumEmplea_GestionEmpleados.Text);
            CargarDatosEmpleado(idEmpleado);
            txt_Nombres_GestionEmpleados.Enabled = false;
            txt_ApellPaterno_GestionEmpleados.Enabled = false;
            txt_ApellMaterno_GestionEmpleados.Enabled = false;
            Cmbox_Departamento_GestionEmpleados.Enabled = false;
            CmBox_Puesto_GestionEmpleados.Enabled = false;
            dateTimer_FechaNacim_GestionEmpleados.Enabled = false;
            txt_Curp_GestionEmpleados.Enabled = false;
            txt_NSS_GestionEmpleados.Enabled = false;
            txt_RFC_GestionEmpleados.Enabled = false;
            txt_DomCompleto_GestionEmpleados.Enabled = false;
            txt_Banco_GestionEmpleados.Enabled = false;
            txt_NumCuenta_GestionEmpleados.Enabled = false;
            txt_Email_GestionEmpleados.Enabled = false;
            txt_Telefono_GestionEmpleados.Enabled = false;
            dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = false;

            txt_SalarioDiario_GestionEmpleados.Enabled = false;
            txt_SalarioDIntegrado_GestionEmpleados.Enabled = false;
           
            //CmBox_Turno_GestionEmpleados.Enabled = false;

            btn_Modificar_GestionEmpleados.Visible = false;
            btn_Agregar_GestionEmpleados.Visible = true;
            btn_Eliminar_GestionEmpleados.Visible = false;
            btn_AceptarMOD_GestionEmpleados.Visible = false;
            btn_CancelarMOD_GestionEmpleados.Visible = false;
            btn_Buscar_GestionEmpleados.Visible = true;
        }


        private void CargarDatosEmpleado(int idEmpleado)
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Empleado WHERE ID_Empleado = @idEmpleado";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txt_MosNumEmplea_GestionEmpleados.Text = reader["ID_Empleado"].ToString();
                    txt_Nombres_GestionEmpleados.Text = reader["Nombre"].ToString();
                    txt_ApellPaterno_GestionEmpleados.Text = reader["ApellidoPaterno"].ToString();
                    txt_ApellMaterno_GestionEmpleados.Text = reader["ApellidoMaterno"].ToString();

                    Cmbox_Departamento_GestionEmpleados.SelectedValue = reader["DepID"];
                    CmBox_Puesto_GestionEmpleados.SelectedValue = reader["PuestoID"];

                    dateTimer_FechaNacim_GestionEmpleados.Value = Convert.ToDateTime(reader["FechaNacimiento"]);
                    txt_Curp_GestionEmpleados.Text = reader["CURP"].ToString();
                    txt_NSS_GestionEmpleados.Text = reader["NSS"].ToString();
                    txt_RFC_GestionEmpleados.Text = reader["RFC"].ToString();
                    txt_DomCompleto_GestionEmpleados.Text = reader["Direccion"].ToString();
                    txt_Banco_GestionEmpleados.Text = reader["Banco"].ToString();
                    txt_NumCuenta_GestionEmpleados.Text = reader["NumeroCuenta"].ToString();
                    txt_Email_GestionEmpleados.Text = reader["Email"].ToString();
                    txt_Telefono_GestionEmpleados.Text = reader["Telefono"].ToString();
                    dateTimer_FechaIngreEmpr_GestionEmpleados.Value = Convert.ToDateTime(reader["FechaIngreso"]);

                    txt_SalarioDiario_GestionEmpleados.Text = reader["SalarioDiario"].ToString();
                    txt_SalarioDIntegrado_GestionEmpleados.Text = reader["SalarioDiarioIntegrado"].ToString();
                    bool activo = Convert.ToBoolean(reader["estatus"]);
                    
                }

                cn.Close();
            }
        }


        private void btn_Regresar_GestionEmpleados_Click(object sender, EventArgs e)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_Eliminar_GestionEmpleados_Click(object sender, EventArgs e)
        {
            int idEmpleado = int.Parse(txt_MosNumEmplea_GestionEmpleados.Text);

            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este empleado?",
                                                  "Confirmación de Eliminación",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                SqlConnection cn = BD_Conexion.ObtenerConexion();

                try
                {
                    string query = "DELETE FROM Empleado WHERE ID_Empleado = @idEmpleado";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                    
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Empleado eliminado exitosamente.");
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el empleado.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
               
            }
        }

        private void LimpiarCampos()
        {
            txt_MosNumEmplea_GestionEmpleados.Clear();
            txt_Nombres_GestionEmpleados.Clear();
            txt_ApellPaterno_GestionEmpleados.Clear();
            txt_ApellMaterno_GestionEmpleados.Clear();
            Cmbox_Departamento_GestionEmpleados.SelectedIndex = -1;//Es la manera correcta de dejar el ComboBox en un estado sin selección.
            CmBox_Puesto_GestionEmpleados.SelectedIndex = -1;
            dateTimer_FechaNacim_GestionEmpleados.Value = DateTime.Now;
            txt_Curp_GestionEmpleados.Clear();
            txt_NSS_GestionEmpleados.Clear();
            txt_RFC_GestionEmpleados.Clear();
            txt_DomCompleto_GestionEmpleados.Clear();
            txt_Banco_GestionEmpleados.Clear();
            txt_NumCuenta_GestionEmpleados.Clear();
            txt_Email_GestionEmpleados.Clear();
            txt_Telefono_GestionEmpleados.Clear();
            dateTimer_FechaIngreEmpr_GestionEmpleados.Value = DateTime.Now;
            txt_SalarioDiario_GestionEmpleados.Clear();
            txt_SalarioDIntegrado_GestionEmpleados.Clear();
            
        }

        private void btn_Agregar_GestionEmpleados_Click(object sender, EventArgs e)
        {
           // txt_MosNumEmplea_GestionEmpleados.Clear();
            LimpiarCampos();
            txt_Nombres_GestionEmpleados.Enabled = true;
            txt_ApellPaterno_GestionEmpleados.Enabled = true;
            txt_ApellMaterno_GestionEmpleados.Enabled = true;
            Cmbox_Departamento_GestionEmpleados.Enabled = true;
            CmBox_Puesto_GestionEmpleados.Enabled = true;
            dateTimer_FechaNacim_GestionEmpleados.Enabled = true;
            txt_Curp_GestionEmpleados.Enabled = true;
            txt_NSS_GestionEmpleados.Enabled = true;
            txt_RFC_GestionEmpleados.Enabled = true;
            txt_DomCompleto_GestionEmpleados.Enabled = true;
            txt_Banco_GestionEmpleados.Enabled = true;
            txt_NumCuenta_GestionEmpleados.Enabled = true;
            txt_Email_GestionEmpleados.Enabled = true;
            txt_Telefono_GestionEmpleados.Enabled = true;
           // dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = true;
            dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = false;


            // Asignar el valor al DateTimePicker usando las variables
            dateTimer_FechaIngreEmpr_GestionEmpleados.Value = DateTime.Today;


            txt_SalarioDiario_GestionEmpleados.Enabled = true;
            txt_SalarioDIntegrado_GestionEmpleados.Enabled = false;
           
            //CmBox_Turno_GestionEmpleados.Enabled = true;

            btn_Modificar_GestionEmpleados.Visible = false;
            btn_Agregar_GestionEmpleados.Visible = false;
            btn_Eliminar_GestionEmpleados.Visible = false;
            btn_AceptarMOD_GestionEmpleados.Visible = false;
            btn_CancelarMOD_GestionEmpleados.Visible = false;
            btn_Buscar_GestionEmpleados.Visible = false;
            btn_AgregarAceptar_GestionEmpleados.Visible = true;
            btn_AgregarCancelar_GestionEmpleados.Visible = true;
        }

        private void btn_AgregarAceptar_GestionEmpleados_Click(object sender, EventArgs e)
        {
            // Validar campos antes de proceder
            if (!ValidarCampos())
            {
                return; // Detener el proceso si alguna validación falla
            }
            decimal salarioDiario = 0;
            decimal salarioDiarioIntegrado = 0;
            

            // Obtener y calcular salario diario integrado
            if (!string.IsNullOrEmpty(txt_SalarioDiario_GestionEmpleados.Text) &&
                decimal.TryParse(txt_SalarioDiario_GestionEmpleados.Text, out salarioDiario))
            {
                salarioDiarioIntegrado = salarioDiario * 1.0493m;
            }

            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string query = @"
    INSERT INTO Empleado (
        Nombre, ApellidoPaterno, ApellidoMaterno,
        DepID, PuestoID, EmpresaID, Gerente, Contrasena,
        FechaNacimiento, CURP, NSS, RFC, Direccion, Banco, NumeroCuenta,
        Email, Telefono, FechaIngreso, SalarioDiario, SalarioDiarioIntegrado, estatus
    ) VALUES (
        @Nombre, @ApellidoPaterno, @ApellidoMaterno,
        @Departamento, @Puesto, @EmpresaID, @Gerente, @Contrasena,
        @FechaNacimiento, @Curp, @NSS, @RFC, @Domicilio, @Banco, @NumeroCuenta,
        @Email, @Telefono, @FechaIngreso, @SalarioDiario, @SalarioDiarioIntegrado, @Activo
    )";

                SqlCommand cmd = new SqlCommand(query, cn);

                // Parámetros alineados con la tabla Empleado
                cmd.Parameters.AddWithValue("@Nombre", txt_Nombres_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", txt_ApellPaterno_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", txt_ApellMaterno_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Departamento", Cmbox_Departamento_GestionEmpleados.SelectedValue);
                cmd.Parameters.AddWithValue("@Puesto", CmBox_Puesto_GestionEmpleados.SelectedValue);
                cmd.Parameters.AddWithValue("@EmpresaID", 1); // ← fijo si solo hay una empresa
                cmd.Parameters.AddWithValue("@Gerente", false); // ← o true si aplica
                cmd.Parameters.AddWithValue("@Contrasena", "1234"); // ← temporal, luego aplicar hash

                cmd.Parameters.AddWithValue("@FechaNacimiento", dateTimer_FechaNacim_GestionEmpleados.Value);
                cmd.Parameters.AddWithValue("@Curp", txt_Curp_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@NSS", txt_NSS_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@RFC", txt_RFC_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Domicilio", txt_DomCompleto_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Banco", txt_Banco_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@NumeroCuenta", txt_NumCuenta_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Email", txt_Email_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Telefono", txt_Telefono_GestionEmpleados.Text);

                cmd.Parameters.AddWithValue("@FechaIngreso", dateTimer_FechaIngreEmpr_GestionEmpleados.Value);
                cmd.Parameters.AddWithValue("@SalarioDiario", salarioDiario);
                cmd.Parameters.AddWithValue("@SalarioDiarioIntegrado", salarioDiarioIntegrado);
                

                try
                {
                    
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Empleado agregado exitosamente.");
                        // LimpiarCampos();
                        // DeshabilitarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar el empleado.");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error SQL: " + ex.Message);
                }
            }

            LimpiarCampos();
            txt_Nombres_GestionEmpleados.Enabled = false;
            txt_ApellPaterno_GestionEmpleados.Enabled = false;
            txt_ApellMaterno_GestionEmpleados.Enabled = false;
            Cmbox_Departamento_GestionEmpleados.Enabled = false;
            CmBox_Puesto_GestionEmpleados.Enabled = false;
            dateTimer_FechaNacim_GestionEmpleados.Enabled = false;
            txt_Curp_GestionEmpleados.Enabled = false;
            txt_NSS_GestionEmpleados.Enabled = false;
            txt_RFC_GestionEmpleados.Enabled = false;
            txt_DomCompleto_GestionEmpleados.Enabled = false;
            txt_Banco_GestionEmpleados.Enabled = false;
            txt_NumCuenta_GestionEmpleados.Enabled = false;
            txt_Email_GestionEmpleados.Enabled = false;
            txt_Telefono_GestionEmpleados.Enabled = false;
            dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = false;

            txt_SalarioDiario_GestionEmpleados.Enabled = false;
            txt_SalarioDIntegrado_GestionEmpleados.Enabled = false;
           
            //CmBox_Turno_GestionEmpleados.Enabled = false;

            btn_Modificar_GestionEmpleados.Visible = false;
            btn_Agregar_GestionEmpleados.Visible = true;
            btn_Eliminar_GestionEmpleados.Visible = false;
            btn_AceptarMOD_GestionEmpleados.Visible = false;
            btn_CancelarMOD_GestionEmpleados.Visible = false;
            btn_Buscar_GestionEmpleados.Visible = true;
            btn_AgregarAceptar_GestionEmpleados.Visible = false;
            btn_AgregarCancelar_GestionEmpleados.Visible = false;
        }

        private void btn_AgregarCancelar_GestionEmpleados_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            txt_Nombres_GestionEmpleados.Enabled = false;
            txt_ApellPaterno_GestionEmpleados.Enabled = false;
            txt_ApellMaterno_GestionEmpleados.Enabled = false;
            Cmbox_Departamento_GestionEmpleados.Enabled = false;
            CmBox_Puesto_GestionEmpleados.Enabled = false;
            dateTimer_FechaNacim_GestionEmpleados.Enabled = false;
            txt_Curp_GestionEmpleados.Enabled = false;
            txt_NSS_GestionEmpleados.Enabled = false;
            txt_RFC_GestionEmpleados.Enabled = false;
            txt_DomCompleto_GestionEmpleados.Enabled = false;
            txt_Banco_GestionEmpleados.Enabled = false;
            txt_NumCuenta_GestionEmpleados.Enabled = false;
            txt_Email_GestionEmpleados.Enabled = false;
            txt_Telefono_GestionEmpleados.Enabled = false;
            dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = false;

            txt_SalarioDiario_GestionEmpleados.Enabled = false;
            txt_SalarioDIntegrado_GestionEmpleados.Enabled = false;
          
            //CmBox_Turno_GestionEmpleados.Enabled = false;

            btn_Modificar_GestionEmpleados.Visible = false;
            btn_Agregar_GestionEmpleados.Visible = true;
            btn_Eliminar_GestionEmpleados.Visible = false;
            btn_AceptarMOD_GestionEmpleados.Visible = false;
            btn_CancelarMOD_GestionEmpleados.Visible = false;
            btn_Buscar_GestionEmpleados.Visible = true;
            btn_AgregarAceptar_GestionEmpleados.Visible = false;
            btn_AgregarCancelar_GestionEmpleados.Visible = false;
        }

      
      /* public int ConvertirMesATexto(string mess)
        {
            switch (mess.ToLower())
            {
                case "enero":
                    return 1;
                case "febrero":
                    return 2;
                case "marzo":
                    return 3;
                case "abril":
                    return 4;
                case "mayo":
                    return 5;
                case "junio":
                    return 6;
                case "julio":
                    return 7;
                case "agosto":
                    return 8;
                case "septiembre":
                    return 9;
                case "octubre":
                    return 10;
                case "noviembre":
                    return 11;
                case "diciembre":
                    return 12;
                default:
                    throw new ArgumentException("Mes inválido");
            }
        }
      */

        private bool ValidarCampos()
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txt_Nombres_GestionEmpleados.Text) ||
                string.IsNullOrWhiteSpace(txt_ApellPaterno_GestionEmpleados.Text) ||
                string.IsNullOrWhiteSpace(txt_Curp_GestionEmpleados.Text) ||
                string.IsNullOrWhiteSpace(txt_NSS_GestionEmpleados.Text) ||
                string.IsNullOrWhiteSpace(txt_RFC_GestionEmpleados.Text) ||
                string.IsNullOrWhiteSpace(txt_DomCompleto_GestionEmpleados.Text) ||
                string.IsNullOrWhiteSpace(txt_Banco_GestionEmpleados.Text) ||
                string.IsNullOrWhiteSpace(txt_NumCuenta_GestionEmpleados.Text) ||
                string.IsNullOrWhiteSpace(txt_Email_GestionEmpleados.Text) ||
                string.IsNullOrWhiteSpace(txt_Telefono_GestionEmpleados.Text) ||
                string.IsNullOrWhiteSpace(txt_SalarioDiario_GestionEmpleados.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.");
                return false;
            }

            
           
            if (!ValidarEmail(txt_Email_GestionEmpleados.Text))
            {
                MessageBox.Show("Ingrese un formato de correo electrónico válido.");
                return false;
            }


            return true; // Todos los campos están completos
        }

        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y el carácter de control (backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Evitar que se procese el carácter no válido
                MessageBox.Show("Este campo solo acepta números.");
            }
        }
        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una letra o espacio
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; // Cancelar la entrada del carácter no permitido
                MessageBox.Show("Este campo solo acepta letras.");
            }
        }
        private bool ValidarEmail(string email)
        {
            // Expresión regular para validar email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
      

        private void CargarPuestosPorDepartamento(int idDepartamento)
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string queryPuesto = "SELECT ID_Puesto, Nombre FROM Puesto WHERE DepartamentoID= @id_Departamento";
                SqlDataAdapter daPuesto = new SqlDataAdapter(queryPuesto, cn);
                daPuesto.SelectCommand.Parameters.AddWithValue("@id_Departamento", idDepartamento);

                DataTable dtPuesto = new DataTable();
                daPuesto.Fill(dtPuesto);

                CmBox_Puesto_GestionEmpleados.DataSource = dtPuesto;
                CmBox_Puesto_GestionEmpleados.DisplayMember = "Nombre";
                CmBox_Puesto_GestionEmpleados.ValueMember = "ID_Puesto";
            }
        }

        private void Cmbox_Departamento_GestionEmpleados_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (Cmbox_Departamento_GestionEmpleados.SelectedValue != null)
            {
                var selectedValue = Cmbox_Departamento_GestionEmpleados.SelectedValue as DataRowView;

                int idDepartamento;

                if (selectedValue != null)
                {
                    // Si el valor es un DataRowView, tomamos el valor específico de la columna id_Departamento
                    idDepartamento = Convert.ToInt32(selectedValue["id_Departamento"]);
                }
                else
                {
                    // Si no es un DataRowView, asumimos que es un valor convertible a int
                    idDepartamento = Convert.ToInt32(Cmbox_Departamento_GestionEmpleados.SelectedValue);
                }

                // Llamamos a la función para cargar los puestos correspondientes al departamento seleccionado
                CmBox_Puesto_GestionEmpleados.SelectedIndex = -1;
                CargarPuestosPorDepartamento(idDepartamento);
            }

        }

        private void btn_ReciboNomina_GestionEmpleados_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dateTimer_FechaNacim_GestionEmpleados_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void txt_Banco_GestionEmpleados_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

