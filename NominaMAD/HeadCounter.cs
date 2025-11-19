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

namespace NominaMAD
{
    public partial class P_HeadCounter : Form
    {
        string Conexion = "Data Source=LUISMTZ\\SQLEXPRESS;Initial Catalog=Nomina;Integrated Security=True";
        public P_HeadCounter()
        {
            InitializeComponent();
        }


        int idPeriodoActual;
        string MesPeriodo;
        int AnoPeriodo;
        private int anoSeleccionado;
        private string mes;

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
        private void btn_Regresar_HC_Click(object sender, EventArgs e)
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

        private void btn_ReportesDepartamento_HC_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_ReporteDepartamentos.pdf";
            string paginahtml_texto = Properties.Resources.Reporte_Departamentos.ToString();

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                // 1. Obtener los datos de los departamentos y almacenarlos en una lista
                string queryDepartamentos = "SELECT id_Departamento, NombreDepartamento FROM Departamento";
                SqlCommand cmdDepartamentos = new SqlCommand(queryDepartamentos, cn);
                SqlDataReader readerDepartamentos = cmdDepartamentos.ExecuteReader();

                // Almacenar los departamentos en memoria
                var departamentos = new List<(string idDepartamento, string nombreDepartamento)>();

                while (readerDepartamentos.Read())
                {
                    departamentos.Add((
                        idDepartamento: readerDepartamentos["id_Departamento"].ToString(),
                        nombreDepartamento: readerDepartamentos["NombreDepartamento"].ToString()
                    ));
                }
                readerDepartamentos.Close(); // Cerrar el DataReader de departamentos aquí

                // 2. Generar el HTML para cada departamento en una sola tabla
                string departamentosHtml = "";

                foreach (var (idDepartamento, nombreDepartamento) in departamentos)
                {
                    // Consulta para obtener los empleados activos en el departamento actual
                    string queryEmpleados = @"
            SELECT e.id_Empleado, e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, 
                   p.NombrePuesto, e.FechaIngresoEmpresa, e.SalarioDiario
            FROM Empleado e
            JOIN Puestos p ON e.id_Puesto = p.id_Puesto
            WHERE e.id_Departamento = @idDepartamento AND e.activo = 1"; // Filtra solo empleados activos

                    SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn);
                    cmdEmpleados.Parameters.AddWithValue("@idDepartamento", idDepartamento);

                    SqlDataReader readerEmpleados = cmdEmpleados.ExecuteReader();

                    // Contador de empleados activos para cada departamento
                    int totalEmpleados = 0;
                    string empleadosHtml = "";

                    // Generar filas de empleados
                    while (readerEmpleados.Read())
                    {
                        totalEmpleados++;
                        string idEmpleado = readerEmpleados["id_Empleado"].ToString();
                        string nombreEmpleado = $"{readerEmpleados["NombreEmpleado"]} {readerEmpleados["ApelPaternoEmpleado"]} {readerEmpleados["ApelMaternoEmpleado"]}";
                        string puesto = readerEmpleados["NombrePuesto"].ToString();
                        string fechaIngreso = Convert.ToDateTime(readerEmpleados["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy");
                        decimal salarioDiario = Convert.ToDecimal(readerEmpleados["SalarioDiario"]);

                        empleadosHtml += $@"
                <tr>
                    <td>{idEmpleado}</td>
                    <td>{nombreEmpleado}</td>
                    <td>{puesto}</td>
                    <td>{fechaIngreso}</td>
                    <td>{salarioDiario:C}</td>
                </tr>";
                    }
                    readerEmpleados.Close(); // Cerrar el DataReader de empleados después de procesar cada departamento

                    // Solo agrega la sección de un departamento si tiene empleados activos
                    if (totalEmpleados > 0)
                    {
                        string departamentoHtml = $@"
                <tr class='department-header'>
                    <td colspan='5'>Departamento: {nombreDepartamento}</td>
                </tr>
                <tr>
                    <td><strong>ID Departamento:</strong> {idDepartamento}</td>
                    <td colspan='4'><strong>Empleados en Departamento:</strong> {totalEmpleados}</td>
                </tr>
                <tr class='employee-header'>
                    <th>ID Empleado</th>
                    <th>Nombre</th>
                    <th>Puesto</th>
                    <th>Fecha de Ingreso</th>
                    <th>Salario Diario</th>
                </tr>
                {empleadosHtml}";

                        departamentosHtml += departamentoHtml;
                    }
                }

