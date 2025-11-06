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
    public partial class P_GenerarNomina : Form
    {
        public P_GenerarNomina()
        {
            InitializeComponent();
        }
        int idPeriodoActual;
        string MesPeriodo;
        int AnoPeriodo;
        private void P_GenerarNomina_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            mostrarTablaEmpleadosNomina();
            mostrarTablaDPNomina();
            ObtenerPeriodoActual();
            
            dtgv_Empleados_GenerarNomina.Visible = false;
            //CargarDatosEmpleadosConMatriz();
            // mostrarTablaMatriz();
            //LlenarMatrizConEmpleados();
            //  LlenarMatriz();

            //// Crear una instancia de P_Inicio y asignar valores
            //P_Inicio Periodo = new P_Inicio();
            //mes = Periodo.MesActual;
            // anoSeleccionado = Periodo.AnoActual;
            // Definir columnas para el DataGridView intermedio

            // Definir columnas en el DataGridView
            dtgv_EmDP_GenerarNomina.Columns.Add("D_P", "Tipo");
            dtgv_EmDP_GenerarNomina.Columns.Add("Nombre_PD", "Nombre");
            dtgv_EmDP_GenerarNomina.Columns.Add("MontoPD", "Monto");
            dtgv_EmDP_GenerarNomina.Columns.Add("Porcentaje_PD", "Porcentaje");
            dtgv_EmDP_GenerarNomina.Columns.Add("id_DPN", "ID DPN");
            dtgv_EmDP_GenerarNomina.Columns.Add("id_Empleado", "ID Empleado");
            dtgv_EmDP_GenerarNomina.Columns.Add("Mes", "Mes");
            dtgv_EmDP_GenerarNomina.Columns.Add("Ano", "Año");

            /////////
            // Definir columnas en el DataGridView
            dtgv_Matriz_GenerarNomina.Columns.Clear(); // Limpia cualquier columna previa

            dtgv_Matriz_GenerarNomina.Columns.Add("id_Matriz", "ID Matriz");

            // Agregar columna tipo CheckBox para "Activo"
            DataGridViewCheckBoxColumn activoColumn = new DataGridViewCheckBoxColumn();
            activoColumn.Name = "Activo";
            activoColumn.HeaderText = "Activo";
            activoColumn.TrueValue = true;
            activoColumn.FalseValue = false;
            dtgv_Matriz_GenerarNomina.Columns.Add(activoColumn);

            // Agrega las columnas al DataGridView si aún no están añadidas
            dtgv_Matriz_GenerarNomina.Columns.Add("id_Empleado", "ID Empleado");
            dtgv_Matriz_GenerarNomina.Columns.Add("NombreEmpleado", "Nombre Empleado");
            dtgv_Matriz_GenerarNomina.Columns.Add("Faltas", "Faltas");
            dtgv_Matriz_GenerarNomina.Columns.Add("Productividad", "Productividad");
            dtgv_Matriz_GenerarNomina.Columns.Add("Puntualidad", "Puntualidad");
            dtgv_Matriz_GenerarNomina.Columns.Add("Asistencia", "Asistencia");
            dtgv_Matriz_GenerarNomina.Columns.Add("Despensa", "Despensa");
            dtgv_Matriz_GenerarNomina.Columns.Add("Vacaciones", "Vacaciones");
            dtgv_Matriz_GenerarNomina.Columns.Add("PrimaVacacional", "Prima Vacacional");
           // dtgv_Matriz_GenerarNomina.Columns.Add("PrestamoEmpresa", "Préstamo Empresa");
            dtgv_Matriz_GenerarNomina.Columns.Add("PrestamoInfo", "Préstamo Infonavit");
            dtgv_Matriz_GenerarNomina.Columns.Add("FondoAhorro", "Fondo de Ahorro");
            dtgv_Matriz_GenerarNomina.Columns.Add("HoraExtra", "Horas Extras");
           // dtgv_Matriz_GenerarNomina.Columns.Add("PensionAlimenticia", "Pensión Alimenticia");
            //dtgv_Matriz_GenerarNomina.Columns.Add("IMSS", "IMSS");
            //dtgv_Matriz_GenerarNomina.Columns.Add("ISR", "ISR");
            //dtgv_Matriz_GenerarNomina.Columns.Add("TotalDeducciones", "Total Deducciones");
            //dtgv_Matriz_GenerarNomina.Columns.Add("TotalPercepciones", "Total Percepciones");
            //dtgv_Matriz_GenerarNomina.Columns.Add("SueldoBruto", "Sueldo Bruto");
            //dtgv_Matriz_GenerarNomina.Columns.Add("Neto", "Neto");


            //// Cargar los datos desde la base de datos
            //using (SqlConnection cn = new SqlConnection(Conexion))
            //{
            //    string query = "SELECT * FROM Matriz";
            //    SqlCommand cmd = new SqlCommand(query, cn);

            //    cn.Open();
            //    SqlDataReader reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        dtgv_Matriz_GenerarNomina.Rows.Add(
            //            reader["id_Matriz"],
            //            false, // Configura "Activo" como falso por defecto
            //            reader["id_Empleado"],
            //            reader["NombreEmpleado"],
            //            reader["Faltas"],
            //            reader["Asistencia"],
            //            reader["Puntualidad"],
            //            reader["Despensa"],
            //            reader["Vacaciones"],
            //            reader["PrimaVacacional"],
            //            reader["PrestamoEmpresa"],
            //            reader["PrestamoInfo"],
            //            reader["FondoAhorro"],
            //            reader["Productividad"],
            //            reader["HorasExtras"],
            //            reader["PensionAlimenticia"]
            //        );
            //    }

            //    reader.Close();
            //}

            ////

            mes = MesPeriodo;
            anoSeleccionado = AnoPeriodo;
            txt_Mes_GenerarNomina.Enabled = false;
            txt_Mes_GenerarNomina.Text = mes;
            txt_Ano_GenerarNomina.Enabled = false;
            txt_Ano_GenerarNomina.Text = anoSeleccionado.ToString();
            // AsignarMes(); // Esto asignará el nombre del mes a la variable 'mes' al cargar el formulario

          //  BloquearCeldasEspecificas();
            ColocarDatos();

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
        // Después de llenar el DataGridView o en un evento de carga
        private void BloquearCeldasEspecificas()
        {
            foreach (DataGridViewRow row in dtgv_Matriz_GenerarNomina.Rows)
            {
                // Verificar que la fila no sea la nueva fila vacía
                if (!row.IsNewRow)
                {
                    int idEmpleado = Convert.ToInt32(row.Cells["id_Empleado"].Value);

                    // Si el ID de empleado es 102, deshabilitar la celda de Vacaciones
                    if (idEmpleado == 102)
                    {
                        row.Cells["Vacaciones"].ReadOnly = true;
                        row.Cells["Vacaciones"].Style.BackColor = Color.LightGray; // Cambiar color para indicar que está bloqueada
                        row.Cells["Vacaciones"].Style.ForeColor = Color.DarkGray; // Cambiar color del texto
                    }
                }
            }
        }

        public void LlenarMatriz()
        {
            // Conexión a la base de datos
            string conexion = "Data Source=LUISMTZ\\SQLEXPRESS;Initial Catalog=Nomina;Integrated Security=True";

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                cn.Open();

                // Consulta para obtener todos los empleados
                string queryEmpleados = "SELECT id_Empleado, NombreEmpleado FROM Empleado";
                SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn);
                SqlDataReader readerEmpleados = cmdEmpleados.ExecuteReader();

                // Recorre cada empleado y crea un registro en la tabla Matriz
                while (readerEmpleados.Read())
                {
                    int idEmpleado = readerEmpleados.GetInt32(0);
                    string nombreEmpleado = readerEmpleados.GetString(1);

                    // Cerrar el lector antes de ejecutar el comando de inserción
                    readerEmpleados.Close();

                    // Comando de inserción para agregar el registro en la tabla Matriz
                    string queryInsert = @"
                INSERT INTO Matriz (
                    id_Empleado, NombreEmpleado, Faltas, Asistencia, Puntualidad, Despensa, 
                    Vacaciones, PrimaVacacional, PrestamoEmpresa, PrestamoInfo, FondoAhorro, 
                    Productividad, HorasExtras, PensionAlimenticia
                ) VALUES (
                    @idEmpleado, @NombreEmpleado, 0, 0.00, 0.00, 0.00, 0.00, 0.00, 
                    0.00, 0.00, 0.00, 0.00, 0, 0.00
                )";

                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
                    {
                        // Asignar valores a los parámetros
                        cmdInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        cmdInsert.Parameters.AddWithValue("@NombreEmpleado", nombreEmpleado);

                        // Ejecutar el comando de inserción
                        cmdInsert.ExecuteNonQuery();
                    }

                    // Volver a abrir el lector para continuar con el siguiente empleado
                    readerEmpleados = cmdEmpleados.ExecuteReader();
                }

                readerEmpleados.Close();
                cn.Close();
            }
        }
        public void LlenarMatrizConEmpleados()
        {
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                // Asegurarse de que cada empleado tenga un registro en la tabla Matriz
                string insertarEmpleadosEnMatriz = @"
                INSERT INTO Matriz (id_Empleado, NombreEmpleado, Faltas, Asistencia, Puntualidad, Despensa, 
                                    Vacaciones, PrimaVacacional, PrestamoEmpresa, PrestamoInfo, FondoAhorro, 
                                    Productividad, HorasExtras, PensionAlimenticia)
                SELECT e.id_Empleado, e.NombreEmpleado, 0, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0, 0.00
                FROM Empleado e
                LEFT JOIN Matriz m ON e.id_Empleado = m.id_Empleado
                WHERE m.id_Empleado IS NULL";

                using (SqlCommand cmdInsertar = new SqlCommand(insertarEmpleadosEnMatriz, cn))
                {
                    cmdInsertar.ExecuteNonQuery();
                }

                // Cargar los datos de la tabla Matriz en el DataGridView
                string queryMatriz = "SELECT * FROM Matriz";
                SqlDataAdapter da = new SqlDataAdapter(queryMatriz, cn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Suponiendo que tu DataGridView se llama dgvMatriz
                dtgv_Matriz_GenerarNomina.DataSource = dt;
            }
        }

        public void ColocarDatos()
        {
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                string query = @"
SELECT E.id_Empleado, E.NombreEmpleado, D.D_P, D.Nombre_PD, D.MontoPD, D.Porcentaje_PD, DP.Mes, DP.Ano, E.SalarioDiario
FROM Empleado E
LEFT JOIN DEDPERNOMINA DP ON E.id_Empleado = DP.id_Empleado AND DP.Mes = @Mes AND DP.Ano = @Ano
LEFT JOIN DeduccionesPercepciones D ON DP.id_PD = D.id_PD
WHERE E.Activo = 1
ORDER BY E.id_Empleado";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Mes", mes);
                cmd.Parameters.AddWithValue("@Ano", anoSeleccionado);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                var matrizData = new Dictionary<int, Dictionary<string, object>>();

                while (reader.Read())
                {
                    int idEmpleado = reader.GetInt32(reader.GetOrdinal("id_Empleado"));
                    string nombreEmpleado = reader.GetString(reader.GetOrdinal("NombreEmpleado"));
                    string nombrePD = reader.IsDBNull(reader.GetOrdinal("Nombre_PD")) ? "" : reader.GetString(reader.GetOrdinal("Nombre_PD"));
                    decimal monto = reader.IsDBNull(reader.GetOrdinal("MontoPD")) ? 0m : Convert.ToDecimal(reader["MontoPD"]);
                    decimal porcentaje = reader.IsDBNull(reader.GetOrdinal("Porcentaje_PD")) ? 0m : Convert.ToDecimal(reader["Porcentaje_PD"]);
                    decimal salarioDiario = reader.GetDecimal(reader.GetOrdinal("SalarioDiario"));
                    var (diasVacaciones, montoVacaciones, primaVacacional) = CalcularVacaciones(idEmpleado, mes, anoSeleccionado, salarioDiario);
                    int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);
                    int diasTrabajados = 30 - faltas - diasVacaciones;
                    decimal sueldoBruto = salarioDiario * diasTrabajados;

                    if (!matrizData.ContainsKey(idEmpleado))
                    {
                        matrizData[idEmpleado] = new Dictionary<string, object>
                {
                    { "NombreEmpleado", nombreEmpleado }
                };
                    }

                    if (!string.IsNullOrEmpty(nombrePD))
                    {
                        decimal valorCalculado = (porcentaje > 0) ? (porcentaje / 100) * sueldoBruto : monto;
                        matrizData[idEmpleado][nombrePD] = valorCalculado;
                    }

                    matrizData[idEmpleado]["Falta"] = faltas;

                    if (nombrePD == "Vacaciones")
                    {
                        matrizData[idEmpleado]["Vacaciones"] = montoVacaciones;
                        matrizData[idEmpleado]["Prima Vacacional"] = primaVacacional;
                    }
                    else if (nombrePD == "Prestamo Infonavit")
                    {
                        matrizData[idEmpleado]["Prestamo Infonavit"] = salarioDiario * (porcentaje / 100);
                    }
                    else if (nombrePD == "Fondo de Ahorro")
                    {
                        matrizData[idEmpleado]["Fondo de Ahorro"] = sueldoBruto < 10000 ? 500 : 1000;
                    }
                    else if (nombrePD == "Hora Extra")
                    {
                        int horasExtras = ContarHorasExtrasEmpleado(idEmpleado, mes, anoSeleccionado);
                        matrizData[idEmpleado]["Hora Extra"] = (salarioDiario / 8) * 2 * horasExtras;
                    }
                }

                reader.Close();

                dtgv_Matriz_GenerarNomina.Rows.Clear();

                foreach (var empleadoData in matrizData)
                {
                    int idEmpleado = empleadoData.Key;
                    var valores = empleadoData.Value;

                    var rowIndex = dtgv_Matriz_GenerarNomina.Rows.Add();
                    var row = dtgv_Matriz_GenerarNomina.Rows[rowIndex];

                    row.Cells["id_Empleado"].Value = idEmpleado;
                    row.Cells["NombreEmpleado"].Value = valores.ContainsKey("NombreEmpleado") ? valores["NombreEmpleado"].ToString() : "Desconocido";

                    row.Cells["Faltas"].Value = valores.ContainsKey("Falta") ? valores["Falta"] : 0;
                    row.Cells["Productividad"].Value = valores.ContainsKey("Bono Productividad") ? valores["Bono Productividad"] : 0;
                    row.Cells["Puntualidad"].Value = valores.ContainsKey("Bono Puntualidad") ? valores["Bono Puntualidad"] : 0;
                    row.Cells["Asistencia"].Value = valores.ContainsKey("Asistencia") ? valores["Asistencia"] : 0;
                    row.Cells["Despensa"].Value = valores.ContainsKey("Despensa") ? valores["Despensa"] : 0;
                    row.Cells["Vacaciones"].Value = valores.ContainsKey("Vacaciones") ? valores["Vacaciones"] : 0;
                    row.Cells["PrimaVacacional"].Value = valores.ContainsKey("Prima Vacacional") ? valores["Prima Vacacional"] : 0;
                    row.Cells["PrestamoInfo"].Value = valores.ContainsKey("Prestamo Infonavit") ? valores["Prestamo Infonavit"] : 0;
                    row.Cells["FondoAhorro"].Value = valores.ContainsKey("Fondo de Ahorro") ? valores["Fondo de Ahorro"] : 0;
                    row.Cells["HoraExtra"].Value = valores.ContainsKey("Hora Extra") ? valores["Hora Extra"] : 0;
                }

                cn.Close();
            }
        }





        private void CargarDatosEmpleadosConMatriz()
        {
            string conexion = "Data Source=LUISMTZ\\SQLEXPRESS;Initial Catalog=Nomina;Integrated Security=True"; // Cambia esto por tu cadena de conexión

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                // Consulta con LEFT JOIN para traer todos los empleados, aunque no tengan datos en la tabla Matriz
                string query = @"
            SELECT 
                e.id_Empleado,
                e.NombreEmpleado,
                m.id_Matriz,
                ISNULL(m.Activo, 0) AS Activo,
                ISNULL(m.Faltas, 0) AS Faltas,
                ISNULL(m.Asistencia, 0) AS Asistencia,
                ISNULL(m.Puntualidad, 0) AS Puntualidad,
                ISNULL(m.Despensa, 0) AS Despensa,
                ISNULL(m.Vacaciones, 0) AS Vacaciones,
                ISNULL(m.PrimaVacacional, 0) AS PrimaVacacional,
                ISNULL(m.PrestamoEmpresa, 0) AS PrestamoEmpresa,
                ISNULL(m.PrestamoInfo, 0) AS PrestamoInfo,
                ISNULL(m.FondoAhorro, 0) AS FondoAhorro,
                ISNULL(m.Productividad, 0) AS Productividad,
                ISNULL(m.HorasExtras, 0) AS HorasExtras,
                ISNULL(m.PensionAlimenticia, 0) AS PensionAlimenticia
            FROM Empleado e
            LEFT JOIN Matriz m ON e.id_Empleado = m.id_Empleado";

                SqlCommand cmd = new SqlCommand(query, cn);
                cn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                // Limpiar las filas actuales del DataGridView antes de cargar nuevos datos
                dtgv_EmDP_GenerarNomina.Rows.Clear();

                while (reader.Read())
                {
                    dtgv_EmDP_GenerarNomina.Rows.Add(
                        reader["id_Matriz"],
                        Convert.ToBoolean(reader["Activo"]),
                        reader["id_Empleado"],
                        reader["NombreEmpleado"],
                        reader["Faltas"],
                        reader["Asistencia"],
                        reader["Puntualidad"],
                        reader["Despensa"],
                        reader["Vacaciones"],
                        reader["PrimaVacacional"],
                        reader["PrestamoEmpresa"],
                        reader["PrestamoInfo"],
                        reader["FondoAhorro"],
                        reader["Productividad"],
                        reader["HorasExtras"],
                        reader["PensionAlimenticia"]
                    );
                }

                reader.Close();
            }
        }

        //private int idEmpleadoSeleccionado;
        //private List<int> deduccionesPercepcionesSeleccionadas = new List<int>();
        // private Dictionary<int, List<int>> deduccionesPercepcionesPorEmpleado = new Dictionary<int, List<int>>();

        private int idEmpleadoSeleccionado;
        private int mesSeleccionado = DateTime.Now.Month; // Mes actual
        private int anoSeleccionado = DateTime.Now.Year; // Año actual
        private string mes;
        string Conexion = "Data Source=LUISMTZ\\SQLEXPRESS;Initial Catalog=Nomina;Integrated Security=True";
        string modificarOpcion;
        private void mostrarTablaEmpleadosNomina()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                // Solo selecciona empleados activos
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Empleado WHERE activo = 1", cn);
                da.SelectCommand.CommandType = CommandType.Text;
                cn.Open();
                da.Fill(dt);
                dtgv_Empleados_GenerarNomina.DataSource = dt;
            }
        }
        private void mostrarTablaMatriz()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                // Solo selecciona empleados activos
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Matriz", cn);
                da.SelectCommand.CommandType = CommandType.Text;
                cn.Open();
                da.Fill(dt);
                dtgv_Matriz_GenerarNomina.DataSource = dt;
            }
        }

        private void mostrarTablaDPNomina()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DeduccionesPercepciones", cn);
                da.SelectCommand.CommandType = CommandType.Text;
                cn.Open();
                da.Fill(dt);
               dtgv_DP_GenerarNomina.DataSource = dt;

            }
        }

        private void mostrarTablaDEDPERNOMINA()
        {
            //DataTable dt = new DataTable();
            //using (SqlConnection cn = new SqlConnection(Conexion))
            //{
            //    //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DeduccionesPercepciones", cn);
            //    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DEDPERNOMINA WHERE id_Empleado='"++"' AND Mes='"++"' AND Ano='"++"'", cn);
            //    da.SelectCommand.CommandType = CommandType.Text;
            //    cn.Open();
            //    da.Fill(dt);
            //    dtgv_DP_GenerarNomina.DataSource = dt;

            //}
            // Limpiar las filas del DataGridView intermedio
            dtgv_EmDP_GenerarNomina.Rows.Clear();
            if (idEmpleadoSeleccionado > 0)
            {
                //DataTable dt = new DataTable();
                //using (SqlConnection cn = new SqlConnection(Conexion))
                //{
                //    string query = @"
                //SELECT 
                //    DPN.id_DPN,
                //    DPN.id_Empleado,
                //    DPN.Mes,
                //    DPN.Ano,
                //    DP.D_P,
                //    DP.Nombre_PD,
                //    DP.MontoPD,
                //    DP.Porcentaje_PD
                //FROM 
                //    DEDPERNOMINA DPN
                //INNER JOIN 
                //    DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
                //WHERE 
                //    DPN.id_Empleado = @idEmpleado AND 
                //    DPN.Mes = @mes AND 
                //    DPN.Ano = @ano";

                //    SqlDataAdapter da = new SqlDataAdapter(query, cn);

                //    // Asignación de parámetros
                //    da.SelectCommand.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
                //    da.SelectCommand.Parameters.AddWithValue("@mes", mes); // mes como string
                //    da.SelectCommand.Parameters.AddWithValue("@ano", anoSeleccionado); // ano como int

                //    da.SelectCommand.CommandType = CommandType.Text;
                //    cn.Open();
                //    da.Fill(dt);
                //    dtgv_EmDP_GenerarNomina.DataSource = dt; // Mostrar en el DataGridView intermedio
                //}
                // Suponiendo que ya tienes una consulta SQL que combina ambas tablas
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    string query = @"
                        SELECT 
                            DP.D_P,
                            DP.Nombre_PD,
                            DP.MontoPD,
                            DP.Porcentaje_PD,
                            DPN.id_DPN,
                            DPN.id_Empleado,
                            DPN.Mes,
                            DPN.Ano
                        FROM 
                            DEDPERNOMINA DPN
                        INNER JOIN 
                            DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
                        WHERE 
                            DPN.id_Empleado = @idEmpleado AND 
                            DPN.Mes = @mes AND 
                            DPN.Ano = @ano";

                    SqlDataAdapter da = new SqlDataAdapter(query, cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
                    //da.SelectCommand.Parameters.AddWithValue("@mes", mesSeleccionado);
                    da.SelectCommand.Parameters.AddWithValue("@mes", mes);

                    da.SelectCommand.Parameters.AddWithValue("@ano", anoSeleccionado);
                    cn.Open();
                    da.Fill(dt);
                }

                // Agregar los datos al DataGridView
                foreach (DataRow row in dt.Rows)
                {
                    dtgv_EmDP_GenerarNomina.Rows.Add(
                        row["D_P"],
                        row["Nombre_PD"],
                        row["MontoPD"],
                        row["Porcentaje_PD"],
                        row["id_DPN"],
                        row["id_Empleado"],
                        row["Mes"],
                        row["Ano"]
                    );
                }

            }
        }

        private void btn_Regresar_GenerarNomina_Click(object sender, EventArgs e)
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

        private void dtgv_EmDP_GenerarNomina_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dtgv_Empleados_GenerarNomina_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Capturar el ID del empleado seleccionado
                idEmpleadoSeleccionado = Convert.ToInt32(dtgv_Empleados_GenerarNomina.Rows[e.RowIndex].Cells["id_Empleado"].Value);
               // MessageBox.Show("Empleado seleccionado con ID: " + idEmpleadoSeleccionado);

                // Mostrar las deducciones y percepciones para el empleado seleccionado en el mes y año actuales
                mostrarTablaDEDPERNOMINA();
            }
        }
        private DateTime ObtenerFechaIngresoEmpleado(int idEmpleado)
        {
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string query = "SELECT FechaIngresoEmpresa FROM Empleado WHERE id_Empleado = @idEmpleado";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                return (DateTime)cmd.ExecuteScalar();
            }
        }
        private void dtgv_DP_GenerarNomina_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //// Verificar que se haya seleccionado una fila válida
            //if (e.RowIndex >= 0)
            //{
            //    int idPD = Convert.ToInt32(dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["id_PD"].Value);
            //    string nombreDP = dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["Nombre_PD"].Value.ToString();
            //    string tipoDP = dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["D_P"].Value.ToString();

            //    if (nombreDP == "Vacaciones" && tipoDP == "Percepción")
            //    {
            //        DateTime fechaIngreso = ObtenerFechaIngresoEmpleado(idEmpleadoSeleccionado);
            //        DateTime fechaActual = DateTime.Now;
            //        int antiguedad = fechaActual.Year - fechaIngreso.Year;
            //        if (fechaActual < fechaIngreso.AddYears(antiguedad)) antiguedad--;

            //        if (antiguedad < 1)
            //        {
            //            MessageBox.Show("El empleado no tiene un año de antigüedad y no puede tomar vacaciones.");
            //            return;
            //        }

            //        // Determinar los días de vacaciones según la antigüedad
            //        int diasVacaciones = 0;
            //        if (antiguedad == 1) diasVacaciones = 12;
            //        else if (antiguedad == 2) diasVacaciones = 14;
            //        else if (antiguedad == 3) diasVacaciones = 16;
            //        else if (antiguedad == 4) diasVacaciones = 18;
            //        else if (antiguedad == 5) diasVacaciones = 20;
            //        else if (antiguedad >= 6 && antiguedad <= 10) diasVacaciones = 22;
            //        else if (antiguedad >= 11 && antiguedad <= 15) diasVacaciones = 24;

            //        // Obtener el salario diario del empleado
            //        decimal salarioDiario = ObtenerSalarioDiarioEmpleado(idEmpleadoSeleccionado); // Implementa esta función para obtener el salario diario
            //        decimal primaVacacional = (diasVacaciones * 0.35m) * salarioDiario;

            //        // Mostrar mensaje con los días de vacaciones y prima vacacional
            //        MessageBox.Show($"El empleado puede tomar vacaciones y tiene {diasVacaciones} días disponibles.\n" +
            //                        $"Prima Vacacional: {primaVacacional:C}");

            //        // Asignar el valor de la prima vacacional en el campo de "Monto" en la tabla de percepciones
            //        dtgv_EmDP_GenerarNomina.Rows.Add(tipoDP, nombreDP, primaVacacional.ToString("C"));
            //    }
            //    else
            //    {
            //        // Verificar si la DoP ya está asignada para deducciones/percepciones que no sean "Falta"
            //        bool yaAsignado = false;
            //        foreach (DataGridViewRow row in dtgv_EmDP_GenerarNomina.Rows)
            //        {
            //            if (row.Cells["Nombre_PD"].Value != null && row.Cells["D_P"].Value != null)
            //            {
            //                string nombreExistente = row.Cells["Nombre_PD"].Value.ToString();
            //                string tipoExistente = row.Cells["D_P"].Value.ToString();

            //                if (nombreExistente == nombreDP && tipoExistente == tipoDP)
            //                {
            //                    if (nombreDP == "Falta" && tipoDP == "Deducción")
            //                    {
            //                        yaAsignado = false;
            //                        break;
            //                    }
            //                    else
            //                    {
            //                        yaAsignado = true;
            //                        break;
            //                    }
            //                }
            //            }
            //        }

            //        if (yaAsignado)
            //        {
            //            MessageBox.Show("Esta deducción/percepción ya ha sido asignada a este empleado.");
            //        }
            //        else
            //        {
            //            if (idEmpleadoSeleccionado > 0 && e.RowIndex >= 0)
            //            {
            //                using (SqlConnection cn = new SqlConnection(Conexion))
            //                {
            //                    string query = "INSERT INTO DEDPERNOMINA (id_Empleado, id_PD, Mes, Ano) VALUES (@idEmpleado, @idPD, @mes, @ano)";
            //                    SqlCommand cmd = new SqlCommand(query, cn);
            //                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
            //                    cmd.Parameters.AddWithValue("@idPD", idPD);
            //                    cmd.Parameters.AddWithValue("@mes", mes);
            //                    cmd.Parameters.AddWithValue("@ano", anoSeleccionado);

            //                    cn.Open();
            //                    cmd.ExecuteNonQuery();
            //                }

            //                mostrarTablaDEDPERNOMINA();
            //            }
            //            else
            //            {
            //                MessageBox.Show("Por favor, selecciona un empleado y una deducción/percepción válida.");
            //            }
            //        }
            //    }
            //}

            // Verificar que se haya seleccionado una fila válida en el DataGridView de deducciones/percepciones
            //if (e.RowIndex >= 0)
            //{
            //    int idPD = Convert.ToInt32(dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["id_PD"].Value);
            //    string nombreDP = dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["Nombre_PD"].Value.ToString();
            //    string tipoDP = dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["D_P"].Value.ToString();

            //    foreach (DataGridViewRow empleadoRow in dtgv_Matriz_GenerarNomina.Rows)
            //    {
            //        // Verificar si el checkbox "Activo" está marcado
            //        DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)empleadoRow.Cells["Activo"];
            //        bool isChecked = (checkBoxCell.Value != null && (bool)checkBoxCell.Value == true);

            //        if (isChecked)
            //        {
            //            int idEmpleado = Convert.ToInt32(empleadoRow.Cells["id_Empleado"].Value);

            //            // Verificación de deducción/percepción ya asignada
            //            bool yaAsignado = false;
            //            foreach (DataGridViewRow row in dtgv_EmDP_GenerarNomina.Rows)
            //            {
            //                if (row.Cells["Nombre_PD"].Value != null && row.Cells["D_P"].Value != null)
            //                {
            //                    string nombreExistente = row.Cells["Nombre_PD"].Value.ToString();
            //                    string tipoExistente = row.Cells["D_P"].Value.ToString();

            //                    if (nombreExistente == nombreDP && tipoExistente == tipoDP && Convert.ToInt32(row.Cells["id_Empleado"].Value) == idEmpleado)
            //                    {
            //                        yaAsignado = true;
            //                        MessageBox.Show($"La deducción/percepción '{nombreDP}' ya ha sido asignada al empleado con ID {idEmpleado}.");
            //                        break;
            //                    }
            //                }
            //            }

            //            if (yaAsignado)
            //            {
            //                continue; // Saltar al siguiente empleado si ya tiene asignada esta deducción/percepción
            //            }

            //            // Agregar deducción/percepción al empleado en la tabla del medio
            //            DataGridViewRow newRow = new DataGridViewRow();
            //            newRow.CreateCells(dtgv_EmDP_GenerarNomina);
            //            newRow.Cells[dtgv_EmDP_GenerarNomina.Columns["id_Empleado"].Index].Value = idEmpleado;
            //            newRow.Cells[dtgv_EmDP_GenerarNomina.Columns["D_P"].Index].Value = tipoDP;
            //            newRow.Cells[dtgv_EmDP_GenerarNomina.Columns["Nombre_PD"].Index].Value = nombreDP;
            //            newRow.Cells[dtgv_EmDP_GenerarNomina.Columns["Monto_PD"].Index].Value = idPD;
            //            dtgv_EmDP_GenerarNomina.Rows.Add(newRow);

            //            // Insertar en la base de datos
            //            using (SqlConnection cn = new SqlConnection(Conexion))
            //            {
            //                string query = "INSERT INTO DEDPERNOMINA (id_Empleado, id_PD, Mes, Ano) VALUES (@idEmpleado, @idPD, @mes, @ano)";
            //                SqlCommand cmd = new SqlCommand(query, cn);
            //                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
            //                cmd.Parameters.AddWithValue("@idPD", idPD);
            //                cmd.Parameters.AddWithValue("@mes", mes);
            //                cmd.Parameters.AddWithValue("@ano", anoSeleccionado);

            //                cn.Open();
            //                cmd.ExecuteNonQuery();
            //            }

            //            MessageBox.Show($"Deducción/Percepción '{nombreDP}' agregada al empleado con ID {idEmpleado}.");
            //        }
            //    }

            //    // Actualizar la tabla intermedia para mostrar el nuevo registro agregado
            //    mostrarTablaDEDPERNOMINA();
            //    ColocarDatos();
            //}

            // Verificar que se haya seleccionado una fila válida en la tabla de deducciones/percepciones
            if (e.RowIndex >= 0)
            {
                int idPD = Convert.ToInt32(dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["id_PD"].Value);
                string nombreDP = dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["Nombre_PD"].Value.ToString();
                string tipoDP = dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["D_P"].Value.ToString();

                foreach (DataGridViewRow empleadoRow in dtgv_Matriz_GenerarNomina.Rows)
                {
                    // Verificar si el checkbox "Activo" está marcado
                    DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)empleadoRow.Cells["Activo"];
                    bool isChecked = (checkBoxCell.Value != null && (bool)checkBoxCell.Value == true);

                    if (isChecked)
                    {
                        int idEmpleadoSeleccionado = Convert.ToInt32(empleadoRow.Cells["id_Empleado"].Value);

                        // Validación especial para "Vacaciones"
                        if (nombreDP == "Vacaciones" && tipoDP == "Percepción")
                        {
                            DateTime fechaIngreso = ObtenerFechaIngresoEmpleado(idEmpleadoSeleccionado); // Función para obtener fecha de ingreso del empleado
                            DateTime fechaActual = DateTime.Now;
                            int antiguedad = fechaActual.Year - fechaIngreso.Year;
                            if (fechaActual < fechaIngreso.AddYears(antiguedad)) antiguedad--;

                            if (antiguedad < 1)
                            {
                                MessageBox.Show($"El empleado con ID {idEmpleadoSeleccionado} no tiene un año de antigüedad y no puede tomar vacaciones.");
                                continue; // Saltar al siguiente empleado si no cumple con la antigüedad
                            }
                        }

                        // Verificar si la DoP ya está asignada
                        bool yaAsignado = false;
                        foreach (DataGridViewRow row in dtgv_EmDP_GenerarNomina.Rows)
                        {
                            if (row.Cells["Nombre_PD"].Value != null && row.Cells["D_P"].Value != null)
                            {
                                string nombreExistente = row.Cells["Nombre_PD"].Value.ToString();
                                string tipoExistente = row.Cells["D_P"].Value.ToString();
                                // Permitir múltiples asignaciones para "Falta" de tipo Deducción y "Horas Extra" de tipo Percepción
                                if ((nombreExistente == "Falta" && tipoExistente == "Deducción") ||
                                    (nombreExistente == "Hora Extra" && tipoExistente == "Percepción"))
                                {
                                    break;
                                }
                                else if (nombreExistente == nombreDP && tipoExistente == tipoDP)
                                {
                                    yaAsignado = true;
                                    break;
                                }
                            }
                        }

                        if (yaAsignado)
                        {
                            MessageBox.Show($"Esta deducción/percepción ya ha sido asignada al empleado con ID {idEmpleadoSeleccionado}.");
                            continue; // Saltar al siguiente empleado si ya tiene asignada esta deducción/percepción
                        }

                        // Lógica para agregar la DoP a la base de datos
                        using (SqlConnection cn = new SqlConnection(Conexion))
                        {
                            string query = "INSERT INTO DEDPERNOMINA (id_Empleado, id_PD, Mes, Ano) VALUES (@idEmpleado, @idPD, @mes, @ano)";
                            SqlCommand cmd = new SqlCommand(query, cn);
                            cmd.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
                            cmd.Parameters.AddWithValue("@idPD", idPD);
                            cmd.Parameters.AddWithValue("@mes", mes);
                            cmd.Parameters.AddWithValue("@ano", anoSeleccionado);

                            cn.Open();
                            cmd.ExecuteNonQuery();
                        }

                      //  MessageBox.Show($"Deducción/Percepción '{nombreDP}' agregada al empleado con ID {idEmpleadoSeleccionado}.");
                    }
                }

                // Actualizar la tabla intermedia para mostrar los nuevos registros agregados
                mostrarTablaDEDPERNOMINA();
                ColocarDatos();
            }

            //// Verificar que se haya seleccionado una fila válida
            // if (e.RowIndex >= 0)
            // {
            //     int idPD = Convert.ToInt32(dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["id_PD"].Value);
            //     string nombreDP = dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["Nombre_PD"].Value.ToString();
            //     string tipoDP = dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["D_P"].Value.ToString();

            //    // Validación especial para "Vacaciones"
            //     if (nombreDP == "Vacaciones" && tipoDP == "Percepción")
            //     {
            //         DateTime fechaIngreso = ObtenerFechaIngresoEmpleado(idEmpleadoSeleccionado); // Función para obtener fecha de ingreso del empleado
            //         DateTime fechaActual = DateTime.Now;
            //         int antiguedad = fechaActual.Year - fechaIngreso.Year;
            //         if (fechaActual < fechaIngreso.AddYears(antiguedad)) antiguedad--;

            //         if (antiguedad < 1)
            //         {
            //             MessageBox.Show("El empleado no tiene un año de antigüedad y no puede tomar vacaciones.");
            //             return; // Salir si no cumple con la antigüedad
            //         }
            //         else
            //         {
            //             int diasVacaciones = 0;
            //             if (antiguedad == 1) diasVacaciones = 12;
            //             else if (antiguedad == 2) diasVacaciones = 14;
            //             else if (antiguedad == 3) diasVacaciones = 16;
            //             else if (antiguedad == 4) diasVacaciones = 18;
            //             else if (antiguedad == 5) diasVacaciones = 20;
            //             else if (antiguedad >= 6 && antiguedad <= 10) diasVacaciones = 22;
            //             else if (antiguedad >= 11 && antiguedad <= 15) diasVacaciones = 24;

            //             MessageBox.Show($"El empleado tiene derecho a vacaciones y dispone de {diasVacaciones} días.");
            //         }
            //     }

            //    // Verificar si la DoP ya está asignada para deducciones / percepciones que no sean "Falta"
            //     bool yaAsignado = false;

            //    //// Permitir múltiples registros solo para "Falta"
            //     foreach (DataGridViewRow row in dtgv_EmDP_GenerarNomina.Rows)
            //     {
            //         if (row.Cells["Nombre_PD"].Value != null && row.Cells["D_P"].Value != null)
            //         {
            //             string nombreExistente = row.Cells["Nombre_PD"].Value.ToString();
            //             string tipoExistente = row.Cells["D_P"].Value.ToString();

            //             if (nombreExistente == nombreDP && tipoExistente == tipoDP)
            //             {
            //                 if (nombreDP == "Falta" && tipoDP == "Deducción")
            //                 {
            //                     yaAsignado = false;
            //                     break;
            //                 }
            //                 else
            //                 {
            //                     yaAsignado = true;
            //                     break;
            //                 }
            //             }
            //         }
            //     }

            //     if (yaAsignado)
            //     {
            //         MessageBox.Show("Esta deducción/percepción ya ha sido asignada a este empleado.");
            //     }
            //     else
            //     {
            //        // Lógica para agregar la DoP a la tabla y base de datos
            //         if (idEmpleadoSeleccionado > 0)
            //         {
            //             using (SqlConnection cn = new SqlConnection(Conexion))
            //             {
            //                 string query = "INSERT INTO DEDPERNOMINA (id_Empleado, id_PD, Mes, Ano) VALUES (@idEmpleado, @idPD, @mes, @ano)";
            //                 SqlCommand cmd = new SqlCommand(query, cn);
            //                 cmd.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
            //                 cmd.Parameters.AddWithValue("@idPD", idPD);
            //                 cmd.Parameters.AddWithValue("@mes", mes);
            //                 cmd.Parameters.AddWithValue("@ano", anoSeleccionado);

            //                 cn.Open();
            //                 cmd.ExecuteNonQuery();
            //             }

            //            // Actualizar la tabla intermedia para mostrar el nuevo registro agregado
            //             mostrarTablaDEDPERNOMINA();
            //             ColocarDatos();
            //         }
            //         else
            //         {
            //             MessageBox.Show("Por favor, selecciona un empleado y una deducción/percepción válida.");
            //         }
            //     }
            // }

            //////////// FUNCIONAL
            //// Verificar que se haya seleccionado una fila válida
            //if (e.RowIndex >= 0)
            //{
            //    int idPD = Convert.ToInt32(dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["id_PD"].Value);
            //    string nombreDP = dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["Nombre_PD"].Value.ToString();
            //    string tipoDP = dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["D_P"].Value.ToString();

            //    // Verificar si la DoP ya está asignada para deducciones/percepciones que no sean "Falta"
            //    bool yaAsignado = false;

            //    // Permitir múltiples registros solo para "Falta"
            //    foreach (DataGridViewRow row in dtgv_EmDP_GenerarNomina.Rows)
            //    {
            //        // Verifica que las celdas no sean nulas antes de acceder a sus valores
            //        if (row.Cells["Nombre_PD"].Value != null && row.Cells["D_P"].Value != null)
            //        {
            //            string nombreExistente = row.Cells["Nombre_PD"].Value.ToString();
            //            string tipoExistente = row.Cells["D_P"].Value.ToString();

            //            if (nombreExistente == nombreDP && tipoExistente == tipoDP)
            //            {
            //                if (nombreDP == "Falta" && tipoDP == "Deducción")
            //                {
            //                    // Permitir múltiples registros de "Falta" en deducciones
            //                    yaAsignado = false;
            //                    break;
            //                }
            //                else
            //                {
            //                    yaAsignado = true;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    if (yaAsignado)
            //    {
            //        MessageBox.Show("Esta deducción/percepción ya ha sido asignada a este empleado.");
            //    }
            //    else
            //    {
            //        // Llama a la función para agregar DoP
            //        // AgregarDoPEmpleado(idPD);
            //        // Agregar la DoP a la tabla del medio y a la base de datos
            //        // AgregarDoPEmpleado(idPD);
            //        // Aquí agregas la deducción/percepción al empleado en la base de datos y en la tabla del medio
            //        // (Este es tu código existente para agregar el registro)
            //        // Verifica que haya un empleado seleccionado y una celda válida
            //        if (idEmpleadoSeleccionado > 0 && e.RowIndex >= 0)
            //        {
            //            // Captura el ID de la deducción/percepción seleccionada
            //            // int idPD = Convert.ToInt32(dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["id_PD"].Value);

            //            // Inserta el nuevo registro en la tabla DEDPERNOMINA
            //            using (SqlConnection cn = new SqlConnection(Conexion))
            //            {
            //                string query = "INSERT INTO DEDPERNOMINA (id_Empleado, id_PD, Mes, Ano) VALUES (@idEmpleado, @idPD, @mes, @ano)";
            //                SqlCommand cmd = new SqlCommand(query, cn);
            //                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
            //                cmd.Parameters.AddWithValue("@idPD", idPD);
            //                cmd.Parameters.AddWithValue("@mes", mes); // Mes actual, asegúrate de que sea el mismo tipo de datos en tu DB
            //                cmd.Parameters.AddWithValue("@ano", anoSeleccionado); // Año actual

            //                cn.Open();
            //                cmd.ExecuteNonQuery();
            //            }

            //            // Actualiza la tabla intermedia para mostrar el nuevo registro agregado
            //            mostrarTablaDEDPERNOMINA();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Por favor, selecciona un empleado y una deducción/percepción válida.");
            //        }
            //    }
            //}

            //////////////
            //// Verificar que se haya seleccionado una fila válida
            //if (e.RowIndex >= 0)
            //{
            //    int idPD = Convert.ToInt32(dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["id_PD"].Value);

            //    // Verificar si la DoP ya está asignada al empleado en la tabla del medio
            //    bool yaAsignado = false;
            //    foreach (DataGridViewRow row in dtgv_EmDP_GenerarNomina.Rows)
            //    {
            //        if (Convert.ToInt32(row.Cells["id_DPN"].Value) == idPD)
            //        {
            //            yaAsignado = true;
            //            break;
            //        }
            //    }

            //    if (yaAsignado)
            //    {
            //        MessageBox.Show("Esta deducción/percepción ya ha sido asignada a este empleado.");
            //    }
            //    else
            //    {
            //        // Agregar la DoP a la tabla del medio y a la base de datos
            //       // AgregarDoPEmpleado(idPD);
            //        // Aquí agregas la deducción/percepción al empleado en la base de datos y en la tabla del medio
            //        // (Este es tu código existente para agregar el registro)
            //        // Verifica que haya un empleado seleccionado y una celda válida
            //        if (idEmpleadoSeleccionado > 0 && e.RowIndex >= 0)
            //        {
            //            // Captura el ID de la deducción/percepción seleccionada
            //           // int idPD = Convert.ToInt32(dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["id_PD"].Value);

            //            // Inserta el nuevo registro en la tabla DEDPERNOMINA
            //            using (SqlConnection cn = new SqlConnection(Conexion))
            //            {
            //                string query = "INSERT INTO DEDPERNOMINA (id_Empleado, id_PD, Mes, Ano) VALUES (@idEmpleado, @idPD, @mes, @ano)";
            //                SqlCommand cmd = new SqlCommand(query, cn);
            //                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
            //                cmd.Parameters.AddWithValue("@idPD", idPD);
            //                cmd.Parameters.AddWithValue("@mes", mes); // Mes actual, asegúrate de que sea el mismo tipo de datos en tu DB
            //                cmd.Parameters.AddWithValue("@ano", anoSeleccionado); // Año actual

            //                cn.Open();
            //                cmd.ExecuteNonQuery();
            //            }

            //            // Actualiza la tabla intermedia para mostrar el nuevo registro agregado
            //            mostrarTablaDEDPERNOMINA();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Por favor, selecciona un empleado y una deducción/percepción válida.");
            //        }
            //    }
            //}
        }
        private decimal ObtenerSalarioDiarioEmpleado(int idEmpleado)
        {
            decimal salarioDiario = 0;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                string query = "SELECT SalarioDiario FROM Empleado WHERE id_Empleado = @idEmpleado";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                cn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    salarioDiario = Convert.ToDecimal(result);
                }
            }

            return salarioDiario;
        }

        //private void AgregarDoPEmpleado(int idPD)
        //{
        //    // Aquí agregas la deducción/percepción al empleado en la base de datos y en la tabla del medio
        //    // (Este es tu código existente para agregar el registro)
        //     // Verifica que haya un empleado seleccionado y una celda válida
        //    if (idEmpleadoSeleccionado > 0 && e.RowIndex >= 0)
        //    {
        //        // Captura el ID de la deducción/percepción seleccionada
        //        int idPD = Convert.ToInt32(dtgv_DP_GenerarNomina.Rows[e.RowIndex].Cells["id_PD"].Value);

        //        // Inserta el nuevo registro en la tabla DEDPERNOMINA
        //        using (SqlConnection cn = new SqlConnection(Conexion))
        //        {
        //            string query = "INSERT INTO DEDPERNOMINA (id_Empleado, id_PD, Mes, Ano) VALUES (@idEmpleado, @idPD, @mes, @ano)";
        //            SqlCommand cmd = new SqlCommand(query, cn);
        //            cmd.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
        //            cmd.Parameters.AddWithValue("@idPD", idPD);
        //            cmd.Parameters.AddWithValue("@mes", mes); // Mes actual, asegúrate de que sea el mismo tipo de datos en tu DB
        //            cmd.Parameters.AddWithValue("@ano", anoSeleccionado); // Año actual

        //            cn.Open();
        //            cmd.ExecuteNonQuery();
        //        }

        //        // Actualiza la tabla intermedia para mostrar el nuevo registro agregado
        //        mostrarTablaDEDPERNOMINA();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Por favor, selecciona un empleado y una deducción/percepción válida.");
        //    }
        //}




        //private void CalcularTotalesDesdeBD(int idEmpleado, string mes, int ano)
        //{
        //    decimal totalDeducciones = 0;
        //    decimal totalPercepciones = 0;

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        // Consulta para el total de deducciones
        //        string queryDeducciones = @"
        //SELECT SUM(CASE 
        //    WHEN DP.Porcentaje_PD IS NOT NULL 
        //        THEN DP.Porcentaje_PD * p.SalarioDiario / 100
        //    ELSE DP.MontoPD END) AS TotalDeducciones
        //FROM DEDPERNOMINA DPN
        //JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
        //JOIN Empleado e ON DPN.id_Empleado = e.id_Empleado
        //JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //WHERE DPN.id_Empleado = @idEmpleado
        //  AND DP.D_P = 'Deducción'
        //  AND DPN.Mes = @mes
        //  AND DPN.Ano = @ano";

        //        SqlCommand cmdDeducciones = new SqlCommand(queryDeducciones, cn);
        //        cmdDeducciones.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //        cmdDeducciones.Parameters.AddWithValue("@mes", mes); // Asumiendo que `mes` es el nombre del mes en texto
        //        cmdDeducciones.Parameters.AddWithValue("@ano", ano);

        //        var resultDeducciones = cmdDeducciones.ExecuteScalar();
        //        totalDeducciones = resultDeducciones != DBNull.Value ? Convert.ToDecimal(resultDeducciones) : 0;

        //        // Consulta para el total de percepciones
        //        string queryPercepciones = @"
        //SELECT SUM(CASE 
        //    WHEN DP.Porcentaje_PD IS NOT NULL 
        //        THEN DP.Porcentaje_PD * p.SalarioDiario / 100
        //    ELSE DP.MontoPD END) AS TotalPercepciones
        //FROM DEDPERNOMINA DPN
        //JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
        //JOIN Empleado e ON DPN.id_Empleado = e.id_Empleado
        //JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //WHERE DPN.id_Empleado = @idEmpleado
        //  AND DP.D_P = 'Percepción'
        //  AND DPN.Mes = @mes
        //  AND DPN.Ano = @ano";

        //        SqlCommand cmdPercepciones = new SqlCommand(queryPercepciones, cn);
        //        cmdPercepciones.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //        cmdPercepciones.Parameters.AddWithValue("@mes", mes); // Nombre del mes en texto
        //        cmdPercepciones.Parameters.AddWithValue("@ano", ano);

        //        var resultPercepciones = cmdPercepciones.ExecuteScalar();
        //         totalPercepciones = resultPercepciones != DBNull.Value ? Convert.ToDecimal(resultPercepciones) : 0;

        //        // Mostrar los resultados para verificar
        //        MessageBox.Show($"Total Deducciones: {totalDeducciones}, Total Percepciones: {totalPercepciones}");
        //    }


        //    // Mostrar los totales para verificar
        //   // MessageBox.Show($"Total Deducciones: {totalDeducciones}, Total Percepciones: {totalPercepciones}");
        //}



        /// <summary>
        /// //////////////////
        /// </summary>
        /// 
        ///---------
        ///
        private (decimal totalDeducciones, decimal totalPercepciones) CalcularTotalesDesdeBD(int idEmpleado, string mes, int ano, decimal sueldoBruto, int faltas, decimal sueldoDiario)
        {
            decimal totalDeducciones = 0;
            decimal totalPercepciones = 0;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                // Calcular deducción por faltas usando sueldo diario
                decimal deduccionFaltas = faltas * sueldoDiario;
                totalDeducciones += deduccionFaltas;

                // Resto de deducciones
                string queryDeducciones = @"
        SELECT SUM(CASE 
            WHEN DP.Porcentaje_PD IS NOT NULL 
                THEN DP.Porcentaje_PD * @sueldoBruto / 100
            ELSE DP.MontoPD END) AS TotalDeducciones
        FROM DEDPERNOMINA DPN
        JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
        WHERE DPN.id_Empleado = @idEmpleado
          AND DP.D_P = 'Deducción'
          AND DP.Nombre_PD != 'Falta'
          AND DPN.Mes = @mes
          AND DPN.Ano = @ano";

                SqlCommand cmdDeducciones = new SqlCommand(queryDeducciones, cn);
                cmdDeducciones.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                cmdDeducciones.Parameters.AddWithValue("@mes", mes);
                cmdDeducciones.Parameters.AddWithValue("@ano", ano);
                cmdDeducciones.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);

                var resultDeducciones = cmdDeducciones.ExecuteScalar();
                totalDeducciones += resultDeducciones != DBNull.Value ? Convert.ToDecimal(resultDeducciones) : 0;

                // Calcular percepciones
                string queryPercepciones = @"
        SELECT SUM(CASE 
            WHEN DP.Porcentaje_PD IS NOT NULL 
                THEN DP.Porcentaje_PD * @sueldoBruto / 100
            ELSE DP.MontoPD END) AS TotalPercepciones
        FROM DEDPERNOMINA DPN
        JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
        WHERE DPN.id_Empleado = @idEmpleado
          AND DP.D_P = 'Percepción'
          AND DPN.Mes = @mes
          AND DPN.Ano = @ano";

                SqlCommand cmdPercepciones = new SqlCommand(queryPercepciones, cn);
                cmdPercepciones.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                cmdPercepciones.Parameters.AddWithValue("@mes", mes);
                cmdPercepciones.Parameters.AddWithValue("@ano", ano);
                cmdPercepciones.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);

                var resultPercepciones = cmdPercepciones.ExecuteScalar();
                totalPercepciones = resultPercepciones != DBNull.Value ? Convert.ToDecimal(resultPercepciones) : 0;
            }

            return (totalDeducciones, totalPercepciones);
        }

        //private (decimal totalDeducciones, decimal totalPercepciones) CalcularTotalesDesdeBD(int idEmpleado, string mes, int ano, decimal sueldoBruto)
        //{
        //    decimal totalDeducciones = 0;
        //    decimal totalPercepciones = 0;

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        // Consulta para el total de deducciones usando SueldoBruto
        //        string queryDeducciones = @"
        //SELECT SUM(CASE 
        //    WHEN DP.Porcentaje_PD IS NOT NULL 
        //        THEN DP.Porcentaje_PD * @sueldoBruto / 100
        //    ELSE DP.MontoPD END) AS TotalDeducciones
        //FROM DEDPERNOMINA DPN
        //JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
        //WHERE DPN.id_Empleado = @idEmpleado
        //  AND DP.D_P = 'Deducción'
        //  AND DPN.Mes = @mes
        //  AND DPN.Ano = @ano";

        //        SqlCommand cmdDeducciones = new SqlCommand(queryDeducciones, cn);
        //        cmdDeducciones.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //        cmdDeducciones.Parameters.AddWithValue("@mes", mes);
        //        cmdDeducciones.Parameters.AddWithValue("@ano", ano);
        //        cmdDeducciones.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);

        //        var resultDeducciones = cmdDeducciones.ExecuteScalar();
        //        totalDeducciones = resultDeducciones != DBNull.Value ? Convert.ToDecimal(resultDeducciones) : 0;

        //        // Consulta para el total de percepciones usando SueldoBruto
        //        string queryPercepciones = @"
        //SELECT SUM(CASE 
        //    WHEN DP.Porcentaje_PD IS NOT NULL 
        //        THEN DP.Porcentaje_PD * @sueldoBruto / 100
        //    ELSE DP.MontoPD END) AS TotalPercepciones
        //FROM DEDPERNOMINA DPN
        //JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
        //WHERE DPN.id_Empleado = @idEmpleado
        //  AND DP.D_P = 'Percepción'
        //  AND DPN.Mes = @mes
        //  AND DPN.Ano = @ano";

        //        SqlCommand cmdPercepciones = new SqlCommand(queryPercepciones, cn);
        //        cmdPercepciones.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //        cmdPercepciones.Parameters.AddWithValue("@mes", mes);
        //        cmdPercepciones.Parameters.AddWithValue("@ano", ano);
        //        cmdPercepciones.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);

        //        var resultPercepciones = cmdPercepciones.ExecuteScalar();
        //        totalPercepciones = resultPercepciones != DBNull.Value ? Convert.ToDecimal(resultPercepciones) : 0;
        //    }

        //    return (totalDeducciones, totalPercepciones);
        //}
        ///------


        //private (decimal totalDeducciones, decimal totalPercepciones) CalcularTotalesDesdeBD(int idEmpleado, string mes, int ano)
        //{
        //    decimal totalDeducciones = 0;
        //    decimal totalPercepciones = 0;

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        // Consulta para el total de deducciones
        //        string queryDeducciones = @"
        //SELECT SUM(CASE 
        //    WHEN DP.Porcentaje_PD IS NOT NULL 
        //        THEN DP.Porcentaje_PD * p.SalarioDiario / 100
        //    ELSE DP.MontoPD END) AS TotalDeducciones
        //FROM DEDPERNOMINA DPN
        //JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
        //JOIN Empleado e ON DPN.id_Empleado = e.id_Empleado
        //JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //WHERE DPN.id_Empleado = @idEmpleado
        //  AND DP.D_P = 'Deducción'
        //  AND DPN.Mes = @mes
        //  AND DPN.Ano = @ano";

        //        SqlCommand cmdDeducciones = new SqlCommand(queryDeducciones, cn);
        //        cmdDeducciones.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //        cmdDeducciones.Parameters.AddWithValue("@mes", mes); // Asumiendo que `mes` es el nombre del mes en texto
        //        cmdDeducciones.Parameters.AddWithValue("@ano", ano);

        //        var resultDeducciones = cmdDeducciones.ExecuteScalar();
        //        totalDeducciones = resultDeducciones != DBNull.Value ? Convert.ToDecimal(resultDeducciones) : 0;

        //        // Consulta para el total de percepciones
        //        string queryPercepciones = @"
        //SELECT SUM(CASE 
        //    WHEN DP.Porcentaje_PD IS NOT NULL 
        //        THEN DP.Porcentaje_PD * p.SalarioDiario / 100
        //    ELSE DP.MontoPD END) AS TotalPercepciones
        //FROM DEDPERNOMINA DPN
        //JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
        //JOIN Empleado e ON DPN.id_Empleado = e.id_Empleado
        //JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //WHERE DPN.id_Empleado = @idEmpleado
        //  AND DP.D_P = 'Percepción'
        //  AND DPN.Mes = @mes
        //  AND DPN.Ano = @ano";

        //        SqlCommand cmdPercepciones = new SqlCommand(queryPercepciones, cn);
        //        cmdPercepciones.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //        cmdPercepciones.Parameters.AddWithValue("@mes", mes); // Nombre del mes en texto
        //        cmdPercepciones.Parameters.AddWithValue("@ano", ano);

        //        var resultPercepciones = cmdPercepciones.ExecuteScalar();
        //        totalPercepciones = resultPercepciones != DBNull.Value ? Convert.ToDecimal(resultPercepciones) : 0;
        //    }

        //    return (totalDeducciones, totalPercepciones);
        //}
        ////////////////////////

        public void AsignarMes()
        {
            int mesNumero = DateTime.Now.Month; // Obtiene el mes en formato numérico (1 a 12)
            mesNumero = 12;
            switch (mesNumero)
            {
                case 1:
                    mes = "Enero";
                    break;
                case 2:
                    mes = "Febrero";
                    break;
                case 3:
                    mes = "Marzo";
                    break;
                case 4:
                    mes = "Abril";
                    break;
                case 5:
                    mes = "Mayo";
                    break;
                case 6:
                    mes = "Junio";
                    break;
                case 7:
                    mes = "Julio";
                    break;
                case 8:
                    mes = "Agosto";
                    break;
                case 9:
                    mes = "Septiembre";
                    break;
                case 10:
                    mes = "Octubre";
                    break;
                case 11:
                    mes = "Noviembre";
                    break;
                case 12:
                    mes = "Diciembre";
                    break;
                default:
                    mes = "Desconocido"; // Caso de error, aunque no debería suceder
                    break;
            }
        }

        private void btn_Eliminar_GenerarNomina_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada en el DataGridView del medio
            if (dtgv_EmDP_GenerarNomina.SelectedRows.Count > 0)
            {
                // Obtiene el ID de la DoP (id_DPN) de la fila seleccionada
                int idDPN = Convert.ToInt32(dtgv_EmDP_GenerarNomina.SelectedRows[0].Cells["id_DPN"].Value);

                // Confirmar la eliminación con el usuario
                DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar esta deducción/percepción?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection cn = new SqlConnection(Conexion))
                    {
                        cn.Open();
                        // Elimina el registro de la tabla DEDPERNOMINA
                        string query = "DELETE FROM DEDPERNOMINA WHERE id_DPN = @idDPN";
                        using (SqlCommand cmd = new SqlCommand(query, cn))
                        {
                            cmd.Parameters.AddWithValue("@idDPN", idDPN);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Refrescar la tabla del medio para mostrar los cambios
                    mostrarTablaDEDPERNOMINA();
                    ColocarDatos();
                    MessageBox.Show("Deducción/Percepción eliminada exitosamente.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una deducción/percepción para eliminar.");
            }
        }


        private void GenerarNominaIndividual()
        {
            // Asegurarnos de tener un empleado seleccionado
            if (idEmpleadoSeleccionado > 0)
            {
                // Obtener los datos necesarios de las tablas relacionadas
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    // Iniciar la transacción para asegurarnos de que todos los cambios se realicen juntos
                    cn.Open();
                    using (SqlTransaction transaction = cn.BeginTransaction())
                    {
                        try
                        {
                            // Obtener información del empleado, departamento y puesto
                            string queryEmpleado = @"
                        SELECT E.id_Empleado, E.id_Departamento, E.id_Puesto, P.SalarioDiario, D.SueldoBase
                        FROM Empleado E
                        JOIN Puestos P ON E.id_Puesto = P.id_Puesto
                        JOIN Departamento D ON E.id_Departamento = D.id_Departamento
                        WHERE E.id_Empleado = @idEmpleado";

                            SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn, transaction);
                            cmdEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);

                            SqlDataReader reader = cmdEmpleado.ExecuteReader();

                            if (!reader.Read())
                            {
                                MessageBox.Show("Error: El empleado seleccionado no existe.");
                                return;
                            }

                            // Extraer los datos del empleado, puesto y departamento
                            int idDepartamento = reader.GetInt32(reader.GetOrdinal("id_Departamento"));
                            int idPuesto = reader.GetInt32(reader.GetOrdinal("id_Puesto"));
                            decimal salarioDiario = reader.GetDecimal(reader.GetOrdinal("SalarioDiario"));
                            decimal sueldoBase = reader.GetDecimal(reader.GetOrdinal("SueldoBase"));
                            reader.Close();

                            // Calcular el sueldo bruto
                            int diasTrabajados = 30; // Asumimos que el mes tiene 30 días
                            decimal sueldoBruto = salarioDiario * diasTrabajados;

                            // Calcular las deducciones y percepciones para el mes y año actual
                            decimal totalDeducciones = 0;
                            decimal totalPercepciones = 0;

                            // Consultar deducciones y percepciones del empleado
                            string queryDeduccionesPercepciones = @"
                        SELECT DP.D_P, DP.MontoPD, DP.Porcentaje_PD
                        FROM DEDPERNOMINA DPN
                        JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
                        WHERE DPN.id_Empleado = @idEmpleado
                        AND DPN.Mes = @mes AND DPN.Ano = @ano";

                            SqlCommand cmdDP = new SqlCommand(queryDeduccionesPercepciones, cn, transaction);
                            cmdDP.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
                            cmdDP.Parameters.AddWithValue("@mes", mes);
                            cmdDP.Parameters.AddWithValue("@ano", anoSeleccionado);

                            SqlDataReader dpReader = cmdDP.ExecuteReader();
                            while (dpReader.Read())
                            {
                                string tipo = dpReader.GetString(dpReader.GetOrdinal("D_P"));
                                decimal monto = dpReader.IsDBNull(dpReader.GetOrdinal("MontoPD")) ? 0 : dpReader.GetDecimal(dpReader.GetOrdinal("MontoPD"));
                                decimal porcentaje = dpReader.IsDBNull(dpReader.GetOrdinal("Porcentaje_PD")) ? 0 : dpReader.GetDecimal(dpReader.GetOrdinal("Porcentaje_PD"));

                                if (tipo == "Deducción")
                                {
                                    totalDeducciones += monto > 0 ? monto : sueldoBruto * porcentaje / 100;
                                }
                                else if (tipo == "Percepción")
                                {
                                    totalPercepciones += monto > 0 ? monto : sueldoBruto * porcentaje / 100;
                                }
                            }
                            dpReader.Close();

                            // Calcular sueldo neto
                            decimal sueldoNeto = sueldoBruto + totalPercepciones - totalDeducciones;

                            // Insertar los datos calculados en la tabla NominaIndividual
                            string queryInsertNomina = @"
                        INSERT INTO NominaIndividual (
                            idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto,
                            DiasTrabajados, totalDeducciones, totalPercepciones, Mes, Ano)
                        VALUES (
                            @idEmpleado, @idDepartamento, @idPuesto, @sueldoBruto, @sueldoNeto,
                            @diasTrabajados, @totalDeducciones, @totalPercepciones, @mes, @ano)";

                            SqlCommand cmdInsertNomina = new SqlCommand(queryInsertNomina, cn, transaction);
                            cmdInsertNomina.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
                            cmdInsertNomina.Parameters.AddWithValue("@idDepartamento", idDepartamento);
                            cmdInsertNomina.Parameters.AddWithValue("@idPuesto", idPuesto);
                            cmdInsertNomina.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);
                            cmdInsertNomina.Parameters.AddWithValue("@sueldoNeto", sueldoNeto);
                            cmdInsertNomina.Parameters.AddWithValue("@diasTrabajados", diasTrabajados);
                            cmdInsertNomina.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
                            cmdInsertNomina.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
                            cmdInsertNomina.Parameters.AddWithValue("@mes", mes);
                            cmdInsertNomina.Parameters.AddWithValue("@ano", anoSeleccionado);

                            cmdInsertNomina.ExecuteNonQuery();

                            // Confirmar la transacción
                            transaction.Commit();

                            MessageBox.Show("Nómina generada exitosamente para el empleado.");
                        }
                        catch (Exception ex)
                        {
                            // Revertir la transacción en caso de error
                            transaction.Rollback();
                            MessageBox.Show("Error al generar la nómina: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un empleado para generar la nómina.");
            }
        }

