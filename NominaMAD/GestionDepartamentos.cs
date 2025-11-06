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
//using System.Data.SqlClient;

namespace NominaMAD
{
    public partial class P_GestionDepar : Form
    {
        private int ColumnaSeleccionada = 0;
        public P_GestionDepar()
        {
            InitializeComponent();
        }
        
        string Conexion = "Data Source=LUISMTZ\\SQLEXPRESS;Initial Catalog=Nomina;Integrated Security=True";
        string modificarOpcion;
        private void P_GestionDepar_Load(object sender, EventArgs e)
        {
            txt_Departamento_GestDepar.MaxLength = 20;
            //Que no se vea
            btn_Guardar_GestionDepar.Visible = false;
            btn_Modificar_GestionDepar.Visible = false;
            btn_limpiar_GestionDepar.Visible = false;
            btn_AceptarMod_GestionDepar.Visible = false;
            btn_CancelarMod_GestionDepar.Visible = false;

            btn_Agregar_GestionDepar.Visible = true;
            //Que no pueda escribir
            txt_Departamento_GestDepar.Enabled = false;
            //txt_SueldoBase_GestionDepar.Enabled = false;
            //txt_Empleados_GestionDepar.Enabled = false;

            //DataTable dt= new DataTable();
            //using(SqlConnection cn=new SqlConnection(Conexion))
            //{
            //    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Departamento",cn);
            //    da.SelectCommand.CommandType = CommandType.Text;
            //    cn.Open();
            //    da.Fill(dt);
            //    dtgv_GestionDepar.DataSource = dt;

            //}
            mostrarTablaDepart();
        }
        private void mostrarTablaDepart()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Departamento", cn);
                da.SelectCommand.CommandType = CommandType.Text;
                cn.Open();
                da.Fill(dt);
                dtgv_GestionDepar.DataSource = dt;

            }
        }
        bool existe = false;
        private void ValidarExistencia()
        {
            //bool existe = false;

            // Recorrer cada fila en el DataGridView
            foreach (DataGridViewRow fila in dtgv_GestionDepar.Rows)
            {
                // Comparar el valor de la columna que deseas verificar (NombreDepartamento en este caso)
                if (fila.Cells["NombreDepartamento"].Value != null &&
                    fila.Cells["NombreDepartamento"].Value.ToString().ToLower() == txt_Departamento_GestDepar.Text.ToLower())
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
        private void btn_Agregar_GestionDepar_Click(object sender, EventArgs e)
        {
          
                txt_Departamento_GestDepar.Text = "";
               // txt_SueldoBase_GestionDepar.Text = "";
               // txt_Empleados_GestionDepar.Text = "";
                //mostrar botones
                btn_Guardar_GestionDepar.Visible = true;
                btn_Modificar_GestionDepar.Visible = false;
                btn_limpiar_GestionDepar.Visible = true;
                btn_Agregar_GestionDepar.Visible = false;
                btn_AceptarMod_GestionDepar.Visible = false;
                btn_CancelarMod_GestionDepar.Visible = false;

                //habilitar txts
                txt_Departamento_GestDepar.Enabled = true;
               // txt_SueldoBase_GestionDepar.Enabled = true;
               // txt_Empleados_GestionDepar.Enabled = true;
           
        }

        private void btn_Guardar_GestionDepar_Click(object sender, EventArgs e)
        {
            if(txt_Departamento_GestDepar.Text=="" ){
                MessageBox.Show("Algun Dato Vacio");
            }
            else
            {
                ValidarExistencia();
                if ( existe == false)
                {
                    using (SqlConnection cn = new SqlConnection(Conexion))
                    {
                        //SqlCommand cmd = new SqlCommand("INSERT INTO Departamento(NombreDepartamento) VALUES ('" + txt_Departamento_GestDepar.Text + "'," + txt_SueldoBase_GestionDepar.Text + ")", cn);
                        SqlCommand cmd = new SqlCommand("INSERT INTO Departamento(NombreDepartamento) VALUES ('" + txt_Departamento_GestDepar.Text + "')", cn);

                        cmd.CommandType = CommandType.Text;
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        mostrarTablaDepart();
                    }

                }
                else { MessageBox.Show("Este departamento ya existe. Por favor, ingresa un departamento diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); existe = false; }
                //using (SqlConnection cn = new SqlConnection(Conexion))
                //{
                //    SqlCommand cmd = new SqlCommand("INSERT INTO Departamento(NombreDepartamento,SueldoBase) VALUES ('" + txt_Departamento_GestDepar.Text + "'," + txt_SueldoBase_GestionDepar.Text + ")", cn);
                //    cmd.CommandType = CommandType.Text;
                //    cn.Open();
                //    cmd.ExecuteNonQuery();
                //    mostrarTablaDepart();
                //}
            }
            //using (SqlConnection cn = new SqlConnection(Conexion))
            //{
            //    SqlCommand cmd = new SqlCommand("INSERT INTO Departamento(NombreDepartamento,SueldoBase) VALUES ('"+ txt_Departamento_GestDepar.Text + "',"+ txt_SueldoBase_GestionDepar.Text+")", cn);
            //    cmd.CommandType= CommandType.Text;
            //    cn.Open();
            //    cmd.ExecuteNonQuery();
            //    mostrarTablaDepart();
            //}

            ////limpia
            //txt_Departamento_GestDepar.Text="";
            //txt_SueldoBase_GestionDepar.Text = "";
            //txt_Empleados_GestionDepar.Text = "";

            ////oculta botones
            //btn_Guardar_GestionDepar.Visible = false;
            //btn_Modificar_GestionDepar.Visible = false;
            //btn_AceptarMod_GestionDepar.Visible = false;
            //btn_CancelarMod_GestionDepar.Visible = false;
            //btn_limpiar_GestionDepar.Visible = false;
            //btn_Agregar_GestionDepar.Visible = true;


            ////desabilita txt
            //txt_Departamento_GestDepar.Enabled = false;
            //txt_SueldoBase_GestionDepar.Enabled = false;
            //txt_Empleados_GestionDepar.Enabled = false;
        }

        private void btn_Modificar_GestionDepar_Click(object sender, EventArgs e)
        {
            // update Departamento set NombreDepartamento='zxc', SueldoBase = 123 where NombreDepartamento='vxcv'
            modificarOpcion = txt_Departamento_GestDepar.Text;
            txt_Departamento_GestDepar.Enabled = true;
           // txt_SueldoBase_GestionDepar.Enabled = true;
            //txt_Empleados_GestionDepar.Enabled = true;

            btn_Agregar_GestionDepar.Visible = false;

            btn_AceptarMod_GestionDepar.Visible = true;
            btn_CancelarMod_GestionDepar.Visible = true;
            btn_Modificar_GestionDepar.Visible = false;
        }

        private void btn_limpiar_GestionDepar_Click(object sender, EventArgs e)
        {
            txt_Departamento_GestDepar.Text = "";
           // txt_SueldoBase_GestionDepar.Text = "";
            //txt_Empleados_GestionDepar.Text = "";
            txt_Departamento_GestDepar.Enabled = false;
           // txt_SueldoBase_GestionDepar.Enabled = false;
          //  txt_Empleados_GestionDepar.Enabled = false;

            //oculta botones
            btn_Guardar_GestionDepar.Visible = false;
            btn_Modificar_GestionDepar.Visible = false;
            btn_AceptarMod_GestionDepar.Visible = false;
            btn_CancelarMod_GestionDepar.Visible = false;
            btn_limpiar_GestionDepar.Visible = false;
            btn_Agregar_GestionDepar.Visible = true;
        }
        private void eliminar()
        {

            // delete from Departamento where NombreDepartamento = 'asd'
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                SqlCommand cmd = new SqlCommand("delete from Departamento where NombreDepartamento = 'asd'", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                cmd.ExecuteNonQuery();
                mostrarTablaDepart();
            }
        }

        private void btn_AceptarMod_GestionDepar_Click(object sender, EventArgs e)
        {

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                //SqlCommand cmd = new SqlCommand("update Departamento set NombreDepartamento='" + txt_Departamento_GestDepar.Text + "', SueldoBase =" + txt_SueldoBase_GestionDepar.Text + " where NombreDepartamento='" + modificarOpcion + "'", cn);
                SqlCommand cmd = new SqlCommand("UPDATE Departamento SET NombreDepartamento = '" + txt_Departamento_GestDepar.Text + "' WHERE NombreDepartamento = '" + modificarOpcion + "'", cn);

                cmd.CommandType = CommandType.Text;
                cn.Open();
                cmd.ExecuteNonQuery();
                mostrarTablaDepart();
            }

            txt_Departamento_GestDepar.Text = "";
            //txt_SueldoBase_GestionDepar.Text = "";
           // txt_Empleados_GestionDepar.Text = "";
            txt_Departamento_GestDepar.Enabled = false;
           // txt_SueldoBase_GestionDepar.Enabled = false;
            //txt_Empleados_GestionDepar.Enabled = false;

            //oculta botones
            btn_Guardar_GestionDepar.Visible = false;
            btn_Modificar_GestionDepar.Visible = false;
            btn_AceptarMod_GestionDepar.Visible = false;
            btn_CancelarMod_GestionDepar.Visible = false;
            btn_limpiar_GestionDepar.Visible = false;
            btn_Agregar_GestionDepar.Visible = true;
            mostrarTablaDepart();
        }

        private void btn_CancelarMod_GestionDepar_Click(object sender, EventArgs e)
        {
            txt_Departamento_GestDepar.Text = "";
            //txt_SueldoBase_GestionDepar.Text = "";
            //txt_Empleados_GestionDepar.Text = "";
            txt_Departamento_GestDepar.Enabled = false;
            //txt_SueldoBase_GestionDepar.Enabled = false;
            ///txt_Empleados_GestionDepar.Enabled = false;

            //oculta botones
            btn_Guardar_GestionDepar.Visible = false;
            btn_Modificar_GestionDepar.Visible = false;
            btn_AceptarMod_GestionDepar.Visible = false;
            btn_CancelarMod_GestionDepar.Visible = false;
            btn_limpiar_GestionDepar.Visible = false;
            btn_Agregar_GestionDepar.Visible = true;
        }

        private void btn_Regresar_GestionDepar_Click(object sender, EventArgs e)
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

        private void dtgv_GestionDepar_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            ColumnaSeleccionada = e.RowIndex;

            if (ColumnaSeleccionada != -1)
            {
                //limpa txt
                txt_Departamento_GestDepar.Text = "";
               //txt_SueldoBase_GestionDepar.Text = "";
                //txt_Empleados_GestionDepar.Text = "";
                //ingresa datos en los txt
                //txt_Departamento_GestDepar.Text = (string)dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[0].Value;
                //txt_SueldoBase_GestionDepar.Text = (string)dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[1].Value;
                //txt_Empleados_GestionDepar.Text = (string)dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[2].Value;

                txt_Departamento_GestDepar.Text = dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[1].Value.ToString();
               // txt_SueldoBase_GestionDepar.Text = dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[2].Value.ToString();
               // txt_Empleados_GestionDepar.Text = dtgv_GestionDepar.Rows[ColumnaSeleccionada].Cells[2].Value.ToString();


                //desabilita txt
                txt_Departamento_GestDepar.Enabled = false;
                //txt_SueldoBase_GestionDepar.Enabled = false;
                //txt_Empleados_GestionDepar.Enabled = false;
                //muestra boton modificar y oculta los demas
                btn_Guardar_GestionDepar.Visible = false;
                btn_limpiar_GestionDepar.Visible = false;
                btn_Agregar_GestionDepar.Visible = true;
                btn_Modificar_GestionDepar.Visible = true;
                btn_AceptarMod_GestionDepar.Visible = false;
                btn_CancelarMod_GestionDepar.Visible = false;
            }
        }

        //private void btn_Imprimir_GestionDepar_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog guardar = new SaveFileDialog();
        //    guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_ReporteDepartamentos.pdf";
        //    string paginahtml_texto = Properties.Resources.Reporte_Departamentos.ToString();

        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        cn.Open();

        //        // 1. Obtener los datos de los departamentos y almacenarlos en una lista
        //        string queryDepartamentos = "SELECT id_Departamento, NombreDepartamento FROM Departamento";
        //        SqlCommand cmdDepartamentos = new SqlCommand(queryDepartamentos, cn);
        //        SqlDataReader readerDepartamentos = cmdDepartamentos.ExecuteReader();

        //        // Almacenar los departamentos en memoria
        //        var departamentos = new List<(string idDepartamento, string nombreDepartamento)>();

        //        while (readerDepartamentos.Read())
        //        {
        //            departamentos.Add((
        //                idDepartamento: readerDepartamentos["id_Departamento"].ToString(),
        //                nombreDepartamento: readerDepartamentos["NombreDepartamento"].ToString()
        //            ));
        //        }
        //        readerDepartamentos.Close(); // Cerrar el DataReader de departamentos aquí

        //        // 2. Generar el HTML para cada departamento en una sola tabla
        //        string departamentosHtml = "";

        //        foreach (var (idDepartamento, nombreDepartamento) in departamentos)
        //        {
        //            // Consulta para obtener los empleados activos en el departamento actual
        //            string queryEmpleados = @"
        //    SELECT e.id_Empleado, e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, 
        //           p.NombrePuesto, e.FechaIngresoEmpresa, e.SalarioDiario
        //    FROM Empleado e
        //    JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //    WHERE e.id_Departamento = @idDepartamento AND e.activo = 1"; // Filtra solo empleados activos

        //            SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn);
        //            cmdEmpleados.Parameters.AddWithValue("@idDepartamento", idDepartamento);

        //            SqlDataReader readerEmpleados = cmdEmpleados.ExecuteReader();

        //            // Contador de empleados activos para cada departamento
        //            int totalEmpleados = 0;
        //            string empleadosHtml = "";

        //            // Generar filas de empleados
        //            while (readerEmpleados.Read())
        //            {
        //                totalEmpleados++;
        //                string idEmpleado = readerEmpleados["id_Empleado"].ToString();
        //                string nombreEmpleado = $"{readerEmpleados["NombreEmpleado"]} {readerEmpleados["ApelPaternoEmpleado"]} {readerEmpleados["ApelMaternoEmpleado"]}";
        //                string puesto = readerEmpleados["NombrePuesto"].ToString();
        //                string fechaIngreso = Convert.ToDateTime(readerEmpleados["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy");
        //                decimal salarioDiario = Convert.ToDecimal(readerEmpleados["SalarioDiario"]);

        //                empleadosHtml += $@"
        //        <tr>
        //            <td>{idEmpleado}</td>
        //            <td>{nombreEmpleado}</td>
        //            <td>{puesto}</td>
        //            <td>{fechaIngreso}</td>
        //            <td>{salarioDiario:C}</td>
        //        </tr>";
        //            }
        //            readerEmpleados.Close(); // Cerrar el DataReader de empleados después de procesar cada departamento

        //            // Solo agrega la sección de un departamento si tiene empleados activos
        //            if (totalEmpleados > 0)
        //            {
        //                string departamentoHtml = $@"
        //        <tr class='department-header'>
        //            <td colspan='5'>Departamento: {nombreDepartamento}</td>
        //        </tr>
        //        <tr>
        //            <td><strong>ID Departamento:</strong> {idDepartamento}</td>
        //            <td colspan='4'><strong>Empleados en Departamento:</strong> {totalEmpleados}</td>
        //        </tr>
        //        <tr class='employee-header'>
        //            <th>ID Empleado</th>
        //            <th>Nombre</th>
        //            <th>Puesto</th>
        //            <th>Fecha de Ingreso</th>
        //            <th>Salario Diario</th>
        //        </tr>
        //        {empleadosHtml}";

        //                departamentosHtml += departamentoHtml;
        //            }
        //        }

        //        // Reemplazar la marca de lugar en la plantilla
        //        paginahtml_texto = paginahtml_texto.Replace("@MES_REPORTE", "Noviembre");
        //        paginahtml_texto = paginahtml_texto.Replace("@ANO_REPORTE", "2024");
        //        paginahtml_texto = paginahtml_texto.Replace("@DEPARTAMENTOS", departamentosHtml);
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

        //        MessageBox.Show("Reporte de departamentos generado exitosamente.");
        //    }
        //    //SaveFileDialog guardar = new SaveFileDialog();
        //    //guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_ReporteDepartamentos.pdf";
        //    //string paginahtml_texto = Properties.Resources.Reporte_Departamentos.ToString();

        //    //using (SqlConnection cn = new SqlConnection(Conexion))
        //    //{
        //    //    cn.Open();

        //    //    // 1. Obtener los datos de los departamentos y almacenarlos en una lista
        //    //    string queryDepartamentos = "SELECT id_Departamento, NombreDepartamento FROM Departamento";
        //    //    SqlCommand cmdDepartamentos = new SqlCommand(queryDepartamentos, cn);
        //    //    SqlDataReader readerDepartamentos = cmdDepartamentos.ExecuteReader();

        //    //    // Almacenar los departamentos en memoria
        //    //    var departamentos = new List<(string idDepartamento, string nombreDepartamento)>();

        //    //    while (readerDepartamentos.Read())
        //    //    {
        //    //        departamentos.Add((
        //    //            idDepartamento: readerDepartamentos["id_Departamento"].ToString(),
        //    //            nombreDepartamento: readerDepartamentos["NombreDepartamento"].ToString()
        //    //        ));
        //    //    }
        //    //    readerDepartamentos.Close(); // Cerrar el DataReader de departamentos aquí

        //    //    // 2. Generar el HTML para cada departamento en una sola tabla
        //    //    string departamentosHtml = "";

        //    //    foreach (var (idDepartamento, nombreDepartamento) in departamentos)
        //    //    {
        //    //        // Consulta para obtener los empleados en el departamento actual
        //    //        string queryEmpleados = @"
        //    //SELECT e.id_Empleado, e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, 
        //    //       p.NombrePuesto, e.FechaIngresoEmpresa, e.SalarioDiario
        //    //FROM Empleado e
        //    //JOIN Puestos p ON e.id_Puesto = p.id_Puesto
        //    //WHERE e.id_Departamento = @idDepartamento";

        //    //        SqlCommand cmdEmpleados = new SqlCommand(queryEmpleados, cn);
        //    //        cmdEmpleados.Parameters.AddWithValue("@idDepartamento", idDepartamento);

        //    //        SqlDataReader readerEmpleados = cmdEmpleados.ExecuteReader();

        //    //        // Contador de empleados para cada departamento
        //    //        int totalEmpleados = 0;
        //    //        string empleadosHtml = "";

        //    //        // Generar filas de empleados
        //    //        while (readerEmpleados.Read())
        //    //        {
        //    //            totalEmpleados++;
        //    //            string idEmpleado = readerEmpleados["id_Empleado"].ToString();
        //    //            string nombreEmpleado = $"{readerEmpleados["NombreEmpleado"]} {readerEmpleados["ApelPaternoEmpleado"]} {readerEmpleados["ApelMaternoEmpleado"]}";
        //    //            string puesto = readerEmpleados["NombrePuesto"].ToString();
        //    //            string fechaIngreso = Convert.ToDateTime(readerEmpleados["FechaIngresoEmpresa"]).ToString("dd/MM/yyyy");
        //    //            decimal salarioDiario = Convert.ToDecimal(readerEmpleados["SalarioDiario"]);

        //    //            empleadosHtml += $@"
        //    //        <tr>
        //    //            <td>{idEmpleado}</td>
        //    //            <td>{nombreEmpleado}</td>
        //    //            <td>{puesto}</td>
        //    //            <td>{fechaIngreso}</td>
        //    //            <td>{salarioDiario:C}</td>
        //    //        </tr>";
        //    //        }
        //    //        readerEmpleados.Close(); // Cerrar el DataReader de empleados después de procesar cada departamento

        //    //        // Encabezado para el departamento y sus empleados
        //    //        string departamentoHtml = $@"
        //    //    <tr class='department-header'>
        //    //        <td colspan='5'>Departamento: {nombreDepartamento}</td>
        //    //    </tr>
        //    //    <tr>
        //    //        <td><strong>ID Departamento:</strong> {idDepartamento}</td>
        //    //        <td colspan='4'><strong>Empleados en Departamento:</strong> {totalEmpleados}</td>
        //    //    </tr>
        //    //    <tr class='employee-header'>
        //    //        <th>ID Empleado</th>
        //    //        <th>Nombre</th>
        //    //        <th>Puesto</th>
        //    //        <th>Fecha de Ingreso</th>
        //    //        <th>Salario Diario</th>
        //    //    </tr>
        //    //    {empleadosHtml}";

        //    //        departamentosHtml += departamentoHtml;
        //    //    }

        //    //    // Reemplazar la marca de lugar en la plantilla
        //    //    paginahtml_texto = paginahtml_texto.Replace("@MES_REPORTE", "Noviembre");
        //    //    paginahtml_texto = paginahtml_texto.Replace("@ANO_REPORTE", "2024");
        //    //    paginahtml_texto = paginahtml_texto.Replace("@DEPARTAMENTOS", departamentosHtml);
        //    //}

        //    //// Generar el PDF con el contenido verificado
        //    //if (guardar.ShowDialog() == DialogResult.OK)
        //    //{
        //    //    using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
        //    //    {
        //    //        Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
        //    //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

        //    //        pdfDoc.Open();
        //    //        pdfDoc.Add(new Phrase(""));

        //    //        using (StringReader sr = new StringReader(paginahtml_texto))
        //    //        {
        //    //            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //    //        }

        //    //        pdfDoc.Close();
        //    //        stream.Close();
        //    //    }

        //    //    MessageBox.Show("Reporte de departamentos generado exitosamente.");
        //    //}
        //}

        private void txt_Departamento_GestDepar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