                // Reemplazar la marca de lugar en la plantilla
                paginahtml_texto = paginahtml_texto.Replace("@MES_REPORTE", mes);
                paginahtml_texto = paginahtml_texto.Replace("@ANO_REPORTE", anoSeleccionado.ToString());
                paginahtml_texto = paginahtml_texto.Replace("@DEPARTAMENTOS", departamentosHtml);
            }

            // Generar el PDF con el contenido verificado
            if (guardar.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    using (StringReader sr = new StringReader(paginahtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

                MessageBox.Show("Reporte de departamentos generado exitosamente.");
            }
        }

        private void btn_ReportesPuesto_HC_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_ReportePuestos.pdf";
            string paginahtml_texto = Properties.Resources.Reporte_Puestos.ToString();

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                // 1. Obtener los datos de los puestos y almacenarlos en una lista
                string queryPuestos = "SELECT p.id_Puesto, p.NombrePuesto, p.DescripcionPuesto, d.NombreDepartamento " +
                                      "FROM Puestos p " +
                                      "JOIN Departamento d ON p.id_Departamento = d.id_Departamento";
                SqlCommand cmdPuestos = new SqlCommand(queryPuestos, cn);
                SqlDataReader readerPuestos = cmdPuestos.ExecuteReader();

                // Almacenar los puestos en memoria
                var puestos = new List<(string idPuesto, string nombrePuesto, string descripcionPuesto, string nombreDepartamento)>();

                while (readerPuestos.Read())
                {
                    puestos.Add((
                        idPuesto: readerPuestos["id_Puesto"].ToString(),
                        nombrePuesto: readerPuestos["NombrePuesto"].ToString(),
                        descripcionPuesto: readerPuestos["DescripcionPuesto"].ToString(),
                        nombreDepartamento: readerPuestos["NombreDepartamento"].ToString()
                    ));
                }
                readerPuestos.Close(); // Cerrar el DataReader de puestos aquí

                // 2. Generar el HTML para cada puesto en una sola tabla
                string puestosHtml = "";

                foreach (var (idPuesto, nombrePuesto, descripcionPuesto, nombreDepartamento) in puestos)
                {
                    // Consulta para obtener los empleados en el puesto actual y que estén activos
                    string queryEmpleados = @"
                SELECT e.id_Empleado, e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, 
                       e.FechaIngresoEmpresa, e.SalarioDiario
                FROM Empleado e
                WHERE e.id_Puesto = @idPuesto AND e.Activo = 1";

                    SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn);
                    cmdEmpleados.Parameters.AddWithValue("@idPuesto", idPuesto);

                    SqlDataReader readerEmpleados = cmdEmpleados.ExecuteReader();

                    // Contador de empleados para cada puesto
                    string empleadosHtml = "";
                    int totalEmpleados = 0;

                    // Generar filas de empleados
                    while (readerEmpleados.Read())
                    {
                        totalEmpleados++;
                        string idEmpleado = readerEmpleados["id_Empleado"].ToString();
                        string nombreEmpleado = $"{readerEmpleados["NombreEmpleado"]} {readerEmpleados["ApelPaternoEmpleado"]} {readerEmpleados["ApelMaternoEmpleado"]}";
                        string fechaIngreso = Convert.ToDateTime(readerEmpleados["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy");
                        decimal salarioDiario = Convert.ToDecimal(readerEmpleados["SalarioDiario"]);

                        empleadosHtml += $@"
                    <tr>
                        <td>{idEmpleado}</td>
                        <td>{nombreEmpleado}</td>
                        <td>{fechaIngreso}</td>
                        <td>{salarioDiario:C}</td>
                    </tr>";
                    }
                    readerEmpleados.Close(); // Cerrar el DataReader de empleados después de procesar cada puesto

                    // Encabezado para el puesto y sus empleados
                    string puestoHtml = $@"
                <tr class='position-header'>
                    <td colspan='4'>Puesto: {nombrePuesto}</td>
                </tr>
                <tr>
                    <td><strong>ID Puesto:</strong> {idPuesto}</td>
                    <td><strong>Descripción:</strong> {descripcionPuesto}</td>
                    <td colspan='2'><strong>Departamento:</strong> {nombreDepartamento}</td>
                </tr>
                <tr class='employee-header'>
                    <th>ID Empleado</th>
                    <th>Nombre</th>
                    <th>Fecha de Ingreso</th>
                    <th>Salario Diario</th>
                </tr>
                {empleadosHtml}";

                    puestosHtml += puestoHtml;
                }

                // Reemplazar los marcadores en la plantilla
                paginahtml_texto = paginahtml_texto.Replace("@MES_REPORTE", mes);
                paginahtml_texto = paginahtml_texto.Replace("@ANO_REPORTE", anoSeleccionado.ToString());
                paginahtml_texto = paginahtml_texto.Replace("@PUESTOS", puestosHtml);
            }

