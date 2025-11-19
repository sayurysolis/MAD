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

namespace NominaMAD
{
    public partial class P_RepGenNomina : Form
    {
        string Conexion = "Data Source=RAGE-PC\\SQLEXPRESS;Initial Catalog=DSB_topografia;Integrated Security=True";
        public P_RepGenNomina()
        {
            InitializeComponent();
        }

        private void btn_Imprimir_RepGenNomina_Click(object sender, EventArgs e)
        {
            int año = int.Parse(txt_Ano_RepGenNomina.Text);
            string plantillaHtml = Properties.Resources.ReporteGeneral_Kardex_.ToString();
            string empleadosHtml = "";

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                // Obtener todos los empleados con sus nombres completos y estatus
                string queryEmpleados = "SELECT ID_Empleado, CONCAT(Nombre, ' ', ApellidoPaterno, ' ', ApellidoMaterno) AS NombreCompleto, SalarioDiario, Activo FROM Empleado";
                SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn);
                List<(int idEmpleado, string nombreCompleto, decimal salarioDiario, bool activo)> empleados = new List<(int, string, decimal, bool)>();

                using (SqlDataReader readerEmpleados = cmdEmpleados.ExecuteReader())
                {
                    while (readerEmpleados.Read())
                    {
                        int idEmpleado = (int)readerEmpleados["ID_Empleado"];
                        string nombreCompleto = readerEmpleados["NombreCompleto"].ToString();
                        decimal salarioDiario = (decimal)readerEmpleados["SalarioDiario"];
                        bool activo = (bool)readerEmpleados["Activo"];
                        empleados.Add((idEmpleado, nombreCompleto, salarioDiario, activo));
                    }
                }

                // Procesar cada empleado
                foreach (var (idEmpleado, nombreCompleto, salarioDiario, activo) in empleados)
                {
                    decimal aguinaldo = salarioDiario * 18;

                    decimal totalSueldoBrutoAnual = 0;
                    decimal totalSueldoNetoAnual = aguinaldo; // Incluye el aguinaldo en el total neto anual
                    decimal totalPercepcionesAnual = 0;
                    decimal totalDeduccionesAnual = 0;

                    // Mostrar estatus del empleado
                    string estatus = activo ? "Activo" : "Inactivo";

                    string empHtml = $@"
            <div class='employee-header'>
                <p>ID Empleado: {idEmpleado} - Estatus: {estatus}</p>
                <p>Nombre: {nombreCompleto}</p>
            </div>
            <table class='table'>
                <tr>
                    <th>Mes</th>
                    <th>Departamento</th>
                    <th>Puesto</th>
                    <th>Sueldo Bruto</th>
                    <th>Neto</th>
                    <th>Total Percepciones</th>
                    <th>Total Deducciones</th>
                </tr>";

                    for (int mes = 1; mes <= 12; mes++)
                    {
                        string nombreMes = MesNombre(mes);
                        string queryNomina = @"
                SELECT d.NombreDepartamento, p.NombrePuesto, ni.SueldoBruto, ni.SueldoNeto, ni.totalPercepciones, ni.totalDeducciones
                FROM NominaIndividual ni
                JOIN Departamento d ON ni.idDepartamento = d.id_Departamento
                JOIN Puestos p ON ni.idPuesto = p.id_Puesto
                WHERE ni.idEmpleadoFK = @idEmpleado AND ni.Mes = @Mes AND ni.Ano = @Ano";

                        using (SqlCommand cmdNomina = new SqlCommand(queryNomina, cn))
                        {
                            cmdNomina.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                            cmdNomina.Parameters.AddWithValue("@Mes", nombreMes);
                            cmdNomina.Parameters.AddWithValue("@Ano", año);

                            using (SqlDataReader readerNomina = cmdNomina.ExecuteReader())
                            {
                                if (readerNomina.Read())
                                {
                                    string departamento = readerNomina["NombreDepartamento"].ToString();
                                    string puesto = readerNomina["NombrePuesto"].ToString();
                                    decimal sueldoBruto = (decimal)readerNomina["SueldoBruto"];
                                    decimal sueldoNeto = (decimal)readerNomina["SueldoNeto"];
                                    decimal totalPercepciones = (decimal)readerNomina["totalPercepciones"];
                                    decimal totalDeducciones = (decimal)readerNomina["totalDeducciones"];

                                    // Acumular los valores anuales
                                    totalSueldoBrutoAnual += sueldoBruto;
                                    totalSueldoNetoAnual += sueldoNeto;
                                    totalPercepcionesAnual += totalPercepciones;
                                    totalDeduccionesAnual += totalDeducciones;

                                    empHtml += $@"
                            <tr>
                                <td>{MesNombre(mes)}</td>
                                <td>{departamento}</td>
                                <td>{puesto}</td>
                                <td>{sueldoBruto:C}</td>
                                <td>{sueldoNeto:C}</td>
                                <td>{totalPercepciones:C}</td>
                                <td>{totalDeducciones:C}</td>
                            </tr>";
                                }
                                else
                                {
                                    empHtml += $@"
                            <tr>
                                <td>{MesNombre(mes)}</td>
                                <td>-</td>
                                <td>-</td>
                                <td>{0:C}</td>
                                <td>{0:C}</td>
                                <td>{0:C}</td>
                                <td>{0:C}</td>
                            </tr>";
                                }
                            }
                        }
                    }

                    // Agregar el aguinaldo en la columna de sueldo neto
                    empHtml += $@"
            <tr>
                <td>Aguinaldo</td>
                <td>-</td>
                <td>-</td>
                <td>-</td>
                <td>{aguinaldo:C}</td>
                <td>-</td>
                <td>-</td>
            </tr>";

                    // Agregar los totales anuales
                    empHtml += $@"
            <tr class='total-row'>
                <td>Total Anual</td>
                <td>-</td>
                <td>-</td>
                <td>{totalSueldoBrutoAnual:C}</td>
                <td>{totalSueldoNetoAnual:C}</td>
                <td>{totalPercepcionesAnual:C}</td>
                <td>{totalDeduccionesAnual:C}</td>
            </tr>
            </table>";

                    empleadosHtml += empHtml;
                }
            }

