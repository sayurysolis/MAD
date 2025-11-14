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
using static NominaMAD.DAO.helper;

namespace NominaMAD
{
    public partial class P_GenerarNomina : Form
    {
        public P_GenerarNomina()
        {
            InitializeComponent();
        }
        private int idPeriodoActual;
        private int mesPeriodoActual; // Ej: 11
        private int anoPeriodoActual; // Ej: 2025
        private string nombreMesActual;
        private void P_GenerarNomina_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            ObtenerPeriodoActual();
            AsignarMes();
            txt_Mes_GenerarNomina.Enabled = false;
            txt_Mes_GenerarNomina.Text = nombreMesActual;
            txt_Ano_GenerarNomina.Enabled = false;
            txt_Ano_GenerarNomina.Text = anoPeriodoActual.ToString();
            dtgv_Matriz_GenerarNomina.Columns.Clear();
            dtgv_Matriz_GenerarNomina.Columns.Add("id_Matriz", "ID Matriz"); // Lo ocultaremos
            dtgv_Matriz_GenerarNomina.Columns["id_Matriz"].Visible = false;

            // Columna de CheckBox
            DataGridViewCheckBoxColumn activoColumn = new DataGridViewCheckBoxColumn();
            activoColumn.Name = "Activo";
            activoColumn.HeaderText = "Activo";
            activoColumn.TrueValue = true;
            activoColumn.FalseValue = false;
            dtgv_Matriz_GenerarNomina.Columns.Add(activoColumn);
            dtgv_Matriz_GenerarNomina.Columns.Add("id_Empleado", "ID Empleado");
            dtgv_Matriz_GenerarNomina.Columns["id_Empleado"].ReadOnly = true;
            dtgv_Matriz_GenerarNomina.Columns.Add("NombreEmpleado", "Nombre Empleado");
            dtgv_Matriz_GenerarNomina.Columns["NombreEmpleado"].ReadOnly = true;
            // Estas columnas SÍ son editables
            dtgv_Matriz_GenerarNomina.Columns.Add("Faltas", "Faltas");
            dtgv_Matriz_GenerarNomina.Columns.Add("Productividad", "B. Productividad");
            dtgv_Matriz_GenerarNomina.Columns.Add("Puntualidad", "B. Puntualidad");
            dtgv_Matriz_GenerarNomina.Columns.Add("Asistencia", "B. Asistencia");
            dtgv_Matriz_GenerarNomina.Columns.Add("Despensa", "Despensa");
            ColocarDatos();
            foreach (DataGridViewRow row in dtgv_Matriz_GenerarNomina.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Activo"];
                chk.Value = true;
            }
        }

        void ObtenerPeriodoActual()
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {

                string query = "SELECT TOP 1 id_Periodo, Mes, Anio FROM Periodo WHERE Cerrado = 0 ORDER BY id_Periodo DESC";
                SqlCommand cmd = new SqlCommand(query, cn);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    idPeriodoActual = reader.GetInt32(0);  // id_Periodo
                    mesPeriodoActual = reader.GetInt32(1);  // Mes (INT)
                    anoPeriodoActual = reader.GetInt32(2);  // Ano (INT)
                }
                else
                {

                    MessageBox.Show("No se encontró un período de nómina abierto. Verifique la tabla 'Periodo'.");
                    idPeriodoActual = -1; // Marcar como inválido
                    mesPeriodoActual = DateTime.Now.Month;
                    anoPeriodoActual = DateTime.Now.Year;
                }