            // Generar el PDF con el contenido verificado
            if (guardar.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    using (StringReader sr = new StringReader(paginahtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

                MessageBox.Show("Reporte de puestos generado exitosamente.");
            }
        }

        private void btn_ReportesTurnos_HC_Click(object sender, EventArgs e)
        {

            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_ReporteTurnos.pdf";
            string paginahtml_texto = Properties.Resources.Reporte_Turnos.ToString();

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                // 1. Obtener los datos de los turnos y almacenarlos en una lista
                string queryTurnos = "SELECT id_Turno, NombreTurno, Descripcion FROM Turno";
                SqlCommand cmdTurnos = new SqlCommand(queryTurnos, cn);
                SqlDataReader readerTurnos = cmdTurnos.ExecuteReader();

                // Almacenar los turnos en memoria
                var turnos = new List<(string idTurno, string nombreTurno, string descripcionTurno)>();

                while (readerTurnos.Read())
                {
                    turnos.Add((
                        idTurno: readerTurnos["id_Turno"].ToString(),
                        nombreTurno: readerTurnos["NombreTurno"].ToString(),
                        descripcionTurno: readerTurnos["Descripcion"].ToString()
                    ));
                }
                readerTurnos.Close(); // Cerrar el DataReader de turnos aquí

                // 2. Generar el HTML para cada turno en una sola tabla
                string turnosHtml = "";

                foreach (var (idTurno, nombreTurno, descripcionTurno) in turnos)
                {
                    // Consulta para obtener los empleados en el turno actual y que estén activos
                    string queryEmpleados = @"
                SELECT e.id_Empleado, e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, 
                       e.FechaIngresoEmpresa, e.SalarioDiario
                FROM Empleado e
                WHERE e.id_Turno = @idTurno AND e.Activo = 1";

                    SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn);
                    cmdEmpleados.Parameters.AddWithValue("@idTurno", idTurno);

                    SqlDataReader readerEmpleados = cmdEmpleados.ExecuteReader();

                    // Contador de empleados para cada turno
                    string empleadosHtml = "";
                    int totalEmpleados = 0;

                    // Generar filas de empleados
                    while (readerEmpleados.Read())
                    {
                        totalEmpleados++;
                        string idEmpleado = readerEmpleados["id_Empleado"].ToString();
                        string nombreEmpleado = $"{readerEmpleados["NombreEmpleado"]} {readerEmpleados["ApelPaternoEmpleado"]} {readerEmpleados["ApelMaternoEmpleado"]}";
                        string fechaIngreso = Convert.ToDateTime(readerEmpleados["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy");
                        decimal salarioDiario = Convert.ToDecimal(readerEmpleados["SalarioDiario"]);

                        empleadosHtml += $@"
                    <tr>
                        <td>{idEmpleado}</td>
                        <td>{nombreEmpleado}</td>
                        <td>{fechaIngreso}</td>
                        <td>{salarioDiario:C}</td>
                    </tr>";
                    }
                    readerEmpleados.Close(); // Cerrar el DataReader de empleados después de procesar cada turno

                    // Encabezado para el turno y sus empleados
                    string turnoHtml = $@"
                <tr class='turno-header'>
                    <td colspan='4'>Turno: {nombreTurno}</td>
                </tr>
                <tr>
                    <td><strong>ID Turno:</strong> {idTurno}</td>
                    <td colspan='3'><strong>Descripción:</strong> {descripcionTurno}</td>
                </tr>
                <tr class='employee-header'>
                    <th>ID Empleado</th>
                    <th>Nombre</th>
                    <th>Fecha de Ingreso</th>
                    <th>Salario Diario</th>
                </tr>
                {empleadosHtml}";

                    turnosHtml += turnoHtml;
                }

                // Reemplazar los marcadores en la plantilla
                paginahtml_texto = paginahtml_texto.Replace("@MES_REPORTE",mes);
                paginahtml_texto = paginahtml_texto.Replace("@ANO_REPORTE", anoSeleccionado.ToString());
                paginahtml_texto = paginahtml_texto.Replace("@TURNOS", turnosHtml);
            }

            // Generar el PDF con el contenido verificado
            if (guardar.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    using (StringReader sr = new StringReader(paginahtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }

                MessageBox.Show("Reporte de turnos generado exitosamente.");
            }
        }

        private void P_HeadCounter_Load(object sender, EventArgs e)
        {
            ObtenerPeriodoActual();
            mes = MesPeriodo;
            anoSeleccionado = AnoPeriodo;
        }
    }
}