//        private void btn_GenerarNomina_GenerarNomina_Click(object sender, EventArgs e)
//        {
//            // GenerarNominaIndividual();
//            //GenerarNomina(100,10, 2024);
//            //ObtenerInformacionEmpleado(100);
//            //ObtenerSalarioDiario(1);
//            // Verifica si hay una fila seleccionada en el DataGridView (ejemplo: dtgv_Empleados)
//            if (dtgv_Empleados_GenerarNomina.SelectedRows.Count > 0)
//            {
//                // Obtiene la fila seleccionada
//                DataGridViewRow filaSeleccionada = dtgv_Empleados_GenerarNomina.SelectedRows[0];

//                // Obtiene los valores de las celdas de esa fila (ajusta los nombres de columna según tu DataGridView)
//                int idEmpleado = Convert.ToInt32(filaSeleccionada.Cells["id_Empleado"].Value);
//                //string nombreEmpleado = filaSeleccionada.Cells["NombreEmpleado"].Value.ToString();
//                //string apellidoPaterno = filaSeleccionada.Cells["ApelPaternoEmpleado"].Value.ToString();
//                //string apellidoMaterno = filaSeleccionada.Cells["ApelMaternoEmpleado"].Value.ToString();

//                // Muestra un mensaje con los datos del empleado seleccionado
//                // MessageBox.Show($"Empleado seleccionado:\nID: {idEmpleado}\nNombre: {nombreEmpleado} {apellidoPaterno} {apellidoMaterno}");
//               // ObtenerDatosEmpleado(idEmpleado); //1
//                //int faltas=ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);//2
//                //int DiasTrabajados = 30 - faltas;
//                //float SueldoBruto = DiasTrabajados*
//                //CalcularISR(float sueldoBruto);
//                //CalcularTotalesDesdeBD(100, "Noviembre", 2024);

