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
            txt_Telefono_GestionEmpleados.KeyPress += SoloNumeros_KeyPress;
            txt_NSS_GestionEmpleados.KeyPress += SoloNumeros_KeyPress;
            txt_NumCuenta_GestionEmpleados.KeyPress += SoloNumeros_KeyPress;
            txt_NumEmplea_GestionEmpleados.KeyPress += SoloNumeros_KeyPress;
            txt_SalarioDiario_GestionEmpleados.KeyPress += SoloNumeros_KeyPress;

            txt_Nombres_GestionEmpleados.KeyPress += SoloLetras_KeyPress;
            txt_ApellPaterno_GestionEmpleados.KeyPress += SoloLetras_KeyPress;
            txt_ApellMaterno_GestionEmpleados.KeyPress += SoloLetras_KeyPress;
            txt_Banco_GestionEmpleados.KeyPress += SoloLetras_KeyPress;


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

     
            txt_MosNumEmplea_GestionEmpleados.Enabled = false;
            txt_SalarioDiario_GestionEmpleados.Enabled = false;
            txt_SalarioDIntegrado_GestionEmpleados.Enabled = false;
            CmBox_Estatus_GestionEmpleados.Enabled = false;
            CmBox_Turno_GestionEmpleados.Enabled=false;

            btn_Modificar_GestionEmpleados.Visible = false;
            //btn_Agregar_GestionEmpleados.Visible = false;
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
           

            // Configurar ComboBox de Estatus
            CmBox_Estatus_GestionEmpleados.Items.Add("Activo");
            CmBox_Estatus_GestionEmpleados.Items.Add("Inactivo");
            CargarTurnos();

        }

        string Conexion = "Data Source=LUISMTZ\\SQLEXPRESS;Initial Catalog=Nomina;Integrated Security=True";
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
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                string queryDepartamento = "SELECT id_Departamento, NombreDepartamento FROM Departamento";
                SqlDataAdapter daDepartamento = new SqlDataAdapter(queryDepartamento, cn);
                DataTable dtDepartamento = new DataTable();
                daDepartamento.Fill(dtDepartamento);

                Cmbox_Departamento_GestionEmpleados.DataSource = dtDepartamento;
                Cmbox_Departamento_GestionEmpleados.DisplayMember = "NombreDepartamento";
                Cmbox_Departamento_GestionEmpleados.ValueMember = "id_Departamento";
            }

            // Llenar ComboBox de Puesto
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                string queryPuesto = "SELECT id_Puesto, NombrePuesto FROM Puestos";
                SqlDataAdapter daPuesto = new SqlDataAdapter(queryPuesto, cn);
                DataTable dtPuesto = new DataTable();
                daPuesto.Fill(dtPuesto);

                CmBox_Puesto_GestionEmpleados.DataSource = dtPuesto;
                CmBox_Puesto_GestionEmpleados.DisplayMember = "NombrePuesto";
                CmBox_Puesto_GestionEmpleados.ValueMember = "id_Puesto";
            }
            ObtenerPeriodoActual();
            mes = MesPeriodo;
            anoSeleccionado = AnoPeriodo;
        }

        void ObtenerPeriodoActual()
        {
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                // Seleccionar el último registro de la tabla Periodo según id_Periodo
                string query = "SELECT TOP 1 id_Periodo, Mes, Ano FROM Periodo ORDER BY id_Periodo DESC";
                SqlCommand cmd = new SqlCommand(query, cn);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // Si hay un registro, cargar los datos del último periodo
                if (reader.Read())
                {
                    idPeriodoActual = reader.GetInt32(0);  // id_Periodo
                    MesPeriodo = reader.GetString(1);      // Mes
                    AnoPeriodo = reader.GetInt32(2);       // Ano
                }

                reader.Close();
            }
        }
        private void CargarTurnos()
        {
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                string queryTurno = "SELECT id_Turno, NombreTurno FROM Turno";
                SqlDataAdapter daTurno = new SqlDataAdapter(queryTurno, cn);

                DataTable dtTurno = new DataTable();
                daTurno.Fill(dtTurno);

                CmBox_Turno_GestionEmpleados.DataSource = dtTurno;
                CmBox_Turno_GestionEmpleados.DisplayMember = "NombreTurno";
                CmBox_Turno_GestionEmpleados.ValueMember = "id_Turno";
            }
        }

        //private void btn_Buscar_GestionEmpleados_Click(object sender, EventArgs e)
        //{
        //    string NumEmplBus= txt_NumEmplea_GestionEmpleados.Text;

        //    DataTable dt = new DataTable();
        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Empleado WHERE id_Empleado="+ NumEmplBus+";", cn);
        //        da.SelectCommand.CommandType = CommandType.Text;
        //        cn.Open();
        //        da.Fill(dt);
        //        dtgv_GestionDepar.DataSource = dt;

        //    }
        //}


        private void btn_Buscar_GestionEmpleados_Click(object sender, EventArgs e)
        {

            string NumEmplBus = txt_NumEmplea_GestionEmpleados.Text;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                string query = "SELECT * FROM Empleado WHERE id_Empleado = @idEmpleado";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", NumEmplBus);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // Si el empleado existe
                {
                    // Asignar valores a cada TextBox
                    txt_MosNumEmplea_GestionEmpleados.Text = reader["id_Empleado"].ToString();
                    txt_Nombres_GestionEmpleados.Text = reader["NombreEmpleado"].ToString();
                    txt_ApellPaterno_GestionEmpleados.Text = reader["ApelPaternoEmpleado"].ToString();
                    txt_ApellMaterno_GestionEmpleados.Text = reader["ApelMaternoEmpleado"].ToString();
                    Cmbox_Departamento_GestionEmpleados.SelectedValue = reader["id_Departamento"];
                    CmBox_Puesto_GestionEmpleados.SelectedValue = reader["id_Puesto"];
                    dateTimer_FechaNacim_GestionEmpleados.Text = reader["FechadeNaci"].ToString();
                    txt_Curp_GestionEmpleados.Text = reader["Curp"].ToString();
                    txt_NSS_GestionEmpleados.Text = reader["NSS"].ToString();
                    txt_RFC_GestionEmpleados.Text = reader["RFC"].ToString();
                    txt_DomCompleto_GestionEmpleados.Text = reader["Domicilio"].ToString();
                    txt_Banco_GestionEmpleados.Text = reader["Banco"].ToString();
                    txt_NumCuenta_GestionEmpleados.Text = reader["NumeroCuenta"].ToString();
                    txt_Email_GestionEmpleados.Text = reader["Email"].ToString();
                    txt_Telefono_GestionEmpleados.Text = reader["Telefonos"].ToString();
                    dateTimer_FechaIngreEmpr_GestionEmpleados.Text = reader["FechaIngresoEmpresa"].ToString();
                    //txt_FechaIngresoPuesto_GestionEmpleados.Text = reader["FechaIngresoPuesto"].ToString();

                    txt_SalarioDiario_GestionEmpleados.Text = reader["SalarioDiario"].ToString();
                    txt_SalarioDIntegrado_GestionEmpleados.Text = reader["SalarioDiarioIntegrado"].ToString();
                    bool activo = Convert.ToBoolean(reader["Activo"]);
                    CmBox_Estatus_GestionEmpleados.SelectedIndex = activo ? 0 : 1;
                    // Asignar el turno del empleado al ComboBox
                    CmBox_Turno_GestionEmpleados.SelectedValue = reader["id_Turno"];

                    btn_Modificar_GestionEmpleados.Visible = true;
                    btn_Agregar_GestionEmpleados.Visible = false;
                    btn_Eliminar_GestionEmpleados.Visible = true;

                }
                else
                {
                    MessageBox.Show("Empleado no encontrado.");
                }

                cn.Close();
            }


            //// Obtener el número de empleado ingresado
            //string NumEmplBus = txt_NumEmplea_GestionEmpleados.Text;

            //// Crear la conexión y el comando SQL
            //using (SqlConnection cn = new SqlConnection(Conexion))
            //{
            //    string query = "SELECT * FROM Empleado WHERE id_Empleado = @idEmpleado";
            //    SqlCommand cmd = new SqlCommand(query, cn);
            //    cmd.Parameters.AddWithValue("@idEmpleado", NumEmplBus);

            //    cn.Open();
            //    SqlDataReader reader = cmd.ExecuteReader();

            //    if (reader.Read()) // Si el empleado existe
            //    {
            //        // Asignar valores a cada TextBox
            //        txt_Nombres_GestionEmpleados.Text = reader["NombreEmpleado"].ToString();
            //        txt_ApellPaterno_GestionEmpleados.Text = reader["ApelPaternoEmpleado"].ToString();
            //        txt_ApellMaterno_GestionEmpleados.Text = reader["ApelMaternoEmpleado"].ToString();
            //        Cmbox_Departamento_GestionEmpleados.SelectedValue = reader["id_Departamento"];
            //        CmBox_Puesto_GestionEmpleados.SelectedValue = reader["id_Puesto"];
            //        dateTimer_FechaNacim_GestionEmpleados.Text = reader["FechadeNaci"].ToString();
            //        txt_Curp_GestionEmpleados.Text = reader["Curp"].ToString();
            //        txt_NSS_GestionEmpleados.Text = reader["NSS"].ToString();
            //        txt_RFC_GestionEmpleados.Text = reader["RFC"].ToString();
            //        txt_DomCompleto_GestionEmpleados.Text = reader["Domicilio"].ToString();
            //        txt_Banco_GestionEmpleados.Text = reader["Banco"].ToString();
            //        txt_NumCuenta_GestionEmpleados.Text = reader["NumeroCuenta"].ToString();
            //        txt_Email_GestionEmpleados.Text = reader["Email"].ToString();
            //        txt_Telefono_GestionEmpleados.Text = reader["Telefonos"].ToString();
            //        dateTimer_FechaIngreEmpr_GestionEmpleados.Text = reader["FechaIngresoEmpresa"].ToString();
            //        //txt_FechaIngresoPuesto_GestionEmpleados.Text = reader["FechaIngresoPuesto"].ToString();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Empleado no encontrado.");
            //    }

            //    cn.Close();
            //}
        }

        private void btn_Modificar_GestionEmpleados_Click(object sender, EventArgs e)
        {
           // txt_Nombres_GestionEmpleados.Enabled = true;
           // txt_ApellPaterno_GestionEmpleados.Enabled = true;
           // txt_ApellMaterno_GestionEmpleados.Enabled = true;
            Cmbox_Departamento_GestionEmpleados.Enabled = true;
            CmBox_Puesto_GestionEmpleados.Enabled = true;
            dateTimer_FechaNacim_GestionEmpleados.Enabled = true;
            txt_Curp_GestionEmpleados.Enabled = true;
           // txt_NSS_GestionEmpleados.Enabled = true;
            txt_RFC_GestionEmpleados.Enabled = true;
            txt_DomCompleto_GestionEmpleados.Enabled = true;
            txt_Banco_GestionEmpleados.Enabled = true;
            txt_NumCuenta_GestionEmpleados.Enabled = true;
            txt_Email_GestionEmpleados.Enabled = true; ;
            txt_Telefono_GestionEmpleados.Enabled = true;
            //dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = true;
            dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = false;
            // txt_NumEmplea_GestionEmpleados.Enabled = false;

            txt_SalarioDiario_GestionEmpleados.Enabled = true;
            txt_SalarioDIntegrado_GestionEmpleados.Enabled = false;
            CmBox_Estatus_GestionEmpleados.Enabled = true;
            CmBox_Turno_GestionEmpleados.Enabled = true;

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
            bool nuevoEstatusActivo = CmBox_Estatus_GestionEmpleados.SelectedIndex == 0;
            decimal salarioDiario = 0;
            decimal salarioDiarioIntegrado = 0;

            // Verificar si el salario diario ha cambiado y recalcular el salario diario integrado
            if (!string.IsNullOrEmpty(txt_SalarioDiario_GestionEmpleados.Text) &&
                decimal.TryParse(txt_SalarioDiario_GestionEmpleados.Text, out salarioDiario))
            {
                salarioDiarioIntegrado = salarioDiario * 1.0493m;
            }
            //2020, 01,10
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                // Crear el query SQL
                string query = @"
        UPDATE Empleado SET 
            NombreEmpleado = @Nombre, 
            ApelPaternoEmpleado = @ApellidoPaterno, 
            ApelMaternoEmpleado = @ApellidoMaterno, 
            id_Departamento = @Departamento, 
            id_Puesto = @Puesto, 
            id_Turno = @Turno,
            FechadeNaci = @FechaNacimiento, 
            Curp = @Curp, 
            NSS = @NSS, 
            RFC = @RFC, 
            Domicilio = @Domicilio, 
            Banco = @Banco, 
            NumeroCuenta = @NumeroCuenta, 
            Email = @Email, 
            Telefonos = @Telefono, 
            Activo = @NuevoEstatusActivo, 
            SalarioDiario = @SalarioDiario, 
            SalarioDiarioIntegrado = @SalarioDiarioIntegrado,
            FechaIngresoEmpresa = CASE 
                WHEN Activo = 0 AND @NuevoEstatusActivo = 1 THEN @FechaActual 
                ELSE FechaIngresoEmpresa 
            END 
        WHERE id_Empleado = @idEmpleado";

                SqlCommand cmd = new SqlCommand(query, cn);

                // Asignar valores a los parámetros
                cmd.Parameters.AddWithValue("@Nombre", txt_Nombres_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", txt_ApellPaterno_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", txt_ApellMaterno_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Departamento", Cmbox_Departamento_GestionEmpleados.SelectedValue);
                cmd.Parameters.AddWithValue("@Puesto", CmBox_Puesto_GestionEmpleados.SelectedValue);
                cmd.Parameters.AddWithValue("@Turno", CmBox_Turno_GestionEmpleados.SelectedValue); // Agrega el turno seleccionado
                cmd.Parameters.AddWithValue("@FechaNacimiento", dateTimer_FechaNacim_GestionEmpleados.Value);
                cmd.Parameters.AddWithValue("@Curp", txt_Curp_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@NSS", txt_NSS_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@RFC", txt_RFC_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Domicilio", txt_DomCompleto_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Banco", txt_Banco_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@NumeroCuenta", txt_NumCuenta_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Email", txt_Email_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Telefono", txt_Telefono_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@NuevoEstatusActivo", nuevoEstatusActivo ? 1 : 0);
                cmd.Parameters.AddWithValue("@SalarioDiario", salarioDiario);
                cmd.Parameters.AddWithValue("@SalarioDiarioIntegrado", salarioDiarioIntegrado);
                // Definir una fecha específica
                int day = 1;
                int numeroMes = ConvertirMesATexto(mes); // Suponiendo que "mes" ya contiene el nombre del mes en texto
                DateTime fechaEspecifica = new DateTime(anoSeleccionado, numeroMes, day);
                cmd.Parameters.AddWithValue("@FechaActual", fechaEspecifica);
               // cmd.Parameters.AddWithValue("@FechaActual", DateTime.Now);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);


                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Empleado actualizado exitosamente.");
                    // Volver a cargar los datos del empleado para mostrar los cambios
                    CargarDatosEmpleado(idEmpleado);

                    // Deshabilitar campos después de la actualización
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
                    CmBox_Estatus_GestionEmpleados.Enabled = false;
                    CmBox_Turno_GestionEmpleados.Enabled = false;

                    btn_Modificar_GestionEmpleados.Visible = false;
                    btn_Agregar_GestionEmpleados.Visible = true;
                    btn_Eliminar_GestionEmpleados.Visible = false;
                    btn_AceptarMOD_GestionEmpleados.Visible = false;
                    btn_CancelarMOD_GestionEmpleados.Visible = false;
                    btn_Buscar_GestionEmpleados.Visible = true;
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el empleado.");
                }
            }

            ///////////////
            //    // Obtener el ID del empleado a modificar
            //   // int idEmpleado = int.Parse(txt_NumEmplea_GestionEmpleados.Text);
            //    int idEmpleado = int.Parse(txt_MosNumEmplea_GestionEmpleados.Text);
            //bool nuevoEstatusActivo = CmBox_Estatus_GestionEmpleados.SelectedIndex == 0;

            //using (SqlConnection cn = new SqlConnection(Conexion))
            //    {
            //        string query = "UPDATE Empleado SET " +
            //                       "NombreEmpleado = @Nombre, " +
            //                       "ApelPaternoEmpleado = @ApellidoPaterno, " +
            //                       "ApelMaternoEmpleado = @ApellidoMaterno, " +
            //                       "id_Departamento = @Departamento, " +
            //                       "id_Puesto = @Puesto, " +
            //                       "FechadeNaci = @FechaNacimiento, " +
            //                       "Curp = @Curp, " +
            //                       "NSS = @NSS, " +
            //                       "RFC = @RFC, " +
            //                       "Domicilio = @Domicilio, " +
            //                       "Banco = @Banco, " +
            //                       "NumeroCuenta = @NumeroCuenta, " +
            //                       "Email = @Email, " +
            //                       "Telefonos = @Telefono, " +
            //                       "FechaIngresoEmpresa = @FechaIngresoEmpresa " +
            //                       "WHERE id_Empleado = @idEmpleado";

            //        SqlCommand cmd = new SqlCommand(query, cn);
            //        cmd.Parameters.AddWithValue("@Nombre", txt_Nombres_GestionEmpleados.Text);
            //        cmd.Parameters.AddWithValue("@ApellidoPaterno", txt_ApellPaterno_GestionEmpleados.Text);
            //        cmd.Parameters.AddWithValue("@ApellidoMaterno", txt_ApellMaterno_GestionEmpleados.Text);
            //        cmd.Parameters.AddWithValue("@Departamento", Cmbox_Departamento_GestionEmpleados.SelectedValue);
            //        cmd.Parameters.AddWithValue("@Puesto", CmBox_Puesto_GestionEmpleados.SelectedValue);
            //        cmd.Parameters.AddWithValue("@FechaNacimiento", dateTimer_FechaNacim_GestionEmpleados.Value);
            //        cmd.Parameters.AddWithValue("@Curp", txt_Curp_GestionEmpleados.Text);
            //        cmd.Parameters.AddWithValue("@NSS", txt_NSS_GestionEmpleados.Text);
            //        cmd.Parameters.AddWithValue("@RFC", txt_RFC_GestionEmpleados.Text);
            //        cmd.Parameters.AddWithValue("@Domicilio", txt_DomCompleto_GestionEmpleados.Text);
            //        cmd.Parameters.AddWithValue("@Banco", txt_Banco_GestionEmpleados.Text);
            //        cmd.Parameters.AddWithValue("@NumeroCuenta", txt_NumCuenta_GestionEmpleados.Text);
            //        cmd.Parameters.AddWithValue("@Email", txt_Email_GestionEmpleados.Text);
            //        cmd.Parameters.AddWithValue("@Telefono", txt_Telefono_GestionEmpleados.Text);
            //        cmd.Parameters.AddWithValue("@FechaIngresoEmpresa", dateTimer_FechaIngreEmpr_GestionEmpleados.Value);
            //        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

            //        cn.Open();
            //        int rowsAffected = cmd.ExecuteNonQuery();
            //        cn.Close();

            //        if (rowsAffected > 0)
            //        {
            //            MessageBox.Show("Empleado actualizado exitosamente.");
            //            // Volver a cargar los datos del empleado para mostrar los cambios
            //             CargarDatosEmpleado(idEmpleado);

            //            txt_Nombres_GestionEmpleados.Enabled = false;
            //            txt_ApellPaterno_GestionEmpleados.Enabled = false;
            //            txt_ApellMaterno_GestionEmpleados.Enabled = false;
            //            Cmbox_Departamento_GestionEmpleados.Enabled = false;
            //            CmBox_Puesto_GestionEmpleados.Enabled = false;
            //            dateTimer_FechaNacim_GestionEmpleados.Enabled = false;
            //            txt_Curp_GestionEmpleados.Enabled = false;
            //            txt_NSS_GestionEmpleados.Enabled = false;
            //            txt_RFC_GestionEmpleados.Enabled = false;
            //            txt_DomCompleto_GestionEmpleados.Enabled = false;
            //            txt_Banco_GestionEmpleados.Enabled = false;
            //            txt_NumCuenta_GestionEmpleados.Enabled = false;
            //            txt_Email_GestionEmpleados.Enabled = false;
            //            txt_Telefono_GestionEmpleados.Enabled = false;
            //            dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = false;

            //            btn_Modificar_GestionEmpleados.Visible = false;
            //            btn_Agregar_GestionEmpleados.Visible = true;
            //            btn_Eliminar_GestionEmpleados.Visible = false;
            //            btn_AceptarMOD_GestionEmpleados.Visible = false;
            //            btn_CancelarMOD_GestionEmpleados.Visible = false;
            //            btn_Buscar_GestionEmpleados.Visible = true;
            //        }
            //        else
            //        {
            //            MessageBox.Show("No se pudo actualizar el empleado.");
            //        }
            //}
            /////////////////////
            // Deshabilitar los controles después de actualizar
            // DeshabilitarCampos();
            //ConfigurarBotonesModoVista();




            //txt_Nombres_GestionEmpleados.Enabled = false;
            //txt_ApellPaterno_GestionEmpleados.Enabled = false;
            //txt_ApellMaterno_GestionEmpleados.Enabled = false;
            //Cmbox_Departamento_GestionEmpleados.Enabled = false;
            //CmBox_Puesto_GestionEmpleados.Enabled = false;
            //dateTimer_FechaNacim_GestionEmpleados.Enabled = false;
            //txt_Curp_GestionEmpleados.Enabled = false;
            //txt_NSS_GestionEmpleados.Enabled = false;
            //txt_RFC_GestionEmpleados.Enabled = false;
            //txt_DomCompleto_GestionEmpleados.Enabled = false;
            //txt_Banco_GestionEmpleados.Enabled = false;
            //txt_NumCuenta_GestionEmpleados.Enabled = false;
            //txt_Email_GestionEmpleados.Enabled = false;
            //txt_Telefono_GestionEmpleados.Enabled = false;
            //dateTimer_FechaIngreEmpr_GestionEmpleados.Enabled = false;

            //btn_Modificar_GestionEmpleados.Visible = false;
            //btn_Agregar_GestionEmpleados.Visible = true;
            //btn_Eliminar_GestionEmpleados.Visible = false;
            //btn_AceptarMOD_GestionEmpleados.Visible = false;
            //btn_CancelarMOD_GestionEmpleados.Visible = false;
            //btn_Buscar_GestionEmpleados.Visible = true;
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
            CmBox_Estatus_GestionEmpleados.Enabled = false;
            CmBox_Turno_GestionEmpleados.Enabled = false;

            btn_Modificar_GestionEmpleados.Visible = false;
            btn_Agregar_GestionEmpleados.Visible = true;
            btn_Eliminar_GestionEmpleados.Visible = false;
            btn_AceptarMOD_GestionEmpleados.Visible = false;
            btn_CancelarMOD_GestionEmpleados.Visible = false;
            btn_Buscar_GestionEmpleados.Visible = true;
        }


        private void CargarDatosEmpleado(int idEmpleado)
        {
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                string query = "SELECT * FROM Empleado WHERE id_Empleado = @idEmpleado";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txt_MosNumEmplea_GestionEmpleados.Text = reader["id_Empleado"].ToString();
                    txt_Nombres_GestionEmpleados.Text = reader["NombreEmpleado"].ToString();
                    txt_ApellPaterno_GestionEmpleados.Text = reader["ApelPaternoEmpleado"].ToString();
                    txt_ApellMaterno_GestionEmpleados.Text = reader["ApelMaternoEmpleado"].ToString();
                    Cmbox_Departamento_GestionEmpleados.SelectedValue = reader["id_Departamento"];
                    CmBox_Puesto_GestionEmpleados.SelectedValue = reader["id_Puesto"];
                    dateTimer_FechaNacim_GestionEmpleados.Value = Convert.ToDateTime(reader["FechadeNaci"]);
                    txt_Curp_GestionEmpleados.Text = reader["Curp"].ToString();
                    txt_NSS_GestionEmpleados.Text = reader["NSS"].ToString();
                    txt_RFC_GestionEmpleados.Text = reader["RFC"].ToString();
                    txt_DomCompleto_GestionEmpleados.Text = reader["Domicilio"].ToString();
                    txt_Banco_GestionEmpleados.Text = reader["Banco"].ToString();
                    txt_NumCuenta_GestionEmpleados.Text = reader["NumeroCuenta"].ToString();
                    txt_Email_GestionEmpleados.Text = reader["Email"].ToString();
                    txt_Telefono_GestionEmpleados.Text = reader["Telefonos"].ToString();
                    dateTimer_FechaIngreEmpr_GestionEmpleados.Value = Convert.ToDateTime(reader["FechaIngresoEmpresa"]);
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
           
                // Obtener el ID del empleado del TextBox correspondiente
                int idEmpleado = int.Parse(txt_MosNumEmplea_GestionEmpleados.Text);

                // Mostrar cuadro de confirmación
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este empleado?",
                                                      "Confirmación de Eliminación",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Si el usuario confirma, procede a eliminar el empleado
                    using (SqlConnection cn = new SqlConnection(Conexion))
                    {
                        string query = "DELETE FROM Empleado WHERE id_Empleado = @idEmpleado";
                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                        cn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        cn.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Empleado eliminado exitosamente.");

                            // Limpiar los TextBox después de eliminar
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el empleado.");
                        }
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
            CmBox_Estatus_GestionEmpleados.SelectedIndex = -1;//Es la manera correcta de dejar el ComboBox en un estado sin selección.
            CmBox_Turno_GestionEmpleados.SelectedIndex = -1;
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

            int day = 1;
            int numeroMes = ConvertirMesATexto(mes);
            // Asignar el valor al DateTimePicker usando las variables
            dateTimer_FechaIngreEmpr_GestionEmpleados.Value = new DateTime(anoSeleccionado, numeroMes, day);

            txt_SalarioDiario_GestionEmpleados.Enabled = true;
            txt_SalarioDIntegrado_GestionEmpleados.Enabled = false;
            CmBox_Estatus_GestionEmpleados.Enabled = false;
            CmBox_Estatus_GestionEmpleados.SelectedIndex = 0; // Seleccionar "Activo" por defecto
            CmBox_Turno_GestionEmpleados.Enabled = true;

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
            bool estatusActivo = CmBox_Estatus_GestionEmpleados.SelectedIndex == 0; // Si es 0, está activo

            // Obtener y calcular salario diario integrado
            if (!string.IsNullOrEmpty(txt_SalarioDiario_GestionEmpleados.Text) &&
                decimal.TryParse(txt_SalarioDiario_GestionEmpleados.Text, out salarioDiario))
            {
                salarioDiarioIntegrado = salarioDiario * 1.0493m;
            }

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                // Comando SQL para insertar un nuevo empleado
                //string query = "INSERT INTO Empleado (NombreEmpleado, ApelPaternoEmpleado, ApelMaternoEmpleado, " +
                //               "id_Departamento, id_Puesto, FechadeNaci, Curp, NSS, RFC, Domicilio, Banco, NumeroCuenta, " +
                //               "Email, Telefonos, FechaIngresoEmpresa) " +
                //               "VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Departamento, @Puesto, @FechaNacimiento, " +
                //               "@Curp, @NSS, @RFC, @Domicilio, @Banco, @NumeroCuenta, @Email, @Telefono, @FechaIngresoEmpresa)";

                //SqlCommand cmd = new SqlCommand(query, cn);

                //// Asignar valores a los parámetros
                //cmd.Parameters.AddWithValue("@Nombre", txt_Nombres_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@ApellidoPaterno", txt_ApellPaterno_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@ApellidoMaterno", txt_ApellMaterno_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@Departamento", Cmbox_Departamento_GestionEmpleados.SelectedValue);
                //cmd.Parameters.AddWithValue("@Puesto", CmBox_Puesto_GestionEmpleados.SelectedValue);
                //cmd.Parameters.AddWithValue("@FechaNacimiento", dateTimer_FechaNacim_GestionEmpleados.Value);
                //cmd.Parameters.AddWithValue("@Curp", txt_Curp_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@NSS", txt_NSS_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@RFC", txt_RFC_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@Domicilio", txt_DomCompleto_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@Banco", txt_Banco_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@NumeroCuenta", txt_NumCuenta_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@Email", txt_Email_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@Telefono", txt_Telefono_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@FechaIngresoEmpresa", dateTimer_FechaIngreEmpr_GestionEmpleados.Value);
                // Comando SQL para insertar un nuevo empleado

                /////////////
                //string query = "INSERT INTO Empleado (NombreEmpleado, ApelPaternoEmpleado, ApelMaternoEmpleado, " +
                //               "id_Departamento, id_Puesto, FechadeNaci, Curp, NSS, RFC, Domicilio, Banco, NumeroCuenta, " +
                //               "Email, Telefonos, FechaIngresoEmpresa, FechaIngresoPuesto) " + // Añade FechaIngresoPuesto aquí
                //               "VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Departamento, @Puesto, @FechaNacimiento, " +
                //               "@Curp, @NSS, @RFC, @Domicilio, @Banco, @NumeroCuenta, @Email, @Telefono, @FechaIngresoEmpresa, @FechaIngresoPuesto)";

                //SqlCommand cmd = new SqlCommand(query, cn);

                //// Asignar valores a los parámetros
                //cmd.Parameters.AddWithValue("@Nombre", txt_Nombres_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@ApellidoPaterno", txt_ApellPaterno_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@ApellidoMaterno", txt_ApellMaterno_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@Departamento", Cmbox_Departamento_GestionEmpleados.SelectedValue);
                //cmd.Parameters.AddWithValue("@Puesto", CmBox_Puesto_GestionEmpleados.SelectedValue);
                //cmd.Parameters.AddWithValue("@FechaNacimiento", dateTimer_FechaNacim_GestionEmpleados.Value);
                //cmd.Parameters.AddWithValue("@Curp", txt_Curp_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@NSS", txt_NSS_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@RFC", txt_RFC_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@Domicilio", txt_DomCompleto_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@Banco", txt_Banco_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@NumeroCuenta", txt_NumCuenta_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@Email", txt_Email_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@Telefono", txt_Telefono_GestionEmpleados.Text);
                //cmd.Parameters.AddWithValue("@FechaIngresoEmpresa", dateTimer_FechaIngreEmpr_GestionEmpleados.Value);
                //// Asigna el mismo valor de FechaIngresoEmpresa a FechaIngresoPuesto
                //cmd.Parameters.AddWithValue("@FechaIngresoPuesto", dateTimer_FechaIngreEmpr_GestionEmpleados.Value);
                ///////////
                // Comando SQL para insertar un nuevo empleado
                string query = "INSERT INTO Empleado (NombreEmpleado, ApelPaternoEmpleado, ApelMaternoEmpleado, " +
                        "id_Departamento, id_Puesto, id_Turno, FechadeNaci, Curp, NSS, RFC, Domicilio, Banco, NumeroCuenta, " +
                        "Email, Telefonos, FechaIngresoEmpresa, FechaIngresoPuesto, SalarioDiario, SalarioDiarioIntegrado, Activo) " +
                        "VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Departamento, @Puesto, @Turno, @FechaNacimiento, " +
                        "@Curp, @NSS, @RFC, @Domicilio, @Banco, @NumeroCuenta, @Email, @Telefono, @FechaIngresoEmpresa, " +
                        "@FechaIngresoPuesto, @SalarioDiario, @SalarioDiarioIntegrado, @Activo)";

                SqlCommand cmd = new SqlCommand(query, cn);

                // Asignar valores a los parámetros
                cmd.Parameters.AddWithValue("@Nombre", txt_Nombres_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", txt_ApellPaterno_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", txt_ApellMaterno_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Departamento", Cmbox_Departamento_GestionEmpleados.SelectedValue);
                cmd.Parameters.AddWithValue("@Puesto", CmBox_Puesto_GestionEmpleados.SelectedValue);
                cmd.Parameters.AddWithValue("@Turno", CmBox_Turno_GestionEmpleados.SelectedValue); // Agregar el turno seleccionado
                cmd.Parameters.AddWithValue("@FechaNacimiento", dateTimer_FechaNacim_GestionEmpleados.Value);
                cmd.Parameters.AddWithValue("@Curp", txt_Curp_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@NSS", txt_NSS_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@RFC", txt_RFC_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Domicilio", txt_DomCompleto_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Banco", txt_Banco_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@NumeroCuenta", txt_NumCuenta_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Email", txt_Email_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@Telefono", txt_Telefono_GestionEmpleados.Text);
                cmd.Parameters.AddWithValue("@FechaIngresoEmpresa", dateTimer_FechaIngreEmpr_GestionEmpleados.Value);
                cmd.Parameters.AddWithValue("@FechaIngresoPuesto", dateTimer_FechaIngreEmpr_GestionEmpleados.Value);
                cmd.Parameters.AddWithValue("@SalarioDiario", salarioDiario);
                cmd.Parameters.AddWithValue("@SalarioDiarioIntegrado", salarioDiarioIntegrado);
                cmd.Parameters.AddWithValue("@Activo", estatusActivo ? 1 : 0);

                // Abrir la conexión y ejecutar el comando
                cn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cn.Close();

                // Verificar si el registro fue exitoso
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Empleado agregado exitosamente.");
                   // LimpiarCampos();
                   // DeshabilitarCampos();
                }
                else
                {
                    MessageBox.Show("Error al agregar el empleado.");
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
            CmBox_Estatus_GestionEmpleados.Enabled = false;
            CmBox_Turno_GestionEmpleados.Enabled = false;

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
            CmBox_Estatus_GestionEmpleados.Enabled = false;
            CmBox_Turno_GestionEmpleados.Enabled = false;

            btn_Modificar_GestionEmpleados.Visible = false;
            btn_Agregar_GestionEmpleados.Visible = true;
            btn_Eliminar_GestionEmpleados.Visible = false;
            btn_AceptarMOD_GestionEmpleados.Visible = false;
            btn_CancelarMOD_GestionEmpleados.Visible = false;
            btn_Buscar_GestionEmpleados.Visible = true;
            btn_AgregarAceptar_GestionEmpleados.Visible = false;
            btn_AgregarCancelar_GestionEmpleados.Visible = false;
        }

        //private void txt_Telefono_GestionEmpleados_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    // Permitir solo números y el carácter de control (backspace)
        //    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = true; // Evitar que se procese el carácter no válido
        //        MessageBox.Show("Este campo solo acepta números.");
        //    }
        //}

        public int ConvertirMesATexto(string mess)
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

            // Validar que se haya seleccionado un departamento y un puesto
            if (Cmbox_Departamento_GestionEmpleados.SelectedIndex == -1 ||
                CmBox_Puesto_GestionEmpleados.SelectedIndex == -1 ||
                CmBox_Estatus_GestionEmpleados.SelectedIndex == -1 ||
                CmBox_Turno_GestionEmpleados.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un Departamento , un Puesto y un turno.");
                return false;
            }
           
            if (!ValidarEmail(txt_Email_GestionEmpleados.Text))
            {
                MessageBox.Show("Ingrese un formato de correo electrónico válido.");
                return false;
            }


            return true; // Todos los campos están completos
        }

        //private void txt_NSS_GestionEmpleados_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //    // Permitir solo números y el carácter de control (backspace)
        //    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
        //    {
        //        e.Handled = true; // Evitar que se procese el carácter no válido
        //        MessageBox.Show("Este campo solo acepta números.");
        //    }
        //}
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
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                string queryPuesto = "SELECT id_Puesto, NombrePuesto FROM Puestos WHERE id_Departamento = @id_Departamento";
                SqlDataAdapter daPuesto = new SqlDataAdapter(queryPuesto, cn);
                daPuesto.SelectCommand.Parameters.AddWithValue("@id_Departamento", idDepartamento);

                DataTable dtPuesto = new DataTable();
                daPuesto.Fill(dtPuesto);

                CmBox_Puesto_GestionEmpleados.DataSource = dtPuesto;
                CmBox_Puesto_GestionEmpleados.DisplayMember = "NombrePuesto";
                CmBox_Puesto_GestionEmpleados.ValueMember = "id_Puesto";
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
    }
}