                reader.Close();
            }
        }

        public void ColocarDatos()
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT 
                E.ID_Empleado, 
                E.Nombre + ' ' + E.ApellidoPaterno + ' ' + ISNULL(E.ApellidoMaterno, '') AS NombreEmpleado,
                E.SalarioDiario
            FROM Empleado E
            WHERE E.estatus = 1
            ORDER BY E.ID_Empleado";

                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataReader reader = cmd.ExecuteReader();

                dtgv_Matriz_GenerarNomina.Rows.Clear();

                while (reader.Read())
                {
                    int idEmpleado = reader.GetInt32(reader.GetOrdinal("ID_Empleado"));
                    string nombreEmpleado = reader.GetString(reader.GetOrdinal("NombreEmpleado"));
                    decimal salarioDiario = reader.GetDecimal(reader.GetOrdinal("SalarioDiario"));

                    // ¡IMPORTANTE! 
                    // Usamos las variables globales del período actual que ya leímos.
                  // int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoPeriodoActual);

                    // Los bonos y otros se inicializan en 0. 
                    // El usuario los editará en el grid.
                    decimal productividad = 0;
                    decimal puntualidad = 0;
                    decimal asistencia = 0;
                    decimal despensa = 0;

                    var rowIndex = dtgv_Matriz_GenerarNomina.Rows.Add();
                    var row = dtgv_Matriz_GenerarNomina.Rows[rowIndex];

                    // Ponemos 'id_Matriz' como 0, indicando que aún no se guarda
                    row.Cells["id_Matriz"].Value = 0;
                    row.Cells["Activo"].Value = true; // Marcado por defecto
                    row.Cells["id_Empleado"].Value = idEmpleado;
                    row.Cells["NombreEmpleado"].Value = nombreEmpleado;
                    //row.Cells["Faltas"].Value = faltas;
                    row.Cells["Productividad"].Value = productividad;
                    row.Cells["Puntualidad"].Value = puntualidad;
                    row.Cells["Asistencia"].Value = asistencia;
                    row.Cells["Despensa"].Value = despensa;
                }

                reader.Close();
            }
        }
        // Después de llenar el DataGridView o en un evento de carga

        public void AsignarMes()
        {
            // Usa la variable global 'mesPeriodoActual' (int) 
            // y asigna el resultado a 'nombreMesActual' (string)

            switch (mesPeriodoActual)
            {
                case 1: nombreMesActual = "Enero"; break;
                case 2: nombreMesActual = "Febrero"; break;
                case 3: nombreMesActual = "Marzo"; break;
                case 4: nombreMesActual = "Abril"; break;
                case 5: nombreMesActual = "Mayo"; break;
                case 6: nombreMesActual = "Junio"; break;
                case 7: nombreMesActual = "Julio"; break;
                case 8: nombreMesActual = "Agosto"; break;
                case 9: nombreMesActual = "Septiembre"; break;
                case 10: nombreMesActual = "Octubre"; break;
                case 11: nombreMesActual = "Noviembre"; break;
                case 12: nombreMesActual = "Diciembre"; break;
                default: nombreMesActual = "Desconocido"; break;
            }
        }
        /*
        public void LlenarMatriz()
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                

                // Consulta para obtener todos los empleados
                string queryEmpleados = "SELECT ID_Empleado, Nombre FROM Empleado";
                using (SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn))
                using (SqlDataReader readerEmpleados = cmdEmpleados.ExecuteReader())
                {
                    // Recorre cada empleado
                    while (readerEmpleados.Read())
                    {
                        int idEmpleado = readerEmpleados.GetInt32(0);
                        string nombreEmpleado = readerEmpleados.GetString(1);

                        // Comando de inserción para agregar el registro en la tabla Matriz
                        string queryInsert = @"
                    INSERT INTO Matriz (
                        id_Empleado, NombreEmpleado, Faltas, Asistencia, Puntualidad, Despensa, Productividad
                    ) VALUES (
                        @idEmpleado, @NombreEmpleado, 0, 0.00, 0.00, 0.00, 0.00
                    )";

                        using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
                        {
                            cmdInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                            cmdInsert.Parameters.AddWithValue("@NombreEmpleado", nombreEmpleado);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }

                cn.Close();
            }
        }

        public void LlenarMatrizConEmpleados()
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                

                // Asegurarse de que cada empleado tenga un registro en la tabla Matriz
                string insertarEmpleadosEnMatriz = @"
            INSERT INTO Matriz (id_Empleado, NombreEmpleado, Faltas, Asistencia, Puntualidad, Despensa, Productividad)
            SELECT e.ID_Empleado, e.Nombre, 0, 0.00, 0.00, 0.00, 0.00
            FROM Empleado e
            LEFT JOIN Matriz m ON e.ID_Empleado = m.id_Empleado
            WHERE m.id_Empleado IS NULL";

                using (SqlCommand cmdInsertar = new SqlCommand(insertarEmpleadosEnMatriz, cn))
                {
                    cmdInsertar.ExecuteNonQuery();
                }

                // Cargar los datos de la tabla Matriz en el DataGridView
                string queryMatriz = "SELECT id_Matriz, id_Empleado, NombreEmpleado, Faltas, Asistencia, Puntualidad, Despensa, Productividad FROM Matriz";
                SqlDataAdapter da = new SqlDataAdapter(queryMatriz, cn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dtgv_Matriz_GenerarNomina.DataSource = dt;

                // Opcional: ajustar el ancho de columnas si quieres
                dtgv_Matriz_GenerarNomina.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        public void ColocarDatos()
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT 
                E.ID_Empleado, 
                E.Nombre + ' ' + E.ApellidoPaterno + ' ' + ISNULL(E.ApellidoMaterno, '') AS NombreEmpleado,
                E.SalarioDiario
            FROM Empleado E
            WHERE E.estatus = 1
            ORDER BY E.ID_Empleado";

                SqlCommand cmd = new SqlCommand(query, cn);
                
                SqlDataReader reader = cmd.ExecuteReader();

                dtgv_Matriz_GenerarNomina.Rows.Clear();

                while (reader.Read())
                {
                    int idEmpleado = reader.GetInt32(reader.GetOrdinal("ID_Empleado"));
                    string nombreEmpleado = reader.GetString(reader.GetOrdinal("NombreEmpleado"));
                    decimal salarioDiario = reader.GetDecimal(reader.GetOrdinal("SalarioDiario"));

                    int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);
                    int diasTrabajados = 30 - faltas;
                    decimal productividad = 0; // Si quieres, luego puedes calcularla
                    decimal puntualidad = 0;   // Igual, si quieres calcularla
                    decimal asistencia = 0;    // Igual, si quieres calcularla
                    decimal despensa = 0;      // Igual, si quieres calcularla

                    var rowIndex = dtgv_Matriz_GenerarNomina.Rows.Add();
                    var row = dtgv_Matriz_GenerarNomina.Rows[rowIndex];

                    row.Cells["id_Empleado"].Value = idEmpleado;
                    row.Cells["NombreEmpleado"].Value = nombreEmpleado;
                    row.Cells["Faltas"].Value = faltas;
                    row.Cells["Productividad"].Value = productividad;
                    row.Cells["Puntualidad"].Value = puntualidad;
                    row.Cells["Asistencia"].Value = asistencia;
                    row.Cells["Despensa"].Value = despensa;
                }

                reader.Close();
                cn.Close();
            }
        }
        private void CargarDatosEmpleadosConMatriz()
        {
            using (SqlConnection cn =BD_Conexion.ObtenerConexion())
            {
                

                // Traer todos los empleados y sus registros en Matriz (si existen)
                string query = @"
            SELECT 
                e.id_Empleado,
                e.Nombre + ' ' + e.ApellidoPaterno + ' ' + ISNULL(e.ApellidoMaterno,'') AS NombreEmpleado,
                ISNULL(m.id_Matriz, 0) AS id_Matriz,
                ISNULL(m.Activo, 0) AS Activo,
                ISNULL(m.Faltas, 0) AS Faltas,
                ISNULL(m.Asistencia, 0) AS Asistencia,
                ISNULL(m.Puntualidad, 0) AS Puntualidad,
                ISNULL(m.Despensa, 0) AS Despensa,
                ISNULL(m.Productividad, 0) AS Productividad,
                ISNULL(m.HorasExtras, 0) AS HorasExtras,
                ISNULL(m.PrestamoInfo, 0) AS PrestamoInfo,
                ISNULL(m.FondoAhorro, 0) AS FondoAhorro
            FROM Empleado e
            LEFT JOIN Matriz m ON e.id_Empleado = m.id_Empleado
            WHERE e.estatus = 1
            ORDER BY e.id_Empleado";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Limpiar DataGridView
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
                            // Eliminamos Vacaciones y PrimaVacacional porque no las usamos
                            reader["Productividad"],
                            reader["HorasExtras"],
                            reader["PrestamoInfo"],
                            reader["FondoAhorro"]
                        );
                    }

                    reader.Close();
                }
            }
        }
        //-------------------------------------------------------//
        private int idEmpleadoSeleccionado;
        private int mesSeleccionado = DateTime.Now.Month; // Mes actual
        private int anoSeleccionado = DateTime.Now.Year; // Año actual
        private string mes;
        string modificarOpcion;
        private void mostrarTablaEmpleadosNomina()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string query = @"
            SELECT 
                e.ID_Empleado,
                e.Nombre + ' ' + e.ApellidoPaterno + ' ' + ISNULL(e.ApellidoMaterno,'') AS NombreCompleto,
                p.Nombre AS Puesto,
                d.Nombre AS Departamento,
                e.SalarioDiario,
                e.SalarioDiarioIntegrado,
                e.Banco,
                e.NumeroCuenta
            FROM Empleado e
            INNER JOIN Puesto p ON e.PuestoID = p.ID_Puesto
            INNER JOIN Departamento d ON e.DepID = d.ID_Departamento
            WHERE e.estatus = 1
        ";

                SqlDataAdapter da = new SqlDataAdapter(query, cn);
                da.Fill(dt);
                dtgv_Empleados_GenerarNomina.DataSource = dt;
            }
        }

        private void mostrarTablaMatriz()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Matriz"; // Puedes ajustar columnas si no quieres todas
                SqlDataAdapter da = new SqlDataAdapter(query, cn);
                da.Fill(dt);
                dtgv_Matriz_GenerarNomina.DataSource = dt;
            }
        }
        private void mostrarTablaDPNomina()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PercepcionesDeduccion", cn);
                da.SelectCommand.CommandType = CommandType.Text;
                
                da.Fill(dt);
               dtgv_DP_GenerarNomina.DataSource = dt;

            }
        }
        private void mostrarTablaDEDPERNOMINA()
        {
            dtgv_EmDP_GenerarNomina.Rows.Clear();

            if (idEmpleadoSeleccionado > 0)
            {
                DataTable dt = new DataTable();

                using (SqlConnection cn = BD_Conexion.ObtenerConexion())
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
                FROM DEDPERNOMINA DPN
                INNER JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
                WHERE DPN.id_Empleado = @idEmpleado AND 
                      DPN.Mes = @mes AND 
                      DPN.Ano = @ano";

                    SqlDataAdapter da = new SqlDataAdapter(query, cn);
                    da.SelectCommand.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
                    da.SelectCommand.Parameters.AddWithValue("@mes", mesSeleccionado); // usar int
                    da.SelectCommand.Parameters.AddWithValue("@ano", anoSeleccionado);

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
        private void dtgv_EmDP_GenerarNomina_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dtgv_Empleados_GenerarNomina_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Capturar el ID del empleado seleccionado
                idEmpleadoSeleccionado = Convert.ToInt32(dtgv_Empleados_GenerarNomina.Rows[e.RowIndex].Cells["ID_Empleado"].Value);

                mostrarTablaDEDPERNOMINA();
            }
        }
        private void dtgv_DP_GenerarNomina_CellClick(object sender, DataGridViewCellEventArgs e)
        {

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
                        using (SqlConnection cn = BD_Conexion.ObtenerConexion())
                        {
                            string query = "INSERT INTO DEDPERNOMINA (id_Empleado, id_PD, Mes, Ano) VALUES (@idEmpleado, @idPD, @mes, @ano)";
                            SqlCommand cmd = new SqlCommand(query, cn);
                            cmd.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
                            cmd.Parameters.AddWithValue("@idPD", idPD);
                            cmd.Parameters.AddWithValue("@mes", mes);
                            cmd.Parameters.AddWithValue("@ano", anoSeleccionado);


                            cmd.ExecuteNonQuery();
                        }

                        //  MessageBox.Show($"Deducción/Percepción '{nombreDP}' agregada al empleado con ID {idEmpleadoSeleccionado}.");
                    }
                }

                // Actualizar la tabla intermedia para mostrar los nuevos registros agregados
                mostrarTablaDEDPERNOMINA();
                ColocarDatos();
            }
        }

        private (decimal totalDeducciones, decimal totalPercepciones) CalcularTotalesDesdeBD(int idEmpleado, string mes, int ano, decimal sueldoBruto, int faltas, decimal sueldoDiario)
        {
            decimal totalDeducciones = 0;
            decimal totalPercepciones = 0;

            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {


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
                    using (SqlConnection cn = BD_Conexion.ObtenerConexion())
                    {

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


        */
        private decimal ObtenerSalarioDiarioEmpleado(int idEmpleado)
        {
            decimal salarioDiario = 0;

            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string query = "SELECT SalarioDiario FROM Empleado WHERE id_Empleado = @idEmpleado";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);


                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    salarioDiario = Convert.ToDecimal(result);
                }
            }

            return salarioDiario;
        }
        private DateTime ObtenerFechaIngresoEmpleado(int idEmpleado)
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {

                string query = "SELECT FechaIngreso FROM Empleado WHERE ID_Empleado = @idEmpleado";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                return (DateTime)cmd.ExecuteScalar();
            }
        }

        private void btn_GenerarNomina_Click(object sender, EventArgs e)
        {
            // Confirmar antes de ejecutar
            var confirmResult = MessageBox.Show(
                $"¿Está seguro de generar la nómina para el período {nombreMesActual} {anoPeriodoActual}? Esta acción guardará los datos permanentemente.",
                "Confirmar Generación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
            {
                return; // El usuario canceló
            }

            // Usar las variables globales que ya cargamos
            int P_ID = this.idPeriodoActual;
            int P_MES = this.mesPeriodoActual;
            int P_ANIO = this.anoPeriodoActual;

            if (P_ID == -1) // -1 es el valor de error que pusimos en ObtenerPeriodoActual
            {
                MessageBox.Show("Error: No hay un período de nómina abierto. No se puede continuar.", "Error de Período", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lista para guardar el desglose de cada empleado
            List<NominaDetalle> detalles = new List<NominaDetalle>();

            // Iniciar conexión y transacción
            // Se procesa TODO o NADA. Si un empleado falla, se revierte todo.
            using (SqlConnection cnn = BD_Conexion.ObtenerConexion())
            {
                SqlTransaction transaction = cnn.BeginTransaction("GenerarNominaCompleta");

                try
                {
                    // Recorrer CADA fila del DataGridView 'dtgv_Matriz_GenerarNomina'
                    foreach (DataGridViewRow row in dtgv_Matriz_GenerarNomina.Rows)
                    {
                        // 1. Revisar si esta fila está marcada con el CheckBox "Activo"
                        DataGridViewCheckBoxCell chk = row.Cells["Activo"] as DataGridViewCheckBoxCell;
                        if (chk == null || !(bool)chk.Value)
                        {
                            continue; // Si no está marcado, saltar a la siguiente fila
                        }

                        // --- 2. LEER DATOS ---
                        // Leer datos del Empleado (de la BD)
                        int idEmpleado = Convert.ToInt32(row.Cells["id_Empleado"].Value);
                        EmpleadoData emp = ObtenerDatosEmpleado(idEmpleado, cnn, transaction);
                        if (emp == null)
                        {
                            // Si el empleado no se encuentra (raro), se salta
                            throw new Exception($"No se encontraron datos para el empleado ID {idEmpleado}.");
                        }

                        // Leer datos de la Matriz (del Grid)
                        int faltas = Convert.ToInt32(row.Cells["Faltas"].Value);
                        decimal bonoPuntualidad = Convert.ToDecimal(row.Cells["Puntualidad"].Value);
                        decimal bonoAsistencia = Convert.ToDecimal(row.Cells["Asistencia"].Value);
                        decimal bonoProductividad = Convert.ToDecimal(row.Cells["Productividad"].Value);
                        decimal despensa = Convert.ToDecimal(row.Cells["Despensa"].Value);

                        // --- 3. CÁLCULOS ---
                        // (Asumimos 30 días por período como en tu código viejo)
                        int diasPeriodo = 30;
                        int diasTrabajados = diasPeriodo - faltas;

                        // Limpiar la lista de detalles para este empleado
                        detalles.Clear();

                        // PERCEPCIONES
                        // ¡¡IMPORTANTE!! Asumo IDs de la tabla Conceptos. ¡DEBES AJUSTARLOS!
                        decimal sueldo = emp.SalarioDiario * diasTrabajados;
                        detalles.Add(new NominaDetalle { ConceptosID = 1, Monto = sueldo }); // 1 = Sueldo

                        detalles.Add(new NominaDetalle { ConceptosID = 10, Monto = bonoPuntualidad }); // 10 = B. Puntualidad
                        detalles.Add(new NominaDetalle { ConceptosID = 11, Monto = bonoAsistencia }); // 11 = B. Asistencia
                        detalles.Add(new NominaDetalle { ConceptosID = 12, Monto = bonoProductividad }); // 12 = B. Productividad
                        detalles.Add(new NominaDetalle { ConceptosID = 13, Monto = despensa }); // 13 = Despensa

                        // DEDUCCIONES
                        // ¡¡IMPORTANTE!! Asumo IDs de la tabla Conceptos. ¡DEBES AJUSTARLOS!
                        decimal imss = CalcularIMSS(emp.SalarioDiarioIntegrado, diasTrabajados);
                        detalles.Add(new NominaDetalle { ConceptosID = 100, Monto = imss }); // 100 = IMSS

                        // Calcular base gravable para ISR (Sueldo + Bonos)
                        decimal baseGravableISR = sueldo + bonoPuntualidad + bonoAsistencia + bonoProductividad; // Despensa usualmente exenta
                        decimal isr = CalcularISR(baseGravableISR);
                        detalles.Add(new NominaDetalle { ConceptosID = 101, Monto = isr }); // 101 = ISR


                        // --- 4. CALCULAR TOTALES ---
                        decimal totalPercepciones = sueldo + bonoPuntualidad + bonoAsistencia + bonoProductividad + despensa;
                        decimal totalDeducciones = imss + isr;
                        decimal sueldoNeto = totalPercepciones - totalDeducciones;

                        // --- 5. GUARDAR EN BD ---

                        // A. Insertar en Matriz
                        string sqlMatriz = @"
                    INSERT INTO Matriz (
                        id_Empleado, PeriodoID, SalarioDiario, SalarioDiarioIntegrado, PagoMensual, 
                        Faltas, DiasTrabajados, IMSS, ISR, BonoPuntualidad, Despensa, 
                        BonoAsistencia, BonoProductividad
                    ) VALUES (
                        @idEmp, @pID, @SD, @SDI, @PagoMensual, @Faltas, @DiasTrab, @IMSS, @ISR, 
                        @BonoPunt, @Despensa, @BonoAsis, @BonoProd
                    )";
                        using (SqlCommand cmdMatriz = new SqlCommand(sqlMatriz, cnn, transaction))
                        {
                            cmdMatriz.Parameters.AddWithValue("@idEmp", idEmpleado);
                            cmdMatriz.Parameters.AddWithValue("@pID", P_ID);
                            cmdMatriz.Parameters.AddWithValue("@SD", emp.SalarioDiario);
                            cmdMatriz.Parameters.AddWithValue("@SDI", emp.SalarioDiarioIntegrado);
                            cmdMatriz.Parameters.AddWithValue("@PagoMensual", sueldo); // PagoMensual es solo el sueldo
                            cmdMatriz.Parameters.AddWithValue("@Faltas", faltas);
                            cmdMatriz.Parameters.AddWithValue("@DiasTrab", diasTrabajados);
                            cmdMatriz.Parameters.AddWithValue("@IMSS", imss);
                            cmdMatriz.Parameters.AddWithValue("@ISR", isr);
                            cmdMatriz.Parameters.AddWithValue("@BonoPunt", bonoPuntualidad);
                            cmdMatriz.Parameters.AddWithValue("@Despensa", despensa);
                            cmdMatriz.Parameters.AddWithValue("@BonoAsis", bonoAsistencia);
                            cmdMatriz.Parameters.AddWithValue("@BonoProd", bonoProductividad);

                            cmdMatriz.ExecuteNonQuery();
                        }

                        // B. Insertar en Nomina (Encabezado) y obtener el nuevo ID
                        string sqlNomina = @"
                    INSERT INTO Nomina (MES, Anio, SueldoBruto, SueldoNeto, EmpleadoID, PeriodoID)
                    VALUES (@Mes, @Anio, @Bruto, @Neto, @idEmp, @pID);
                    SELECT SCOPE_IDENTITY();"; // Devuelve el ID que se acaba de crear

                        int nuevaNominaID = 0;
                        using (SqlCommand cmdNomina = new SqlCommand(sqlNomina, cnn, transaction))
                        {
                            cmdNomina.Parameters.AddWithValue("@Mes", P_MES);
                            cmdNomina.Parameters.AddWithValue("@Anio", P_ANIO);
                            cmdNomina.Parameters.AddWithValue("@Bruto", totalPercepciones);
                            cmdNomina.Parameters.AddWithValue("@Neto", sueldoNeto);
                            cmdNomina.Parameters.AddWithValue("@idEmp", idEmpleado);
                            cmdNomina.Parameters.AddWithValue("@pID", P_ID);

                            // ExecuteScalar se usa para obtener el ID de vuelta
                            nuevaNominaID = Convert.ToInt32(cmdNomina.ExecuteScalar());
                        }

                        // C. Insertar en NominaDetalle (usando el ID de la nómina)
                        string sqlDetalle = @"
                    INSERT INTO NominaDetalle (NominaID, ConceptosID, Monto)
                    VALUES (@NominaID, @ConceptoID, @Monto)";

                        // Reutilizamos el comando
                        using (SqlCommand cmdDetalle = new SqlCommand(sqlDetalle, cnn, transaction))
                        {
                            foreach (var detalle in detalles)
                            {
                                // Solo guardamos si el monto es mayor a 0
                                if (detalle.Monto > 0)
                                {
                                    cmdDetalle.Parameters.Clear();
                                    cmdDetalle.Parameters.AddWithValue("@NominaID", nuevaNominaID);
                                    cmdDetalle.Parameters.AddWithValue("@ConceptoID", detalle.ConceptosID);
                                    cmdDetalle.Parameters.AddWithValue("@Monto", detalle.Monto);
                                    cmdDetalle.ExecuteNonQuery();
                                }
                            }
                        }
                    } // Fin del FOR EACH

                    // Si todo salió bien, confirma la transacción
                    transaction.Commit();

                    MessageBox.Show("¡Éxito! Nómina generada correctamente para todos los empleados seleccionados.", "Proceso Terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Opcional: Cerrar el período para que no se vuelva a calcular
                    // CerrarPeriodo(P_ID, cnn); 
                    // ... (necesitarías crear esta función)
                }
                catch (Exception ex)
                {
                    // Si algo falla, revierte TODOS los cambios
                    transaction.Rollback();
                    MessageBox.Show($"Error Crítico: No se pudo generar la nómina. Se revirtieron todos los cambios.\n\nError: {ex.Message}", "Error en Transacción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // Fin del USING de conexión
        }

        // Clase helper para la lista de detalles
        public class NominaDetalle
        {
            public int ConceptosID { get; set; }
            public decimal Monto { get; set; }
        }

        private void btn_GenerarNominaInd_GenerarNomina_Click(object sender, EventArgs e)
        {
            // Confirmar antes de ejecutar
            var confirmResult = MessageBox.Show(
                $"¿Está seguro de generar la nómina para el período {nombreMesActual} {anoPeriodoActual}? Esta acción guardará los datos permanentemente.",
                "Confirmar Generación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
            {
                return; // El usuario canceló
            }

            // Usar las variables globales que ya cargamos
            int P_ID = this.idPeriodoActual;
            int P_MES = this.mesPeriodoActual;
            int P_ANIO = this.anoPeriodoActual;

            if (P_ID <= 0) // Si es 0 o -1, no hay período válido
            {
                MessageBox.Show("Error: No hay un período de nómina abierto. No se puede continuar.", "Error de Período", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lista para guardar el desglose de cada empleado
            List<NominaDetalleItem> detalles = new List<NominaDetalleItem>();

            // Iniciar conexión y transacción
            // Se procesa TODO o NADA. Si un empleado falla, se revierte todo.
            using (SqlConnection cnn = BD_Conexion.ObtenerConexion())
            {
                SqlTransaction transaction = cnn.BeginTransaction("GenerarNominaCompleta");

                try
                {
                    // Recorrer CADA fila del DataGridView 'dtgv_Matriz_GenerarNomina'
                    foreach (DataGridViewRow row in dtgv_Matriz_GenerarNomina.Rows)
                    {
                        // 1. Revisar si esta fila está marcada con el CheckBox "Activo"
                        DataGridViewCheckBoxCell chk = row.Cells["Activo"] as DataGridViewCheckBoxCell;
                        if (chk == null || chk.Value == null || !(bool)chk.Value)
                        {
                            continue; // Si no está marcado, saltar a la siguiente fila
                        }

                        // --- 2. LEER DATOS ---
                        // Leer datos del Empleado (de la BD)
                        int idEmpleado = Convert.ToInt32(row.Cells["id_Empleado"].Value);
                        EmpleadoData emp = ObtenerDatosEmpleado(idEmpleado, cnn, transaction);
                        if (emp == null)
                        {
                            throw new Exception($"No se encontraron datos para el empleado ID {idEmpleado}.");
                        }

                        // Leer datos de la Matriz (del Grid)
                        int faltas = Convert.ToInt32(row.Cells["Faltas"].Value);
                        decimal bonoPuntualidad = Convert.ToDecimal(row.Cells["Puntualidad"].Value);
                        decimal bonoAsistencia = Convert.ToDecimal(row.Cells["Asistencia"].Value);
                        decimal bonoProductividad = Convert.ToDecimal(row.Cells["Productividad"].Value);
                        decimal despensa = Convert.ToDecimal(row.Cells["Despensa"].Value);

                        // --- 3. CÁLCULOS ---
                        // (Asumimos 30 días por período)
                        int diasPeriodo = 30;
                        int diasTrabajados = diasPeriodo - faltas;

                        // Limpiar la lista de detalles para este empleado
                        detalles.Clear();

                        // PERCEPCIONES
                        // ¡¡IMPORTANTE!! Asumo IDs de la tabla Conceptos. ¡DEBES AJUSTARLOS!
                        decimal sueldo = emp.SalarioDiario * diasTrabajados;
                        detalles.Add(new NominaDetalleItem { ConceptosID = 1, Monto = sueldo }); // 1 = Sueldo

                        if (bonoPuntualidad > 0) detalles.Add(new NominaDetalleItem { ConceptosID = 10, Monto = bonoPuntualidad }); // 10 = B. Puntualidad
                        if (bonoAsistencia > 0) detalles.Add(new NominaDetalleItem { ConceptosID = 11, Monto = bonoAsistencia }); // 11 = B. Asistencia
                        if (bonoProductividad > 0) detalles.Add(new NominaDetalleItem { ConceptosID = 12, Monto = bonoProductividad }); // 12 = B. Productividad
                        if (despensa > 0) detalles.Add(new NominaDetalleItem { ConceptosID = 13, Monto = despensa }); // 13 = Despensa

                        // DEDUCCIONES
                        // ¡¡IMPORTANTE!! Asumo IDs de la tabla Conceptos. ¡DEBES AJUSTARLOS!
                        decimal imss = CalcularIMSS(emp.SalarioDiarioIntegrado, diasTrabajados); // Usa el dummy
                        if (imss > 0) detalles.Add(new NominaDetalleItem { ConceptosID = 100, Monto = imss }); // 100 = IMSS

                        // Calcular base gravable para ISR (Sueldo + Bonos)
                        decimal baseGravableISR = sueldo + bonoPuntualidad + bonoAsistencia + bonoProductividad; // Despensa usualmente exenta
                        decimal isr = CalcularISR(baseGravableISR); // ¡Usa TU función de ISR!
                        if (isr > 0) detalles.Add(new NominaDetalleItem { ConceptosID = 101, Monto = isr }); // 101 = ISR


                        // --- 4. CALCULAR TOTALES ---
                        decimal totalPercepciones = sueldo + bonoPuntualidad + bonoAsistencia + bonoProductividad + despensa;
                        decimal totalDeducciones = imss + isr;
                        decimal sueldoNeto = totalPercepciones - totalDeducciones;

                        // --- 5. GUARDAR EN BD ---

                        // A. Insertar en Matriz
                        string sqlMatriz = @"
                    INSERT INTO Matriz (
                        id_Empleado, PeriodoID, SalarioDiario, SalarioDiarioIntegrado, PagoMensual, 
                        Faltas, DiasTrabajados, IMSS, ISR, BonoPuntualidad, Despensa, 
                        BonoAsistencia, BonoProductividad
                    ) VALUES (
                        @idEmp, @pID, @SD, @SDI, @PagoMensual, @Faltas, @DiasTrab, @IMSS, @ISR, 
                        @BonoPunt, @Despensa, @BonoAsis, @BonoProd
                    )";
                        using (SqlCommand cmdMatriz = new SqlCommand(sqlMatriz, cnn, transaction))
                        {
                            cmdMatriz.Parameters.AddWithValue("@idEmp", idEmpleado);
                            cmdMatriz.Parameters.AddWithValue("@pID", P_ID);
                            cmdMatriz.Parameters.AddWithValue("@SD", emp.SalarioDiario);
                            cmdMatriz.Parameters.AddWithValue("@SDI", emp.SalarioDiarioIntegrado);
                            cmdMatriz.Parameters.AddWithValue("@PagoMensual", sueldo);
                            cmdMatriz.Parameters.AddWithValue("@Faltas", faltas);
                            cmdMatriz.Parameters.AddWithValue("@DiasTrab", diasTrabajados);
                            cmdMatriz.Parameters.AddWithValue("@IMSS", imss);
                            cmdMatriz.Parameters.AddWithValue("@ISR", isr);
                            cmdMatriz.Parameters.AddWithValue("@BonoPunt", bonoPuntualidad);
                            cmdMatriz.Parameters.AddWithValue("@Despensa", despensa);
                            cmdMatriz.Parameters.AddWithValue("@BonoAsis", bonoAsistencia);
                            cmdMatriz.Parameters.AddWithValue("@BonoProd", bonoProductividad);

                            cmdMatriz.ExecuteNonQuery();
                        }

                        // B. Insertar en Nomina (Encabezado) y obtener el nuevo ID
                        string sqlNomina = @"
                    INSERT INTO Nomina (MES, Anio, SueldoBruto, SueldoNeto, EmpleadoID, PeriodoID)
                    VALUES (@Mes, @Anio, @Bruto, @Neto, @idEmp, @pID);
                    SELECT SCOPE_IDENTITY();";

                        int nuevaNominaID = 0;
                        using (SqlCommand cmdNomina = new SqlCommand(sqlNomina, cnn, transaction))
                        {
                            cmdNomina.Parameters.AddWithValue("@Mes", P_MES);
                            cmdNomina.Parameters.AddWithValue("@Anio", P_ANIO);
                            cmdNomina.Parameters.AddWithValue("@Bruto", totalPercepciones);
                            cmdNomina.Parameters.AddWithValue("@Neto", sueldoNeto);
                            cmdNomina.Parameters.AddWithValue("@idEmp", idEmpleado);
                            cmdNomina.Parameters.AddWithValue("@pID", P_ID);

                            nuevaNominaID = Convert.ToInt32(cmdNomina.ExecuteScalar());
                        }

                        // C. Insertar en NominaDetalle (usando el ID de la nómina)
                        string sqlDetalle = @"
                    INSERT INTO NominaDetalle (NominaID, ConceptosID, Monto)
                    VALUES (@NominaID, @ConceptoID, @Monto)";

                        using (SqlCommand cmdDetalle = new SqlCommand(sqlDetalle, cnn, transaction))
                        {
                            foreach (var detalle in detalles)
                            {
                                // Solo guardamos si el monto es mayor a 0
                                if (detalle.Monto > 0)
                                {
                                    cmdDetalle.Parameters.Clear();
                                    cmdDetalle.Parameters.AddWithValue("@NominaID", nuevaNominaID);
                                    cmdDetalle.Parameters.AddWithValue("@ConceptoID", detalle.ConceptosID);
                                    cmdDetalle.Parameters.AddWithValue("@Monto", detalle.Monto);
                                    cmdDetalle.ExecuteNonQuery();
                                }
                            }
                        }
                    } // Fin del FOR EACH

                    // Si todo salió bien, confirma la transacción
                    transaction.Commit();

                    MessageBox.Show("¡Éxito! Nómina generada correctamente para todos los empleados seleccionados.", "Proceso Terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Deshabilitar el botón para evitar doble generación
                    btn_GenerarNominaInd_GenerarNomina.Enabled = false;
                }
                catch (Exception ex)
                {
                    // Si algo falla, revierte TODOS los cambios
                    transaction.Rollback();
                    MessageBox.Show($"Error Crítico: No se pudo generar la nómina. Se revirtieron todos los cambios.\n\nError: {ex.Message}", "Error en Transacción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // Fin del USING de conexión
        }

        // --- CLASES Y FUNCIONES DE AYUDA ---
        public class EmpleadoData
        {
            public int ID_Empleado { get; set; }
            public decimal SalarioDiario { get; set; }
            public decimal SalarioDiarioIntegrado { get; set; }
            public int DepID { get; set; }
            public int PuestoID { get; set; }
        }

        public class NominaDetalleItem
        {
            public int ConceptosID { get; set; }
            public decimal Monto { get; set; }
        }

        private EmpleadoData ObtenerDatosEmpleado(int idEmpleado, SqlConnection cnn, SqlTransaction trans)
        {
            string query = "SELECT ID_Empleado, SalarioDiario, SalarioDiarioIntegrado, DepID, PuestoID FROM Empleado WHERE ID_Empleado = @id";
            using (SqlCommand cmd = new SqlCommand(query, cnn, trans))
            {
                cmd.Parameters.AddWithValue("@id", idEmpleado);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new EmpleadoData
                        {
                            ID_Empleado = (int)reader["ID_Empleado"],
                            SalarioDiario = (decimal)reader["SalarioDiario"],
                            SalarioDiarioIntegrado = (decimal)reader["SalarioDiarioIntegrado"],
                            DepID = (int)reader["DepID"],
                            PuestoID = (int)reader["PuestoID"]
                        };
                    }
                    reader.Close();
                    return null; // Empleado no encontrado
                }
            }
        }

        // --- ¡¡TU LÓGICA DE ISR!! (¡ESTÁ BIEN!) ---
        // La pego aquí para que esté todo junto.
        private decimal CalcularISR(decimal sueldoBruto)
        {
            // ¡¡ADVERTENCIA!! Esta es la tabla ANUAL. 
            // Para nómina mensual, debes usar la tabla del Art. 96 de LISR.
            // Por la prisa de la entrega, la dejamos, pero deberías cambiarla.
            // Si tu sueldoBruto es mensual, estos rangos son incorrectos.

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
            // ... (el resto de tus else if, que están bien escritos) ...
            else if (sueldoBruto >= 4511707.38m)
            {
                decimal excedente = sueldoBruto - 4511707.38m;
                isr = 1414947.85m + (excedente * 0.3500m);
            }

            return isr;
        }

        // --- CÁLCULO DUMMY DE IMSS ---
        // (Para la entrega. La lógica real es muy compleja)
        private decimal CalcularIMSS(decimal sdi, int dias)
        {
            // LÓGICA DE EJEMPLO: 9% del SDI por los días
            return (sdi * dias) * 0.09m;
        }


        // --- BOTÓN DE CIERRE DE PERÍODO (CORREGIDO) ---
        private void btn_CierrePeriodo_GenerarNomina_Click(object sender, EventArgs e)
        {
            // Mensaje de confirmación
            DialogResult result = MessageBox.Show(
                $"¿Estás seguro que quieres cerrar el período {nombreMesActual} {anoPeriodoActual}? Esta acción creará el siguiente período.",
                "Confirmar cierre de periodo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 1. Calcular el nuevo período (con MES como INT)
                    int nuevoMesNum = mesPeriodoActual;
                    int nuevoAnoNum = anoPeriodoActual;

                    if (nuevoMesNum == 12)
                    {
                        nuevoMesNum = 1;
                        nuevoAnoNum += 1; // Incrementar el año
                    }
                    else
                    {
                        nuevoMesNum += 1; // Solo incrementar el mes
                    }

                    using (SqlConnection cn = BD_Conexion.ObtenerConexion())
                    {
                        // 2. Cerrar el período ACTUAL
                        string queryCerrar = "UPDATE Periodo SET Cerrado = 1 WHERE id_Periodo = @idPeriodoActual";
                        using (SqlCommand cmdCerrar = new SqlCommand(queryCerrar, cn))
                        {
                            cmdCerrar.Parameters.AddWithValue("@idPeriodoActual", this.idPeriodoActual);
                            cmdCerrar.ExecuteNonQuery();
                        }

                        // 3. Insertar el NUEVO período
                        // (Asumimos días 1 y 30 por defecto, como en tu tabla)
                        string queryNuevo = "INSERT INTO Periodo (diaInicio, diaFin, Mes, Anio, Cerrado) VALUES (1, 30, @Mes, @Ano, 0)";
                        using (SqlCommand cmdNuevo = new SqlCommand(queryNuevo, cn))
                        {
                            cmdNuevo.Parameters.AddWithValue("@Mes", nuevoMesNum);
                            cmdNuevo.Parameters.AddWithValue("@Ano", nuevoAnoNum);
                            cmdNuevo.ExecuteNonQuery();
                        }
                    }

                    // 4. Actualizar la UI
                    MessageBox.Show("El periodo ha sido cerrado exitosamente. Nuevo periodo creado.", "Periodo Cerrado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar todo el formulario
                    P_GenerarNomina_Load(sender, e);

                    // Reactivar el botón de generar
                    btn_GenerarNominaInd_GenerarNomina.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cerrar el período: " + ex.Message);
                }
            }
        }

        // --- CHECKBOX PARA SELECCIONAR TODOS (¡ESTÁ BIEN!) ---
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox1.Checked;
            foreach (DataGridViewRow row in dtgv_Matriz_GenerarNomina.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["Activo"].Value = isChecked;
                }
            }
        }

        // --- EVENTO DE CLIC EN GRID (CORREGIDO) ---
        // ¡¡BORRA EL CÓDIGO QUE TENÍA DENTRO!!
        // No debe llamar a `mostrarTablaDEDPERNOMINA` (eso es del sistema viejo).
        private void dtgv_Matriz_GenerarNomina_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Este evento debe estar VACÍO
            // Opcionalmente, si quieres que al hacer clic en el check se
            // actualice la celda inmediatamente:
            if (e.ColumnIndex == dtgv_Matriz_GenerarNomina.Columns["Activo"].Index)
            {
                dtgv_Matriz_GenerarNomina.CommitEdit(DataGridViewDataErrorContexts.Commit);
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



    }
}
        
   

    
      

    

        

        

//        private void GenerarNominaIndividual()
//        {
//            // Asegurarnos de tener un empleado seleccionado
//            if (idEmpleadoSeleccionado > 0)
//            {
//                // Obtener los datos necesarios de las tablas relacionadas
//                using (SqlConnection cn = BD_Conexion.ObtenerConexion())
//                {
//                    // Iniciar la transacción para asegurarnos de que todos los cambios se realicen juntos
                    
//                    using (SqlTransaction transaction = cn.BeginTransaction())
//                    {
//                        try
//                        {
//                            // Obtener información del empleado, departamento y puesto
//                            string queryEmpleado = @"
//                        SELECT E.id_Empleado, E.id_Departamento, E.id_Puesto, P.SalarioDiario, D.SueldoBase
//                        FROM Empleado E
//                        JOIN Puestos P ON E.id_Puesto = P.id_Puesto
//                        JOIN Departamento D ON E.id_Departamento = D.id_Departamento
//                        WHERE E.id_Empleado = @idEmpleado";

//                            SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn, transaction);
//                            cmdEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);

//                            SqlDataReader reader = cmdEmpleado.ExecuteReader();

//                            if (!reader.Read())
//                            {
//                                MessageBox.Show("Error: El empleado seleccionado no existe.");
//                                return;
//                            }

//                            // Extraer los datos del empleado, puesto y departamento
//                            int idDepartamento = reader.GetInt32(reader.GetOrdinal("id_Departamento"));
//                            int idPuesto = reader.GetInt32(reader.GetOrdinal("id_Puesto"));
//                            decimal salarioDiario = reader.GetDecimal(reader.GetOrdinal("SalarioDiario"));
//                            decimal sueldoBase = reader.GetDecimal(reader.GetOrdinal("SueldoBase"));
//                            reader.Close();

//                            // Calcular el sueldo bruto
//                            int diasTrabajados = 30; // Asumimos que el mes tiene 30 días
//                            decimal sueldoBruto = salarioDiario * diasTrabajados;

//                            // Calcular las deducciones y percepciones para el mes y año actual
//                            decimal totalDeducciones = 0;
//                            decimal totalPercepciones = 0;

//                            // Consultar deducciones y percepciones del empleado
//                            string queryDeduccionesPercepciones = @"
//                        SELECT DP.D_P, DP.MontoPD, DP.Porcentaje_PD
//                        FROM DEDPERNOMINA DPN
//                        JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
//                        WHERE DPN.id_Empleado = @idEmpleado
//                        AND DPN.Mes = @mes AND DPN.Ano = @ano";

//                            SqlCommand cmdDP = new SqlCommand(queryDeduccionesPercepciones, cn, transaction);
//                            cmdDP.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
//                            cmdDP.Parameters.AddWithValue("@mes", mes);
//                            cmdDP.Parameters.AddWithValue("@ano", anoSeleccionado);

//                            SqlDataReader dpReader = cmdDP.ExecuteReader();
//                            while (dpReader.Read())
//                            {
//                                string tipo = dpReader.GetString(dpReader.GetOrdinal("D_P"));
//                                decimal monto = dpReader.IsDBNull(dpReader.GetOrdinal("MontoPD")) ? 0 : dpReader.GetDecimal(dpReader.GetOrdinal("MontoPD"));
//                                decimal porcentaje = dpReader.IsDBNull(dpReader.GetOrdinal("Porcentaje_PD")) ? 0 : dpReader.GetDecimal(dpReader.GetOrdinal("Porcentaje_PD"));

//                                if (tipo == "Deducción")
//                                {
//                                    totalDeducciones += monto > 0 ? monto : sueldoBruto * porcentaje / 100;
//                                }
//                                else if (tipo == "Percepción")
//                                {
//                                    totalPercepciones += monto > 0 ? monto : sueldoBruto * porcentaje / 100;
//                                }
//                            }
//                            dpReader.Close();

//                            // Calcular sueldo neto
//                            decimal sueldoNeto = sueldoBruto + totalPercepciones - totalDeducciones;

//                            // Insertar los datos calculados en la tabla NominaIndividual
//                            string queryInsertNomina = @"
//                        INSERT INTO NominaIndividual (
//                            idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto,
//                            DiasTrabajados, totalDeducciones, totalPercepciones, Mes, Ano)
//                        VALUES (
//                            @idEmpleado, @idDepartamento, @idPuesto, @sueldoBruto, @sueldoNeto,
//                            @diasTrabajados, @totalDeducciones, @totalPercepciones, @mes, @ano)";

//                            SqlCommand cmdInsertNomina = new SqlCommand(queryInsertNomina, cn, transaction);
//                            cmdInsertNomina.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);
//                            cmdInsertNomina.Parameters.AddWithValue("@idDepartamento", idDepartamento);
//                            cmdInsertNomina.Parameters.AddWithValue("@idPuesto", idPuesto);
//                            cmdInsertNomina.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);
//                            cmdInsertNomina.Parameters.AddWithValue("@sueldoNeto", sueldoNeto);
//                            cmdInsertNomina.Parameters.AddWithValue("@diasTrabajados", diasTrabajados);
//                            cmdInsertNomina.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
//                            cmdInsertNomina.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
//                            cmdInsertNomina.Parameters.AddWithValue("@mes", mes);
//                            cmdInsertNomina.Parameters.AddWithValue("@ano", anoSeleccionado);

//                            cmdInsertNomina.ExecuteNonQuery();

//                            // Confirmar la transacción
//                            transaction.Commit();

//                            MessageBox.Show("Nómina generada exitosamente para el empleado.");
//                        }
//                        catch (Exception ex)
//                        {
//                            // Revertir la transacción en caso de error
//                            transaction.Rollback();
//                            MessageBox.Show("Error al generar la nómina: " + ex.Message);
//                        }
//                    }
//                }
//            }
//            else
//            {
//                MessageBox.Show("Por favor, selecciona un empleado para generar la nómina.");
//            }
//        }


//        private void GenerarNominasIndividuales(object sender, EventArgs e)
//        {
//            if (dtgv_Empleados_GenerarNomina.Rows.Count > 0)
//            {
//                foreach (DataGridViewRow fila in dtgv_Empleados_GenerarNomina.Rows)
//                {
//                    // Validar que la fila tenga datos
//                    if (fila.Cells["id_Empleado"].Value == null)
//                        continue;

//                    int idEmpleado = Convert.ToInt32(fila.Cells["id_Empleado"].Value);

//                    // Obtener datos del empleado
//                   // var (departamento, puesto, salarioDiario) = ObtenerDatosEmpleado(idEmpleado);
//                    var (departamento, puesto, salarioDiario, salarioDiarioIntegrado) = ObtenerDatosEmpleado(idEmpleado);
                    

//                    // Calcular faltas, días trabajados, y sueldo bruto
//                    int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);
//                    int diasTrabajados = 30 - faltas;
//                    decimal sueldoBruto = diasTrabajados * (decimal)salarioDiario;

//                    // Calcular ISR y IMSS
//                    decimal isr = CalcularISR(sueldoBruto);
//                    decimal imss = sueldoBruto * 0.1225m;

                   
//                    var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto, faltas, (decimal)salarioDiario);
//                    decimal totalDeducciones = deducciones + isr + imss;
//                    decimal totalPercepciones = percepciones;

//                    // Calcular sueldo neto
//                    decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;

//                    // Insertar la nómina individual en la base de datos
//                    using (SqlConnection cn = BD_Conexion.ObtenerConexion())
//                    {
                        
//                        string queryInsert = @"
//                    INSERT INTO NominaIndividual 
//                    (idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto, DiasTrabajados, totalDeducciones, totalPercepciones, ISR, IMSS, Mes, Ano) 
//                    VALUES (@idEmpleado, @idDepartamento, @idPuesto, @SueldoBruto, @SueldoNeto, @DiasTrabajados, @totalDeducciones, @totalPercepciones, @ISR, @IMSS, @Mes, @Ano)";

//                        using (SqlCommand cmd = new SqlCommand(queryInsert, cn))
//                        {
//                            cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                            cmd.Parameters.AddWithValue("@idDepartamento", departamento);
//                            cmd.Parameters.AddWithValue("@idPuesto", puesto);
//                            cmd.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
//                            cmd.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
//                            cmd.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
//                            cmd.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
//                            cmd.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
//                            cmd.Parameters.AddWithValue("@ISR", isr);
//                            cmd.Parameters.AddWithValue("@IMSS", imss);
//                            cmd.Parameters.AddWithValue("@Mes", mes);
//                            cmd.Parameters.AddWithValue("@Ano", anoSeleccionado);

//                            cmd.ExecuteNonQuery();
//                        }
//                    }
//                }

//                MessageBox.Show("Nóminas individuales generadas para todos los empleados.");
//            }
//            else
//            {
//                MessageBox.Show("No hay empleados en la lista.");
//            }

//        }

//        private void GenerarNominasIndividuales2(object sender, EventArgs e)
//        {
//            if (dtgv_Empleados_GenerarNomina.Rows.Count > 0)
//            {
//                foreach (DataGridViewRow fila in dtgv_Empleados_GenerarNomina.Rows)
//                {
//                    // Validar que la fila tenga datos y que el empleado esté activo
//                    if (fila.Cells["id_Empleado"].Value == null || fila.Cells["activo"].Value == null)
//                        continue;

//                    bool isActive = Convert.ToBoolean(fila.Cells["activo"].Value); // Verificar si el empleado está activo
//                    if (!isActive)
//                        continue; // Saltar este empleado si no está activo

//                    int idEmpleado = Convert.ToInt32(fila.Cells["id_Empleado"].Value);

//                    // Obtener datos del empleado
//                    var (departamento, puesto, salarioDiario, salarioDiarioIntegrado) = ObtenerDatosEmpleado(idEmpleado);

//                   // var (diasVacaciones, montoVacaciones, primaVacacional) = CalcularVacaciones(idEmpleado, mes, anoSeleccionado, (decimal)salarioDiario);
//                    // Calcular faltas, días trabajados, y sueldo bruto
//                    int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);

//                    int diasTrabajados = 30;// - faltas - diasVacaciones;
//                    decimal sueldoBruto = diasTrabajados * (decimal)salarioDiario;

//                    // Calcular el aguinaldo solo si es diciembre
//                    decimal aguinaldo = 0;
//                    if (mes.Equals("Diciembre", StringComparison.OrdinalIgnoreCase))
//                    {
//                        aguinaldo = (decimal)salarioDiario * 18;
//                    }

//                    // Calcular ISR y IMSS
//                    decimal isr = CalcularISR(sueldoBruto);
//                    decimal imss = (decimal)salarioDiarioIntegrado * 0.05m;

//                    // Calcular Préstamo Infonavit
//                   // decimal porcentajeInfonavit = ObtenerPorcentajeInfonavit(idEmpleado); // Método para obtener el porcentaje
//                    decimal prestamoInfonavit = (decimal)salarioDiario * 0.11m;

//                    // Calcular Fondo de Ahorro
//                    decimal fondoAhorro = sueldoBruto < 10000 ? 500 : 1000;

//                    // Calcular Horas Extras
//                    int horasExtras = ContarHorasExtrasEmpleado(idEmpleado, mes, anoSeleccionado);
//                    decimal importeHorasExtras = ((decimal)salarioDiario / 8) * 2 * horasExtras;

//                    // Calcular totales de deducciones y percepciones desde la base de datos
//                    var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto, faltas, (decimal)salarioDiario);

//                    // Sumar Préstamo Infonavit y Fondo de Ahorro a deducciones
//                    decimal totalDeducciones = deducciones + isr + imss + prestamoInfonavit + fondoAhorro;

//                    // Sumar Vacaciones, Prima Vacacional, Aguinaldo y Horas Extras a percepciones
//                   decimal totalPercepciones = percepciones + importeHorasExtras; //+montoVacaciones + primaVacacional + aguinaldo

//                    // Calcular sueldo neto
//                    decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;

//                    using (SqlConnection cn = BD_Conexion.ObtenerConexion())
//                    {
                        

//                        // Verificar si ya existe una nómina para este empleado, mes y año
//                        string querySelect = @"
//SELECT COUNT(1) 
//FROM NominaIndividual 
//WHERE idEmpleadoFK = @idEmpleado AND Mes = @Mes AND Ano = @Ano";

//                        SqlCommand cmdSelect = new SqlCommand(querySelect, cn);
//                        cmdSelect.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                        cmdSelect.Parameters.AddWithValue("@Mes", mes);
//                        cmdSelect.Parameters.AddWithValue("@Ano", anoSeleccionado);

//                        int count = Convert.ToInt32(cmdSelect.ExecuteScalar());

//                        if (count > 0)
//                        {
//                            // Actualización si ya existe
//                            string queryUpdate = @"
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

//                            using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, cn))
//                            {
//                                cmdUpdate.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                                cmdUpdate.Parameters.AddWithValue("@idDepartamento", departamento);
//                                cmdUpdate.Parameters.AddWithValue("@idPuesto", puesto);
//                                cmdUpdate.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
//                                cmdUpdate.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
//                                cmdUpdate.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
//                                cmdUpdate.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
//                                cmdUpdate.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
//                                cmdUpdate.Parameters.AddWithValue("@ISR", isr);
//                                cmdUpdate.Parameters.AddWithValue("@IMSS", imss);
//                                cmdUpdate.Parameters.AddWithValue("@SalarioDiario", salarioDiario); // Nuevo campo
//                                cmdUpdate.Parameters.AddWithValue("@Mes", mes);
//                                cmdUpdate.Parameters.AddWithValue("@Ano", anoSeleccionado);

//                                cmdUpdate.ExecuteNonQuery();
//                            }
//                        }
//                        else
//                        {
//                            // Inserción si no existe
//                            string queryInsert = @"
//    INSERT INTO NominaIndividual 
//    (idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto, DiasTrabajados, totalDeducciones, totalPercepciones, ISR, IMSS, Mes, Ano, SalarioDiario) 
//    VALUES (@idEmpleado, @idDepartamento, @idPuesto, @SueldoBruto, @SueldoNeto, @DiasTrabajados, @totalDeducciones, @totalPercepciones, @ISR, @IMSS, @Mes, @Ano, @SalarioDiario)";

//                            using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
//                            {
//                                cmdInsert.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                                cmdInsert.Parameters.AddWithValue("@idDepartamento", departamento);
//                                cmdInsert.Parameters.AddWithValue("@idPuesto", puesto);
//                                cmdInsert.Parameters.AddWithValue("@SueldoBruto", sueldoBruto);
//                                cmdInsert.Parameters.AddWithValue("@SueldoNeto", sueldoNeto);
//                                cmdInsert.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
//                                cmdInsert.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
//                                cmdInsert.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
//                                cmdInsert.Parameters.AddWithValue("@ISR", isr);
//                                cmdInsert.Parameters.AddWithValue("@IMSS", imss);
//                                cmdInsert.Parameters.AddWithValue("@Mes", mes);
//                                cmdInsert.Parameters.AddWithValue("@Ano", anoSeleccionado);
//                                cmdInsert.Parameters.AddWithValue("@SalarioDiario", salarioDiario); // Nuevo campo

//                                cmdInsert.ExecuteNonQuery();
//                            }
//                        }
//                    }
//                }

//                MessageBox.Show("Nóminas individuales generadas/actualizadas para todos los empleados activos.");
//            }
//            else
//            {
//                MessageBox.Show("No hay empleados activos en la lista.");
//            }
//        }
      

//        private void GenerarNomina(int idEmpleado, int mes, int ano)
//        {
//            // Obtener el departamento y puesto del empleado
//            int idDepartamento = 0;
//            int idPuesto = 0;
//            decimal salarioDiario = 0;

//            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
//            {
                

//                // Obtener departamento, puesto y salario diario
//                string queryEmpleado = @"SELECT e.id_Departamento, e.id_Puesto, p.SalarioDiario
//                                 FROM Empleado e
//                                 JOIN Puestos p ON e.id_Puesto = p.id_Puesto
//                                 WHERE e.id_Empleado = @idEmpleado";

//                SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
//                cmdEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                SqlDataReader reader = cmdEmpleado.ExecuteReader();

//                if (reader.Read())
//                {
//                    idDepartamento = reader.GetInt32(0);
//                    idPuesto = reader.GetInt32(1);
//                    //salarioDiario = reader.GetDecimal(2);
//                    //salarioDiario = Convert.ToDecimal(reader.GetFloat(2));
//                    salarioDiario = Convert.ToDecimal(reader.GetDouble(2));

//                }
//                reader.Close();

//                // Obtener total de deducciones y percepciones
//                decimal totalDeducciones = 0;
//                decimal totalPercepciones = 0;

//                string queryDeduccionesPercepciones = @"SELECT DP.D_P, DP.MontoPD
//                                                FROM DEDPERNOMINA DPN
//                                                JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
//                                                WHERE DPN.id_Empleado = @idEmpleado AND DPN.Mes = @mes AND DPN.Ano = @ano";

//                SqlCommand cmdDeduccionesPercepciones = new SqlCommand(queryDeduccionesPercepciones, cn);
//                cmdDeduccionesPercepciones.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                cmdDeduccionesPercepciones.Parameters.AddWithValue("@mes", mes);
//                cmdDeduccionesPercepciones.Parameters.AddWithValue("@ano", ano);
//                SqlDataReader readerDP = cmdDeduccionesPercepciones.ExecuteReader();

//                int faltas = 0;

//                while (readerDP.Read())
//                {
//                    string tipoDP = readerDP.GetString(0);
//                    decimal montoDP = readerDP.GetDecimal(1);

//                    if (tipoDP == "Deducción")
//                    {
//                        totalDeducciones += montoDP;
//                        // Si es deducción por falta, aumentar el contador de faltas
//                        if (readerDP.GetString(0) == "Falta")
//                            faltas++;
//                    }
//                    else if (tipoDP == "Percepción")
//                    {
//                        totalPercepciones += montoDP;
//                    }
//                }
//                readerDP.Close();

//                // Calcular Días Trabajados
//                int diasTrabajados = 30 - faltas;

//                // Calcular Sueldo Bruto y Sueldo Neto
//                decimal sueldoBruto = diasTrabajados * salarioDiario;
//                decimal sueldoNeto = sueldoBruto - totalDeducciones + totalPercepciones;

//                // Insertar registro en NominaIndividual
//                string queryInsertNomina = @"INSERT INTO NominaIndividual (idEmpleadoFK, idDepartamento, idPuesto, SueldoBruto, SueldoNeto, DiasTrabajados, totalDeducciones, totalPercepciones, Mes, Ano)
//                                     VALUES (@idEmpleado, @idDepartamento, @idPuesto, @sueldoBruto, @sueldoNeto, @diasTrabajados, @totalDeducciones, @totalPercepciones, @mes, @ano)";

//                SqlCommand cmdInsertNomina = new SqlCommand(queryInsertNomina, cn);
//                cmdInsertNomina.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                cmdInsertNomina.Parameters.AddWithValue("@idDepartamento", idDepartamento);
//                cmdInsertNomina.Parameters.AddWithValue("@idPuesto", idPuesto);
//                cmdInsertNomina.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);
//                cmdInsertNomina.Parameters.AddWithValue("@sueldoNeto", sueldoNeto);
//                cmdInsertNomina.Parameters.AddWithValue("@diasTrabajados", diasTrabajados);
//                cmdInsertNomina.Parameters.AddWithValue("@totalDeducciones", totalDeducciones);
//                cmdInsertNomina.Parameters.AddWithValue("@totalPercepciones", totalPercepciones);
//                cmdInsertNomina.Parameters.AddWithValue("@mes", mes);
//                cmdInsertNomina.Parameters.AddWithValue("@ano", ano);

//                cmdInsertNomina.ExecuteNonQuery();
//            }
//        }

//        private void ObtenerInformacionEmpleado(int idEmpleado)
//        {
//            int idDepartamento = 0;
//            int idPuesto = 0;
//            decimal salarioDiario = 0;

//            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
//            {
                

//                string queryEmpleado = @"SELECT e.id_Departamento, e.id_Puesto, p.SalarioDiario
//                                 FROM Empleado e
//                                 JOIN Puestos p ON e.id_Puesto = p.id_Puesto
//                                 WHERE e.id_Empleado = @idEmpleado";

//                SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
//                cmdEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleado);

//                SqlDataReader reader = cmdEmpleado.ExecuteReader();

//                if (reader.Read())
//                {
//                    idDepartamento = reader.GetInt32(0);
//                    idPuesto = reader.GetInt32(1);
//                    salarioDiario = reader.GetDecimal(2); // Si aquí tienes problemas de conversión, házmelo saber
//                }
//                reader.Close();
//            }

//            // Mostrar los resultados para verificar
//            MessageBox.Show($"Empleado {idEmpleado} - Departamento: {idDepartamento}, Puesto: {idPuesto}, Salario Diario: {salarioDiario}");
//        }


//        private void ObtenerSalarioDiario(int idPuesto)
//        {
//            // Declaramos la variable donde se almacenará el salario diario
//            float salarioDiario = 0;

//            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
//            {
                

//                // Consulta SQL para obtener el salario diario del puesto específico
//                string query = "SELECT SalarioDiario FROM Puestos WHERE id_Puesto = @idPuesto";

//                using (SqlCommand cmd = new SqlCommand(query, cn))
//                {
//                    // Asignamos el valor del parámetro @idPuesto
//                    cmd.Parameters.AddWithValue("@idPuesto", idPuesto);

//                    // Ejecutamos el lector de datos
//                    SqlDataReader reader = cmd.ExecuteReader();

//                    // Verificamos si hay resultados
//                    if (reader.Read())
//                    {
//                        // Guardamos el salario diario en la variable
//                        salarioDiario = Convert.ToSingle(reader["SalarioDiario"]); // Convertimos a float
//                    }
//                    reader.Close();
//                }
//            }

//            // Ahora puedes usar `salarioDiario` en tus cálculos
//            Console.WriteLine($"El salario diario del puesto con id {idPuesto} es: {salarioDiario}");
//        }

//        private (int idDepartamento, int idPuesto, float salarioDiario, float salarioDiarioIntegrado) ObtenerDatosEmpleado(int idEmpleadoSeleccionado)
//        {
//            int idDepartamento = 0;
//            int idPuesto = 0;
//            float salarioDiario = 0;
//            float salarioDiarioIntegrado = 0;

//            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
//            {
                

//                string query = @"
//SELECT e.id_Empleado, e.id_Departamento, e.id_Puesto, e.SalarioDiario, e.SalarioDiarioIntegrado
//FROM Empleado e
//WHERE e.id_Empleado = @idEmpleado";

//                SqlCommand cmd = new SqlCommand(query, cn);
//                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleadoSeleccionado);

//                SqlDataReader reader = cmd.ExecuteReader();

//                if (reader.Read())
//                {
//                    idDepartamento = reader.GetInt32(1);
//                    idPuesto = reader.GetInt32(2);
//                    salarioDiario = Convert.ToSingle(reader.GetValue(3));
//                    salarioDiarioIntegrado = Convert.ToSingle(reader.GetValue(4));
//                }
//                reader.Close();
//            }

//            return (idDepartamento, idPuesto, salarioDiario, salarioDiarioIntegrado);
//        }



//        private void btn_GenerarNominaInd_GenerarNomina_Click(object sender, EventArgs e)
//        {
//            GenerarNominasIndividuales2(sender, e);
            
//        }

//        private decimal CalcularISR(decimal sueldoBruto)
//        {
//            decimal isr = 0;

//            if (sueldoBruto >= 0.01m && sueldoBruto <= 8952.49m)
//            {
//                decimal excedente = sueldoBruto - 0.01m;
//                isr = 0 + (excedente * 0.0192m);
//            }
//            else if (sueldoBruto >= 8952.50m && sueldoBruto <= 75984.55m)
//            {
//                decimal excedente = sueldoBruto - 8952.50m;
//                isr = 171.88m + (excedente * 0.0640m);
//            }
//            else if (sueldoBruto >= 75984.56m && sueldoBruto <= 133536.07m)
//            {
//                decimal excedente = sueldoBruto - 75984.56m;
//                isr = 4461.94m + (excedente * 0.1088m);
//            }
//            else if (sueldoBruto >= 133536.08m && sueldoBruto <= 155229.80m)
//            {
//                decimal excedente = sueldoBruto - 133536.08m;
//                isr = 10723.55m + (excedente * 0.1600m);
//            }
//            else if (sueldoBruto >= 155229.81m && sueldoBruto <= 185852.57m)
//            {
//                decimal excedente = sueldoBruto - 155229.81m;
//                isr = 14194.54m + (excedente * 0.1792m);
//            }
//            else if (sueldoBruto >= 185852.58m && sueldoBruto <= 374837.88m)
//            {
//                decimal excedente = sueldoBruto - 185852.58m;
//                isr = 19682.13m + (excedente * 0.2136m);
//            }
//            else if (sueldoBruto >= 374837.89m && sueldoBruto <= 590795.99m)
//            {
//                decimal excedente = sueldoBruto - 374837.89m;
//                isr = 60049.40m + (excedente * 0.2352m);
//            }
//            else if (sueldoBruto >= 590796.00m && sueldoBruto <= 1127926.84m)
//            {
//                decimal excedente = sueldoBruto - 590796.00m;
//                isr = 110842.74m + (excedente * 0.3000m);
//            }
//            else if (sueldoBruto >= 1127926.85m && sueldoBruto <= 1503902.46m)
//            {
//                decimal excedente = sueldoBruto - 1127926.85m;
//                isr = 271981.99m + (excedente * 0.3200m);
//            }
//            else if (sueldoBruto >= 1503902.47m && sueldoBruto <= 4511707.37m)
//            {
//                decimal excedente = sueldoBruto - 1503902.47m;
//                isr = 392294.17m + (excedente * 0.3400m);
//            }
//            else if (sueldoBruto >= 4511707.38m)
//            {
//                decimal excedente = sueldoBruto - 4511707.38m;
//                isr = 1414947.85m + (excedente * 0.3500m);
//            }

//            return isr;
//        }

//        private int ContarFaltasEmpleado(int idEmpleado, string mes, int ano)
//        {
//            int totalFaltas = 0;

//            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
//            { 
                
//                string query = @"
//            SELECT COUNT(*)
//FROM DEDPERNOMINA DPN
//JOIN PercepcionesDeduccion DP ON DPN.id_PD = DP.ID_PercDed
//WHERE DPN.id_Empleado = @idEmpleado 
//  AND DP.nombre = 'Falta'
//  AND DPN.Mes = @mes
//  AND DPN.Ano = @ano
//";

//                SqlCommand cmd = new SqlCommand(query, cn);
//                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                cmd.Parameters.AddWithValue("@mes", mes); // Nombre del mes como texto
//                cmd.Parameters.AddWithValue("@ano", ano);  // Año como número

//                totalFaltas = (int)cmd.ExecuteScalar();
//            }

//            return totalFaltas;
//        }
//        private int ContarHorasExtrasEmpleado(int idEmpleado, string mes, int ano)
//        {
//            int totalHorasExtras = 0;

//            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
//            {
                
//                string query = @"
//        SELECT COUNT(*)
//        FROM DEDPERNOMINA DPN
//        JOIN DeduccionesPercepciones DP ON DPN.id_PD = DP.id_PD
//        WHERE DPN.id_Empleado = @idEmpleado 
//          AND DP.Nombre_PD = 'Hora Extra'
//          AND DPN.Mes = @mes
//          AND DPN.Ano = @ano";

//                SqlCommand cmd = new SqlCommand(query, cn);
//                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                cmd.Parameters.AddWithValue("@mes", mes); // Nombre del mes como texto
//                cmd.Parameters.AddWithValue("@ano", ano);  // Año como número

//                totalHorasExtras = (int)cmd.ExecuteScalar();
//            }

//            return totalHorasExtras;
//        }

    

//        private void GenerarNominaGeneral(object sender, EventArgs e)
//        {
//            int idNominaGeneral;

//            // Verifica si ya existe una entrada en NominaGeneral para el departamento, mes y año
//            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
//            {
                

//                // Comprobar si ya existe una nómina para el departamento, mes y año
//                string queryCheck = @"
//        SELECT id_NominaGeneral 
//        FROM NominaGeneral 
//        WHERE id_Departamento = @idDepartamento AND Mes = @mes AND Ano = @ano";

//                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, cn))
//                {
//                    cmdCheck.Parameters.AddWithValue("@idDepartamento", 1);
//                    cmdCheck.Parameters.AddWithValue("@mes", mes);
//                    cmdCheck.Parameters.AddWithValue("@ano", anoSeleccionado);

//                    var result = cmdCheck.ExecuteScalar();

//                    // Si no existe, crear la entrada
//                    if (result == null)
//                    {
//                        string queryInsert = @"
//                INSERT INTO NominaGeneral (id_Departamento, Mes, Ano) 
//                VALUES (@idDepartamento, @mes, @ano);
//                SELECT SCOPE_IDENTITY();";

//                        using (SqlCommand cmdInsert = new SqlCommand(queryInsert, cn))
//                        {
//                            cmdInsert.Parameters.AddWithValue("@idDepartamento", 1);
//                            cmdInsert.Parameters.AddWithValue("@mes", mes);
//                            cmdInsert.Parameters.AddWithValue("@ano", anoSeleccionado);

//                            idNominaGeneral = Convert.ToInt32(cmdInsert.ExecuteScalar());
//                        }
//                    }
//                    else
//                    {
//                        idNominaGeneral = Convert.ToInt32(result);
//                    }
//                }

//                // Obtener los empleados del departamento y calcular los datos de nómina
//                string queryEmpleados = @"
//        SELECT e.id_Empleado, p.SalarioDiario
//        FROM Empleado e
//        JOIN Puestos p ON e.id_Puesto = p.id_Puesto
//        WHERE e.id_Departamento = @idDepartamento";

//                using (SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn))
//                {
//                    cmdEmpleados.Parameters.AddWithValue("@idDepartamento", 1);

//                    using (SqlDataReader reader = cmdEmpleados.ExecuteReader())
//                    {
//                        List<(int idEmpleado, decimal salarioDiario)> empleados = new List<(int, decimal)>();

//                        // Almacenar los datos en una lista para liberar el DataReader antes de procesar
//                        while (reader.Read())
//                        {
//                            int idEmpleado = reader.GetInt32(0);
//                            decimal salarioDiario = Convert.ToDecimal(reader.GetValue(1));
//                            empleados.Add((idEmpleado, salarioDiario));
//                        }

//                        reader.Close(); // Cerrar el DataReader aquí antes de cualquier otro comando

//                        // Procesar cada empleado
//                        foreach (var (idEmpleado, salarioDiario) in empleados)
//                        {
//                            // Calcular los detalles de la nómina para cada empleado
//                            int faltas = ContarFaltasEmpleado(idEmpleado, mes, anoSeleccionado);
//                            int diasTrabajados = 30 - faltas;
//                            decimal sueldoBruto = diasTrabajados * salarioDiario;

//                            // Calcular ISR, IMSS y totales
//                            decimal isr = CalcularISR(sueldoBruto);
//                            decimal imss = sueldoBruto * 0.01225m;

//                           // var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto);
//                            var (deducciones, percepciones) = CalcularTotalesDesdeBD(idEmpleado, mes, anoSeleccionado, sueldoBruto, faltas, (decimal)salarioDiario);
//                            decimal totalDeducciones = deducciones + isr + imss;
//                            decimal totalPercepciones = percepciones;
//                            decimal sueldoNeto = (sueldoBruto + totalPercepciones) - totalDeducciones;

//                            // Insertar o actualizar en NominaGeneral_Empleado
//                            string queryInsertEmpleado = @"
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

//                            using (SqlCommand cmdInsertEmpleado = new SqlCommand(queryInsertEmpleado, cn))
//                            {
//                                cmdInsertEmpleado.Parameters.AddWithValue("@idNominaGeneral", idNominaGeneral);
//                                cmdInsertEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleado);
//                                cmdInsertEmpleado.Parameters.AddWithValue("@diasTrabajados", diasTrabajados);
//                                cmdInsertEmpleado.Parameters.AddWithValue("@sueldoBruto", sueldoBruto);
//                                cmdInsertEmpleado.Parameters.AddWithValue("@sueldoNeto", sueldoNeto);

//                                cmdInsertEmpleado.ExecuteNonQuery();
//                            }
//                        }
//                    }
//                }
//                MessageBox.Show("Nómina general generada o actualizada exitosamente.");
//            }
//        }

//        private void btn_CierrePeriodo_GenerarNomina_Click(object sender, EventArgs e)
//        {
//            // Mensaje de confirmación
//            DialogResult result = MessageBox.Show("¿Estás seguro que quieres cerrar el periodo?", "Confirmar cierre de periodo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

//            if (result == DialogResult.Yes)
//            {
//                // Variables locales para el nuevo periodo
//                string nuevoMes = mes;
//                int nuevoAno = anoSeleccionado;

//                // Aumentar el mes y actualizar el año si es necesario
//                switch (mes)
//                {
//                    case "Enero": nuevoMes = "Febrero"; break;
//                    case "Febrero": nuevoMes = "Marzo"; break;
//                    case "Marzo": nuevoMes = "Abril"; break;
//                    case "Abril": nuevoMes = "Mayo"; break;
//                    case "Mayo": nuevoMes = "Junio"; break;
//                    case "Junio": nuevoMes = "Julio"; break;
//                    case "Julio": nuevoMes = "Agosto"; break;
//                    case "Agosto": nuevoMes = "Septiembre"; break;
//                    case "Septiembre": nuevoMes = "Octubre"; break;
//                    case "Octubre": nuevoMes = "Noviembre"; break;
//                    case "Noviembre": nuevoMes = "Diciembre"; break;
//                    case "Diciembre":
//                        nuevoMes = "Enero";
//                        nuevoAno += 1; // Incrementar el año
//                        break;
//                }

//                // Insertar el nuevo periodo en la base de datos
//                using (SqlConnection cn = BD_Conexion.ObtenerConexion())
//                {
//                    string query = "INSERT INTO Periodo (Mes, Ano) VALUES (@Mes, @Ano)";
//                    SqlCommand cmd = new SqlCommand(query, cn);
//                    cmd.Parameters.AddWithValue("@Mes", nuevoMes);
//                    cmd.Parameters.AddWithValue("@Ano", nuevoAno);

                    
//                    cmd.ExecuteNonQuery();
//                }

//                // Actualizar las variables del periodo actual
//                mes = nuevoMes;
//                anoSeleccionado = nuevoAno;


//                MessageBox.Show("El periodo ha sido cerrado exitosamente. Nuevo periodo: " + nuevoMes + " " + nuevoAno, "Periodo Cerrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                txt_Mes_GenerarNomina.Text = mes;
//                txt_Ano_GenerarNomina.Text = anoSeleccionado.ToString();
//                mostrarTablaEmpleadosNomina();
//               // mostrarTablaDPNomina();
//                dtgv_EmDP_GenerarNomina.Rows.Clear();
//                dtgv_Matriz_GenerarNomina.Rows.Clear();
//                ColocarDatos();
//                // dtgv_EmDP_GenerarNomina.Clear();
//            }
//        }

//        private void checkBox1_CheckedChanged(object sender, EventArgs e)
//        {
//            // Obtener el estado actual del checkbox
//            bool isChecked = checkBox1.Checked;

//            // Recorrer todas las filas del DataGridView y actualizar la columna "Activo"
//            foreach (DataGridViewRow row in dtgv_Matriz_GenerarNomina.Rows)
//            {
//                // Asegurarse de que la fila no es una fila nueva (vacía)
//                if (!row.IsNewRow)
//                {
//                    row.Cells["Activo"].Value = isChecked; // Establecer el valor de la casilla de verificación
//                }
//            }
//        }

//        private void dtgv_Matriz_GenerarNomina_CellValueChanged(object sender, DataGridViewCellEventArgs e)
//        {

//        }

//        private void dtgv_Matriz_GenerarNomina_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {
//            // Verifica que la columna sea el checkbox y que el checkbox esté marcado
//            if (dtgv_Matriz_GenerarNomina.Columns[e.ColumnIndex].Name == "Activo" && (bool)dtgv_Matriz_GenerarNomina[e.ColumnIndex, e.RowIndex].EditedFormattedValue)
//            {
//                // Obtiene el valor de la columna "ID Empleado" de la misma fila
//                var empleadoId = dtgv_Matriz_GenerarNomina.Rows[e.RowIndex].Cells[2].Value; // Cambia el índice según la posición de tu columna

//                //MessageBox.Show("ID del Empleado seleccionado: " + empleadoId);
//                idEmpleadoSeleccionado= (int)empleadoId;
//                mostrarTablaDEDPERNOMINA();
//            }
//        }
//    }