//                /////////////////////////////
//            //    //int idEmpleado = 0;
//            //    int idDepartamento = 0;
//            //    int idPuesto = 0;
//            //    float salarioDiario = 0;

//            //    using (SqlConnection cn = new SqlConnection(Conexion))
//            //    {
//            //        cn.Open();

//            //        string query = @"
//            //SELECT e.id_Empleado, e.id_Departamento, e.id_Puesto, p.SalarioDiario
//            //FROM Empleado e
//            //JOIN Puestos p ON e.id_Puesto = p.id_Puesto
//            //WHERE e.id_Empleado = @idEmpleado";

//            //        SqlCommand cmd = new SqlCommand(query, cn);
//            //        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

//            //        SqlDataReader reader = cmd.ExecuteReader();

//            //        if (reader.Read())
//            //        {
//            //            idEmpleado = reader.GetInt32(0);
//            //            idDepartamento = reader.GetInt32(1);
//            //            idPuesto = reader.GetInt32(2);
//            //            // salarioDiario = reader.GetFloat(3); // Cambiado a GetFloat para el tipo float
//            //            salarioDiario = Convert.ToSingle(reader.GetValue(3));

//            //        }
//            //        reader.Close();
//            //    }
//                /////////////////////

//               // var (departamento, puesto, salarioDiario) = ObtenerDatosEmpleado(idEmpleado);//TUPLA
//               var (departamento, puesto, salarioDiario, salarioDiarioIntegrado) = ObtenerDatosEmpleado(idEmpleado);