            plantillaHtml = plantillaHtml.Replace("@EMPLEADOS", empleadosHtml);
            plantillaHtml = plantillaHtml.Replace("@ANO_REPORTE", año.ToString());

            // Generar el PDF con el contenido
            using (SaveFileDialog guardar = new SaveFileDialog())
            {
                guardar.FileName = $"{año}_ReporteGeneralNomina.pdf";

                if (guardar.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        pdfDoc.Add(new Phrase(""));

                        using (StringReader sr = new StringReader(plantillaHtml))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        }

                        pdfDoc.Close();
                        stream.Close();
                    }

                    MessageBox.Show("Reporte general de nómina generado exitosamente.");
                    btn_Imprimir_RepGenNomina.Visible = false;
                    txt_Ano_RepGenNomina.Enabled = true;
                    btn_Buscar_RepGenNomina.Visible = true;
                    txt_Ano_RepGenNomina.Clear();


                }
            }
           
        }

        private void btn_Buscar_RepGenNomina_Click(object sender, EventArgs e)
        {
            // Verificar si el usuario ingresó un año válido
            if (!int.TryParse(txt_Ano_RepGenNomina.Text, out int ano))
            {
                MessageBox.Show("Por favor, ingrese un año válido.");
                return;
            }

            // Verificar si existe la nómina general para el año ingresado
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string query = "SELECT COUNT(*) FROM NominaIndividual WHERE Ano = @Ano";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Ano", ano);

                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Nómina general encontrada para el año " + ano + ".");
                    btn_Imprimir_RepGenNomina.Visible=true;
                    txt_Ano_RepGenNomina.Enabled = false;
                    btn_Buscar_RepGenNomina.Visible = false;
                }
                else
                {
                    MessageBox.Show("No se encontró nómina general para el año " + ano + ".");
                }
            }
        }

        private string MesNombre(int mes)
        {
            string[] nombresMes = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            return nombresMes[mes - 1];
        }

        private void P_RepGenNomina_Load(object sender, EventArgs e)
        {
            txt_Ano_RepGenNomina.MaxLength = 4;
            btn_Imprimir_RepGenNomina.Visible = false;
        }

        private void btn_Regresar_RepGenNomina_Click(object sender, EventArgs e)
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
