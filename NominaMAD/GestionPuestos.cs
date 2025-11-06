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

        string Conexion = "Data Source=LUISMTZ\\SQLEXPRESS;Initial Catalog=Nomina;Integrated Security=True";
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
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Puestos", cn);
                da.SelectCommand.CommandType = CommandType.Text;
                cn.Open();
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
                if (fila.Cells["NombrePuesto"].Value != null &&
                    fila.Cells["NombrePuesto"].Value.ToString().ToLower() == txtPuesto_GestionPuestos.Text.ToLower())
                {
                    existe = true;
                    break;
                }
            }

            //if (!existe)
            //{
            //    // Agregar una nueva fila si no existe
            //    //  dtgv_GestionDepar.Rows.Add(nombreDepartamento, sueldoBase);
            //    using (SqlConnection cn = new SqlConnection(Conexion))
            //    {
            //        SqlCommand cmd = new SqlCommand("INSERT INTO Departamento(NombreDepartamento,SueldoBase) VALUES ('" + txt_Departamento_GestDepar.Text + "'," + txt_SueldoBase_GestionDepar.Text + ")", cn);
            //        cmd.CommandType = CommandType.Text;
            //        cn.Open();
            //        cmd.ExecuteNonQuery();
            //        mostrarTablaDepart();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Este departamento ya existe. Por favor, ingresa un departamento diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


        }
        private void btn_Guardar_GestionPuestos_Click(object sender, EventArgs e)
        {
            if (txtPuesto_GestionPuestos.Text == "" || txt_DescripcionPuesto_GestionPuestos.Text == "")
            {
                MessageBox.Show("Algun Dato Vacio");
            }
            else
            {
               // ValidarExistencia();
                //if (existe == false)
                {
                    int idDepartamentoSeleccionado = ((ComboBoxItem)cmBox_Departamento_GestionPuestos.SelectedItem).Value;

                    using (SqlConnection cn = new SqlConnection(Conexion))
                    {
                       // SqlCommand cmd = new SqlCommand("INSERT INTO Puesto(id_Departamento,NombreDepartamento,SueldoBase) VALUES ('"+idDepartamentoSeleccionado+"," + txtPuesto_GestionPuestos.Text + "'," + txt_SalarioDiario_GestionPuestos.Text + ")", cn);
                        //SqlCommand cmd = new SqlCommand("INSERT INTO Puestos (id_Departamento, NombrePuesto, SalarioDiario, DescripcionPuesto) VALUES (" + idDepartamentoSeleccionado + ", '" + txtPuesto_GestionPuestos.Text + "', " + txt_SalarioDiario_GestionPuestos.Text + ","+ txt_DescripcionPuesto_GestionPuestos + ")", cn);
                        SqlCommand cmd = new SqlCommand("INSERT INTO Puestos (id_Departamento, NombrePuesto, DescripcionPuesto) VALUES (" + idDepartamentoSeleccionado + ", '" + txtPuesto_GestionPuestos.Text + "', '" + txt_DescripcionPuesto_GestionPuestos.Text + "')", cn);

                        cmd.CommandType = CommandType.Text;
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        //mostrarTablaPuestos();
                        CargarPuestos( idDepartamentoSeleccionado);
                    }

                }
              //  else { MessageBox.Show("Este departamento ya existe. Por favor, ingresa un departamento diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); existe = false; }
                //using (SqlConnection cn = new SqlConnection(Conexion))
                //{
                //    SqlCommand cmd = new SqlCommand("INSERT INTO Departamento(NombreDepartamento,SueldoBase) VALUES ('" + txt_Departamento_GestDepar.Text + "'," + txt_SueldoBase_GestionDepar.Text + ")", cn);
                //    cmd.CommandType = CommandType.Text;
                //    cn.Open();
                //    cmd.ExecuteNonQuery();
                //    mostrarTablaDepart();
                //}
            }
        }




        //private void MostrarComboBoxDep()
        //{

        //    //using (SqlConnection cn = new SqlConnection(Conexion))
        //    //{
        //    //    SqlCommand cmd = new SqlCommand("SELECT * FROM Departamento", cn);
        //    // //   SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Departamento", cn);
        //    //    cmd.CommandType = CommandType.Text;
        //    //    cn.Open();
        //    //    cmd.ExecuteNonQuery();
        //    //  //  mostrarTablaDepart();
        //    //}

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        try
        //        {
        //            cn.Open();
        //            SqlCommand cmd = new SqlCommand("SELECT NombreDepartamento FROM Departamento", cn);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            // Limpiar el ComboBox antes de llenarlo
        //            cmBox_Departamento_GestionPuestos.Items.Clear();

        //            // Agregar cada departamento al ComboBox
        //            while (reader.Read())
        //            {
        //                cmBox_Departamento_GestionPuestos.Items.Add(reader["NombreDepartamento"].ToString());
        //            }
        //            reader.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error al cargar los departamentos: " + ex.Message);
        //        }
        //    }

        //}
        private void MostrarComboBoxDep()
        {
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_Departamento, NombreDepartamento FROM Departamento", cn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpiar el ComboBox antes de llenarlo
                    cmBox_Departamento_GestionPuestos.Items.Clear();

                    // Agregar cada departamento al ComboBox
                    while (reader.Read())
                    {
                        // Crear un nuevo item en el ComboBox con el nombre y el ID del departamento
                        cmBox_Departamento_GestionPuestos.Items.Add(new ComboBoxItem
                        {
                            Text = reader["NombreDepartamento"].ToString(),
                            Value = (int)reader["id_Departamento"]
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

        //private void CargarPuestos(int idDepartamento)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        SqlDataAdapter da = new SqlDataAdapter("SELECT NombrePuesto, SalarioDiario FROM Puestos WHERE id_Departamento = @id_Departamento", cn);
        //        da.SelectCommand.Parameters.AddWithValue("@id_Departamento", idDepartamento);
        //        cn.Open();
        //        da.Fill(dt);

        //        // Mostrar los puestos en un DataGridView o similar
        //        dtgv_GestionPustos.DataSource = dt;
        //    }
        //}
        private void CargarPuestos(int idDepartamento)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                //SqlDataAdapter da = new SqlDataAdapter("SELECT NombrePuesto, SalarioDiario, DescripcionPuesto FROM Puestos WHERE id_Departamento = @id_Departamento", cn);
                SqlDataAdapter da = new SqlDataAdapter("SELECT NombrePuesto, DescripcionPuesto FROM Puestos WHERE id_Departamento = @id_Departamento", cn);
                da.SelectCommand.Parameters.AddWithValue("@id_Departamento", idDepartamento);
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
                txtPuesto_GestionPuestos.Text = dtgv_GestionPustos.Rows[ColumnaSeleccionada].Cells["NombrePuesto"].Value.ToString();
               // txt_SalarioDiario_GestionPuestos.Text = dtgv_GestionPustos.Rows[ColumnaSeleccionada].Cells["SalarioDiario"].Value.ToString();
                txt_DescripcionPuesto_GestionPuestos.Text = dtgv_GestionPustos.Rows[ColumnaSeleccionada].Cells["DescripcionPuesto"].Value.ToString(); // Rellenar la descripción

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


        //private void dtgv_GestionPustos_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    ColumnaSeleccionada = e.RowIndex;

        //    if (ColumnaSeleccionada != -1)
        //    {
        //        // limpiar
        //        txtPuesto_GestionPuestos.Text = "";
        //        txt_SalarioDiario_GestionPuestos.Text = "";
        //        //ingresa datos en los txt
        //        //txt_Departamento_GestDepar.Text = (string)dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[0].Value;
        //        //txt_SueldoBase_GestionDepar.Text = (string)dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[1].Value;
        //        //txt_Empleados_GestionDepar.Text = (string)dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[2].Value;

        //        txtPuesto_GestionPuestos.Text = dtgv_GestionPustos.Rows[ColumnaSeleccionada].Cells[0].Value.ToString();
        //        txt_SalarioDiario_GestionPuestos.Text = dtgv_GestionPustos.Rows[ColumnaSeleccionada].Cells[1].Value.ToString();
        //        // txt_Empleados_GestionDepar.Text = dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[2].Value.ToString();


        //        //habilitar txts
        //        txtPuesto_GestionPuestos.Enabled = false;
        //        txt_SalarioDiario_GestionPuestos.Enabled = false;

        //        ////muestra boton modificar y oculta los demas
        //        //btn_Guardar_GestionDepar.Visible = false;
        //        //btn_limpiar_GestionDepar.Visible = false;
        //        //btn_Agregar_GestionDepar.Visible = true;
        //        //btn_Modificar_GestionDepar.Visible = true;
        //        //btn_AceptarMod_GestionDepar.Visible = false;
        //        //btn_CancelarMod_GestionDepar.Visible = false;
        //        btn_Agregar_GestionPuestos.Visible = true;
        //        btn_Guardar_GestionPuestos.Visible = false;
        //        btn_Limpiar_GestionPuestos.Visible = false;
        //        btn_Modifcar_GestionPuestos.Visible = true;
        //        btn_AceptarMOD_GestionPuestos.Visible = false;
        //        btn_CancelarMOD_GestionPuestos.Visible = false;
        //    }
        //}

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
            // update Departamento set NombreDepartamento='zxc', SueldoBase = 123 where NombreDepartamento='vxcv'
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
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                //SqlCommand cmd = new SqlCommand("update Departamento set NombreDepartamento='" + txt_Departamento_GestDepar.Text + "', SueldoBase =" + txt_SueldoBase_GestionDepar.Text + " where NombreDepartamento='" + modificarOpcion + "'", cn);
                //SqlCommand cmd = new SqlCommand("update Puestos set NombrePuesto='"+ txtPuesto_GestionPuestos.Text+ "' ,SalarioDiario= " + txt_SalarioDiario_GestionPuestos.Text+" where NombrePuesto='"+ modificarOpcion+"'", cn);
                //SqlCommand cmd = new SqlCommand("update Puestos set NombrePuesto='" + txtPuesto_GestionPuestos.Text + "' ,SalarioDiario= " + txt_SalarioDiario_GestionPuestos.Text + " where NombrePuesto='" + modificarOpcion + "'", cn);
                //SqlCommand cmd = new SqlCommand("UPDATE Puestos SET NombrePuesto='" + txtPuesto_GestionPuestos.Text + "', SalarioDiario=" + txt_SalarioDiario_GestionPuestos.Text + ", DescripcionPuesto='" + txt_DescripcionPuesto_GestionPuestos.Text + "' WHERE NombrePuesto='" + modificarOpcion + "'", cn);
                SqlCommand cmd = new SqlCommand("UPDATE Puestos SET NombrePuesto='" + txtPuesto_GestionPuestos.Text + "', DescripcionPuesto='" + txt_DescripcionPuesto_GestionPuestos.Text + "' WHERE NombrePuesto='" + modificarOpcion + "'", cn);

                cmd.CommandType = CommandType.Text;
                cn.Open();
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

        //private void btn_Imprimir_GestionPuestos_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog guardar = new SaveFileDialog();
        //    guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_ReportePuestos.pdf";
        //    string paginahtml_texto = Properties.Resources.Reporte_Puestos.ToString();

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        // 1. Obtener los datos de los puestos y almacenarlos en una lista
        //        string queryPuestos = "SELECT p.id_Puesto, p.NombrePuesto, p.DescripcionPuesto, d.NombreDepartamento " +
        //                              "FROM Puestos p " +
        //                              "JOIN Departamento d ON p.id_Departamento = d.id_Departamento";
        //        SqlCommand cmdPuestos = new SqlCommand(queryPuestos, cn);
        //        SqlDataReader readerPuestos = cmdPuestos.ExecuteReader();

        //        // Almacenar los puestos en memoria
        //        var puestos = new List<(string idPuesto, string nombrePuesto, string descripcionPuesto, string nombreDepartamento)>();

        //        while (readerPuestos.Read())
        //        {
        //            puestos.Add((
        //                idPuesto: readerPuestos["id_Puesto"].ToString(),
        //                nombrePuesto: readerPuestos["NombrePuesto"].ToString(),
        //                descripcionPuesto: readerPuestos["DescripcionPuesto"].ToString(),
        //                nombreDepartamento: readerPuestos["NombreDepartamento"].ToString()
        //            ));
        //        }
        //        readerPuestos.Close(); // Cerrar el DataReader de puestos aquí

        //        // 2. Generar el HTML para cada puesto en una sola tabla
        //        string puestosHtml = "";

        //        foreach (var (idPuesto, nombrePuesto, descripcionPuesto, nombreDepartamento) in puestos)
        //        {
        //            // Consulta para obtener los empleados en el puesto actual y que estén activos
        //            string queryEmpleados = @"
        //        SELECT e.id_Empleado, e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, 
        //               e.FechaIngresoEmpresa, e.SalarioDiario
        //        FROM Empleado e
        //        WHERE e.id_Puesto = @idPuesto AND e.Activo = 1";

        //            SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn);
        //            cmdEmpleados.Parameters.AddWithValue("@idPuesto", idPuesto);

        //            SqlDataReader readerEmpleados = cmdEmpleados.ExecuteReader();

        //            // Contador de empleados para cada puesto
        //            string empleadosHtml = "";
        //            int totalEmpleados = 0;

        //            // Generar filas de empleados
        //            while (readerEmpleados.Read())
        //            {
        //                totalEmpleados++;
        //                string idEmpleado = readerEmpleados["id_Empleado"].ToString();
        //                string nombreEmpleado = $"{readerEmpleados["NombreEmpleado"]} {readerEmpleados["ApelPaternoEmpleado"]} {readerEmpleados["ApelMaternoEmpleado"]}";
        //                string fechaIngreso = Convert.ToDateTime(readerEmpleados["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy");
        //                decimal salarioDiario = Convert.ToDecimal(readerEmpleados["SalarioDiario"]);

        //                empleadosHtml += $@"
        //            <tr>
        //                <td>{idEmpleado}</td>
        //                <td>{nombreEmpleado}</td>
        //                <td>{fechaIngreso}</td>
        //                <td>{salarioDiario:C}</td>
        //            </tr>";
        //            }
        //            readerEmpleados.Close(); // Cerrar el DataReader de empleados después de procesar cada puesto

        //            // Encabezado para el puesto y sus empleados
        //            string puestoHtml = $@"
        //        <tr class='position-header'>
        //            <td colspan='4'>Puesto: {nombrePuesto}</td>
        //        </tr>
        //        <tr>
        //            <td><strong>ID Puesto:</strong> {idPuesto}</td>
        //            <td><strong>Descripción:</strong> {descripcionPuesto}</td>
        //            <td colspan='2'><strong>Departamento:</strong> {nombreDepartamento}</td>
        //        </tr>
        //        <tr class='employee-header'>
        //            <th>ID Empleado</th>
        //            <th>Nombre</th>
        //            <th>Fecha de Ingreso</th>
        //            <th>Salario Diario</th>
        //        </tr>
        //        {empleadosHtml}";

        //            puestosHtml += puestoHtml;
        //        }

        //        // Reemplazar los marcadores en la plantilla
        //        paginahtml_texto = paginahtml_texto.Replace("@MES_REPORTE", "Noviembre");
        //        paginahtml_texto = paginahtml_texto.Replace("@ANO_REPORTE", "2024");
        //        paginahtml_texto = paginahtml_texto.Replace("@PUESTOS", puestosHtml);
        //    }

        //    // Generar el PDF con el contenido verificado
        //    if (guardar.ShowDialog() == DialogResult.OK)
        //    {
        //        using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
        //        {
        //            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
        //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

        //            pdfDoc.Open();
        //            pdfDoc.Add(new Phrase(""));

        //            using (StringReader sr = new StringReader(paginahtml_texto))
        //            {
        //                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //            }

        //            pdfDoc.Close();
        //            stream.Close();
        //        }

        //        MessageBox.Show("Reporte de puestos generado exitosamente.");
        //    }
        //}

        //private void btn_ImprimirTurno_GestionPuestos_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog guardar = new SaveFileDialog();
        //    guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_ReporteTurnos.pdf";
        //    string paginahtml_texto = Properties.Resources.Reporte_Turnos.ToString();

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        // 1. Obtener los datos de los turnos y almacenarlos en una lista
        //        string queryTurnos = "SELECT id_Turno, NombreTurno, Descripcion FROM Turno";
        //        SqlCommand cmdTurnos = new SqlCommand(queryTurnos, cn);
        //        SqlDataReader readerTurnos = cmdTurnos.ExecuteReader();

        //        // Almacenar los turnos en memoria
        //        var turnos = new List<(string idTurno, string nombreTurno, string descripcionTurno)>();

        //        while (readerTurnos.Read())
        //        {
        //            turnos.Add((
        //                idTurno: readerTurnos["id_Turno"].ToString(),
        //                nombreTurno: readerTurnos["NombreTurno"].ToString(),
        //                descripcionTurno: readerTurnos["Descripcion"].ToString()
        //            ));
        //        }
        //        readerTurnos.Close(); // Cerrar el DataReader de turnos aquí

        //        // 2. Generar el HTML para cada turno en una sola tabla
        //        string turnosHtml = "";

        //        foreach (var (idTurno, nombreTurno, descripcionTurno) in turnos)
        //        {
        //            // Consulta para obtener los empleados en el turno actual y que estén activos
        //            string queryEmpleados = @"
        //        SELECT e.id_Empleado, e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, 
        //               e.FechaIngresoEmpresa, e.SalarioDiario
        //        FROM Empleado e
        //        WHERE e.id_Turno = @idTurno AND e.Activo = 1";

        //            SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn);
        //            cmdEmpleados.Parameters.AddWithValue("@idTurno", idTurno);

        //            SqlDataReader readerEmpleados = cmdEmpleados.ExecuteReader();

        //            // Contador de empleados para cada turno
        //            string empleadosHtml = "";
        //            int totalEmpleados = 0;

        //            // Generar filas de empleados
        //            while (readerEmpleados.Read())
        //            {
        //                totalEmpleados++;
        //                string idEmpleado = readerEmpleados["id_Empleado"].ToString();
        //                string nombreEmpleado = $"{readerEmpleados["NombreEmpleado"]} {readerEmpleados["ApelPaternoEmpleado"]} {readerEmpleados["ApelMaternoEmpleado"]}";
        //                string fechaIngreso = Convert.ToDateTime(readerEmpleados["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy");
        //                decimal salarioDiario = Convert.ToDecimal(readerEmpleados["SalarioDiario"]);

        //                empleadosHtml += $@"
        //            <tr>
        //                <td>{idEmpleado}</td>
        //                <td>{nombreEmpleado}</td>
        //                <td>{fechaIngreso}</td>
        //                <td>{salarioDiario:C}</td>
        //            </tr>";
        //            }
        //            readerEmpleados.Close(); // Cerrar el DataReader de empleados después de procesar cada turno

        //            // Encabezado para el turno y sus empleados
        //            string turnoHtml = $@"
        //        <tr class='turno-header'>
        //            <td colspan='4'>Turno: {nombreTurno}</td>
        //        </tr>
        //        <tr>
        //            <td><strong>ID Turno:</strong> {idTurno}</td>
        //            <td colspan='3'><strong>Descripción:</strong> {descripcionTurno}</td>
        //        </tr>
        //        <tr class='employee-header'>
        //            <th>ID Empleado</th>
        //            <th>Nombre</th>
        //            <th>Fecha de Ingreso</th>
        //            <th>Salario Diario</th>
        //        </tr>
        //        {empleadosHtml}";

        //            turnosHtml += turnoHtml;
        //        }

        //        // Reemplazar los marcadores en la plantilla
        //        paginahtml_texto = paginahtml_texto.Replace("@MES_REPORTE", "Noviembre");
        //        paginahtml_texto = paginahtml_texto.Replace("@ANO_REPORTE", "2024");
        //        paginahtml_texto = paginahtml_texto.Replace("@TURNOS", turnosHtml);
        //    }

        //    // Generar el PDF con el contenido verificado
        //    if (guardar.ShowDialog() == DialogResult.OK)
        //    {
        //        using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
        //        {
        //            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
        //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

        //            pdfDoc.Open();
        //            pdfDoc.Add(new Phrase(""));

        //            using (StringReader sr = new StringReader(paginahtml_texto))
        //            {
        //                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //            }

        //            pdfDoc.Close();
        //            stream.Close();
        //        }

        //        MessageBox.Show("Reporte de turnos generado exitosamente.");
        //    }
        //}
    }
}