//                // MessageBox.Show($"Departamento: {departamento}, Puesto: {puesto}, Salario Diario: {salario}");

//                int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);//2
//                int DiasTrabajados = 30 - faltas;
//                //decimal SueldoBruto = DiasTrabajados * salarioDiario;
//                //float ISR=CalcularISR(SueldoBruto);
//                //float SueldoBruto = (float)(DiasTrabajados * salarioDiario);
//                decimal SueldoBruto = DiasTrabajados * (decimal)salarioDiario;

//                //float ISR = CalcularISR(SueldoBruto);
//                decimal ISR = CalcularISR(SueldoBruto);
//                // decimal ISR = CalcularISR((float)SueldoBruto);
//                decimal IMSS= SueldoBruto*0.1225m;

//                // CalcularTotalesDesdeBD(100, "Noviembre", 2024);
//               // var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado);
//               // var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, SueldoBruto);
//                var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, SueldoBruto, faltas, (decimal)salarioDiario);
//                // MessageBox.Show($"Total Deducciones: {deducciones}, Total Percepciones: {percepciones}");
//                decimal TotalDeducciones = deducciones+ (ISR + IMSS);
//                decimal TotalPercepciones = percepciones;
//                decimal SueldoNeto = (SueldoBruto + TotalPercepciones) - TotalDeducciones;


//                using (SqlConnection cn = new SqlConnection(Conexion))
//                {
//                    cn.Open();

//                    //            string queryInsert = @"
//                    //INSERT INTO NominaIndividual 
//                    //(idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto, DiasTrabajados, totalDeducciones, totalPercepciones, Mes, Ano) 
//                    //VALUES (@idEmpleado, @idDepartamento, @idPuesto, @SueldoBruto, @SueldoNeto, @DiasTrabajados, @totalDeducciones, @totalPercepciones, @Mes, @Ano)";

