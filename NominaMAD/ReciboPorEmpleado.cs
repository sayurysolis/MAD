using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class P_ReciboEmpleado : Form
    {
        string Conexion = "Data Source=DESKTOP-R0TMTLN\\SQLEXPRESS;Initial Catalog=Nomina;Integrated Security=True";
        public P_ReciboEmpleado()
       {
            InitializeComponent();
           // combo_Mes.Items.Add("Noviembre");
        }


        private void btn_Imprimir_RecPEmpleado_Click(object sender, EventArgs e)
        {
            imprimirr(sender, e);
    //        //SaveFileDialog guardar=new SaveFileDialog();
    //        //guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
    //        ////guardar.ShowDialog();

    //        // //string paginahtml_texto = "<table border=1><tr><td>HOLA MUNDO</td></tr></table>";
    //        //string paginahtml_texto = Properties.Resources.plantilla.ToString();
    //        //paginahtml_texto = paginahtml_texto.Replace("@NUMERO_EMPLEADO", );

    //        //if (guardar.ShowDialog() == DialogResult.OK)
    //        //{

    //        //    using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
    //        //    {
    //        //        Document pdfDoc = new Document(PageSize.A4, 25,25,25,25);

    //        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

    //        //        pdfDoc.Open();

    //        //        pdfDoc.Add(new Phrase(""));

    //        //        using (StringReader sr = new StringReader(paginahtml_texto))
    //        //        {

    //        //            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);   
    //        //        }
    //        //        pdfDoc.Close();

    //        //        stream.Close();
    //        //    }
    //        //}


    //        SaveFileDialog guardar = new SaveFileDialog();
    //        guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";

    //        // Cargar la plantilla HTML desde los recursos
    //        string paginahtml_texto = Properties.Resources.plantilla.ToString();

    //        // Obtener y reemplazar datos de la Empresa
    //        using (SqlConnection cn = new SqlConnection(Conexion))
    //        {
    //            cn.Open();

    //            // 1. Datos de la Empresa
    //            string queryEmpresa = "SELECT Nombre, RazonFiscal, DomicilioFiscal, Telefono, RegistroPatronal, RFC, FechaInOperaciones FROM Empresa";
    //            SqlCommand cmdEmpresa = new SqlCommand(queryEmpresa, cn);
    //            SqlDataReader readerEmpresa = cmdEmpresa.ExecuteReader();

    //            if (readerEmpresa.Read())
    //            {
    //                paginahtml_texto = paginahtml_texto.Replace("@MES_NOMINA", combo_Mes.Text);
    //                paginahtml_texto = paginahtml_texto.Replace("@ANO_NOMINA", txt_Ano_RecPEmpleado.Text);
    //                paginahtml_texto = paginahtml_texto.Replace("@NOMBRE_EMPRESA", readerEmpresa["Nombre"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@RAZON_SOCIAL", readerEmpresa["RazonFiscal"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@DOMICILIO_FISCAL", readerEmpresa["DomicilioFiscal"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@TELEFONO_EMPRESA", readerEmpresa["Telefono"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@REGISTRO_PATRONAL", readerEmpresa["RegistroPatronal"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@RFC_EMPRESA", readerEmpresa["RFC"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@FECHA_INICIO_OPERACIONES", Convert.ToDateTime(readerEmpresa["FechaInOperaciones"]).ToString("dd/MM/yyyy"));
    //            }
    //            readerEmpresa.Close();

    //            // 2. Datos del Empleado
    //            int numeroEmpleado = int.Parse(txt_NumEmpleado_RecPEmpleado.Text);
    //            string queryEmpleado = @"
    //                SELECT e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, e.RFC, e.Curp, e.NSS, e.FechaIngresoEmpresa, 
    //                       d.NombreDepartamento, p.NombrePuesto, p.SalarioDiario, e.Banco, e.NumeroCuenta
    //                FROM Empleado e
    //                JOIN Departamento d ON e.id_Departamento = d.id_Departamento
    //                JOIN Puestos p ON e.id_Puesto = p.id_Puesto
    //                WHERE e.id_Empleado = @idEmpleado";
    //            SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
    //            cmdEmpleado.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
    //            SqlDataReader readerEmpleado = cmdEmpleado.ExecuteReader();

    //            if (readerEmpleado.Read())
    //            {
    //                paginahtml_texto = paginahtml_texto.Replace("@NOMBRE_EMPLEADO", $"{readerEmpleado["NombreEmpleado"]} {readerEmpleado["ApelPaternoEmpleado"]} {readerEmpleado["ApelMaternoEmpleado"]}");
    //                paginahtml_texto = paginahtml_texto.Replace("@NUMERO_EMPLEADO", numeroEmpleado.ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@RFC_EMPLEADO", readerEmpleado["RFC"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@CURP_EMPLEADO", readerEmpleado["Curp"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@IMSS_EMPLEADO", readerEmpleado["NSS"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@FECHA_INGRESO", Convert.ToDateTime(readerEmpleado["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy"));
    //                paginahtml_texto = paginahtml_texto.Replace("@DEPARTAMENTO", readerEmpleado["NombreDepartamento"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@PUESTO", readerEmpleado["NombrePuesto"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@BANCO", readerEmpleado["Banco"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@NUMERO_CUENTA", readerEmpleado["NumeroCuenta"].ToString());
    //            }
    //            readerEmpleado.Close();

    //            // 3. Datos de la Nómina Individual y Deducciones/Percepciones
    //            //string mes = combo_Mes.Text;
    //            //int ano = int.Parse(txt_Ano_RecPEmpleado.Text);
    //            //string queryNomina = @"
    //            //    SELECT DiasTrabajados, SueldoBruto, SueldoNeto, totalDeducciones, totalPercepciones
    //            //    FROM NominaIndividual
    //            //    WHERE idEmpleadoFK = @idEmpleado AND Mes = @mes AND Ano = @ano";
    //            //SqlCommand cmdNomina = new SqlCommand(queryNomina, cn);
    //            //cmdNomina.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
    //            //cmdNomina.Parameters.AddWithValue("@mes", mes);
    //            //cmdNomina.Parameters.AddWithValue("@ano", ano);
    //            //SqlDataReader readerNomina = cmdNomina.ExecuteReader();

    //            //if (readerNomina.Read())
    //            //{
    //            //    paginahtml_texto = paginahtml_texto.Replace("@DIAS_TRABAJADOS", readerNomina["DiasTrabajados"].ToString());
    //            //    paginahtml_texto = paginahtml_texto.Replace("@TOTAL_PERCEPCIONES", readerNomina["totalPercepciones"].ToString());
    //            //    paginahtml_texto = paginahtml_texto.Replace("@TOTAL_DEDUCCIONES", readerNomina["totalDeducciones"].ToString());
    //            //    paginahtml_texto = paginahtml_texto.Replace("@TOTAL_NETO", readerNomina["SueldoNeto"].ToString());
    //            //}

    //            //readerNomina.Close();
    //            // 3. Datos de la Nómina Individual y Deducciones/Percepciones
    //            string mes = combo_Mes.Text;
    //            int ano = int.Parse(txt_Ano_RecPEmpleado.Text);
    //            decimal isr = 0;
    //            decimal imss = 0;

    //            string queryNomina = @"
    //SELECT DiasTrabajados, SueldoBruto, SueldoNeto, totalDeducciones, totalPercepciones, ISR, IMSS
    //FROM NominaIndividual
    //WHERE idEmpleadoFK = @idEmpleado AND Mes = @mes AND Ano = @ano";
    //            SqlCommand cmdNomina = new SqlCommand(queryNomina, cn);
    //            cmdNomina.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
    //            cmdNomina.Parameters.AddWithValue("@mes", mes);
    //            cmdNomina.Parameters.AddWithValue("@ano", ano);
    //            SqlDataReader readerNomina = cmdNomina.ExecuteReader();

    //            if (readerNomina.Read())
    //            {
    //                paginahtml_texto = paginahtml_texto.Replace("@DIAS_TRABAJADOS", readerNomina["DiasTrabajados"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@TOTAL_PERCEPCIONES", readerNomina["totalPercepciones"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@TOTAL_DEDUCCIONES", readerNomina["totalDeducciones"].ToString());
    //                paginahtml_texto = paginahtml_texto.Replace("@TOTAL_NETO", readerNomina["SueldoNeto"].ToString());

    //                // Obtener valores de ISR e IMSS
    //                isr = Convert.ToDecimal(readerNomina["ISR"]);
    //                imss = Convert.ToDecimal(readerNomina["IMSS"]);
    //            }
    //            readerNomina.Close();

    //            // 4. Obtener Deducciones del Empleado y agregar ISR e IMSS
    //            string deduccionesHtml = "";
    //            decimal totalDeducciones = 0;

    //            string queryDeducciones = @"
    //SELECT dp.id_PD, dp.Nombre_PD, COALESCE(dp.MontoPD, 0) AS Importe
    //FROM DEDPERNOMINA dpn
    //JOIN DeduccionesPercepciones dp ON dpn.id_PD = dp.id_PD
    //WHERE dpn.id_Empleado = @idEmpleado AND dpn.Mes = @mes AND dpn.Ano = @ano AND dp.D_P = 'Deducción'";

    //            SqlCommand cmdDeducciones = new SqlCommand(queryDeducciones, cn);
    //            cmdDeducciones.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
    //            cmdDeducciones.Parameters.AddWithValue("@mes", mes);
    //            cmdDeducciones.Parameters.AddWithValue("@ano", ano);
    //            SqlDataReader readerDeducciones = cmdDeducciones.ExecuteReader();

    //            while (readerDeducciones.Read())
    //            {
    //                string clave = readerDeducciones["id_PD"].ToString();
    //                string concepto = readerDeducciones["Nombre_PD"].ToString();
    //                decimal importe = Convert.ToDecimal(readerDeducciones["Importe"]);

    //                deduccionesHtml += $"<tr><td>{clave}</td><td>{concepto}</td><td>{importe:C}</td></tr>";
    //                totalDeducciones += importe;
    //            }
    //            readerDeducciones.Close();

    //            // Agregar ISR e IMSS a la tabla de deducciones
    //            deduccionesHtml += $"<tr><td>ISR</td><td>ISR</td><td>{isr:C}</td></tr>";
    //            deduccionesHtml += $"<tr><td>IMSS</td><td>IMSS</td><td>{imss:C}</td></tr>";

    //            // Sumar ISR e IMSS al total de deducciones
    //            totalDeducciones += isr + imss;

    //            // Reemplazar las deducciones en el HTML
    //            paginahtml_texto = paginahtml_texto.Replace("@FILAS_DEDUCCIONES", deduccionesHtml);
    //            paginahtml_texto = paginahtml_texto.Replace("@TOTAL_DEDUCCIONES", totalDeducciones.ToString("C"));


    //            ///////////////////
    //            ///
    //            // 2. Reemplazar deducciones y percepciones en el PDF
    //            // int numeroEmpleado = int.Parse(txt_NumEmpleado_RecPEmpleado.Text);
    //            //string mes = combo_Mes.Text;
    //            //int ano = int.Parse(txt_Ano_RecPEmpleado.Text);

    //            // Reemplazo dinámico de filas de percepciones
    //            // Código para obtener y reemplazar las percepciones



    //            //////
    //            // 4. Obtener Percepciones del Empleado
    //            string percepcionesHtml = "";
    //            decimal totalPercepciones = 0;

    //            string queryPercepciones = @"
    //SELECT dp.id_PD, dp.Nombre_PD, COALESCE(dp.MontoPD, 0) AS MontoPD, COALESCE(dp.Porcentaje_PD, 0) AS Porcentaje_PD
    //FROM DEDPERNOMINA dpn
    //JOIN DeduccionesPercepciones dp ON dpn.id_PD = dp.id_PD
    //WHERE dpn.id_Empleado = @idEmpleado AND dpn.Mes = @mes AND dpn.Ano = @ano AND dp.D_P = 'Percepción'";

    //            SqlCommand cmdPercepciones = new SqlCommand(queryPercepciones, cn);
    //            cmdPercepciones.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
    //            cmdPercepciones.Parameters.AddWithValue("@mes", mes);
    //            cmdPercepciones.Parameters.AddWithValue("@ano", ano);
    //            SqlDataReader readerPercepciones = cmdPercepciones.ExecuteReader();

    //            while (readerPercepciones.Read())
    //            {
    //                string clave = readerPercepciones["id_PD"].ToString();
    //                string concepto = readerPercepciones["Nombre_PD"].ToString();
    //                decimal monto = Convert.ToDecimal(readerPercepciones["MontoPD"]);
    //                decimal porcentaje = Convert.ToDecimal(readerPercepciones["Porcentaje_PD"]);

    //                // Determinar si se usa Monto o Porcentaje para el Importe
    //                string importe = monto > 0 ? $"${monto}" : $"{porcentaje}%";

    //                percepcionesHtml += $"<tr><td>{clave}</td><td>{concepto}</td><td>{importe}</td></tr>";
    //               // totalPercepciones += monto > 0 ? monto : (SueldoBruto * porcentaje / 100); // Asumiendo que el porcentaje se aplica sobre SueldoBruto
    //            }
    //            readerPercepciones.Close();

    //            // Reemplazar las percepciones en el HTML
    //            paginahtml_texto = paginahtml_texto.Replace("@FILAS_PERCEPCIONES", percepcionesHtml);
    //            paginahtml_texto = paginahtml_texto.Replace("@TOTAL_PERCEPCIONES", totalPercepciones.ToString("C"));


    //            //            string percepcionesHtml = "";
    //            //            decimal totalPercepciones = 0;

    //            //            string queryPercepciones = @"
    //            //SELECT dp.id_PD, dp.Nombre_PD, COALESCE(dp.MontoPD, 0) AS Importe
    //            //FROM DEDPERNOMINA dpn
    //            //JOIN DeduccionesPercepciones dp ON dpn.id_PD = dp.id_PD
    //            //WHERE dpn.id_Empleado = @idEmpleado AND dpn.Mes = @mes AND dpn.Ano = @ano AND dp.D_P = 'Percepción'";

    //            //            SqlCommand cmdPercepciones = new SqlCommand(queryPercepciones, cn);
    //            //            cmdPercepciones.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
    //            //            cmdPercepciones.Parameters.AddWithValue("@mes", mes);
    //            //            cmdPercepciones.Parameters.AddWithValue("@ano", ano);

    //            //            SqlDataReader readerPercepciones = cmdPercepciones.ExecuteReader();

    //            //            while (readerPercepciones.Read())
    //            //            {
    //            //                string clave = readerPercepciones["id_PD"].ToString();
    //            //                string concepto = readerPercepciones["Nombre_PD"].ToString();
    //            //                decimal importe = Convert.ToDecimal(readerPercepciones["Importe"]);

    //            //                percepcionesHtml += $"<tr><td>{clave}</td><td>{concepto}</td><td>{importe:C}</td></tr>";
    //            //                totalPercepciones += importe;
    //            //            }
    //            //            readerPercepciones.Close();
    //            //            paginahtml_texto = paginahtml_texto.Replace("@FILAS_PERCEPCIONES", percepcionesHtml);
    //            //            paginahtml_texto = paginahtml_texto.Replace("@TOTAL_PERCEPCIONES", totalPercepciones.ToString("C"));
    //            /////////////
    //            // Código para obtener y reemplazar las deducciones


    //            //            string deduccionesHtml = "";
    //            //            decimal totalDeducciones = 0;

    //            //            string queryDeducciones = @"
    //            //SELECT dp.id_PD, dp.Nombre_PD, COALESCE(dp.MontoPD, 0) AS Importe
    //            //FROM DEDPERNOMINA dpn
    //            //JOIN DeduccionesPercepciones dp ON dpn.id_PD = dp.id_PD
    //            //WHERE dpn.id_Empleado = @idEmpleado AND dpn.Mes = @mes AND dpn.Ano = @ano AND dp.D_P = 'Deducción'";

    //            //            SqlCommand cmdDeducciones = new SqlCommand(queryDeducciones, cn);
    //            //            cmdDeducciones.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
    //            //            cmdDeducciones.Parameters.AddWithValue("@mes", mes);
    //            //            cmdDeducciones.Parameters.AddWithValue("@ano", ano);

    //            //            SqlDataReader readerDeducciones = cmdDeducciones.ExecuteReader();

    //            //            while (readerDeducciones.Read())
    //            //            {
    //            //                string clave = readerDeducciones["id_PD"].ToString();
    //            //                string concepto = readerDeducciones["Nombre_PD"].ToString();
    //            //                decimal importe = Convert.ToDecimal(readerDeducciones["Importe"]);

    //            //                deduccionesHtml += $"<tr><td>{clave}</td><td>{concepto}</td><td>{importe:C}</td></tr>";
    //            //                totalDeducciones += importe;
    //            //            }
    //            //            readerDeducciones.Close();
    //            //            paginahtml_texto = paginahtml_texto.Replace("@FILAS_DEDUCCIONES", deduccionesHtml);
    //            //            paginahtml_texto = paginahtml_texto.Replace("@TOTAL_DEDUCCIONES", totalDeducciones.ToString("C"));


    //        }
    //        ////////
    //        ///




    //        //// Generar el PDF
    //        //if (guardar.ShowDialog() == DialogResult.OK)
    //        //{
    //        //    using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
    //        //    {
    //        //        Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
    //        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

    //        //        pdfDoc.Open();
    //        //        pdfDoc.Add(new Phrase(""));

    //        //        using (StringReader sr = new StringReader(paginahtml_texto))
    //        //        {
    //        //            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
    //        //        }

    //        //        pdfDoc.Close();
    //        //        stream.Close();
    //        //    }

    //        //    MessageBox.Show("Recibo de nómina generado exitosamente.");
    //        //}
    //        // Generar el PDF con los datos completados
    //        if (guardar.ShowDialog() == DialogResult.OK)
    //        {
    //            using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
    //            {
    //                Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
    //                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

    //                pdfDoc.Open();
    //                pdfDoc.Add(new Phrase(""));

    //                using (StringReader sr = new StringReader(paginahtml_texto))
    //                {
    //                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
    //                }

    //                pdfDoc.Close();
    //                stream.Close();
    //            }

    //            MessageBox.Show("Recibo de nómina generado exitosamente.");
    //        }



        }

        private void btn_Buscar_RecPEmpleado_Click(object sender, EventArgs e)
        {
            //// Valida que el campo de Número de Empleado, Mes y Año estén completos
            //if (string.IsNullOrEmpty(txt_NumEmpleado_RecPEmpleado.Text) ||
            //    string.IsNullOrEmpty(combo_Mes.Text) ||
            //    string.IsNullOrEmpty(txt_Ano_RecPEmpleado.Text))
            //{
            //    MessageBox.Show("Por favor, llena todos los campos.");
            //    return;
            //}

            //int numeroEmpleado = int.Parse(txt_NumEmpleado_RecPEmpleado.Text);
            //string mes = combo_Mes.Text;
            //int ano = int.Parse(txt_Ano_RecPEmpleado.Text);

            //MessageBox.Show($"Empleado encontrado: {numeroEmpleado}"); // Mensaje de confirmación de búsqueda
          
            // Validar campos
            if (string.IsNullOrEmpty(txt_NumEmpleado_RecPEmpleado.Text) ||
                string.IsNullOrEmpty(combo_Mes.Text) ||
                string.IsNullOrEmpty(txt_Ano_RecPEmpleado.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos.");
                return;
            }

            // Validar que el año es numérico y dentro de un rango razonable
            if (!int.TryParse(txt_Ano_RecPEmpleado.Text, out int ano) || ano < 1900 || ano > DateTime.Now.Year)
            {
                MessageBox.Show("Por favor, ingresa un año válido.");
                return;
            }

            // Validar que el número de empleado existe en la base de datos
            int numeroEmpleado = int.Parse(txt_NumEmpleado_RecPEmpleado.Text);
            if (!EmpleadoExiste(numeroEmpleado))
            {
                MessageBox.Show("El número de empleado no existe.");
                return;
            }


            // Obtener los datos y llenar los campos si pasa la validación
            string mes = combo_Mes.Text;

            if (!NominaIndividualExiste(numeroEmpleado, mes, ano))
            {
                MessageBox.Show("No se encontraron registros de nómina para este empleado, mes y año.");
                return;
            }
            MostrarDatosEmpleado(numeroEmpleado, mes, ano);
            btn_Imprimir_RecPEmpleado.Visible = true;
            combo_Mes.Enabled = false;
            txt_Ano_RecPEmpleado.Enabled= false;
            txt_NumEmpleado_RecPEmpleado.Enabled = false;
            btn_Buscar_RecPEmpleado.Visible= false;
        }

        private void combo_Mes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_Ano_RecPEmpleado_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_NumEmpleado_RecPEmpleado_TextChanged(object sender, EventArgs e)
        {

        }

        private void P_ReciboEmpleado_Load(object sender, EventArgs e)
        {
            txt_Ano_RecPEmpleado.MaxLength = 4;
            txt_NumEmpleado_RecPEmpleado.MaxLength= 4;
            combo_Mes.Items.AddRange(new string[]
           {
                "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
           });
            txt_Nombre_RecPEmpleado.Enabled = false;
            txt_NumEmpleadoMostrar_RecPEmpleado.Enabled=false;
            txt_RFC_RecPEmpleado.Enabled =false;
            txt_IMSS_RecPEmpleado.Enabled=false;
            txt_Curp_RecPEmpleado.Enabled = false;
            txt_DiasTrabajados_RecPEmpleado.Enabled = false;
            txt_Departamento_RecPEmpleado.Enabled=false;
            txt_Puesto_RecPEmpleado.Enabled=false;
            txt_Banco_RecPEmpleado.Enabled = false;
            txt_NumCuenta_RecPEmpleado.Enabled=false;
            txt_TOTDeducciones_RecPEmpleado.Enabled=false;
            txt_TOTPercepciones_RecPEmpleado.Enabled = false;
            txt_Neto_RecPEmpleado.Enabled=false;
            btn_Imprimir_RecPEmpleado.Visible=false;
            txt_Bruto_RecPEmpleado.Enabled = false;
            txt_SalarioDiario_RecPEmpleado.Enabled= false;
        }

        private void MostrarDatosEmpleado(int idEmpleado, string mes, int ano)
        {
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                // 1. Obtener datos del empleado
                string queryEmpleado = @"
            SELECT e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, e.RFC, e.Curp, e.NSS,
                   e.FechaIngresoEmpresa, d.NombreDepartamento, p.NombrePuesto, e.Banco, e.NumeroCuenta, e.SalarioDiario
            FROM Empleado e
            JOIN Departamento d ON e.id_Departamento = d.id_Departamento
            JOIN Puestos p ON e.id_Puesto = p.id_Puesto
            WHERE e.id_Empleado = @idEmpleado";

                SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
                cmdEmpleado.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                SqlDataReader readerEmpleado = cmdEmpleado.ExecuteReader();

                if (readerEmpleado.Read())
                {
                    txt_Nombre_RecPEmpleado.Text = $"{readerEmpleado["NombreEmpleado"]} {readerEmpleado["ApelPaternoEmpleado"]} {readerEmpleado["ApelMaternoEmpleado"]}";
                    txt_NumEmpleadoMostrar_RecPEmpleado.Text = idEmpleado.ToString();
                    txt_RFC_RecPEmpleado.Text = readerEmpleado["RFC"].ToString();
                    txt_Curp_RecPEmpleado.Text = readerEmpleado["Curp"].ToString();
                    txt_IMSS_RecPEmpleado.Text = readerEmpleado["NSS"].ToString();
                    txt_Departamento_RecPEmpleado.Text = readerEmpleado["NombreDepartamento"].ToString();
                    txt_Puesto_RecPEmpleado.Text = readerEmpleado["NombrePuesto"].ToString();
                    txt_Banco_RecPEmpleado.Text = readerEmpleado["Banco"].ToString();
                    txt_NumCuenta_RecPEmpleado.Text = readerEmpleado["NumeroCuenta"].ToString();
                    txt_SalarioDiario_RecPEmpleado.Text = Convert.ToDecimal(readerEmpleado["SalarioDiario"]).ToString("C"); // Asigna Salario Diario

                }
                readerEmpleado.Close();

                // 2. Obtener datos de la nómina individual
                string queryNomina = @"
            SELECT DiasTrabajados, totalDeducciones, totalPercepciones, SueldoNeto, SueldoBruto
            FROM NominaIndividual
            WHERE idEmpleadoFK = @idEmpleado AND Mes = @mes AND Ano = @ano";

                SqlCommand cmdNomina = new SqlCommand(queryNomina, cn);
                cmdNomina.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                cmdNomina.Parameters.AddWithValue("@mes", mes);
                cmdNomina.Parameters.AddWithValue("@ano", ano);
                SqlDataReader readerNomina = cmdNomina.ExecuteReader();

                if (readerNomina.Read())
                {
                    txt_DiasTrabajados_RecPEmpleado.Text = readerNomina["DiasTrabajados"].ToString();
                    txt_TOTDeducciones_RecPEmpleado.Text = Convert.ToDecimal(readerNomina["totalDeducciones"]).ToString("C");
                    txt_TOTPercepciones_RecPEmpleado.Text = Convert.ToDecimal(readerNomina["totalPercepciones"]).ToString("C");
                    txt_Neto_RecPEmpleado.Text = Convert.ToDecimal(readerNomina["SueldoNeto"]).ToString("C");
                    txt_Bruto_RecPEmpleado.Text = Convert.ToDecimal(readerNomina["SueldoBruto"]).ToString("C"); // Asigna Sueldo Bruto

                }
                else
                {
                    MessageBox.Show("No se encontraron registros de nómina para este mes y año.");
                }
                readerNomina.Close();
            }
        }

        private bool EmpleadoExiste(int idEmpleado)
        {
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                string query = "SELECT COUNT(*) FROM Empleado WHERE id_Empleado = @idEmpleado";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private bool NominaIndividualExiste(int idEmpleado, string mes, int ano)
        {
            bool existe = false;
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                try
                {
                    cn.Open();
                    string query = @"
                SELECT COUNT(*)
                FROM NominaIndividual
                WHERE idEmpleadoFK = @idEmpleado AND Mes = @mes AND Ano = @ano";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    cmd.Parameters.AddWithValue("@mes", mes);
                    cmd.Parameters.AddWithValue("@ano", ano);

                    int count = (int)cmd.ExecuteScalar();
                    existe = count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al verificar la nómina individual: {ex.Message}");
                }
            }
            return existe;
        }

        //        private void imprimirr(object sender, EventArgs e)
        //        {

        //            SaveFileDialog guardar = new SaveFileDialog();
        //            guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";

        //            // Cargar la plantilla HTML desde los recursos
        //            string paginahtml_texto = Properties.Resources.plantilla.ToString();

        //            using (SqlConnection cn = new SqlConnection(Conexion))
        //            {
        //                cn.Open();

        //                // 1. Datos de la Empresa
        //                string queryEmpresa = "SELECT Nombre, RazonFiscal, DomicilioFiscal, Telefono, RegistroPatronal, RFC, FechaInOperaciones FROM Empresa";
        //                SqlCommand cmdEmpresa = new SqlCommand(queryEmpresa, cn);
        //                SqlDataReader readerEmpresa = cmdEmpresa.ExecuteReader();

        //                if (readerEmpresa.Read())
        //                {
        //                    paginahtml_texto = paginahtml_texto.Replace("@MES_NOMINA", combo_Mes.Text);
        //                    paginahtml_texto = paginahtml_texto.Replace("@ANO_NOMINA", txt_Ano_RecPEmpleado.Text);
        //                    paginahtml_texto = paginahtml_texto.Replace("@NOMBRE_EMPRESA", readerEmpresa["Nombre"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@RAZON_SOCIAL", readerEmpresa["RazonFiscal"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@DOMICILIO_FISCAL", readerEmpresa["DomicilioFiscal"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@TELEFONO_EMPRESA", readerEmpresa["Telefono"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@REGISTRO_PATRONAL", readerEmpresa["RegistroPatronal"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@RFC_EMPRESA", readerEmpresa["RFC"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@FECHA_INICIO_OPERACIONES", Convert.ToDateTime(readerEmpresa["FechaInOperaciones"]).ToString("dd/MM/yyyy"));
        //                }
        //                readerEmpresa.Close();




        //                //2.Empleado
        //                int numeroEmpleado = int.Parse(txt_NumEmpleado_RecPEmpleado.Text);
        //                string queryEmpleado = @"
        //    SELECT e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, e.RFC, e.Curp, e.NSS, e.FechaIngresoEmpresa, 
        //           d.NombreDepartamento, p.NombrePuesto, e.SalarioDiario, e.Banco, e.NumeroCuenta
        //    FROM Empleado e
        //    JOIN Departamento d ON e.id_Departamento = d.id_Departamento
        //    JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //    WHERE e.id_Empleado = @idEmpleado";

        //                SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
        //                cmdEmpleado.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
        //                SqlDataReader readerEmpleado = cmdEmpleado.ExecuteReader();
        //                decimal salarioDiario = 0;
        //                if (readerEmpleado.Read())
        //                {
        //                    paginahtml_texto = paginahtml_texto.Replace("@NOMBRE_EMPLEADO", $"{readerEmpleado["NombreEmpleado"]} {readerEmpleado["ApelPaternoEmpleado"]} {readerEmpleado["ApelMaternoEmpleado"]}");
        //                    paginahtml_texto = paginahtml_texto.Replace("@NUMERO_EMPLEADO", numeroEmpleado.ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@RFC_EMPLEADO", readerEmpleado["RFC"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@CURP_EMPLEADO", readerEmpleado["Curp"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@IMSS_EMPLEADO", readerEmpleado["NSS"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@FECHA_INGRESO", Convert.ToDateTime(readerEmpleado["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy"));
        //                    paginahtml_texto = paginahtml_texto.Replace("@DEPARTAMENTO", readerEmpleado["NombreDepartamento"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@PUESTO", readerEmpleado["NombrePuesto"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@BANCO", readerEmpleado["Banco"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@NUMERO_CUENTA", readerEmpleado["NumeroCuenta"].ToString());
        //                    paginahtml_texto = paginahtml_texto.Replace("@SALARIO_DIARIO", Convert.ToDecimal(readerEmpleado["SalarioDiario"]).ToString("C"));
        //                    salarioDiario = Convert.ToDecimal(readerEmpleado["SalarioDiario"]);
        //                }
        //                readerEmpleado.Close();

        //                string mes = combo_Mes.Text;
        //                int ano = int.Parse(txt_Ano_RecPEmpleado.Text);

        //                //3.Deducciones
        //                int faltas = ContarFaltasEmpleado(numeroEmpleado, mes, ano);
        //                //decimal salarioDiario = Convert.ToDecimal(readerEmpleado["SalarioDiario"]);
        //                decimal deduccionFaltas = faltas * salarioDiario;
        //                decimal totalDeducciones = deduccionFaltas; // Inicia con las faltas

        //                // Agregar las demás deducciones
        //                string deduccionesHtml = "";
        //                string queryDeducciones = @"
        //    SELECT dp.id_PD, dp.Nombre_PD, COALESCE(dp.MontoPD, 0) AS MontoPD, COALESCE(dp.Porcentaje_PD, 0) AS Porcentaje_PD
        //    FROM DEDPERNOMINA dpn
        //    JOIN DeduccionesPercepciones dp ON dpn.id_PD = dp.id_PD
        //    WHERE dpn.id_Empleado = @idEmpleado AND dpn.Mes = @mes AND dpn.Ano = @ano AND dp.D_P = 'Deducción'
        //    AND dp.Nombre_PD != 'Falta'"; // Excluye las faltas aquí para no duplicarlas

        //                SqlCommand cmdDeducciones = new SqlCommand(queryDeducciones, cn);
        //                cmdDeducciones.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
        //                cmdDeducciones.Parameters.AddWithValue("@mes", mes);
        //                cmdDeducciones.Parameters.AddWithValue("@ano", ano);
        //                SqlDataReader readerDeducciones = cmdDeducciones.ExecuteReader();

        //                while (readerDeducciones.Read())
        //                {
        //                    string clave = readerDeducciones["id_PD"].ToString();
        //                    string concepto = readerDeducciones["Nombre_PD"].ToString();
        //                    decimal monto = Convert.ToDecimal(readerDeducciones["MontoPD"]);
        //                    decimal porcentaje = Convert.ToDecimal(readerDeducciones["Porcentaje_PD"]);
        //                    decimal importe = monto > 0 ? monto : (salarioDiario * porcentaje / 100);

        //                    deduccionesHtml += $"<tr><td>{clave}</td><td>{concepto}</td><td>{importe:C}</td></tr>";
        //                    totalDeducciones += importe;
        //                }
        //                readerDeducciones.Close();

        //                // Obtener ISR e IMSS desde la tabla NominaIndividual
        //                string queryNomina = @"
        //SELECT ISR, IMSS
        //FROM NominaIndividual
        //WHERE idEmpleadoFK = @idEmpleado AND Mes = @mes AND Ano = @ano";

        //                SqlCommand cmdNomina = new SqlCommand(queryNomina, cn);
        //                cmdNomina.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
        //                cmdNomina.Parameters.AddWithValue("@mes", mes);
        //                cmdNomina.Parameters.AddWithValue("@ano", ano);
        //                SqlDataReader readerNomina = cmdNomina.ExecuteReader();

        //                decimal isr = 0;
        //                decimal imss = 0;

        //                if (readerNomina.Read())
        //                {
        //                    isr = Convert.ToDecimal(readerNomina["ISR"]);
        //                    imss = Convert.ToDecimal(readerNomina["IMSS"]);
        //                }
        //                readerNomina.Close();

        //                // Agregar ISR e IMSS a las deducciones
        //                totalDeducciones += isr + imss;
        //                deduccionesHtml += $"<tr><td>ISR</td><td>ISR</td><td>{isr:C}</td></tr>";
        //                deduccionesHtml += $"<tr><td>IMSS</td><td>IMSS</td><td>{imss:C}</td></tr>";


        //                // Reemplazar en la plantilla
        //                paginahtml_texto = paginahtml_texto.Replace("@FILAS_DEDUCCIONES", deduccionesHtml);
        //                paginahtml_texto = paginahtml_texto.Replace("@TOTAL_DEDUCCIONES", totalDeducciones.ToString("C"));



        //                //4.percepciones
        //                string percepcionesHtml = "";
        //                decimal totalPercepciones = 0;

        //                string queryPercepciones = @"
        //    SELECT dp.id_PD, dp.Nombre_PD, COALESCE(dp.MontoPD, 0) AS MontoPD, COALESCE(dp.Porcentaje_PD, 0) AS Porcentaje_PD
        //    FROM DEDPERNOMINA dpn
        //    JOIN DeduccionesPercepciones dp ON dpn.id_PD = dp.id_PD
        //    WHERE dpn.id_Empleado = @idEmpleado AND dpn.Mes = @mes AND dpn.Ano = @ano AND dp.D_P = 'Percepción'";

        //                SqlCommand cmdPercepciones = new SqlCommand(queryPercepciones, cn);
        //                cmdPercepciones.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
        //                cmdPercepciones.Parameters.AddWithValue("@mes", mes);
        //                cmdPercepciones.Parameters.AddWithValue("@ano", ano);
        //                SqlDataReader readerPercepciones = cmdPercepciones.ExecuteReader();

        //                while (readerPercepciones.Read())
        //                {
        //                    string clave = readerPercepciones["id_PD"].ToString();
        //                    string concepto = readerPercepciones["Nombre_PD"].ToString();
        //                    decimal monto = Convert.ToDecimal(readerPercepciones["MontoPD"]);
        //                    decimal porcentaje = Convert.ToDecimal(readerPercepciones["Porcentaje_PD"]);
        //                    decimal importe = monto > 0 ? monto : (salarioDiario * porcentaje / 100);

        //                    percepcionesHtml += $"<tr><td>{clave}</td><td>{concepto}</td><td>{importe:C}</td></tr>";
        //                    totalPercepciones += importe;
        //                }
        //                readerPercepciones.Close();

        //                // Reemplazar en la plantilla
        //                paginahtml_texto = paginahtml_texto.Replace("@FILAS_PERCEPCIONES", percepcionesHtml);
        //                paginahtml_texto = paginahtml_texto.Replace("@TOTAL_PERCEPCIONES", totalPercepciones.ToString("C"));



        //            }

        //            // Generar el PDF con los datos completados
        //            if (guardar.ShowDialog() == DialogResult.OK)
        //            {
        //                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
        //                {
        //                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
        //                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

        //                    pdfDoc.Open();
        //                    pdfDoc.Add(new Phrase(""));

        //                    using (StringReader sr = new StringReader(paginahtml_texto))
        //                    {
        //                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //                    }

        //                    pdfDoc.Close();
        //                    stream.Close();
        //                }

        //                MessageBox.Show("Recibo de nómina generado exitosamente.");
        //            }

        //        }

        //private void imprimirr(object sender, EventArgs e)
        //{
        //    SaveFileDialog guardar = new SaveFileDialog();
        //    guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";

        //    // Cargar la plantilla HTML desde los recursos
        //    string paginahtml_texto = Properties.Resources.plantilla.ToString();

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        // 1. Datos de la Empresa
        //        string queryEmpresa = "SELECT Nombre, RazonFiscal, DomicilioFiscal, Telefono, RegistroPatronal, RFC, FechaInOperaciones FROM Empresa";
        //        SqlCommand cmdEmpresa = new SqlCommand(queryEmpresa, cn);
        //        SqlDataReader readerEmpresa = cmdEmpresa.ExecuteReader();

        //        if (readerEmpresa.Read())
        //        {
        //            paginahtml_texto = paginahtml_texto.Replace("@MES_NOMINA", combo_Mes.Text);
        //            paginahtml_texto = paginahtml_texto.Replace("@ANO_NOMINA", txt_Ano_RecPEmpleado.Text);
        //            paginahtml_texto = paginahtml_texto.Replace("@NOMBRE_EMPRESA", readerEmpresa["Nombre"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@RAZON_SOCIAL", readerEmpresa["RazonFiscal"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@DOMICILIO_FISCAL", readerEmpresa["DomicilioFiscal"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@TELEFONO_EMPRESA", readerEmpresa["Telefono"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@REGISTRO_PATRONAL", readerEmpresa["RegistroPatronal"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@RFC_EMPRESA", readerEmpresa["RFC"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@FECHA_INICIO_OPERACIONES", Convert.ToDateTime(readerEmpresa["FechaInOperaciones"]).ToString("dd/MM/yyyy"));
        //        }
        //        readerEmpresa.Close();

        //        // 2. Datos del Empleado
        //        int numeroEmpleado = int.Parse(txt_NumEmpleado_RecPEmpleado.Text);
        //        string queryEmpleado = @"
        //SELECT e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, e.RFC, e.Curp, e.NSS, e.FechaIngresoEmpresa, 
        //       d.NombreDepartamento, p.NombrePuesto, e.SalarioDiario, e.Banco, e.NumeroCuenta
        //FROM Empleado e
        //JOIN Departamento d ON e.id_Departamento = d.id_Departamento
        //JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //WHERE e.id_Empleado = @idEmpleado";

        //        SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
        //        cmdEmpleado.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
        //        SqlDataReader readerEmpleado = cmdEmpleado.ExecuteReader();

        //        decimal salarioDiario = 0;
        //        if (readerEmpleado.Read())
        //        {
        //            paginahtml_texto = paginahtml_texto.Replace("@NOMBRE_EMPLEADO", $"{readerEmpleado["NombreEmpleado"]} {readerEmpleado["ApelPaternoEmpleado"]} {readerEmpleado["ApelMaternoEmpleado"]}");
        //            paginahtml_texto = paginahtml_texto.Replace("@NUMERO_EMPLEADO", numeroEmpleado.ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@RFC_EMPLEADO", readerEmpleado["RFC"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@CURP_EMPLEADO", readerEmpleado["Curp"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@IMSS_EMPLEADO", readerEmpleado["NSS"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@FECHA_INGRESO", Convert.ToDateTime(readerEmpleado["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy"));
        //            paginahtml_texto = paginahtml_texto.Replace("@DEPARTAMENTO", readerEmpleado["NombreDepartamento"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@PUESTO", readerEmpleado["NombrePuesto"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@BANCO", readerEmpleado["Banco"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@NUMERO_CUENTA", readerEmpleado["NumeroCuenta"].ToString());
        //            paginahtml_texto = paginahtml_texto.Replace("@SALARIO_DIARIO", Convert.ToDecimal(readerEmpleado["SalarioDiario"]).ToString("C"));
        //            salarioDiario = Convert.ToDecimal(readerEmpleado["SalarioDiario"]);
        //        }
        //        readerEmpleado.Close();

        //        string mes = combo_Mes.Text;
        //        int ano = int.Parse(txt_Ano_RecPEmpleado.Text);

        //        // Obtener el Sueldo Bruto de la tabla NominaIndividual para el cálculo de porcentajes
        //        decimal sueldoBruto = 0;
        //        string queryNomina = @"
        //SELECT DiasTrabajados, SueldoBruto, ISR, IMSS
        //FROM NominaIndividual
        //WHERE idEmpleadoFK = @idEmpleado AND Mes = @mes AND Ano = @ano";

        //        SqlCommand cmdNomina = new SqlCommand(queryNomina, cn);
        //        cmdNomina.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
        //        cmdNomina.Parameters.AddWithValue("@mes", mes);
        //        cmdNomina.Parameters.AddWithValue("@ano", ano);
        //        SqlDataReader readerNomina = cmdNomina.ExecuteReader();

        //        int diasTrabajados = 0;
        //        decimal isr = 0, imss = 0;
        //        if (readerNomina.Read())
        //        {
        //            diasTrabajados = Convert.ToInt32(readerNomina["DiasTrabajados"]);
        //            sueldoBruto = Convert.ToDecimal(readerNomina["SueldoBruto"]);
        //            isr = Convert.ToDecimal(readerNomina["ISR"]);
        //            imss = Convert.ToDecimal(readerNomina["IMSS"]);
        //        }
        //        readerNomina.Close();

        //        // Reemplazar los días trabajados en la plantilla
        //        paginahtml_texto = paginahtml_texto.Replace("@DIAS_TRABAJADOS", diasTrabajados.ToString());

        //        // 3. Calcular Deducciones, incluyendo faltas
        //        int faltas = ContarFaltasEmpleado(numeroEmpleado, mes, ano);
        //        decimal deduccionFaltas = faltas * salarioDiario;
        //        decimal totalDeducciones = deduccionFaltas;

        //        // Agregar deducción por faltas
        //        string deduccionesHtml = $"<tr><td>Falta</td><td>Faltas</td><td>{deduccionFaltas:C}</td></tr>";

        //        // Agregar otras deducciones
        //        string queryDeducciones = @"
        //SELECT dp.id_PD, dp.Nombre_PD, COALESCE(dp.MontoPD, 0) AS MontoPD, COALESCE(dp.Porcentaje_PD, 0) AS Porcentaje_PD
        //FROM DEDPERNOMINA dpn
        //JOIN DeduccionesPercepciones dp ON dpn.id_PD = dp.id_PD
        //WHERE dpn.id_Empleado = @idEmpleado AND dpn.Mes = @mes AND dpn.Ano = @ano AND dp.D_P = 'Deducción' 
        //AND dp.Nombre_PD != 'Falta'";

        //        SqlCommand cmdDeducciones = new SqlCommand(queryDeducciones, cn);
        //        cmdDeducciones.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
        //        cmdDeducciones.Parameters.AddWithValue("@mes", mes);
        //        cmdDeducciones.Parameters.AddWithValue("@ano", ano);
        //        SqlDataReader readerDeducciones = cmdDeducciones.ExecuteReader();

        //        while (readerDeducciones.Read())
        //        {
        //            string clave = readerDeducciones["id_PD"].ToString();
        //            string concepto = readerDeducciones["Nombre_PD"].ToString();
        //            decimal monto = Convert.ToDecimal(readerDeducciones["MontoPD"]);
        //            decimal porcentaje = Convert.ToDecimal(readerDeducciones["Porcentaje_PD"]);
        //            decimal importe = monto > 0 ? monto : (sueldoBruto * porcentaje / 100);

        //            deduccionesHtml += $"<tr><td>{clave}</td><td>{concepto}</td><td>{importe:C}</td></tr>";
        //            totalDeducciones += importe;
        //        }
        //        readerDeducciones.Close();

        //        // Agregar ISR e IMSS
        //        totalDeducciones += isr + imss;
        //        deduccionesHtml += $"<tr><td>ISR</td><td>ISR</td><td>{isr:C}</td></tr>";
        //        deduccionesHtml += $"<tr><td>IMSS</td><td>IMSS</td><td>{imss:C}</td></tr>";

        //        // Reemplazar en la plantilla
        //        paginahtml_texto = paginahtml_texto.Replace("@FILAS_DEDUCCIONES", deduccionesHtml);
        //        paginahtml_texto = paginahtml_texto.Replace("@TOTAL_DEDUCCIONES", totalDeducciones.ToString("C"));

        //        // 4. Calcular Percepciones
        //        string percepcionesHtml = "";
        //        decimal totalPercepciones = 0;

        //        string queryPercepciones = @"
        //SELECT dp.id_PD, dp.Nombre_PD, COALESCE(dp.MontoPD, 0) AS MontoPD, COALESCE(dp.Porcentaje_PD, 0) AS Porcentaje_PD
        //FROM DEDPERNOMINA dpn
        //JOIN DeduccionesPercepciones dp ON dpn.id_PD = dp.id_PD
        //WHERE dpn.id_Empleado = @idEmpleado AND dpn.Mes = @mes AND dpn.Ano = @ano AND dp.D_P = 'Percepción'";

        //        SqlCommand cmdPercepciones = new SqlCommand(queryPercepciones, cn);
        //        cmdPercepciones.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
        //        cmdPercepciones.Parameters.AddWithValue("@mes", mes);
        //        cmdPercepciones.Parameters.AddWithValue("@ano", ano);
        //        SqlDataReader readerPercepciones = cmdPercepciones.ExecuteReader();

        //        while (readerPercepciones.Read())
        //        {
        //            string clave = readerPercepciones["id_PD"].ToString();
        //            string concepto = readerPercepciones["Nombre_PD"].ToString();
        //            decimal monto = Convert.ToDecimal(readerPercepciones["MontoPD"]);
        //            decimal porcentaje = Convert.ToDecimal(readerPercepciones["Porcentaje_PD"]);
        //            decimal importe = monto > 0 ? monto : (sueldoBruto * porcentaje / 100);

        //            percepcionesHtml += $"<tr><td>{clave}</td><td>{concepto}</td><td>{importe:C}</td></tr>";
        //            totalPercepciones += importe;
        //        }
        //        readerPercepciones.Close();

        //        // Reemplazar en la plantilla
        //        paginahtml_texto = paginahtml_texto.Replace("@FILAS_PERCEPCIONES", percepcionesHtml);
        //        paginahtml_texto = paginahtml_texto.Replace("@TOTAL_PERCEPCIONES", totalPercepciones.ToString("C"));

        //        // Generar el PDF con los datos completados
        //        if (guardar.ShowDialog() == DialogResult.OK)
        //        {
        //            using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
        //            {
        //                Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
        //                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

        //                pdfDoc.Open();
        //                pdfDoc.Add(new Phrase(""));

        //                using (StringReader sr = new StringReader(paginahtml_texto))
        //                {
        //                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //                }

        //                pdfDoc.Close();
        //                stream.Close();
        //            }

        //            MessageBox.Show("Recibo de nómina generado exitosamente.");
        //        }
        //    }
        //}
        private void imprimirr(object sender, EventArgs e)
        {
            //SaveFileDialog guardar = new SaveFileDialog();
            //guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";

            // Obtener la ruta de la carpeta de Descargas
            string carpetaDescargas = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\";
            string nombreArchivo = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            string rutaArchivo = Path.Combine(carpetaDescargas, nombreArchivo);


            // Cargar la plantilla HTML desde los recursos
            string paginahtml_texto = Properties.Resources.plantilla.ToString();

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();

                // 1. Datos de la Empresa
                string queryEmpresa = "SELECT Nombre, RazonFiscal, DomicilioFiscal, Telefono, RegistroPatronal, RFC, FechaInOperaciones FROM Empresa";
                SqlCommand cmdEmpresa = new SqlCommand(queryEmpresa, cn);
                SqlDataReader readerEmpresa = cmdEmpresa.ExecuteReader();

                if (readerEmpresa.Read())
                {
                    paginahtml_texto = paginahtml_texto.Replace("@MES_NOMINA", combo_Mes.Text);
                    paginahtml_texto = paginahtml_texto.Replace("@ANO_NOMINA", txt_Ano_RecPEmpleado.Text);
                    paginahtml_texto = paginahtml_texto.Replace("@NOMBRE_EMPRESA", readerEmpresa["Nombre"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@RAZON_SOCIAL", readerEmpresa["RazonFiscal"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@DOMICILIO_FISCAL", readerEmpresa["DomicilioFiscal"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@TELEFONO_EMPRESA", readerEmpresa["Telefono"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@REGISTRO_PATRONAL", readerEmpresa["RegistroPatronal"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@RFC_EMPRESA", readerEmpresa["RFC"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@FECHA_INICIO_OPERACIONES", Convert.ToDateTime(readerEmpresa["FechaInOperaciones"]).ToString("dd/MM/yyyy"));
                }
                readerEmpresa.Close();

                // 2. Datos del Empleado
                int numeroEmpleado = int.Parse(txt_NumEmpleado_RecPEmpleado.Text);
                string queryEmpleado = @"
    SELECT e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, e.RFC, e.Curp, e.NSS, e.FechaIngresoEmpresa, 
           d.NombreDepartamento, p.NombrePuesto, e.SalarioDiario, e.Banco, e.NumeroCuenta
    FROM Empleado e
    JOIN Departamento d ON e.id_Departamento = d.id_Departamento
    JOIN Puestos p ON e.id_Puesto = p.id_Puesto
    WHERE e.id_Empleado = @idEmpleado";

                SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
                cmdEmpleado.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
                SqlDataReader readerEmpleado = cmdEmpleado.ExecuteReader();

                decimal salarioDiario = 0;
                if (readerEmpleado.Read())
                {
                    paginahtml_texto = paginahtml_texto.Replace("@NOMBRE_EMPLEADO", $"{readerEmpleado["NombreEmpleado"]} {readerEmpleado["ApelPaternoEmpleado"]} {readerEmpleado["ApelMaternoEmpleado"]}");
                    paginahtml_texto = paginahtml_texto.Replace("@NUMERO_EMPLEADO", numeroEmpleado.ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@RFC_EMPLEADO", readerEmpleado["RFC"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@CURP_EMPLEADO", readerEmpleado["Curp"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@IMSS_EMPLEADO", readerEmpleado["NSS"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@FECHA_INGRESO", Convert.ToDateTime(readerEmpleado["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy"));
                    paginahtml_texto = paginahtml_texto.Replace("@DEPARTAMENTO", readerEmpleado["NombreDepartamento"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@PUESTO", readerEmpleado["NombrePuesto"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@BANCO", readerEmpleado["Banco"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@NUMERO_CUENTA", readerEmpleado["NumeroCuenta"].ToString());
                    paginahtml_texto = paginahtml_texto.Replace("@SALARIO_DIARIO", Convert.ToDecimal(readerEmpleado["SalarioDiario"]).ToString("C"));
                    salarioDiario = Convert.ToDecimal(readerEmpleado["SalarioDiario"]);
                }
                readerEmpleado.Close();

                string mes = combo_Mes.Text;
                int ano = int.Parse(txt_Ano_RecPEmpleado.Text);

                // 3. Obtener datos de la nómina individual
                decimal sueldoBruto = 0, sueldoNeto = 0, isr = 0, imss = 0, totalDeducciones = 0, totalPercepciones = 0;
                int diasTrabajados = 0;
                // Calcular las horas extras y el importe correspondiente
                int horasExtras = ContarHorasExtrasEmpleado(numeroEmpleado, mes, ano);
                decimal importeHorasExtras = (salarioDiario / 8) * 2 * horasExtras;


                string queryNomina = @"
    SELECT DiasTrabajados, SueldoBruto, SueldoNeto, ISR, IMSS, totalDeducciones, totalPercepciones
    FROM NominaIndividual
    WHERE idEmpleadoFK = @idEmpleado AND Mes = @mes AND Ano = @ano";

                SqlCommand cmdNomina = new SqlCommand(queryNomina, cn);
                cmdNomina.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
                cmdNomina.Parameters.AddWithValue("@mes", mes);
                cmdNomina.Parameters.AddWithValue("@ano", ano);
                SqlDataReader readerNomina = cmdNomina.ExecuteReader();

                if (readerNomina.Read())
                {
                    diasTrabajados = Convert.ToInt32(readerNomina["DiasTrabajados"]);
                    sueldoBruto = Convert.ToDecimal(readerNomina["SueldoBruto"]);
                    sueldoNeto = Convert.ToDecimal(readerNomina["SueldoNeto"]);
                    isr = Convert.ToDecimal(readerNomina["ISR"]);
                    imss = Convert.ToDecimal(readerNomina["IMSS"]);
                    totalDeducciones = Convert.ToDecimal(readerNomina["totalDeducciones"]);
                    totalPercepciones = Convert.ToDecimal(readerNomina["totalPercepciones"]);
                }
                readerNomina.Close();
                // Calcular vacaciones y prima vacacional si aplican
                var (diasVacaciones, montoVacaciones, primaVacacional) = CalcularVacaciones(numeroEmpleado, mes, ano, salarioDiario);
                
                // Calcular faltas en base a los días trabajados
                int faltas = 30 - (diasTrabajados+diasVacaciones);
                decimal deduccionFaltas = faltas * salarioDiario;

                // Reemplazar los valores en la plantilla
                paginahtml_texto = paginahtml_texto.Replace("@DIAS_TRABAJADOS", diasTrabajados.ToString());
                paginahtml_texto = paginahtml_texto.Replace("@TOTAL_NETO", sueldoNeto.ToString("C"));
                paginahtml_texto = paginahtml_texto.Replace("@SUELDO_BRUTO", sueldoBruto.ToString("C"));
                paginahtml_texto = paginahtml_texto.Replace("@TOTAL_DEDUCCIONES", totalDeducciones.ToString("C"));
                paginahtml_texto = paginahtml_texto.Replace("@TOTAL_PERCEPCIONES", totalPercepciones.ToString("C"));

                //// Calcular vacaciones y prima vacacional si aplican
                //var (diasVacaciones, montoVacaciones, primaVacacional) = CalcularVacaciones(numeroEmpleado, mes, ano, salarioDiario);

                // 4. Generar tabla de deducciones
                // 4. Generar tabla de deducciones
                string deduccionesHtml = "";

                // Calcular la deducción por faltas correctamente
                deduccionesHtml += $"<tr><td>F</td><td>Faltas ({faltas} días)</td><td>{deduccionFaltas:C}</td></tr>";

                // Calcular el ISR e IMSS
                deduccionesHtml += $"<tr><td>ISR</td><td>ISR</td><td>{isr:C}</td></tr>";
                deduccionesHtml += $"<tr><td>IMSS</td><td>IMSS</td><td>{imss:C}</td></tr>";

                // Obtener deducciones adicionales y asegurar que Fondo de Ahorro solo se muestra una vez
                bool fondoAhorroAgregado = false;
                bool prestamoInfonavitAgregado = false;

                string queryDeducciones = @"
SELECT dp.id_PD, dp.Nombre_PD, COALESCE(dp.MontoPD, 0) AS MontoPD, COALESCE(dp.Porcentaje_PD, 0) AS Porcentaje_PD
FROM DEDPERNOMINA dpn
JOIN DeduccionesPercepciones dp ON dpn.id_PD = dp.id_PD
WHERE dpn.id_Empleado = @idEmpleado AND dpn.Mes = @mes AND dpn.Ano = @ano AND dp.D_P = 'Deducción'";

                SqlCommand cmdDeducciones = new SqlCommand(queryDeducciones, cn);
                cmdDeducciones.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
                cmdDeducciones.Parameters.AddWithValue("@mes", mes);
                cmdDeducciones.Parameters.AddWithValue("@ano", ano);
                SqlDataReader readerDeducciones = cmdDeducciones.ExecuteReader();

                while (readerDeducciones.Read())
                {
                    string clave = readerDeducciones["id_PD"].ToString();
                    string concepto = readerDeducciones["Nombre_PD"].ToString();
                    decimal monto = Convert.ToDecimal(readerDeducciones["MontoPD"]);
                    decimal porcentaje = Convert.ToDecimal(readerDeducciones["Porcentaje_PD"]);
                    decimal importe = monto > 0 ? monto : (sueldoBruto * porcentaje / 100);

                    // Evitar duplicados de "Faltas", "Fondo de Ahorro", y "Préstamo Infonavit" en la deducción
                    if (concepto != "Falta" && concepto != "Fondo de Ahorro" && concepto != "Préstamo Infonavit")
                    {
                        deduccionesHtml += $"<tr><td>{clave}</td><td>{concepto}</td><td>{importe:C}</td></tr>";
                    }

                    // Solo agregar Fondo de Ahorro una vez
                    if (concepto == "Fondo de Ahorro" && !fondoAhorroAgregado)
                    {
                        decimal fondoAhorro = sueldoBruto < 10000 ? 500 : 1000;
                        deduccionesHtml += $"<tr><td>FA</td><td>Fondo de Ahorro</td><td>{fondoAhorro:C}</td></tr>";
                        fondoAhorroAgregado = true;
                    }
                    // Solo agregar Préstamo Infonavit una vez si existe
                    if (concepto == "Prestamo Infonavit" && !prestamoInfonavitAgregado)
                    {
                        // Calcular el importe correcto para Préstamo Infonavit
                        decimal prestamoInfonavit = salarioDiario * 0.11m;
                        deduccionesHtml += $"<tr><td>PI</td><td>Préstamo Infonavit</td><td>{prestamoInfonavit:C}</td></tr>";
                        prestamoInfonavitAgregado = true;
                    }
                }
                readerDeducciones.Close();

                // Agregar el Fondo de Ahorro
              //  decimal fondoAhorro = sueldoBruto < 10000 ? 500 : 1000;
               // deduccionesHtml += $"<tr><td>FA</td><td>Fondo de Ahorro</td><td>{fondoAhorro:C}</td></tr>";

                // Reemplazar la tabla de deducciones en la plantilla
                paginahtml_texto = paginahtml_texto.Replace("@FILAS_DEDUCCIONES", deduccionesHtml);







                // Definir el monto del aguinaldo, si aplica
                decimal aguinaldo = 0;
                if (mes.Equals("Diciembre", StringComparison.OrdinalIgnoreCase))
                {
                    aguinaldo = salarioDiario * 18;
                }

                // 5. Generar tabla de percepciones
                string percepcionesHtml = "";
                string queryPercepciones = @"
SELECT dp.id_PD, dp.Nombre_PD, COALESCE(dp.MontoPD, 0) AS MontoPD, COALESCE(dp.Porcentaje_PD, 0) AS Porcentaje_PD
FROM DEDPERNOMINA dpn
JOIN DeduccionesPercepciones dp ON dpn.id_PD = dp.id_PD
WHERE dpn.id_Empleado = @idEmpleado AND dpn.Mes = @mes AND dpn.Ano = @ano AND dp.D_P = 'Percepción'";

                SqlCommand cmdPercepciones = new SqlCommand(queryPercepciones, cn);
                cmdPercepciones.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
                cmdPercepciones.Parameters.AddWithValue("@mes", mes);
                cmdPercepciones.Parameters.AddWithValue("@ano", ano);
                SqlDataReader readerPercepciones = cmdPercepciones.ExecuteReader();

                bool horasExtraAgregada = false;
                decimal totalHorasExtras = 0;
                decimal totalImporteHorasExtras = 0;

                while (readerPercepciones.Read())
                {
                    string clave = readerPercepciones["id_PD"].ToString();
                    string concepto = readerPercepciones["Nombre_PD"].ToString();
                    decimal monto = Convert.ToDecimal(readerPercepciones["MontoPD"]);
                    decimal porcentaje = Convert.ToDecimal(readerPercepciones["Porcentaje_PD"]);
                    decimal importe = monto > 0 ? monto : (sueldoBruto * porcentaje / 100);

                    // Excluir el concepto de "Vacaciones" con monto 0
                    if (!(concepto == "Vacaciones" && monto == 0) && concepto != "Hora Extra")
                    {
                        percepcionesHtml += $"<tr><td>{clave}</td><td>{concepto}</td><td>{importe:C}</td></tr>";
                    }

                    // Si el concepto es "Hora Extra", suma las horas y el importe
                    if (concepto == "Hora Extra")
                    {
                        totalHorasExtras += 1; // Aumenta en 1 hora extra
                        totalImporteHorasExtras += (salarioDiario / 8) * 2; // Calcula el importe de 1 hora extra
                        horasExtraAgregada = true;
                    }
                }
                readerPercepciones.Close();

                //// Agregar el sueldo bruto en la tabla de percepciones
                //percepcionesHtml += $"<tr><td>SB</td><td>Sueldo Bruto</td><td>{sueldoBruto:C}</td></tr>";

                // Agregar las horas extras en la tabla de percepciones si fueron registradas
                if (horasExtraAgregada)
                {
                    percepcionesHtml += $"<tr><td>HE</td><td>Horas Extra ({totalHorasExtras} hrs)</td><td>{totalImporteHorasExtras:C}</td></tr>";
                }

                // Reemplazar la tabla de percepciones en la plantilla
               // paginahtml_texto = paginahtml_texto.Replace("@FILAS_PERCEPCIONES", percepcionesHtml);

                // Agregar vacaciones y prima vacacional si aplica
                if (diasVacaciones > 0)
                {
                    percepcionesHtml += $"<tr><td>V</td><td>Días de Vacaciones</td><td>{montoVacaciones:C}</td></tr>";
                    percepcionesHtml += $"<tr><td>PV</td><td>Prima Vacacional</td><td>{primaVacacional:C}</td></tr>";
                }
                // Agregar aguinaldo si es diciembre
                if (aguinaldo > 0)
                {
                    percepcionesHtml += $"<tr><td>A</td><td>Aguinaldo</td><td>{aguinaldo:C}</td></tr>";
                }
                //// Agregar el sueldo bruto en la tabla de percepciones
                //percepcionesHtml += $"<tr><td>SB</td><td>Sueldo Bruto</td><td>{sueldoBruto:C}</td></tr>";

                // Reemplazar la tabla de percepciones en la plantilla
                paginahtml_texto = paginahtml_texto.Replace("@FILAS_PERCEPCIONES", percepcionesHtml);

                //// Generar el PDF con los datos completados
                //if (guardar.ShowDialog() == DialogResult.OK)
                //{
                //    using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                //    {
                //        Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                //        pdfDoc.Open();
                //        pdfDoc.Add(new Phrase(""));

                //        using (StringReader sr = new StringReader(paginahtml_texto))
                //        {
                //            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                //        }

                //        pdfDoc.Close();
                //        stream.Close();
                //    }

                //    MessageBox.Show("Recibo de nómina generado exitosamente.");
                //}
                // Generar el PDF en la carpeta de Descargas
                using (FileStream stream = new FileStream(rutaArchivo, FileMode.Create))
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

                // Abrir el archivo PDF automáticamente después de generarlo
                System.Diagnostics.Process.Start(rutaArchivo);

               // MessageBox.Show("Recibo de nómina generado exitosamente en la carpeta Descargas.");
            
            }   

            //SaveFileDialog guardar = new SaveFileDialog();
            //guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";

            //// Cargar la plantilla HTML desde los recursos
            //string paginahtml_texto = Properties.Resources.plantilla.ToString();


            //using (SqlConnection cn = new SqlConnection(Conexion))
            //{
            //    cn.Open();

            //    // 1. Datos de la Empresa
            //    string queryEmpresa = "SELECT Nombre, RazonFiscal, DomicilioFiscal, Telefono, RegistroPatronal, RFC, FechaInOperaciones FROM Empresa";
            //    SqlCommand cmdEmpresa = new SqlCommand(queryEmpresa, cn);
            //    SqlDataReader readerEmpresa = cmdEmpresa.ExecuteReader();

            //    if (readerEmpresa.Read())
            //    {
            //        paginahtml_texto = paginahtml_texto.Replace("@MES_NOMINA", combo_Mes.Text);
            //        paginahtml_texto = paginahtml_texto.Replace("@ANO_NOMINA", txt_Ano_RecPEmpleado.Text);
            //        paginahtml_texto = paginahtml_texto.Replace("@NOMBRE_EMPRESA", readerEmpresa["Nombre"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@RAZON_SOCIAL", readerEmpresa["RazonFiscal"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@DOMICILIO_FISCAL", readerEmpresa["DomicilioFiscal"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@TELEFONO_EMPRESA", readerEmpresa["Telefono"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@REGISTRO_PATRONAL", readerEmpresa["RegistroPatronal"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@RFC_EMPRESA", readerEmpresa["RFC"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@FECHA_INICIO_OPERACIONES", Convert.ToDateTime(readerEmpresa["FechaInOperaciones"]).ToString("dd/MM/yyyy"));
            //    }
            //    readerEmpresa.Close();

            //    // 2. Datos del Empleado
            //    int numeroEmpleado = int.Parse(txt_NumEmpleado_RecPEmpleado.Text);
            //    string queryEmpleado = @"
            //SELECT e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, e.RFC, e.Curp, e.NSS, e.FechaIngresoEmpresa, 
            //       d.NombreDepartamento, p.NombrePuesto, e.SalarioDiario, e.Banco, e.NumeroCuenta
            //FROM Empleado e
            //JOIN Departamento d ON e.id_Departamento = d.id_Departamento
            //JOIN Puestos p ON e.id_Puesto = p.id_Puesto
            //WHERE e.id_Empleado = @idEmpleado";

            //    SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
            //    cmdEmpleado.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
            //    SqlDataReader readerEmpleado = cmdEmpleado.ExecuteReader();

            //    decimal salarioDiario = 0;
            //    if (readerEmpleado.Read())
            //    {
            //        paginahtml_texto = paginahtml_texto.Replace("@NOMBRE_EMPLEADO", $"{readerEmpleado["NombreEmpleado"]} {readerEmpleado["ApelPaternoEmpleado"]} {readerEmpleado["ApelMaternoEmpleado"]}");
            //        paginahtml_texto = paginahtml_texto.Replace("@NUMERO_EMPLEADO", numeroEmpleado.ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@RFC_EMPLEADO", readerEmpleado["RFC"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@CURP_EMPLEADO", readerEmpleado["Curp"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@IMSS_EMPLEADO", readerEmpleado["NSS"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@FECHA_INGRESO", Convert.ToDateTime(readerEmpleado["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy"));
            //        paginahtml_texto = paginahtml_texto.Replace("@DEPARTAMENTO", readerEmpleado["NombreDepartamento"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@PUESTO", readerEmpleado["NombrePuesto"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@BANCO", readerEmpleado["Banco"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@NUMERO_CUENTA", readerEmpleado["NumeroCuenta"].ToString());
            //        paginahtml_texto = paginahtml_texto.Replace("@SALARIO_DIARIO", Convert.ToDecimal(readerEmpleado["SalarioDiario"]).ToString("C"));
            //        salarioDiario = Convert.ToDecimal(readerEmpleado["SalarioDiario"]);
            //    }
            //    readerEmpleado.Close();

            //    string mes = combo_Mes.Text;
            //    int ano = int.Parse(txt_Ano_RecPEmpleado.Text);

            //    // 3. Obtener datos de la nómina individual
            //    decimal sueldoBruto = 0, sueldoNeto = 0, isr = 0, imss = 0, totalDeducciones = 0, totalPercepciones = 0;
            //    int diasTrabajados = 0;

            //    string queryNomina = @"
            //SELECT DiasTrabajados, SueldoBruto, SueldoNeto, ISR, IMSS, totalDeducciones, totalPercepciones
            //FROM NominaIndividual
            //WHERE idEmpleadoFK = @idEmpleado AND Mes = @mes AND Ano = @ano";

            //    SqlCommand cmdNomina = new SqlCommand(queryNomina, cn);
            //    cmdNomina.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
            //    cmdNomina.Parameters.AddWithValue("@mes", mes);
            //    cmdNomina.Parameters.AddWithValue("@ano", ano);
            //    SqlDataReader readerNomina = cmdNomina.ExecuteReader();

            //    if (readerNomina.Read())
            //    {
            //        diasTrabajados = Convert.ToInt32(readerNomina["DiasTrabajados"]);
            //        sueldoBruto = Convert.ToDecimal(readerNomina["SueldoBruto"]);
            //        sueldoNeto = Convert.ToDecimal(readerNomina["SueldoNeto"]);
            //        isr = Convert.ToDecimal(readerNomina["ISR"]);
            //        imss = Convert.ToDecimal(readerNomina["IMSS"]);
            //        totalDeducciones = Convert.ToDecimal(readerNomina["totalDeducciones"]);
            //        totalPercepciones = Convert.ToDecimal(readerNomina["totalPercepciones"]);
            //    }
            //    readerNomina.Close();

            //    // Calcular faltas en base a los días trabajados
            //    int faltas = 30 - diasTrabajados;
            //    decimal deduccionFaltas = faltas * salarioDiario;

            //    // Reemplazar los valores en la plantilla
            //    paginahtml_texto = paginahtml_texto.Replace("@DIAS_TRABAJADOS", diasTrabajados.ToString());
            //    paginahtml_texto = paginahtml_texto.Replace("@TOTAL_NETO", sueldoNeto.ToString("C"));
            //    paginahtml_texto = paginahtml_texto.Replace("@SUELDO_BRUTO", sueldoBruto.ToString("C"));
            //    paginahtml_texto = paginahtml_texto.Replace("@TOTAL_DEDUCCIONES", totalDeducciones.ToString("C"));
            //    paginahtml_texto = paginahtml_texto.Replace("@TOTAL_PERCEPCIONES", totalPercepciones.ToString("C"));

            //    // 4. Generar tabla de deducciones
            //    string deduccionesHtml = $"<tr><td>Falta</td><td>Faltas</td><td>{deduccionFaltas:C}</td></tr>";
            //    deduccionesHtml += $"<tr><td>ISR</td><td>ISR</td><td>{isr:C}</td></tr>";
            //    deduccionesHtml += $"<tr><td>IMSS</td><td>IMSS</td><td>{imss:C}</td></tr>";

            //    // Reemplazar la tabla de deducciones en la plantilla
            //    paginahtml_texto = paginahtml_texto.Replace("@FILAS_DEDUCCIONES", deduccionesHtml);

            //    // 5. Generar tabla de percepciones
            //    string percepcionesHtml = "";
            //    string queryPercepciones = @"
            //SELECT dp.id_PD, dp.Nombre_PD, COALESCE(dp.MontoPD, 0) AS MontoPD, COALESCE(dp.Porcentaje_PD, 0) AS Porcentaje_PD
            //FROM DEDPERNOMINA dpn
            //JOIN DeduccionesPercepciones dp ON dpn.id_PD = dp.id_PD
            //WHERE dpn.id_Empleado = @idEmpleado AND dpn.Mes = @mes AND dpn.Ano = @ano AND dp.D_P = 'Percepción'";

            //    SqlCommand cmdPercepciones = new SqlCommand(queryPercepciones, cn);
            //    cmdPercepciones.Parameters.AddWithValue("@idEmpleado", numeroEmpleado);
            //    cmdPercepciones.Parameters.AddWithValue("@mes", mes);
            //    cmdPercepciones.Parameters.AddWithValue("@ano", ano);
            //    SqlDataReader readerPercepciones = cmdPercepciones.ExecuteReader();

            //    while (readerPercepciones.Read())
            //    {
            //        string clave = readerPercepciones["id_PD"].ToString();
            //        string concepto = readerPercepciones["Nombre_PD"].ToString();
            //        decimal monto = Convert.ToDecimal(readerPercepciones["MontoPD"]);
            //        decimal porcentaje = Convert.ToDecimal(readerPercepciones["Porcentaje_PD"]);
            //        decimal importe = monto > 0 ? monto : (sueldoBruto * porcentaje / 100);

            //        percepcionesHtml += $"<tr><td>{clave}</td><td>{concepto}</td><td>{importe:C}</td></tr>";
            //    }
            //    readerPercepciones.Close();

            //    // Reemplazar la tabla de percepciones en la plantilla
            //    paginahtml_texto = paginahtml_texto.Replace("@FILAS_PERCEPCIONES", percepcionesHtml);

            //    // Generar el PDF con los datos completados
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

            //        MessageBox.Show("Recibo de nómina generado exitosamente.");
            //    }
            //}
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

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    imprimirr(sender,e);
        //}

        private void txt_SalarioDiario_RecPEmpleado_TextChanged(object sender, EventArgs e)
        {

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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txt_NumEmpleadoMostrar_RecPEmpleado_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