//                    string queryInsert = @"
//INSERT INTO NominaIndividual 
//(idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto, DiasTrabajados, totalDeducciones, totalPercepciones, ISR, IMSS, Mes, Ano) 
//VALUES (@idEmpleado, @idDepartamento, @idPuesto, @SueldoBruto, @SueldoNeto, @DiasTrabajados, @totalDeducciones, @totalPercepciones, @ISR, @IMSS, @Mes, @Ano)";


//                    //using (SqlCommand cmd = new SqlCommand(queryInsert, cn))
//                    //{
//                    //    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                    //    cmd.Parameters.AddWithValue("@idDepartamento", departamento);
//                    //    cmd.Parameters.AddWithValue("@idPuesto", puesto);
//                    //    cmd.Parameters.AddWithValue("@SueldoBruto", SueldoBruto); // Asegúrate de que esta variable sea decimal
//                    //    cmd.Parameters.AddWithValue("@SueldoNeto", SueldoNeto);   // También debe ser decimal
//                    //    cmd.Parameters.AddWithValue("@DiasTrabajados", DiasTrabajados);
//                    //    cmd.Parameters.AddWithValue("@totalDeducciones", TotalDeducciones); // decimal
//                    //    cmd.Parameters.AddWithValue("@totalPercepciones", TotalPercepciones); // decimal
//                    //    cmd.Parameters.AddWithValue("@Mes", mes);
//                    //    cmd.Parameters.AddWithValue("@Ano", anoSeleccionado);

//                    //    cmd.ExecuteNonQuery();
//                    //}

//                    using (SqlCommand cmd = new SqlCommand(queryInsert, cn))
//                    {
//                        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                        cmd.Parameters.AddWithValue("@idDepartamento", departamento);
//                        cmd.Parameters.AddWithValue("@idPuesto", puesto);
//                        cmd.Parameters.AddWithValue("@SueldoBruto", SueldoBruto); // Asegúrate de que esta variable sea decimal
//                        cmd.Parameters.AddWithValue("@SueldoNeto", SueldoNeto);   // También debe ser decimal
//                        cmd.Parameters.AddWithValue("@DiasTrabajados", DiasTrabajados);
//                        cmd.Parameters.AddWithValue("@totalDeducciones", TotalDeducciones); // decimal
//                        cmd.Parameters.AddWithValue("@totalPercepciones", TotalPercepciones); // decimal
//                        cmd.Parameters.AddWithValue("@ISR", ISR); // Añadir ISR
//                        cmd.Parameters.AddWithValue("@IMSS", IMSS); // Añadir IMSS
//                        cmd.Parameters.AddWithValue("@Mes", mes);
//                        cmd.Parameters.AddWithValue("@Ano", anoSeleccionado);

//                        cmd.ExecuteNonQuery();
//                    }

//                }
//                MessageBox.Show("Nomina Individual Guardada");
//                MessageBox.Show("Empleado seleccionado con ID: " + ISR);
//            }
//            else
//            {
//                // Si no hay una fila seleccionada, muestra un mensaje de advertencia
//                MessageBox.Show("Por favor, selecciona un empleado de la lista.");
//            }

//            //ObtenerDatosEmpleado(100); //1
//            //ContarFaltasEmpleado(int idEmpleado, string mes, int ano);//2
//            ////CalcularTotalesDeduccionesPercepciones(100);
//            //CalcularISR(float sueldoBruto);
//            //CalcularTotalesDesdeBD(100, "Noviembre", 2024);
            
//        }



        private void GenerarNominasIndividuales(object sender, EventArgs e)
        {
            if (dtgv_Empleados_GenerarNomina.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dtgv_Empleados_GenerarNomina.Rows)
                {
                    // Validar que la fila tenga datos
                    if (fila.Cells["id_Empleado"].Value == null)
                        continue;

                    int idEmpleado = Convert.ToInt32(fila.Cells["id_Empleado"].Value);

                    // Obtener datos del empleado
                   // var (departamento, puesto, salarioDiario) = ObtenerDatosEmpleado(idEmpleado);
                    var (departamento, puesto, salarioDiario, salarioDiarioIntegrado) = ObtenerDatosEmpleado(idEmpleado);
                    // Definir mes y año (puedes agregar controles para elegir el mes y año o asignarlos directamente aquí)
                    // string mes = "Noviembre"; // Ejemplo
                    //int anoSeleccionado = 2024; // Ejemplo

                    // Calcular faltas, días trabajados, y sueldo bruto
                    int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);
                    int diasTrabajados = 30 - faltas;
                    decimal sueldoBruto = diasTrabajados * (decimal)salarioDiario;

                    // Calcular ISR y IMSS
                    decimal isr = CalcularISR(sueldoBruto);
                    decimal imss = sueldoBruto * 0.1225m;

                    // Calcular totales de deducciones y percepciones desde la base de datos
                    //var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado);
                   // var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto);
                    var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto, faltas, (decimal)salarioDiario);
                    decimal totalDeducciones = deducciones + isr + imss;
                    decimal totalPercepciones = percepciones;

                    // Calcular sueldo neto
                    decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;

                    // Insertar la nómina individual en la base de datos
                    using (SqlConnection cn = new SqlConnection(Conexion))
                    {
                        cn.Open();
                        string queryInsert = @"
                    INSERT INTO NominaIndividual 
                    (idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto, DiasTrabajados, totalDeducciones, totalPercepciones, ISR, IMSS, Mes, Ano) 
                    VALUES (@idEmpleado, @idDepartamento, @idPuesto, @SueldoBruto, @SueldoNeto, @DiasTrabajados, @totalDeducciones, @totalPercepciones, @ISR, @IMSS, @Mes, @Ano)";

                        using (SqlCommand cmd = new SqlCommand(queryInsert, cn))
                        {
                            cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                            cmd.Parameters.AddWithValue("@idDepartamento", departamento);
                            cmd.Parameters.AddWithValue("@idPuesto", puesto);
                            cmd.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
                            cmd.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
                            cmd.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
                            cmd.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
                            cmd.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
                            cmd.Parameters.AddWithValue("@ISR", isr);
                            cmd.Parameters.AddWithValue("@IMSS", imss);
                            cmd.Parameters.AddWithValue("@Mes", mes);
                            cmd.Parameters.AddWithValue("@Ano", anoSeleccionado);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Nóminas individuales generadas para todos los empleados.");
            }
            else
            {
                MessageBox.Show("No hay empleados en la lista.");
            }

        }

        //private void GenerarNominasIndividuales2(object sender,EventArgs e)
        //{
        //    if (dtgv_Empleados_GenerarNomina.Rows.Count > 0)
        //    {
        //        foreach (DataGridViewRow fila in dtgv_Empleados_GenerarNomina.Rows)
        //        {
        //            // Validar que la fila tenga datos y que el empleado esté activo
        //            if (fila.Cells["id_Empleado"].Value == null || fila.Cells["activo"].Value == null)
        //                continue;

        //            bool isActive = Convert.ToBoolean(fila.Cells["activo"].Value); // Verificar si el empleado está activo
        //            if (!isActive)
        //                continue; // Saltar este empleado si no está activo

        //            int idEmpleado = Convert.ToInt32(fila.Cells["id_Empleado"].Value);

        //            // Obtener datos del empleado
        //            var (departamento, puesto, salarioDiario, salarioDiarioIntegrado) = ObtenerDatosEmpleado(idEmpleado);

        //            // Definir mes y año
        //            // string mes = "Noviembre"; // Ejemplo
        //            // int anoSeleccionado = 2024; // Ejemplo

        //            var (diasVacaciones, montoVacaciones, primaVacacional) = CalcularVacaciones(idEmpleado, mes, anoSeleccionado, (decimal)salarioDiario);
        //            // Calcular faltas, días trabajados, y sueldo bruto
        //            int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);

        //            int diasTrabajados = 30 - faltas -diasVacaciones;
        //            decimal sueldoBruto = diasTrabajados * (decimal)salarioDiario;

        //            // Calcular el aguinaldo solo si es diciembre
        //            decimal aguinaldo = 0;
        //            if (mes.Equals("Diciembre", StringComparison.OrdinalIgnoreCase))
        //            {
        //                aguinaldo = (decimal)salarioDiario * 18;
        //            }

        //            // Calcular ISR y IMSS
        //            decimal isr = CalcularISR(sueldoBruto);
        //            decimal imss = (decimal)salarioDiarioIntegrado * 0.05m;

        //            // Calcular totales de deducciones y percepciones desde la base de datos
        //            var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto, faltas, (decimal)salarioDiario);
        //            decimal totalDeducciones = deducciones + isr + imss;
        //            //decimal totalPercepciones = percepciones;
        //            //decimal totalPercepciones = percepciones + montoVacaciones + primaVacacional; // Agregar vacaciones y prima vacacional a las percepciones
        //            decimal totalPercepciones = percepciones + montoVacaciones + primaVacacional + aguinaldo;



        //            // Calcular sueldo neto
        //            decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;

        //            using (SqlConnection cn = new SqlConnection(Conexion))
        //            {
        //                cn.Open();

        //                // Verificar si ya existe una nómina para este empleado, mes y año
        //                string querySelect = @"
        //SELECT COUNT(1) 
        //FROM NominaIndividual 
        //WHERE idEmpleadoFK = @idEmpleado AND Mes = @Mes AND Ano = @Ano";

        //                SqlCommand cmdSelect = new SqlCommand(querySelect, cn);
        //                cmdSelect.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //                cmdSelect.Parameters.AddWithValue("@Mes", mes);
        //                cmdSelect.Parameters.AddWithValue("@Ano", anoSeleccionado);

        //                int count = Convert.ToInt32(cmdSelect.ExecuteScalar());

        //                if (count > 0)
        //                {
        //                    // Actualización si ya existe
        //                    string queryUpdate = @"
        //    UPDATE NominaIndividual 
        //    SET idDepartamento = @idDepartamento, 
        //        idPuesto = @idPuesto, 
        //        SueldoBruto = @SueldoBruto, 
        //        SueldoNeto = @SueldoNeto, 
        //        DiasTrabajados = @DiasTrabajados, 
        //        totalDeducciones = @totalDeducciones, 
        //        totalPercepciones = @totalPercepciones, 
        //        ISR = @ISR, 
        //        IMSS = @IMSS,
        //        SalarioDiario = @SalarioDiario
        //    WHERE idEmpleadoFK = @idEmpleado AND Mes = @Mes AND Ano = @Ano";

        //                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, cn))
        //                    {
        //                        cmdUpdate.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //                        cmdUpdate.Parameters.AddWithValue("@idDepartamento", departamento);
        //                        cmdUpdate.Parameters.AddWithValue("@idPuesto", puesto);
        //                        cmdUpdate.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
        //                        cmdUpdate.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
        //                        cmdUpdate.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
        //                        cmdUpdate.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
        //                        cmdUpdate.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
        //                        cmdUpdate.Parameters.AddWithValue("@ISR", isr);
        //                        cmdUpdate.Parameters.AddWithValue("@IMSS", imss);
        //                        cmdUpdate.Parameters.AddWithValue("@SalarioDiario", salarioDiario); // Nuevo campo
        //                        cmdUpdate.Parameters.AddWithValue("@Mes", mes);
        //                        cmdUpdate.Parameters.AddWithValue("@Ano", anoSeleccionado);

        //                        cmdUpdate.ExecuteNonQuery();
        //                    }
        //                }
        //                else
        //                {
        //                    // Inserción si no existe
        //                    string queryInsert = @"
        //    INSERT INTO NominaIndividual 
        //    (idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto, DiasTrabajados, totalDeducciones, totalPercepciones, ISR, IMSS, Mes, Ano, SalarioDiario) 
        //    VALUES (@idEmpleado, @idDepartamento, @idPuesto, @SueldoBruto, @SueldoNeto, @DiasTrabajados, @totalDeducciones, @totalPercepciones, @ISR, @IMSS, @Mes, @Ano, @SalarioDiario)";

        //                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
        //                    {
        //                        cmdInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //                        cmdInsert.Parameters.AddWithValue("@idDepartamento", departamento);
        //                        cmdInsert.Parameters.AddWithValue("@idPuesto", puesto);
        //                        cmdInsert.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
        //                        cmdInsert.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
        //                        cmdInsert.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
        //                        cmdInsert.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
        //                        cmdInsert.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
        //                        cmdInsert.Parameters.AddWithValue("@ISR", isr);
        //                        cmdInsert.Parameters.AddWithValue("@IMSS", imss);
        //                        cmdInsert.Parameters.AddWithValue("@Mes", mes);
        //                        cmdInsert.Parameters.AddWithValue("@Ano", anoSeleccionado);
        //                        cmdInsert.Parameters.AddWithValue("@SalarioDiario", salarioDiario); // Nuevo campo

        //                        cmdInsert.ExecuteNonQuery();
        //                    }
        //                }
        //            }
        //        }

        //        MessageBox.Show("Nóminas individuales generadas/actualizadas para todos los empleados activos.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("No hay empleados activos en la lista.");
        //    }
        //    //////
        //    //if (dtgv_Empleados_GenerarNomina.Rows.Count > 0)
        //    //{
        //    //    foreach (DataGridViewRow fila in dtgv_Empleados_GenerarNomina.Rows)
        //    //    {
        //    //        // Validar que la fila tenga datos y que el empleado esté activo
        //    //        if (fila.Cells["id_Empleado"].Value == null || fila.Cells["activo"].Value == null)
        //    //            continue;

        //    //        bool isActive = Convert.ToBoolean(fila.Cells["activo"].Value); // Verificar si el empleado está activo
        //    //        if (!isActive)
        //    //            continue; // Saltar este empleado si no está activo

        //    //        int idEmpleado = Convert.ToInt32(fila.Cells["id_Empleado"].Value);

        //    //        // Obtener datos del empleado
        //    //        var (departamento, puesto, salarioDiario, salarioDiarioIntegrado) = ObtenerDatosEmpleado(idEmpleado);

        //    //        // Definir mes y año
        //    //        // string mes = "Noviembre"; // Ejemplo
        //    //        // int anoSeleccionado = 2024; // Ejemplo

        //    //        // Calcular faltas, días trabajados, y sueldo bruto
        //    //        int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);
        //    //        int diasTrabajados = 30 - faltas;
        //    //        decimal sueldoBruto = diasTrabajados * (decimal)salarioDiario;

        //    //        // Calcular ISR y IMSS
        //    //        decimal isr = CalcularISR(sueldoBruto);
        //    //        decimal imss = (decimal)salarioDiarioIntegrado * 0.05m;

        //    //        // Calcular totales de deducciones y percepciones desde la base de datos
        //    //        var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto, faltas, (decimal)salarioDiario);
        //    //        decimal totalDeducciones = deducciones + isr + imss;
        //    //        decimal totalPercepciones = percepciones;

        //    //        // Calcular sueldo neto
        //    //        decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;

        //    //        using (SqlConnection cn = new SqlConnection(Conexion))
        //    //        {
        //    //            cn.Open();

        //    //            // Verificar si ya existe una nómina para este empleado, mes y año
        //    //            string querySelect = @"
        //    //    SELECT COUNT(1) 
        //    //    FROM NominaIndividual 
        //    //    WHERE idEmpleadoFK = @idEmpleado AND Mes = @Mes AND Ano = @Ano";

        //    //            SqlCommand cmdSelect = new SqlCommand(querySelect, cn);
        //    //            cmdSelect.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //    //            cmdSelect.Parameters.AddWithValue("@Mes", mes);
        //    //            cmdSelect.Parameters.AddWithValue("@Ano", anoSeleccionado);

        //    //            int count = Convert.ToInt32(cmdSelect.ExecuteScalar());

        //    //            if (count > 0)
        //    //            {
        //    //                // Actualización si ya existe
        //    //                string queryUpdate = @"
        //    //        UPDATE NominaIndividual 
        //    //        SET idDepartamento = @idDepartamento, 
        //    //            idPuesto = @idPuesto, 
        //    //            SueldoBruto = @SueldoBruto, 
        //    //            SueldoNeto = @SueldoNeto, 
        //    //            DiasTrabajados = @DiasTrabajados, 
        //    //            totalDeducciones = @totalDeducciones, 
        //    //            totalPercepciones = @totalPercepciones, 
        //    //            ISR = @ISR, 
        //    //            IMSS = @IMSS 
        //    //        WHERE idEmpleadoFK = @idEmpleado AND Mes = @Mes AND Ano = @Ano";

        //    //                using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, cn))
        //    //                {
        //    //                    cmdUpdate.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //    //                    cmdUpdate.Parameters.AddWithValue("@idDepartamento", departamento);
        //    //                    cmdUpdate.Parameters.AddWithValue("@idPuesto", puesto);
        //    //                    cmdUpdate.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
        //    //                    cmdUpdate.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
        //    //                    cmdUpdate.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
        //    //                    cmdUpdate.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
        //    //                    cmdUpdate.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
        //    //                    cmdUpdate.Parameters.AddWithValue("@ISR", isr);
        //    //                    cmdUpdate.Parameters.AddWithValue("@IMSS", imss);
        //    //                    cmdUpdate.Parameters.AddWithValue("@Mes", mes);
        //    //                    cmdUpdate.Parameters.AddWithValue("@Ano", anoSeleccionado);

        //    //                    cmdUpdate.ExecuteNonQuery();
        //    //                }
        //    //            }
        //    //            else
        //    //            {
        //    //                // Inserción si no existe
        //    //                string queryInsert = @"
        //    //        INSERT INTO NominaIndividual 
        //    //        (idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto, DiasTrabajados, totalDeducciones, totalPercepciones, ISR, IMSS, Mes, Ano) 
        //    //        VALUES (@idEmpleado, @idDepartamento, @idPuesto, @SueldoBruto, @SueldoNeto, @DiasTrabajados, @totalDeducciones, @totalPercepciones, @ISR, @IMSS, @Mes, @Ano)";

        //    //                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
        //    //                {
        //    //                    cmdInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //    //                    cmdInsert.Parameters.AddWithValue("@idDepartamento", departamento);
        //    //                    cmdInsert.Parameters.AddWithValue("@idPuesto", puesto);
        //    //                    cmdInsert.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
        //    //                    cmdInsert.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
        //    //                    cmdInsert.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
        //    //                    cmdInsert.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
        //    //                    cmdInsert.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
        //    //                    cmdInsert.Parameters.AddWithValue("@ISR", isr);
        //    //                    cmdInsert.Parameters.AddWithValue("@IMSS", imss);
        //    //                    cmdInsert.Parameters.AddWithValue("@Mes", mes);
        //    //                    cmdInsert.Parameters.AddWithValue("@Ano", anoSeleccionado);

        //    //                    cmdInsert.ExecuteNonQuery();
        //    //                }
        //    //            }
        //    //        }
        //    //    }

        //    //    MessageBox.Show("Nóminas individuales generadas/actualizadas para todos los empleados activos.");
        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("No hay empleados activos en la lista.");
        //    //}
        //    ///////
        //    //if (dtgv_Empleados_GenerarNomina.Rows.Count > 0)
        //    //{
        //    //    foreach (DataGridViewRow fila in dtgv_Empleados_GenerarNomina.Rows)
        //    //    {
        //    //        // Validar que la fila tenga datos
        //    //        if (fila.Cells["id_Empleado"].Value == null)
        //    //            continue;

        //    //        int idEmpleado = Convert.ToInt32(fila.Cells["id_Empleado"].Value);

        //    //        // Obtener datos del empleado
        //    //      //  var (departamento, puesto, salarioDiario) = ObtenerDatosEmpleado(idEmpleado);
        //    //        var (departamento,puesto, salarioDiario, salarioDiarioIntegrado) = ObtenerDatosEmpleado(idEmpleado);

        //    //        // Definir mes y año (puedes agregar controles para elegir el mes y año o asignarlos directamente aquí)
        //    //        // string mes = "Noviembre"; // Ejemplo
        //    //        //int anoSeleccionado = 2024; // Ejemplo

        //    //        // Calcular faltas, días trabajados, y sueldo bruto
        //    //        int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);
        //    //        int diasTrabajados = 30 - faltas;
        //    //        decimal sueldoBruto = diasTrabajados * (decimal)salarioDiario;

        //    //        // Calcular ISR y IMSS
        //    //        decimal isr = CalcularISR(sueldoBruto);
        //    //        //decimal imss = sueldoBruto * 0.01225m;
        //    //        decimal imss = (decimal)salarioDiarioIntegrado * 0.05m;

        //    //        // Calcular totales de deducciones y percepciones desde la base de datos
        //    //        //var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado);
        //    //       // var (deducciones, percepciones)= CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto);
        //    //        var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto, faltas,(decimal) salarioDiario);
        //    //        decimal totalDeducciones = deducciones + isr + imss;
        //    //        decimal totalPercepciones = percepciones;

        //    //        // Calcular sueldo neto
        //    //        decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;

        //    //        using (SqlConnection cn = new SqlConnection(Conexion))
        //    //        {
        //    //            cn.Open();

        //    //            // Primero verificamos si ya existe una nómina para este empleado, mes y año
        //    //            string querySelect = @"
        //    //        SELECT COUNT(1) 
        //    //        FROM NominaIndividual 
        //    //        WHERE idEmpleadoFK = @idEmpleado AND Mes = @Mes AND Ano = @Ano";

        //    //            SqlCommand cmdSelect = new SqlCommand(querySelect, cn);
        //    //            cmdSelect.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //    //            cmdSelect.Parameters.AddWithValue("@Mes", mes);
        //    //            cmdSelect.Parameters.AddWithValue("@Ano", anoSeleccionado);

        //    //            int count = Convert.ToInt32(cmdSelect.ExecuteScalar());

        //    //            if (count > 0)
        //    //            {
        //    //                // Si ya existe, hacemos una actualización
        //    //                string queryUpdate = @"
        //    //            UPDATE NominaIndividual 
        //    //            SET idDepartamento = @idDepartamento, 
        //    //                idPuesto = @idPuesto, 
        //    //                SueldoBruto = @SueldoBruto, 
        //    //                SueldoNeto = @SueldoNeto, 
        //    //                DiasTrabajados = @DiasTrabajados, 
        //    //                totalDeducciones = @totalDeducciones, 
        //    //                totalPercepciones = @totalPercepciones, 
        //    //                ISR = @ISR, 
        //    //                IMSS = @IMSS 
        //    //            WHERE idEmpleadoFK = @idEmpleado AND Mes = @Mes AND Ano = @Ano";

        //    //                using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, cn))
        //    //                {
        //    //                    cmdUpdate.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //    //                    cmdUpdate.Parameters.AddWithValue("@idDepartamento", departamento);
        //    //                    cmdUpdate.Parameters.AddWithValue("@idPuesto", puesto);
        //    //                    cmdUpdate.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
        //    //                    cmdUpdate.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
        //    //                    cmdUpdate.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
        //    //                    cmdUpdate.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
        //    //                    cmdUpdate.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
        //    //                    cmdUpdate.Parameters.AddWithValue("@ISR", isr);
        //    //                    cmdUpdate.Parameters.AddWithValue("@IMSS", imss);
        //    //                    cmdUpdate.Parameters.AddWithValue("@Mes", mes);
        //    //                    cmdUpdate.Parameters.AddWithValue("@Ano", anoSeleccionado);

        //    //                    cmdUpdate.ExecuteNonQuery();
        //    //                }
        //    //            }
        //    //            else
        //    //            {
        //    //                // Si no existe, hacemos una inserción
        //    //                string queryInsert = @"
        //    //            INSERT INTO NominaIndividual 
        //    //            (idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto, DiasTrabajados, totalDeducciones, totalPercepciones, ISR, IMSS, Mes, Ano) 
        //    //            VALUES (@idEmpleado, @idDepartamento, @idPuesto, @SueldoBruto, @SueldoNeto, @DiasTrabajados, @totalDeducciones, @totalPercepciones, @ISR, @IMSS, @Mes, @Ano)";

        //    //                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
        //    //                {
        //    //                    cmdInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //    //                    cmdInsert.Parameters.AddWithValue("@idDepartamento", departamento);
        //    //                    cmdInsert.Parameters.AddWithValue("@idPuesto", puesto);
        //    //                    cmdInsert.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
        //    //                    cmdInsert.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
        //    //                    cmdInsert.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
        //    //                    cmdInsert.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
        //    //                    cmdInsert.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
        //    //                    cmdInsert.Parameters.AddWithValue("@ISR", isr);
        //    //                    cmdInsert.Parameters.AddWithValue("@IMSS", imss);
        //    //                    cmdInsert.Parameters.AddWithValue("@Mes", mes);
        //    //                    cmdInsert.Parameters.AddWithValue("@Ano", anoSeleccionado);

        //    //                    cmdInsert.ExecuteNonQuery();
        //    //                }
        //    //            }
        //    //        }
        //    //    }

        //    //    MessageBox.Show("Nóminas individuales generadas/actualizadas para todos los empleados.");
        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("No hay empleados en la lista.");
        //    //}
        //}
        private void GenerarNominasIndividuales2(object sender, EventArgs e)
        {
            if (dtgv_Empleados_GenerarNomina.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dtgv_Empleados_GenerarNomina.Rows)
                {
                    // Validar que la fila tenga datos y que el empleado esté activo
                    if (fila.Cells["id_Empleado"].Value == null || fila.Cells["activo"].Value == null)
                        continue;

                    bool isActive = Convert.ToBoolean(fila.Cells["activo"].Value); // Verificar si el empleado está activo
                    if (!isActive)
                        continue; // Saltar este empleado si no está activo

                    int idEmpleado = Convert.ToInt32(fila.Cells["id_Empleado"].Value);

                    // Obtener datos del empleado
                    var (departamento, puesto, salarioDiario, salarioDiarioIntegrado) = ObtenerDatosEmpleado(idEmpleado);

                    // Definir mes y año
                    // string mes = "Noviembre"; // Ejemplo
                    // int anoSeleccionado = 2024; // Ejemplo

                    var (diasVacaciones, montoVacaciones, primaVacacional) = CalcularVacaciones(idEmpleado, mes, anoSeleccionado, (decimal)salarioDiario);
                    // Calcular faltas, días trabajados, y sueldo bruto
                    int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);

                    int diasTrabajados = 30 - faltas - diasVacaciones;
                    decimal sueldoBruto = diasTrabajados * (decimal)salarioDiario;

                    // Calcular el aguinaldo solo si es diciembre
                    decimal aguinaldo = 0;
                    if (mes.Equals("Diciembre", StringComparison.OrdinalIgnoreCase))
                    {
                        aguinaldo = (decimal)salarioDiario * 18;
                    }

                    // Calcular ISR y IMSS
                    decimal isr = CalcularISR(sueldoBruto);
                    decimal imss = (decimal)salarioDiarioIntegrado * 0.05m;

                    // Calcular Préstamo Infonavit
                   // decimal porcentajeInfonavit = ObtenerPorcentajeInfonavit(idEmpleado); // Método para obtener el porcentaje
                    decimal prestamoInfonavit = (decimal)salarioDiario * 0.11m;

                    // Calcular Fondo de Ahorro
                    decimal fondoAhorro = sueldoBruto < 10000 ? 500 : 1000;

                    // Calcular Horas Extras
                    int horasExtras = ContarHorasExtrasEmpleado(idEmpleado, mes, anoSeleccionado);
                    decimal importeHorasExtras = ((decimal)salarioDiario / 8) * 2 * horasExtras;

                    // Calcular totales de deducciones y percepciones desde la base de datos
                    var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto, faltas, (decimal)salarioDiario);

                    // Sumar Préstamo Infonavit y Fondo de Ahorro a deducciones
                    decimal totalDeducciones = deducciones + isr + imss + prestamoInfonavit + fondoAhorro;

                    // Sumar Vacaciones, Prima Vacacional, Aguinaldo y Horas Extras a percepciones
                   decimal totalPercepciones = percepciones + montoVacaciones + primaVacacional + aguinaldo + importeHorasExtras;

                    // Calcular sueldo neto
                    decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;

                    using (SqlConnection cn = new SqlConnection(Conexion))
                    {
                        cn.Open();

                        // Verificar si ya existe una nómina para este empleado, mes y año
                        string querySelect = @"
SELECT COUNT(1) 
FROM NominaIndividual 
WHERE idEmpleadoFK = @idEmpleado AND Mes = @Mes AND Ano = @Ano";

                        SqlCommand cmdSelect = new SqlCommand(querySelect, cn);
                        cmdSelect.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        cmdSelect.Parameters.AddWithValue("@Mes", mes);
                        cmdSelect.Parameters.AddWithValue("@Ano", anoSeleccionado);

                        int count = Convert.ToInt32(cmdSelect.ExecuteScalar());

                        if (count > 0)
                        {
                            // Actualización si ya existe
                            string queryUpdate = @"
    UPDATE NominaIndividual 
    SET idDepartamento = @idDepartamento, 
        idPuesto = @idPuesto, 
        SueldoBruto = @SueldoBruto, 
        SueldoNeto = @SueldoNeto, 
        DiasTrabajados = @DiasTrabajados, 
        totalDeducciones = @totalDeducciones, 
        totalPercepciones = @totalPercepciones, 
        ISR = @ISR, 
        IMSS = @IMSS,
        SalarioDiario = @SalarioDiario
    WHERE idEmpleadoFK = @idEmpleado AND Mes = @Mes AND Ano = @Ano";

                            using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, cn))
                            {
                                cmdUpdate.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                                cmdUpdate.Parameters.AddWithValue("@idDepartamento", departamento);
                                cmdUpdate.Parameters.AddWithValue("@idPuesto", puesto);
                                cmdUpdate.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
                                cmdUpdate.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
                                cmdUpdate.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
                                cmdUpdate.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
                                cmdUpdate.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
                                cmdUpdate.Parameters.AddWithValue("@ISR", isr);
                                cmdUpdate.Parameters.AddWithValue("@IMSS", imss);
                                cmdUpdate.Parameters.AddWithValue("@SalarioDiario", salarioDiario); // Nuevo campo
                                cmdUpdate.Parameters.AddWithValue("@Mes", mes);
                                cmdUpdate.Parameters.AddWithValue("@Ano", anoSeleccionado);

                                cmdUpdate.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Inserción si no existe
                            string queryInsert = @"
    INSERT INTO NominaIndividual 
    (idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto, DiasTrabajados, totalDeducciones, totalPercepciones, ISR, IMSS, Mes, Ano, SalarioDiario) 
    VALUES (@idEmpleado, @idDepartamento, @idPuesto, @SueldoBruto, @SueldoNeto, @DiasTrabajados, @totalDeducciones, @totalPercepciones, @ISR, @IMSS, @Mes, @Ano, @SalarioDiario)";

                            using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
                            {
                                cmdInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                                cmdInsert.Parameters.AddWithValue("@idDepartamento", departamento);
                                cmdInsert.Parameters.AddWithValue("@idPuesto", puesto);
                                cmdInsert.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
                                cmdInsert.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
                                cmdInsert.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
                                cmdInsert.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
                                cmdInsert.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
                                cmdInsert.Parameters.AddWithValue("@ISR", isr);
                                cmdInsert.Parameters.AddWithValue("@IMSS", imss);
                                cmdInsert.Parameters.AddWithValue("@Mes", mes);
                                cmdInsert.Parameters.AddWithValue("@Ano", anoSeleccionado);
                                cmdInsert.Parameters.AddWithValue("@SalarioDiario", salarioDiario); // Nuevo campo

                                cmdInsert.ExecuteNonQuery();
                            }
                        }
                    }
                }

                MessageBox.Show("Nóminas individuales generadas/actualizadas para todos los empleados activos.");
            }
            else
            {
                MessageBox.Show("No hay empleados activos en la lista.");
            }
        }
        //private decimal ObtenerPorcentajeInfonavit(int idEmpleado)
        //{
        //    decimal porcentajeInfonavit = 0;

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();
        //        string query = "SELECT PorcentajeInfonavit FROM Empleado WHERE id_Empleado = @idEmpleado";

        //        using (SqlCommand cmd = new SqlCommand(query, cn))
        //        {
        //            cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

        //            object result = cmd.ExecuteScalar();
        //            if (result != null && result != DBNull.Value)
        //            {
        //                porcentajeInfonavit = Convert.ToDecimal(result);
        //            }
        //        }
        //    }

        //    return porcentajeInfonavit;
        //}

        private void GenerarNomina(int idEmpleado, int mes, int ano)
        {
            // Obtener el departamento y puesto del empleado
            int idDepartamento = 0;
            int idPuesto = 0;
            decimal salarioDiario = 0;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                // Obtener departamento, puesto y salario diario
                string queryEmpleado = @"SELECT e.id_Departamento, e.id_Puesto, p.SalarioDiario
                                 FROM Empleado e
                                 JOIN Puestos p ON e.id_Puesto = p.id_Puesto
                                 WHERE e.id_Empleado = @idEmpleado";

                SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
                cmdEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                SqlDataReader reader = cmdEmpleado.ExecuteReader();

                if (reader.Read())
                {
                    idDepartamento = reader.GetInt32(0);
                    idPuesto = reader.GetInt32(1);
                    //salarioDiario = reader.GetDecimal(2);
                    //salarioDiario = Convert.ToDecimal(reader.GetFloat(2));
                    salarioDiario = Convert.ToDecimal(reader.GetDouble(2));

                }
                reader.Close();

                // Obtener total de deducciones y percepciones
                decimal totalDeducciones = 0;
                decimal totalPercepciones = 0;

                string queryDeduccionesPercepciones = @"SELECT DP.D_P, DP.MontoPD
                                                FROM DEDPERNOMINA DPN
                                                JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
                                                WHERE DPN.id_Empleado = @idEmpleado AND DPN.Mes = @mes AND DPN.Ano = @ano";

                SqlCommand cmdDeduccionesPercepciones = new SqlCommand(queryDeduccionesPercepciones, cn);
                cmdDeduccionesPercepciones.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                cmdDeduccionesPercepciones.Parameters.AddWithValue("@mes", mes);
                cmdDeduccionesPercepciones.Parameters.AddWithValue("@ano", ano);
                SqlDataReader readerDP = cmdDeduccionesPercepciones.ExecuteReader();

                int faltas = 0;

                while (readerDP.Read())
                {
                    string tipoDP = readerDP.GetString(0);
                    decimal montoDP = readerDP.GetDecimal(1);

                    if (tipoDP == "Deducción")
                    {
                        totalDeducciones += montoDP;
                        // Si es deducción por falta, aumentar el contador de faltas
                        if (readerDP.GetString(0) == "Falta")
                            faltas++;
                    }
                    else if (tipoDP == "Percepción")
                    {
                        totalPercepciones += montoDP;
                    }
                }
                readerDP.Close();

                // Calcular Días Trabajados
                int diasTrabajados = 30 - faltas;

                // Calcular Sueldo Bruto y Sueldo Neto
                decimal sueldoBruto = diasTrabajados * salarioDiario;
                decimal sueldoNeto = sueldoBruto - totalDeducciones + totalPercepciones;

                // Insertar registro en NominaIndividual
                string queryInsertNomina = @"INSERT INTO NominaIndividual (idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto, DiasTrabajados, totalDeducciones, totalPercepciones, Mes, Ano)
                                     VALUES (@idEmpleado, @idDepartamento, @idPuesto, @sueldoBruto, @sueldoNeto, @diasTrabajados, @totalDeducciones, @totalPercepciones, @mes, @ano)";

                SqlCommand cmdInsertNomina = new SqlCommand(queryInsertNomina, cn);
                cmdInsertNomina.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                cmdInsertNomina.Parameters.AddWithValue("@idDepartamento", idDepartamento);
                cmdInsertNomina.Parameters.AddWithValue("@idPuesto", idPuesto);
                cmdInsertNomina.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);
                cmdInsertNomina.Parameters.AddWithValue("@sueldoNeto", sueldoNeto);
                cmdInsertNomina.Parameters.AddWithValue("@diasTrabajados", diasTrabajados);
                cmdInsertNomina.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
                cmdInsertNomina.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
                cmdInsertNomina.Parameters.AddWithValue("@mes", mes);
                cmdInsertNomina.Parameters.AddWithValue("@ano", ano);

                cmdInsertNomina.ExecuteNonQuery();
            }
        }

        private void ObtenerInformacionEmpleado(int idEmpleado)
        {
            int idDepartamento = 0;
            int idPuesto = 0;
            decimal salarioDiario = 0;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                string queryEmpleado = @"SELECT e.id_Departamento, e.id_Puesto, p.SalarioDiario
                                 FROM Empleado e
                                 JOIN Puestos p ON e.id_Puesto = p.id_Puesto
                                 WHERE e.id_Empleado = @idEmpleado";

                SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
                cmdEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                SqlDataReader reader = cmdEmpleado.ExecuteReader();

                if (reader.Read())
                {
                    idDepartamento = reader.GetInt32(0);
                    idPuesto = reader.GetInt32(1);
                    salarioDiario = reader.GetDecimal(2); // Si aquí tienes problemas de conversión, házmelo saber
                }
                reader.Close();
            }

            // Mostrar los resultados para verificar
            MessageBox.Show($"Empleado {idEmpleado} - Departamento: {idDepartamento}, Puesto: {idPuesto}, Salario Diario: {salarioDiario}");
        }


        private void ObtenerSalarioDiario(int idPuesto)
        {
            // Declaramos la variable donde se almacenará el salario diario
            float salarioDiario = 0;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                // Consulta SQL para obtener el salario diario del puesto específico
                string query = "SELECT SalarioDiario FROM Puestos WHERE id_Puesto = @idPuesto";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Asignamos el valor del parámetro @idPuesto
                    cmd.Parameters.AddWithValue("@idPuesto", idPuesto);

                    // Ejecutamos el lector de datos
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Verificamos si hay resultados
                    if (reader.Read())
                    {
                        // Guardamos el salario diario en la variable
                        salarioDiario = Convert.ToSingle(reader["SalarioDiario"]); // Convertimos a float
                    }
                    reader.Close();
                }
            }

            // Ahora puedes usar `salarioDiario` en tus cálculos
            Console.WriteLine($"El salario diario del puesto con id {idPuesto} es: {salarioDiario}");
        }

        //private void ObtenerDatosEmpleado(int idEmpleadoSeleccionado)
        //{
        //    int idEmpleado = 0;
        //    int idDepartamento = 0;
        //    int idPuesto = 0;
        //    float salarioDiario = 0;

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        string query = @"
        //    SELECT e.id_Empleado, e.id_Departamento, e.id_Puesto, p.SalarioDiario
        //    FROM Empleado e
        //    JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //    WHERE e.id_Empleado = @idEmpleado";

        //        SqlCommand cmd = new SqlCommand(query, cn);
        //        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            idEmpleado = reader.GetInt32(0);
        //            idDepartamento = reader.GetInt32(1);
        //            idPuesto = reader.GetInt32(2);
        //           // salarioDiario = reader.GetFloat(3); // Cambiado a GetFloat para el tipo float
        //            salarioDiario = Convert.ToSingle(reader.GetValue(3));

        //        }
        //        reader.Close();
        //    }

        //    // Ahora tienes los valores guardados en variables
        //    MessageBox.Show($"Empleado: {idEmpleado}, Departamento: {idDepartamento}, Puesto: {idPuesto}, Salario Diario: {salarioDiario}");

        //}
        //----------
        //private (int idDepartamento, int idPuesto, float salarioDiario) ObtenerDatosEmpleado(int idEmpleadoSeleccionado)
        //{
        //    int idDepartamento = 0;
        //    int idPuesto = 0;
        //    float salarioDiario = 0;

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        string query = @"
        //SELECT e.id_Empleado, e.id_Departamento, e.id_Puesto, p.SalarioDiario
        //FROM Empleado e
        //JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //WHERE e.id_Empleado = @idEmpleado";

        //        SqlCommand cmd = new SqlCommand(query, cn);
        //        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);

        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            idDepartamento = reader.GetInt32(1);
        //            idPuesto = reader.GetInt32(2);
        //            salarioDiario = Convert.ToSingle(reader.GetValue(3));
        //        }
        //        reader.Close();
        //    }

        //    return (idDepartamento, idPuesto, salarioDiario);
        //}
        private (int idDepartamento, int idPuesto, float salarioDiario, float salarioDiarioIntegrado) ObtenerDatosEmpleado(int idEmpleadoSeleccionado)
        {
            int idDepartamento = 0;
            int idPuesto = 0;
            float salarioDiario = 0;
            float salarioDiarioIntegrado = 0;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                string query = @"
SELECT e.id_Empleado, e.id_Departamento, e.id_Puesto, e.SalarioDiario, e.SalarioDiarioIntegrado
FROM Empleado e
WHERE e.id_Empleado = @idEmpleado";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    idDepartamento = reader.GetInt32(1);
                    idPuesto = reader.GetInt32(2);
                    salarioDiario = Convert.ToSingle(reader.GetValue(3));
                    salarioDiarioIntegrado = Convert.ToSingle(reader.GetValue(4));
                }
                reader.Close();
            }

            return (idDepartamento, idPuesto, salarioDiario, salarioDiarioIntegrado);
        }



        private void btn_GenerarNominaInd_GenerarNomina_Click(object sender, EventArgs e)
        {
            //int idEmpleadoSeleccionado = 100; // Ejemplo de ID de empleado
            //string mesSeleccionado = "Noviembre"; // Ejemplo de mes
            //int anoSeleccionado = 2024; // Ejemplo de año

            //int totalFaltas = ContarFaltasEmpleado(idEmpleadoSeleccionado, mesSeleccionado, anoSeleccionado);
            //MessageBox.Show($"El empleado {idEmpleadoSeleccionado} tiene {totalFaltas} faltas en {mesSeleccionado} de {anoSeleccionado}.");
            //GenerarNominasIndividuales( sender,  e);


            GenerarNominasIndividuales2(sender, e);
            //GenerarNominaGeneral(sender, e);
        }

        private decimal CalcularISR(decimal sueldoBruto)
        {
            decimal isr = 0;

            if (sueldoBruto >= 0.01m && sueldoBruto <= 8952.49m)
            {
                decimal excedente = sueldoBruto - 0.01m;
                isr = 0 + (excedente * 0.0192m);
            }
            else if (sueldoBruto >= 8952.50m && sueldoBruto <= 75984.55m)
            {
                decimal excedente = sueldoBruto - 8952.50m;
                isr = 171.88m + (excedente * 0.0640m);
            }
            else if (sueldoBruto >= 75984.56m && sueldoBruto <= 133536.07m)
            {
                decimal excedente = sueldoBruto - 75984.56m;
                isr = 4461.94m + (excedente * 0.1088m);
            }
            else if (sueldoBruto >= 133536.08m && sueldoBruto <= 155229.80m)
            {
                decimal excedente = sueldoBruto - 133536.08m;
                isr = 10723.55m + (excedente * 0.1600m);
            }
            else if (sueldoBruto >= 155229.81m && sueldoBruto <= 185852.57m)
            {
                decimal excedente = sueldoBruto - 155229.81m;
                isr = 14194.54m + (excedente * 0.1792m);
            }
            else if (sueldoBruto >= 185852.58m && sueldoBruto <= 374837.88m)
            {
                decimal excedente = sueldoBruto - 185852.58m;
                isr = 19682.13m + (excedente * 0.2136m);
            }
            else if (sueldoBruto >= 374837.89m && sueldoBruto <= 590795.99m)
            {
                decimal excedente = sueldoBruto - 374837.89m;
                isr = 60049.40m + (excedente * 0.2352m);
            }
            else if (sueldoBruto >= 590796.00m && sueldoBruto <= 1127926.84m)
            {
                decimal excedente = sueldoBruto - 590796.00m;
                isr = 110842.74m + (excedente * 0.3000m);
            }
            else if (sueldoBruto >= 1127926.85m && sueldoBruto <= 1503902.46m)
            {
                decimal excedente = sueldoBruto - 1127926.85m;
                isr = 271981.99m + (excedente * 0.3200m);
            }
            else if (sueldoBruto >= 1503902.47m && sueldoBruto <= 4511707.37m)
            {
                decimal excedente = sueldoBruto - 1503902.47m;
                isr = 392294.17m + (excedente * 0.3400m);
            }
            else if (sueldoBruto >= 4511707.38m)
            {
                decimal excedente = sueldoBruto - 4511707.38m;
                isr = 1414947.85m + (excedente * 0.3500m);
            }

            return isr;
        }

        private int ContarFaltasEmpleado(int idEmpleado, string mes, int ano)
        {
            int totalFaltas = 0;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string query = @"
            SELECT COUNT(*)
            FROM DEDPERNOMINA DPN
            JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
            WHERE DPN.id_Empleado = @idEmpleado 
              AND DP.Nombre_PD = 'Falta'
              AND DPN.Mes = @mes
              AND DPN.Ano = @ano";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                cmd.Parameters.AddWithValue("@mes", mes); // Nombre del mes como texto
                cmd.Parameters.AddWithValue("@ano", ano);  // Año como número

                totalFaltas = (int)cmd.ExecuteScalar();
            }

            return totalFaltas;
        }
        private int ContarHorasExtrasEmpleado(int idEmpleado, string mes, int ano)
        {
            int totalHorasExtras = 0;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string query = @"
        SELECT COUNT(*)
        FROM DEDPERNOMINA DPN
        JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
        WHERE DPN.id_Empleado = @idEmpleado 
          AND DP.Nombre_PD = 'Hora Extra'
          AND DPN.Mes = @mes
          AND DPN.Ano = @ano";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                cmd.Parameters.AddWithValue("@mes", mes); // Nombre del mes como texto
                cmd.Parameters.AddWithValue("@ano", ano);  // Año como número

                totalHorasExtras = (int)cmd.ExecuteScalar();
            }

            return totalHorasExtras;
        }

        private (int diasVacaciones, decimal montoVacaciones, decimal primaVacacional) CalcularVacaciones(int idEmpleado, string mes, int ano, decimal salarioDiario)
        {
            int diasVacaciones = 0;
            decimal montoVacaciones = 0;
            decimal primaVacacional = 0;

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string query = @"
        SELECT TOP 1 DP.Nombre_PD
        FROM DEDPERNOMINA DPN
        JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
        WHERE DPN.id_Empleado = @idEmpleado 
          AND DP.Nombre_PD = 'Vacaciones'
          AND DPN.Mes = @mes
          AND DPN.Ano = @ano";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                cmd.Parameters.AddWithValue("@mes", mes);
                cmd.Parameters.AddWithValue("@ano", ano);

                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    // Asignar los días de vacaciones según antigüedad u otros criterios
                    diasVacaciones = ObtenerDiasVacacionesPorAntiguedad(idEmpleado);

                    // Calcular el monto de vacaciones y la prima vacacional
                    montoVacaciones = diasVacaciones * salarioDiario;
                    primaVacacional = (diasVacaciones * 0.35m) * salarioDiario;
                }
            }

            return (diasVacaciones, montoVacaciones, primaVacacional);
        }

        private int ObtenerDiasVacacionesPorAntiguedad(int idEmpleado)
        {
            DateTime fechaIngreso = ObtenerFechaIngresoEmpleado(idEmpleado); // Implementa esta función para obtener la fecha de ingreso del empleado
            DateTime fechaActual = DateTime.Now;
            int antiguedad = fechaActual.Year - fechaIngreso.Year;
            if (fechaActual < fechaIngreso.AddYears(antiguedad)) antiguedad--;

            // Determinar los días de vacaciones según la antigüedad
            if (antiguedad < 1) return 0;
            else if (antiguedad == 1) return 12;
            else if (antiguedad == 2) return 14;
            else if (antiguedad == 3) return 16;
            else if (antiguedad == 4) return 18;
            else if (antiguedad == 5) return 20;
            else if (antiguedad >= 6 && antiguedad <= 10) return 22;
            else if (antiguedad >= 11 && antiguedad <= 15) return 24;
            return 0;
        }

        //private void GenerarNominaGeneral(object sender, EventArgs e) {

        //    int idNominaGeneral;

        //    // Verifica si ya existe una entrada en NominaGeneral para el departamento, mes y año
        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        // Comprobar existencia
        //        string queryCheck = @"
        //SELECT id_NominaGeneral 
        //FROM NominaGeneral 
        //WHERE id_Departamento = @idDepartamento AND Mes = @mes AND Ano = @ano";

        //        using (SqlCommand cmdCheck = new SqlCommand(queryCheck, cn))
        //        {
        //            cmdCheck.Parameters.AddWithValue("@idDepartamento",1);
        //            cmdCheck.Parameters.AddWithValue("@mes", mes);
        //            cmdCheck.Parameters.AddWithValue("@ano", anoSeleccionado);

        //            var result = cmdCheck.ExecuteScalar();

        //            // Si no existe, crear la entrada
        //            if (result == null)
        //            {
        //                string queryInsert = @"
        //        INSERT INTO NominaGeneral (id_Departamento, Mes, Ano) 
        //        VALUES (@idDepartamento, @mes, @ano);
        //        SELECT SCOPE_IDENTITY();";

        //                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
        //                {
        //                    cmdInsert.Parameters.AddWithValue("@idDepartamento", 1);
        //                    cmdInsert.Parameters.AddWithValue("@mes", mes);
        //                    cmdInsert.Parameters.AddWithValue("@ano", anoSeleccionado);

        //                    idNominaGeneral = Convert.ToInt32(cmdInsert.ExecuteScalar());
        //                }
        //            }
        //            else
        //            {
        //                idNominaGeneral = Convert.ToInt32(result);
        //            }
        //        }
        //    }

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        // Obtener los empleados del departamento
        //        string queryEmpleados = @"
        //SELECT e.id_Empleado, p.SalarioDiario
        //FROM Empleado e
        //JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //WHERE e.id_Departamento = @idDepartamento";

        //        using (SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn))
        //        {
        //            cmdEmpleados.Parameters.AddWithValue("@idDepartamento", 1);

        //            using (SqlDataReader reader = cmdEmpleados.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    int idEmpleado = reader.GetInt32(0);
        //                    decimal salarioDiario = reader.GetDecimal(1);

        //                    // Aquí calculamos DiasTrabajados, SueldoBruto, y SueldoNeto
        //                   // int diasTrabajados = CalcularDiasTrabajados(idEmpleado, mes, ano); // Suponiendo que esta función existe
        //                    //decimal sueldoBruto = diasTrabajados * salarioDiario;
        //                    //decimal sueldoNeto = CalcularSueldoNeto(sueldoBruto, idEmpleado, mes, ano); // Suponiendo que esta función existe



        //                    int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);
        //                    int diasTrabajados = 30 - faltas;
        //                    decimal sueldoBruto = diasTrabajados * (decimal)salarioDiario;

        //                    // Calcular ISR y IMSS
        //                    decimal isr = CalcularISR(sueldoBruto);
        //                    decimal imss = sueldoBruto * 0.01225m;

        //                    // Calcular totales de deducciones y percepciones desde la base de datos
        //                    //var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado);
        //                    var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto);
        //                    decimal totalDeducciones = deducciones + isr + imss;
        //                    decimal totalPercepciones = percepciones;

        //                    // Calcular sueldo neto
        //                    decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;


        //                    // Insertar en NominaGeneral_Empleado
        //                    string queryInsertEmpleado = @"
        //            INSERT INTO NominaGeneral_Empleado (id_NominaGeneral, id_Empleado, DiasTrabajados, SueldoBruto, SueldoNeto)
        //            VALUES (@idNominaGeneral, @idEmpleado, @diasTrabajados, @sueldoBruto, @sueldoNeto)";

        //                    using (SqlCommand cmdInsertEmpleado = new SqlCommand(queryInsertEmpleado, cn))
        //                    {
        //                        cmdInsertEmpleado.Parameters.AddWithValue("@idNominaGeneral", idNominaGeneral);
        //                        cmdInsertEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //                        cmdInsertEmpleado.Parameters.AddWithValue("@diasTrabajados", diasTrabajados);
        //                        cmdInsertEmpleado.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);
        //                        cmdInsertEmpleado.Parameters.AddWithValue("@sueldoNeto", sueldoNeto);

        //                        cmdInsertEmpleado.ExecuteNonQuery();
        //                    }
        //                }
        //            }
        //        }
        //    }



        //}

        //    private void GenerarNominaGeneral(object sender, EventArgs e)
        //    {
        //        int idNominaGeneral;

        //        // Verifica si ya existe una entrada en NominaGeneral para el departamento, mes y año
        //        using (SqlConnection cn = new SqlConnection(Conexion))
        //        {
        //            cn.Open();

        //            // Comprobar existencia de la tabla `NominaGeneral`
        //            string tableCheckQuery = "IF OBJECT_ID('NominaGeneral', 'U') IS NOT NULL SELECT 1 ELSE SELECT 0;";
        //            using (SqlCommand tableCheckCmd = new SqlCommand(tableCheckQuery, cn))
        //            {
        //                int tableExists = (int)tableCheckCmd.ExecuteScalar();
        //                if (tableExists == 0)
        //                {
        //                    MessageBox.Show("La tabla NominaGeneral no existe en la base de datos.");
        //                    return;
        //                }
        //            }

        //            // Comprobar si ya existe una nómina para el departamento, mes y año
        //            string queryCheck = @"
        //        SELECT id_NominaGeneral 
        //        FROM NominaGeneral 
        //        WHERE id_Departamento = @idDepartamento AND Mes = @mes AND Ano = @ano";

        //            using (SqlCommand cmdCheck = new SqlCommand(queryCheck, cn))
        //            {
        //                cmdCheck.Parameters.AddWithValue("@idDepartamento", 1);
        //                cmdCheck.Parameters.AddWithValue("@mes", mes);
        //                cmdCheck.Parameters.AddWithValue("@ano", anoSeleccionado);

        //                var result = cmdCheck.ExecuteScalar();

        //                // Si no existe, crear la entrada
        //                if (result == null)
        //                {
        //                    string queryInsert = @"
        //                INSERT INTO NominaGeneral (id_Departamento, Mes, Ano) 
        //                VALUES (@idDepartamento, @mes, @ano);
        //                SELECT SCOPE_IDENTITY();";

        //                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
        //                    {
        //                        cmdInsert.Parameters.AddWithValue("@idDepartamento", 1);
        //                        cmdInsert.Parameters.AddWithValue("@mes", mes);
        //                        cmdInsert.Parameters.AddWithValue("@ano", anoSeleccionado);

        //                        idNominaGeneral = Convert.ToInt32(cmdInsert.ExecuteScalar());
        //                    }
        //                }
        //                else
        //                {
        //                    idNominaGeneral = Convert.ToInt32(result);
        //                }
        //            }

        //            // Obtener los empleados del departamento y calcular los datos de nómina
        //            string queryEmpleados = @"
        //        SELECT e.id_Empleado, p.SalarioDiario
        //        FROM Empleado e
        //        JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //        WHERE e.id_Departamento = @idDepartamento";

        //            using (SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn))
        //            {
        //                cmdEmpleados.Parameters.AddWithValue("@idDepartamento", 1);

        //                using (SqlDataReader reader = cmdEmpleados.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {

        //                        if (dtgv_Empleados_GenerarNomina.Rows.Count > 0)
        //                        {
        //                            foreach (DataGridViewRow fila in dtgv_Empleados_GenerarNomina.Rows)
        //                            {
        //                                //int idEmpleado = reader.GetInt32(0);
        //                                //decimal salarioDiario = reader.GetDecimal(1);
        //                                // Validar que la fila tenga datos
        //                                if (fila.Cells["id_Empleado"].Value == null)
        //                            continue;

        //                        int idEmpleado = Convert.ToInt32(fila.Cells["id_Empleado"].Value);

        //                        // Obtener datos del empleado
        //                        var (departamento, puesto, salarioDiario) = ObtenerDatosEmpleado(idEmpleado);

        //                        // Calcular los detalles de la nómina para cada empleado
        //                        int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);
        //                        int diasTrabajados = 30 - faltas;
        //                        decimal sueldoBruto = diasTrabajados * salarioDiario;

        //                        // Calcular ISR, IMSS y totales
        //                        decimal isr = CalcularISR(sueldoBruto);
        //                        decimal imss = sueldoBruto * 0.01225m;

        //                        var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto);
        //                        decimal totalDeducciones = deducciones + isr + imss;
        //                        decimal totalPercepciones = percepciones;
        //                        decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;

        //                        // Insertar o actualizar en NominaGeneral_Empleado
        //                        string queryInsertEmpleado = @"
        //                    IF NOT EXISTS (SELECT 1 FROM NominaGeneral_Empleado WHERE id_NominaGeneral = @idNominaGeneral AND id_Empleado = @idEmpleado)
        //                    BEGIN
        //                        INSERT INTO NominaGeneral_Empleado (id_NominaGeneral, id_Empleado, DiasTrabajados, SueldoBruto, SueldoNeto)
        //                        VALUES (@idNominaGeneral, @idEmpleado, @diasTrabajados, @sueldoBruto, @sueldoNeto);
        //                    END
        //                    ELSE
        //                    BEGIN
        //                        UPDATE NominaGeneral_Empleado
        //                        SET DiasTrabajados = @diasTrabajados, SueldoBruto = @sueldoBruto, SueldoNeto = @sueldoNeto
        //                        WHERE id_NominaGeneral = @idNominaGeneral AND id_Empleado = @idEmpleado;
        //                    END";

        //                        using (SqlCommand cmdInsertEmpleado = new SqlCommand(queryInsertEmpleado, cn))
        //                        {
        //                            cmdInsertEmpleado.Parameters.AddWithValue("@idNominaGeneral", idNominaGeneral);
        //                            cmdInsertEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //                            cmdInsertEmpleado.Parameters.AddWithValue("@diasTrabajados", diasTrabajados);
        //                            cmdInsertEmpleado.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);
        //                            cmdInsertEmpleado.Parameters.AddWithValue("@sueldoNeto", sueldoNeto);

        //                            cmdInsertEmpleado.ExecuteNonQuery();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        MessageBox.Show("Nómina general generada o actualizada exitosamente.");
        //    }


        //private void GenerarNominaGeneral(object sender, EventArgs e)
        //{
        //    int idNominaGeneral;

        //    // Verifica si ya existe una entrada en NominaGeneral para el departamento, mes y año
        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        // Comprobar si ya existe una nómina para el departamento, mes y año
        //        string queryCheck = @"
        //SELECT id_NominaGeneral 
        //FROM NominaGeneral 
        //WHERE id_Departamento = @idDepartamento AND Mes = @mes AND Ano = @ano";

        //        using (SqlCommand cmdCheck = new SqlCommand(queryCheck, cn))
        //        {
        //            cmdCheck.Parameters.AddWithValue("@idDepartamento", 1);
        //            cmdCheck.Parameters.AddWithValue("@mes", mes);
        //            cmdCheck.Parameters.AddWithValue("@ano", anoSeleccionado);

        //            var result = cmdCheck.ExecuteScalar();

        //            // Si no existe, crear la entrada
        //            if (result == null)
        //            {
        //                string queryInsert = @"
        //        INSERT INTO NominaGeneral (id_Departamento, Mes, Ano) 
        //        VALUES (@idDepartamento, @mes, @ano);
        //        SELECT SCOPE_IDENTITY();";

        //                using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
        //                {
        //                    cmdInsert.Parameters.AddWithValue("@idDepartamento", 1);
        //                    cmdInsert.Parameters.AddWithValue("@mes", mes);
        //                    cmdInsert.Parameters.AddWithValue("@ano", anoSeleccionado);

        //                    idNominaGeneral = Convert.ToInt32(cmdInsert.ExecuteScalar());
        //                }
        //            }
        //            else
        //            {
        //                idNominaGeneral = Convert.ToInt32(result);
        //            }
        //        }

        //        // Obtener los empleados del departamento y calcular los datos de nómina
        //        string queryEmpleados = @"
        //SELECT e.id_Empleado, p.SalarioDiario
        //FROM Empleado e
        //JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //WHERE e.id_Departamento = @idDepartamento";

        //        using (SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn))
        //        {
        //            cmdEmpleados.Parameters.AddWithValue("@idDepartamento", 1);

        //            using (SqlDataReader reader = cmdEmpleados.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    int idEmpleado = reader.GetInt32(0);
        //                    decimal salarioDiario = Convert.ToDecimal(reader.GetValue(1)); // Conversión explícita

        //                    // Calcular los detalles de la nómina para cada empleado
        //                    int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);
        //                    int diasTrabajados = 30 - faltas;
        //                    decimal sueldoBruto = diasTrabajados * salarioDiario;

        //                    // Calcular ISR, IMSS y totales
        //                    decimal isr = CalcularISR(sueldoBruto);
        //                    decimal imss = sueldoBruto * 0.01225m;

        //                    var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto);
        //                    decimal totalDeducciones = deducciones + isr + imss;
        //                    decimal totalPercepciones = percepciones;
        //                    decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;

        //                    // Insertar o actualizar en NominaGeneral_Empleado
        //                    string queryInsertEmpleado = @"
        //            IF NOT EXISTS (SELECT 1 FROM NominaGeneral_Empleado WHERE id_NominaGeneral = @idNominaGeneral AND id_Empleado = @idEmpleado)
        //            BEGIN
        //                INSERT INTO NominaGeneral_Empleado (id_NominaGeneral, id_Empleado, DiasTrabajados, SueldoBruto, SueldoNeto)
        //                VALUES (@idNominaGeneral, @idEmpleado, @diasTrabajados, @sueldoBruto, @sueldoNeto);
        //            END
        //            ELSE
        //            BEGIN
        //                UPDATE NominaGeneral_Empleado
        //                SET DiasTrabajados = @diasTrabajados, SueldoBruto = @sueldoBruto, SueldoNeto = @sueldoNeto
        //                WHERE id_NominaGeneral = @idNominaGeneral AND id_Empleado = @idEmpleado;
        //            END";

        //                    using (SqlCommand cmdInsertEmpleado = new SqlCommand(queryInsertEmpleado, cn))
        //                    {
        //                        cmdInsertEmpleado.Parameters.AddWithValue("@idNominaGeneral", idNominaGeneral);
        //                        cmdInsertEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleado);
        //                        cmdInsertEmpleado.Parameters.AddWithValue("@diasTrabajados", diasTrabajados);
        //                        cmdInsertEmpleado.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);
        //                        cmdInsertEmpleado.Parameters.AddWithValue("@sueldoNeto", sueldoNeto);

        //                        cmdInsertEmpleado.ExecuteNonQuery();
        //                    }
        //                }
        //            }
        //        }
        //        MessageBox.Show("Nómina general generada o actualizada exitosamente.");
        //    }
        // }
        private void GenerarNominaGeneral(object sender, EventArgs e)
        {
            int idNominaGeneral;

            // Verifica si ya existe una entrada en NominaGeneral para el departamento, mes y año
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                // Comprobar si ya existe una nómina para el departamento, mes y año
                string queryCheck = @"
        SELECT id_NominaGeneral 
        FROM NominaGeneral 
        WHERE id_Departamento = @idDepartamento AND Mes = @mes AND Ano = @ano";

                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, cn))
                {
                    cmdCheck.Parameters.AddWithValue("@idDepartamento", 1);
                    cmdCheck.Parameters.AddWithValue("@mes", mes);
                    cmdCheck.Parameters.AddWithValue("@ano", anoSeleccionado);

                    var result = cmdCheck.ExecuteScalar();

                    // Si no existe, crear la entrada
                    if (result == null)
                    {
                        string queryInsert = @"
                INSERT INTO NominaGeneral (id_Departamento, Mes, Ano) 
                VALUES (@idDepartamento, @mes, @ano);
                SELECT SCOPE_IDENTITY();";

                        using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
                        {
                            cmdInsert.Parameters.AddWithValue("@idDepartamento", 1);
                            cmdInsert.Parameters.AddWithValue("@mes", mes);
                            cmdInsert.Parameters.AddWithValue("@ano", anoSeleccionado);

                            idNominaGeneral = Convert.ToInt32(cmdInsert.ExecuteScalar());
                        }
                    }
                    else
                    {
                        idNominaGeneral = Convert.ToInt32(result);
                    }
                }

                // Obtener los empleados del departamento y calcular los datos de nómina
                string queryEmpleados = @"
        SELECT e.id_Empleado, p.SalarioDiario
        FROM Empleado e
        JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        WHERE e.id_Departamento = @idDepartamento";

                using (SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn))
                {
                    cmdEmpleados.Parameters.AddWithValue("@idDepartamento", 1);

                    using (SqlDataReader reader = cmdEmpleados.ExecuteReader())
                    {
                        List<(int idEmpleado, decimal salarioDiario)> empleados = new List<(int, decimal)>();

                        // Almacenar los datos en una lista para liberar el DataReader antes de procesar
                        while (reader.Read())
                        {
                            int idEmpleado = reader.GetInt32(0);
                            decimal salarioDiario = Convert.ToDecimal(reader.GetValue(1));
                            empleados.Add((idEmpleado, salarioDiario));
                        }

                        reader.Close(); // Cerrar el DataReader aquí antes de cualquier otro comando

                        // Procesar cada empleado
                        foreach (var (idEmpleado, salarioDiario) in empleados)
                        {
                            // Calcular los detalles de la nómina para cada empleado
                            int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);
                            int diasTrabajados = 30 - faltas;
                            decimal sueldoBruto = diasTrabajados * salarioDiario;

                            // Calcular ISR, IMSS y totales
                            decimal isr = CalcularISR(sueldoBruto);
                            decimal imss = sueldoBruto * 0.01225m;

                           // var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto);
                            var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto, faltas, (decimal)salarioDiario);
                            decimal totalDeducciones = deducciones + isr + imss;
                            decimal totalPercepciones = percepciones;
                            decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;

                            // Insertar o actualizar en NominaGeneral_Empleado
                            string queryInsertEmpleado = @"
                    IF NOT EXISTS (SELECT 1 FROM NominaGeneral_Empleado WHERE id_NominaGeneral = @idNominaGeneral AND id_Empleado = @idEmpleado)
                    BEGIN
                        INSERT INTO NominaGeneral_Empleado (id_NominaGeneral, id_Empleado, DiasTrabajados, SueldoBruto, SueldoNeto)
                        VALUES (@idNominaGeneral, @idEmpleado, @diasTrabajados, @sueldoBruto, @sueldoNeto);
                    END
                    ELSE
                    BEGIN
                        UPDATE NominaGeneral_Empleado
                        SET DiasTrabajados = @diasTrabajados, SueldoBruto = @sueldoBruto, SueldoNeto = @sueldoNeto
                        WHERE id_NominaGeneral = @idNominaGeneral AND id_Empleado = @idEmpleado;
                    END";

                            using (SqlCommand cmdInsertEmpleado = new SqlCommand(queryInsertEmpleado, cn))
                            {
                                cmdInsertEmpleado.Parameters.AddWithValue("@idNominaGeneral", idNominaGeneral);
                                cmdInsertEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                                cmdInsertEmpleado.Parameters.AddWithValue("@diasTrabajados", diasTrabajados);
                                cmdInsertEmpleado.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);
                                cmdInsertEmpleado.Parameters.AddWithValue("@sueldoNeto", sueldoNeto);

                                cmdInsertEmpleado.ExecuteNonQuery();
                            }
                        }
                    }
                }
                MessageBox.Show("Nómina general generada o actualizada exitosamente.");
            }
        }

        private void btn_CierrePeriodo_GenerarNomina_Click(object sender, EventArgs e)
        {
            // Mensaje de confirmación
            DialogResult result = MessageBox.Show("¿Estás seguro que quieres cerrar el periodo?", "Confirmar cierre de periodo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Variables locales para el nuevo periodo
                string nuevoMes = mes;
                int nuevoAno = anoSeleccionado;

                // Aumentar el mes y actualizar el año si es necesario
                switch (mes)
                {
                    case "Enero": nuevoMes = "Febrero"; break;
                    case "Febrero": nuevoMes = "Marzo"; break;
                    case "Marzo": nuevoMes = "Abril"; break;
                    case "Abril": nuevoMes = "Mayo"; break;
                    case "Mayo": nuevoMes = "Junio"; break;
                    case "Junio": nuevoMes = "Julio"; break;
                    case "Julio": nuevoMes = "Agosto"; break;
                    case "Agosto": nuevoMes = "Septiembre"; break;
                    case "Septiembre": nuevoMes = "Octubre"; break;
                    case "Octubre": nuevoMes = "Noviembre"; break;
                    case "Noviembre": nuevoMes = "Diciembre"; break;
                    case "Diciembre":
                        nuevoMes = "Enero";
                        nuevoAno += 1; // Incrementar el año
                        break;
                }

                // Insertar el nuevo periodo en la base de datos
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    string query = "INSERT INTO Periodo (Mes, Ano) VALUES (@Mes, @Ano)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Mes", nuevoMes);
                    cmd.Parameters.AddWithValue("@Ano", nuevoAno);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                // Actualizar las variables del periodo actual
                mes = nuevoMes;
                anoSeleccionado = nuevoAno;


                MessageBox.Show("El periodo ha sido cerrado exitosamente. Nuevo periodo: " + nuevoMes + " " + nuevoAno, "Periodo Cerrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Mes_GenerarNomina.Text = mes;
                txt_Ano_GenerarNomina.Text = anoSeleccionado.ToString();
                mostrarTablaEmpleadosNomina();
                mostrarTablaDPNomina();
                dtgv_EmDP_GenerarNomina.Rows.Clear();
                dtgv_Matriz_GenerarNomina.Rows.Clear();
                ColocarDatos();
                // dtgv_EmDP_GenerarNomina.Clear();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Obtener el estado actual del checkbox
            bool isChecked = checkBox1.Checked;

            // Recorrer todas las filas del DataGridView y actualizar la columna "Activo"
            foreach (DataGridViewRow row in dtgv_Matriz_GenerarNomina.Rows)
            {
                // Asegurarse de que la fila no es una fila nueva (vacía)
                if (!row.IsNewRow)
                {
                    row.Cells["Activo"].Value = isChecked; // Establecer el valor de la casilla de verificación
                }
            }
        }

        private void dtgv_Matriz_GenerarNomina_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgv_Matriz_GenerarNomina_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica que la columna sea el checkbox y que el checkbox esté marcado
            if (dtgv_Matriz_GenerarNomina.Columns[e.ColumnIndex].Name == "Activo" && (bool)dtgv_Matriz_GenerarNomina[e.ColumnIndex, e.RowIndex].EditedFormattedValue)
            {
                // Obtiene el valor de la columna "ID Empleado" de la misma fila
                var empleadoId = dtgv_Matriz_GenerarNomina.Rows[e.RowIndex].Cells[2].Value; // Cambia el índice según la posición de tu columna

                //MessageBox.Show("ID del Empleado seleccionado: " + empleadoId);
                idEmpleadoSeleccionado= (int)empleadoId;
                mostrarTablaDEDPERNOMINA();
            }
        }
    }
}
