// --- ¡NUEVOS USINGS PARA PDF! ---
// Necesitas instalar el paquete NuGet "itext7"
//using iText.Kernel.Pdf;
//using iText.Layout;
//using iText.Layout.Element;
//using iText.Layout.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml.html.table;
using NominaMAD.Entidad;
using NominaMAD.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// --- FIN DE USINGS PARA PDF ---


namespace NominaMAD
{
    public partial class P_GenerarNomina : Form
    {
        // --- Variables Globales ---

        // Almacenará la info de la tabla Conceptos para cálculos rápidos
        private DataTable dtConceptosGlobal;
        // Almacenará los IDs de ISR e IMSS para cálculos especiales
        private int idConceptoISR = 0;
        private int idConceptoIMSS = 0;

        // --- NUEVAS VARIABLES PARA BONOS ---
        private int idConceptoBonoPuntualidad = 0;
        private int idConceptoBonoProductividad = 0;


        public P_GenerarNomina()
        {
            InitializeComponent();
        }

        private void P_GenerarNomina_Load(object sender, EventArgs e)
        {
            DateTime_Periodo.Format = DateTimePickerFormat.Custom;
            DateTime_Periodo.CustomFormat = "MMMM/yyyy";
            DateTime_Periodo.ShowUpDown = true;

            // Llamar al ValueChanged para cargar la matriz con la lógica de bloqueo
            DateTime_Periodo_ValueChanged(null, null);
        }

        /// <summary>
        /// Lógica de carga principal. Se llama al inicio y cada vez que cambia el período.
        /// Verifica si el período está cerrado y carga/calcula según corresponda.
        /// </summary>
        private void CargarMatriz(bool periodoEstaCerrado)
        {
            // 1. Obtener empleados y conceptos
            DataTable dtEmpleados = new DataTable();
            DataTable dtConceptos = new DataTable();
            DateTime periodoSeleccionado = new DateTime(DateTime_Periodo.Value.Year, DateTime_Periodo.Value.Month, 1);

            using (SqlConnection conn = BD_Conexion.ObtenerConexion())
            {
                // --- Empleados ---
                string queryEmp = @"
                    SELECT 
                        e.ID_Empleado,
                        CONCAT(e.Nombre, ' ', e.ApellidoPaterno, ' ', e.ApellidoMaterno) AS Empleado,
                        e.SalarioDiario,
                        CAST(e.estatus AS BIT) AS estatus
                    FROM Empleado e";

                new SqlDataAdapter(queryEmp, conn).Fill(dtEmpleados);

                // --- Conceptos (¡Query CORREGIDA para traer todos los datos!) ---
                string queryCon = "SELECT ID_Conceptos, nombre, General, Tipo, EsPorcetanje, Valor FROM Conceptos WHERE estatus = 1";
                new SqlDataAdapter(queryCon, conn).Fill(dtConceptos);
            }

            // Guardar conceptos globalmente
            this.dtConceptosGlobal = dtConceptos;

            // Buscar y almacenar IDs de ISR e IMSS
            var rowISR = dtConceptos.AsEnumerable().FirstOrDefault(r => r.Field<string>("nombre").ToUpper() == "ISR");
            var rowIMSS = dtConceptos.AsEnumerable().FirstOrDefault(r => r.Field<string>("nombre").ToUpper() == "IMSS");

            if (rowISR != null) idConceptoISR = rowISR.Field<int>("ID_Conceptos");
            if (rowIMSS != null) idConceptoIMSS = rowIMSS.Field<int>("ID_Conceptos");

            if ((idConceptoISR == 0 || idConceptoIMSS == 0) && !periodoEstaCerrado)
            {
                MessageBox.Show("Error: No se encontraron los conceptos 'ISR' e 'IMSS' en la BD. Son necesarios para el cálculo.", "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btn_CierrePeriodo_GenerarNomina.Enabled = false;
            }

            // --- BUSCAR IDs DE BONOS ---
            // ¡¡IMPORTANTE: Asegúrate que estos nombres coincidan EXACTO con tu BD!!
            var rowBonoP = dtConceptos.AsEnumerable().FirstOrDefault(r => r.Field<string>("nombre").ToUpper() == "BONO DE PUNTUALIDAD");
            var rowBonoProd = dtConceptos.AsEnumerable().FirstOrDefault(r => r.Field<string>("nombre").ToUpper() == "BONO DE PRODUCTIVIDAD");

            if (rowBonoP != null) idConceptoBonoPuntualidad = rowBonoP.Field<int>("ID_Conceptos");
            if (rowBonoProd != null) idConceptoBonoProductividad = rowBonoProd.Field<int>("ID_Conceptos");


            // 2. Limpiar DataGrid
            dtgv_Matriz_GenerarNomina.DataSource = null;
            dtgv_Matriz_GenerarNomina.Columns.Clear();
            dtgv_Matriz_GenerarNomina.Rows.Clear();
            dtgv_Matriz_GenerarNomina.AllowUserToAddRows = false;

            // 3. Crear columnas fijas
            dtgv_Matriz_GenerarNomina.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Empleado",
                HeaderText = "Empleado",
                ReadOnly = true,
                Width = 200
            });

            dtgv_Matriz_GenerarNomina.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SalarioDiario",
                HeaderText = "SD",
                ReadOnly = true,
                Width = 120
            });

            // --- AÑADIR COLUMNA "FALTAS" ---
            dtgv_Matriz_GenerarNomina.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Faltas",
                HeaderText = "Faltas",
                ReadOnly = periodoEstaCerrado, // Editable solo si el período está abierto
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });


            // Columna de Salario Final (Neto)
            dtgv_Matriz_GenerarNomina.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SalarioNeto",
                HeaderText = "Salario Neto",
                ReadOnly = true,
                Width = 120
            });

            // 4. Agregar conceptos como columnas checkbox
            foreach (DataRow con in dtConceptos.Rows)
            {
                dtgv_Matriz_GenerarNomina.Columns.Add(new DataGridViewCheckBoxColumn()
                {
                    Name = con["ID_Conceptos"].ToString(),
                    HeaderText = con["nombre"].ToString()
                });
            }

            // 5. Filtrar empleados activos
            var activos = dtEmpleados.AsEnumerable().Where(r => r.Field<bool>("estatus") == true);

            // 6. Agregar filas
            foreach (var emp in activos)
            {
                decimal sd = emp.Field<decimal>("SalarioDiario");
                int empID = emp.Field<int>("ID_Empleado");

                int rowIndex = dtgv_Matriz_GenerarNomina.Rows.Add(
                    emp["Empleado"].ToString(),
                    sd.ToString("0.00"),
                    "0", // Valor default para Faltas
                    "0.00" // Placeholder para Neto
                );

                DataGridViewRow newRow = dtgv_Matriz_GenerarNomina.Rows[rowIndex];
                // ¡¡IMPORTANTE: Guardar el ID_Empleado en el Tag de la fila!!
                newRow.Tag = (int)empID; // Guardamos el ID temporalmente

                if (periodoEstaCerrado)
                {
                    // --- LÓGICA PARA PERÍODO CERRADO ---
                    // Cargar los datos guardados y bloquear la fila
                    LoadDataForClosedRow(newRow, empID, periodoSeleccionado);
                }
                else
                {
                    // --- LÓGICA PARA PERÍODO ABIERTO ---
                    // 7. MARCAR Y BLOQUEAR LOS CONCEPTOS GENERALES
                    foreach (DataRow con in dtConceptos.Rows)
                    {
                        bool esGeneral = Convert.ToBoolean(con["General"]);
                        string colName = con["ID_Conceptos"].ToString();
                        if (esGeneral)
                        {
                            newRow.Cells[colName].Value = true;
                            newRow.Cells[colName].ReadOnly = true;
                        }
                    }
                    // 8. Calcular el salario
                    RecalcularSalarioFila(newRow);
                }
            }

            // 9. Bloquear toda la cuadrícula si el período está cerrado
            // (La columna Faltas ya se configuró como ReadOnly si está cerrado)
            if (periodoEstaCerrado)
            {
                dtgv_Matriz_GenerarNomina.ReadOnly = true;
            }
        }

        /// <summary>
        /// Carga los datos de una nómina YA CERRADA en una fila del grid.
        /// </summary>
        private void LoadDataForClosedRow(DataGridViewRow row, int empleadoID, DateTime periodo)
        {
            CalculoNominaRow calculo = new CalculoNominaRow
            {
                Importes = new Dictionary<int, decimal>()
            };

            using (SqlConnection conn = BD_Conexion.ObtenerConexion())
            {
                
                // --- QUERY ACTUALIZADA PARA TRAER DiasTrabajados ---
                string query = @"
                    SELECT 
                        N.ID_Nomina, 
                        N.DiasTrabajados,
                        ND.ConceptosID, 
                        ND.Importe, 
                        ND.SueldoBruto, 
                        ND.SueldoNeto 
                    FROM Nomina N
                    JOIN NominaDetalle ND ON N.ID_Nomina = ND.NominaID
                    WHERE N.EmpleadoID = @EmpleadoID AND N.Periodo = @Periodo AND N.Cerrado = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmpleadoID", empleadoID);
                    cmd.Parameters.AddWithValue("@Periodo", periodo);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            // El empleado no tuvo nómina en este período cerrado
                            row.Cells["SalarioNeto"].Value = "N/A";
                            row.Cells["Faltas"].Value = "N/A";
                            return;
                        }

                        while (reader.Read())
                        {
                            // Solo necesitamos SueldoNeto, Bruto y Faltas una vez
                            if (calculo.SueldoNeto == 0)
                            {
                                calculo.SueldoNeto = (decimal)reader["SueldoNeto"];
                                calculo.SueldoBruto = (decimal)reader["SueldoBruto"];
                                row.Cells["SalarioNeto"].Value = calculo.SueldoNeto.ToString("0.00");

                                // --- CALCULAR FALTAS ---
                                int diasMes = DateTime.DaysInMonth(periodo.Year, periodo.Month);
                                int diasTrabajados = (int)reader["DiasTrabajados"];
                                int faltas = diasMes - diasTrabajados;
                                row.Cells["Faltas"].Value = faltas.ToString();
                            }

                            int conceptoID = (int)reader["ConceptosID"];
                            decimal importe = (decimal)reader["Importe"];

                            // Guardar en el diccionario para el PDF
                            if (!calculo.Importes.ContainsKey(conceptoID))
                            {
                                calculo.Importes.Add(conceptoID, importe);
                            }

                            // Marcar el checkbox en el grid
                            string colName = conceptoID.ToString();
                            if (dtgv_Matriz_GenerarNomina.Columns.Contains(colName))
                            {
                                row.Cells[colName].Value = true;
                            }
                        }
                    }
                }
            }

            // Guardar los datos cargados en el Tag para el PDF
            row.Tag = new NominaRowData
            {
                EmpleadoID = empleadoID,
                Calculo = calculo
            };
        }


        /// <summary>
        /// Recalcula el salario neto para TODAS las filas del DataGridView.
        /// </summary>
        private void RecalcularTodosLosSalarios()
        {
            foreach (DataGridViewRow row in dtgv_Matriz_GenerarNomina.Rows)
            {
                RecalcularSalarioFila(row);
            }
        }

        /// <summary>
        /// Lógica principal para calcular el salario de UNA fila.
        /// </summary>
        private void RecalcularSalarioFila(DataGridViewRow row)
        {
            if (row == null || this.dtConceptosGlobal == null || (idConceptoISR == 0 || idConceptoIMSS == 0))
                return;

            // Asegurarse que Faltas no sea nulo
            if (row.Cells["Faltas"].Value == null || string.IsNullOrEmpty(row.Cells["Faltas"].Value.ToString()))
            {
                row.Cells["Faltas"].Value = "0";
            }

            // Recuperar el ID_Empleado original del Tag (antes de que lo reemplacemos)
            int empleadoID;
            if (row.Tag is int)
            {
                empleadoID = (int)row.Tag;
            }
            else if (row.Tag is NominaRowData)
            {
                empleadoID = ((NominaRowData)row.Tag).EmpleadoID;
            }
            else
            {
                return; // Tag no reconocido
            }


            try
            {
                // --- PASO 0: OBTENER DÍAS TRABAJADOS ---
                decimal sd = Convert.ToDecimal(row.Cells["SalarioDiario"].Value);
                int diasMes = DateTime.DaysInMonth(DateTime_Periodo.Value.Year, DateTime_Periodo.Value.Month);
                int faltas = 0;
                int.TryParse(row.Cells["Faltas"].Value.ToString(), out faltas);

                int diasPagados = diasMes - faltas;
                if (diasPagados < 0) diasPagados = 0;

                // --- NUEVO SALARIO BASE PARA CÁLCULO ---
                decimal sm_base_calculo = sd * diasPagados;

                decimal totalPercepciones = sm_base_calculo; // Empezar con el sueldo ya ajustado
                decimal totalDeducciones = 0;

                // Almacenará el importe final de cada concepto (ID, Importe)
                Dictionary<int, decimal> importesFinales = new Dictionary<int, decimal>();

                // --- PASO 1: Calcular percepciones y deducciones (excepto ISR/IMSS) ---
                // Iniciar en la columna 4 (0=Emp, 1=SD, 2=Faltas, 3=Neto)
                for (int i = 4; i < dtgv_Matriz_GenerarNomina.Columns.Count; i++)
                {
                    DataGridViewCheckBoxCell checkCell = row.Cells[i] as DataGridViewCheckBoxCell;
                    bool isChecked = (checkCell.Value != null && (bool)checkCell.Value);

                    if (!isChecked) continue; // Si no está marcado, saltar

                    int conceptoID = Convert.ToInt32(dtgv_Matriz_GenerarNomina.Columns[i].Name);

                    // Saltar ISR e IMSS en este paso
                    if (conceptoID == idConceptoISR || conceptoID == idConceptoIMSS) continue;

                    DataRow con = dtConceptosGlobal.AsEnumerable().First(r => r.Field<int>("ID_Conceptos") == conceptoID);

                    bool esPercepcion = con.Field<bool>("Tipo");
                    bool esPorcentaje = con.Field<bool>("EsPorcetanje"); // Cuidado con el typo de la BD
                    decimal valor = con.Field<decimal>("Valor");

                    // --- CÁLCULO DE IMPORTE BASADO EN DÍAS PAGADOS ---
                    decimal importe = (esPorcentaje) ? (sm_base_calculo * (valor / 100)) : valor;

                    importesFinales.Add(conceptoID, importe); // Guardar importe

                    if (esPercepcion)
                        totalPercepciones += importe;
                    else
                        totalDeducciones += importe;
                }

                // --- PASO 2: Calcular Sueldo Bruto y Deducciones Estatutarias ---
                decimal sueldoBruto = totalPercepciones;

                // IMSS se calcula sobre el SD y los días del mes (no los días pagados, es base mensual)
                decimal importeIMSS_Calculado = CalcularIMSS(sd, diasMes);
                decimal importeISR_Calculado = CalcularISR(sueldoBruto); // ISR se basa en el Bruto TOTAL

                // --- PASO 3: Añadir deducciones estatutarias al total ---
                totalDeducciones += importeIMSS_Calculado;
                totalDeducciones += importeISR_Calculado;

                // Guardar importes de ISR/IMSS (son "General", siempre están)
                importesFinales.Add(idConceptoIMSS, importeIMSS_Calculado);
                importesFinales.Add(idConceptoISR, importeISR_Calculado);

                // --- Cálculo Final ---
                decimal sueldoNeto = sueldoBruto - totalDeducciones;

                // Actualizar la celda "Salario Neto"
                row.Cells["SalarioNeto"].Value = sueldoNeto.ToString("0.00");

                // --- Guardar TODO en el Tag para el botón "Cerrar Período" ---
                NominaRowData newTag = new NominaRowData
                {
                    EmpleadoID = empleadoID, // Usar el ID guardado
                    Calculo = new CalculoNominaRow
                    {
                        SueldoBruto = sueldoBruto,
                        SueldoNeto = sueldoNeto,
                        Importes = importesFinales
                    }
                };
                row.Tag = newTag; // Reemplazar el Tag con el objeto completo
            }
            catch (Exception ex)
            {
                row.Cells["SalarioNeto"].Value = "Error";
                System.Diagnostics.Debug.WriteLine("Error recalculando fila: " + ex.Message);
            }
        }

        /// <summary>
        /// Revisa las faltas y bloquea/desbloquea los bonos correspondientes.
        /// </summary>
        private void AplicarLogicaBonosPorFaltas(DataGridViewRow row, int faltas)
        {
            if (dtgv_Matriz_GenerarNomina.ReadOnly) return; // No hacer nada si el período está cerrado

            bool negarBonos = (faltas > 2);

            // --- Bono Puntualidad ---
            if (idConceptoBonoPuntualidad > 0)
            {
                string colName = idConceptoBonoPuntualidad.ToString();
                if (dtgv_Matriz_GenerarNomina.Columns.Contains(colName))
                {
                    var cell = row.Cells[colName];
                    cell.ReadOnly = negarBonos;
                    if (negarBonos)
                    {
                        cell.Value = false; // Desmarcar si se niega
                    }
                }
            }

            // --- Bono Productividad ---
            if (idConceptoBonoProductividad > 0)
            {
                string colName = idConceptoBonoProductividad.ToString();
                if (dtgv_Matriz_GenerarNomina.Columns.Contains(colName))
                {
                    var cell = row.Cells[colName];
                    cell.ReadOnly = negarBonos;
                    if (negarBonos)
                    {
                        cell.Value = false; // Desmarcar si se niega
                    }
                }
            }
        }


        // --- BOTÓN GENERAR NOMINA INDIVIDUAL (PDF) ---
        private void btn_GenerarNominaInd_GenerarNomina_Click(object sender, EventArgs e)
        {
            // 1. Verificar si hay una fila seleccionada
            if (dtgv_Matriz_GenerarNomina.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un empleado de la lista.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Obtener los datos del Tag de la fila
            DataGridViewRow selectedRow = dtgv_Matriz_GenerarNomina.CurrentRow;
            if (!(selectedRow.Tag is NominaRowData rowData))
            {
                MessageBox.Show("No se pudieron cargar los datos de cálculo para este empleado.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Obtener datos básicos
            string empleadoName = selectedRow.Cells["Empleado"].Value.ToString();
            string faltas = selectedRow.Cells["Faltas"].Value.ToString();
            DateTime periodo = DateTime_Periodo.Value;
            CalculoNominaRow calculo = rowData.Calculo;

            //// 4. Preguntar dónde guardar el archivo
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Archivo PDF (*.pdf)|*.pdf";
            //sfd.Title = "Guardar Recibo de Nómina";
            //sfd.FileName = $"Recibo_{empleadoName.Replace(' ', '_')}_{periodo:yyyyMM}.pdf";

            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        // 5. Generar el PDF usando iText 7
            //        using (PdfWriter writer = new PdfWriter(sfd.FileName))
            //        {
            //            using (PdfDocument pdf = new PdfDocument(writer))
            //            {
            //                using (Document document = new Document(pdf))
            //                {
            //                    // --- Encabezado ---
            //                    document.Add(new Paragraph("Recibo de Nómina")
            //                        .SetTextAlignment(TextAlignment.CENTER)
            //                        .SetFontSize(20)
            //                        .SetBold());

            //                    document.Add(new Paragraph($"Empleado: {empleadoName}"));
            //                    document.Add(new Paragraph($"Período: {periodo:MMMM 'de' yyyy}"));
            //                    document.Add(new Paragraph($"Salario Diario: {selectedRow.Cells["SalarioDiario"].Value}"));
            //                    document.Add(new Paragraph($"Faltas: {faltas}")); // Añadir faltas al PDF
            //                    document.Add(new Paragraph(" ")); // Espacio

            //                    // --- Tablas de Percepciones y Deducciones ---
            //                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1 })).UseAllAvailableWidth();

            //                    // Encabezados de tabla
            //                    table.AddHeaderCell(new Cell().Add(new Paragraph("PERCEPCIONES").SetBold()));
            //                    table.AddHeaderCell(new Cell().Add(new Paragraph("DEDUCCIONES").SetBold()));

            //                    // --- Listas para separar ---
            //                    List<string> percepciones = new List<string>();
            //                    List<string> deducciones = new List<string>();
            //                    decimal totalPercepciones = 0;
            //                    decimal totalDeducciones = 0;

            //                    // --- CÁLCULO DE SALARIO PAGADO (BASE) ---
            //                    decimal sd = Convert.ToDecimal(selectedRow.Cells["SalarioDiario"].Value);
            //                    int diasMes = DateTime.DaysInMonth(periodo.Year, periodo.Month);
            //                    int numFaltas = Convert.ToInt32(faltas);
            //                    int diasPagados = diasMes - numFaltas;
            //                    if (diasPagados < 0) diasPagados = 0;
            //                    decimal sm_pagado = sd * diasPagados;

            //                    percepciones.Add($"Salario ({diasPagados} días): {sm_pagado:C}");
            //                    totalPercepciones = calculo.SueldoBruto; // El bruto ya incluye todas las percepciones

            //                    foreach (var kvp in calculo.Importes)
            //                    {
            //                        DataRow con = dtConceptosGlobal.AsEnumerable().FirstOrDefault(r => r.Field<int>("ID_Conceptos") == kvp.Key);
            //                        if (con == null) continue;

            //                        string nombre = con.Field<string>("nombre");
            //                        bool esPercepcion = con.Field<bool>("Tipo");
            //                        decimal importe = kvp.Value;

            //                        if (esPercepcion)
            //                        {
            //                            // No agregar Salario Mensual de nuevo (ya lo pusimos como 'Salario (x días)')
            //                            if (nombre.ToUpper() != "SALARIO MENSUAL")
            //                                percepciones.Add($"{nombre}: {importe:C}");
            //                        }
            //                        else
            //                        {
            //                            deducciones.Add($"{nombre}: {importe:C}");
            //                            totalDeducciones += importe;
            //                        }
            //                    }

            //                    // --- Llenar la tabla ---
            //                    int maxRows = Math.Max(percepciones.Count, deducciones.Count);
            //                    for (int i = 0; i < maxRows; i++)
            //                    {
            //                        table.AddCell(new Cell().Add(new Paragraph(i < percepciones.Count ? percepciones[i] : "")));
            //                        table.AddCell(new Cell().Add(new Paragraph(i < deducciones.Count ? deducciones[i] : "")));
            //                    }

            //                    document.Add(table);
            //                    document.Add(new Paragraph(" ")); // Espacio

            //                    // --- Totales ---
            //                    document.Add(new Paragraph($"Sueldo Bruto (Total Percepciones): {calculo.SueldoBruto:C}")
            //                        .SetTextAlignment(TextAlignment.RIGHT).SetBold());

            //                    document.Add(new Paragraph($"Total Deducciones: {totalDeducciones:C}")
            //                        .SetTextAlignment(TextAlignment.RIGHT).SetBold());

            //                    document.Add(new Paragraph($"SUELDO NETO: {calculo.SueldoNeto:C}")
            //                        .SetTextAlignment(TextAlignment.RIGHT).SetFontSize(14).SetBold());
            //                }
            //            }
            //        } // Fin de using (cierra el documento)

            //        MessageBox.Show("PDF generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    }
            //    catch (System.IO.FileNotFoundException ex)
            //    {
            //        // Error común si la librería no está
            //        MessageBox.Show("Error: No se encontró la librería iText 7 (itext.kernel.dll).\nAsegúrese de instalar el paquete NuGet 'itext7'.\n\nDetalle: " + ex.Message,
            //                        "Error de Librería", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Ocurrió un error al generar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }


        // --- BOTÓN DE CIERRE DE PERÍODO (LÓGICA DE GUARDADO) ---
        private void btn_CierrePeriodo_GenerarNomina_Click(object sender, EventArgs e)
        {
            // 1. Confirmar
            var confirm = MessageBox.Show("¿Está seguro de que desea cerrar el período? Esta acción guardará la nómina y no se podrá modificar.",
                                        "Confirmar Cierre de Período",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
                return;

            // 2. Obtener período
            DateTime periodo = new DateTime(DateTime_Periodo.Value.Year, DateTime_Periodo.Value.Month, 1);
            int diasMes = DateTime.DaysInMonth(periodo.Year, periodo.Month);

            // 3. Doble chequeo por si acaso
            if (IsPeriodoClosed(periodo))
            {
                MessageBox.Show("El período ya ha sido cerrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Volver a calcular todo para asegurar datos frescos antes de guardar
            RecalcularTodosLosSalarios();

            // 5. Iniciar Transacción
            using (SqlConnection conn = BD_Conexion.ObtenerConexion())
            {
                
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    // 6. Iterar CADA fila de la cuadrícula
                    foreach (DataGridViewRow row in dtgv_Matriz_GenerarNomina.Rows)
                    {
                        // Obtener los datos calculados guardados en el Tag
                        if (!(row.Tag is NominaRowData rowData))
                        {
                            throw new Exception("Error interno: Los datos de cálculo no se encontraron para el empleado " + row.Cells["Empleado"].Value);
                        }

                        int empleadoID = rowData.EmpleadoID;
                        CalculoNominaRow calculo = rowData.Calculo;

                        // --- OBTENER DÍAS TRABAJADOS DESDE LA FILA ---
                        int faltas = 0;
                        int.TryParse(row.Cells["Faltas"].Value.ToString(), out faltas);
                        int diasTrabajados = diasMes - faltas;
                        if (diasTrabajados < 0) diasTrabajados = 0;


                        // --- 7. Crear registro en Nomina (Cabecera) ---
                        string queryNomina = @"
                            INSERT INTO Nomina (Periodo, Cerrado, DiasTrabajados, EmpleadoID)
                            OUTPUT INSERTED.ID_Nomina
                            VALUES (@Periodo, 1, @DiasTrabajados, @EmpleadoID)"; // Cerrado = 1

                        int nominaID;
                        using (SqlCommand cmdNomina = new SqlCommand(queryNomina, conn, tran))
                        {
                            cmdNomina.Parameters.AddWithValue("@Periodo", periodo);
                            cmdNomina.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados); // <-- VALOR CORREGIDO
                            cmdNomina.Parameters.AddWithValue("@EmpleadoID", empleadoID);

                            try
                            {
                                nominaID = (int)cmdNomina.ExecuteScalar();
                            }
                            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627) // Unique constraint
                            {
                                throw new Exception("Ya existe un registro de nómina para el empleado " + row.Cells["Empleado"].Value + " en este período. No se puede duplicar.");
                            }
                        }

                        // --- 8. Insertar NominaDetalle (por cada concepto) ---
                        string queryDetail = @"
                            INSERT INTO NominaDetalle (NominaID, ConceptosID, Importe, SueldoBruto, SueldoNeto)
                            VALUES (@NominaID, @ConceptosID, @Importe, @SueldoBruto, @SueldoNeto)";

                        // Iterar sobre CADA concepto calculado (percepciones, deducciones, ISR, IMSS)
                        foreach (var kvp in calculo.Importes)
                        {
                            int conceptoID = kvp.Key;
                            decimal importe = kvp.Value;

                            using (SqlCommand cmdDetail = new SqlCommand(queryDetail, conn, tran))
                            {
                                cmdDetail.Parameters.AddWithValue("@NominaID", nominaID);
                                cmdDetail.Parameters.AddWithValue("@ConceptosID", conceptoID);
                                cmdDetail.Parameters.AddWithValue("@Importe", importe);
                                // Guardar Bruto y Neto en cada fila de detalle (según tu schema)
                                cmdDetail.Parameters.AddWithValue("@SueldoBruto", calculo.SueldoBruto);
                                cmdDetail.Parameters.AddWithValue("@SueldoNeto", calculo.SueldoNeto);

                                cmdDetail.ExecuteNonQuery();
                            }
                        }
                    } // Fin del bucle de filas

                    // 9. Si todo salió bien, confirmar la transacción
                    tran.Commit();

                    MessageBox.Show("Período cerrado y nómina generada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 10. Bloquear controles
                    dtgv_Matriz_GenerarNomina.ReadOnly = true;
                    btn_CierrePeriodo_GenerarNomina.Enabled = false;

                }
                catch (Exception ex)
                {
                    // 11. Si algo falló, revertir la transacción
                    tran.Rollback();
                    MessageBox.Show("Error al cerrar el período: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // Fin del using (cierra la conexión)
        }


        private decimal CalcularISR(decimal sueldoBruto)
        {
            // (Tu lógica de CalcularISR completa va aquí)
            // ...
            decimal isr = 0;

            if (sueldoBruto >= 0.01m && sueldoBruto < 746.05m)
            {
                decimal excedente = sueldoBruto - 0.01m;
                isr = 0 + (excedente * 0.0192m);
            }
            else if (sueldoBruto >= 746.05m && sueldoBruto < 6332.06m)
            {
                decimal excedente = sueldoBruto - 746.05m;
                isr = 14.32m + (excedente * 0.0640m);
            }
            else if (sueldoBruto >= 6332.06m && sueldoBruto < 11128.02m)
            {
                decimal excedente = sueldoBruto - 6332.06m;
                isr = 371.83m + (excedente * 0.1088m);
            }
            // ... (Asegúrate de poner TODOS tus rangos de 'else if' aquí) ...
            else if (sueldoBruto >= 4511707.38m)
            {
                decimal excedente = sueldoBruto - 4511707.38m;
                isr = 1414947.85m + (excedente * 0.3500m);
            }


            return isr;
        }


        private decimal CalcularIMSS(decimal sdi, int dias)
        {
            // Esta es tu fórmula, la usamos como la proporcionaste.
            // Podría ser más compleja (topada, etc.) pero usamos tu lógica.
            return (sdi * dias) * 0.09m;
        }


        // --- EVENTOS DEL GRID PARA CÁLCULO DINÁMICO ---

        /// <summary>
        /// Se dispara cuando se hace clic en una celda de checkbox.
        /// </summary>
        private void dtgv_Matriz_GenerarNomina_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si la cuadrícula es de solo lectura (período cerrado), no hacer nada
            if (dtgv_Matriz_GenerarNomina.ReadOnly) return;

            // Ignorar clics en el encabezado
            if (e.RowIndex < 0) return;

            // Verificar si es una columna de checkbox
            if (dtgv_Matriz_GenerarNomina.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                // Si la celda NO es de solo lectura (o sea, no es "General" o "Bono Negado")
                if (!dtgv_Matriz_GenerarNomina.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
                {
                    // Forzar que el cambio se guarde inmediatamente
                    // Esto disparará el evento 'CellValueChanged'
                    dtgv_Matriz_GenerarNomina.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        /// <summary>
        /// Se dispara DESPUÉS de que el valor de una celda (el check o las faltas) ha cambiado.
        /// </summary>
        private void dtgv_Matriz_GenerarNomina_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Si la cuadrícula es de solo lectura (período cerrado), no hacer nada
            if (dtgv_Matriz_GenerarNomina.ReadOnly) return;

            // Ignorar cambios en el encabezado
            if (e.RowIndex < 0) return;

            string colName = dtgv_Matriz_GenerarNomina.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = dtgv_Matriz_GenerarNomina.Rows[e.RowIndex];

            if (colName == "Faltas")
            {
                // --- CAMBIO EN FALTAS ---

                // Validar que Faltas sea un número positivo
                int faltas = 0;
                if (row.Cells["Faltas"].Value == null || !int.TryParse(row.Cells["Faltas"].Value.ToString(), out faltas) || faltas < 0)
                {
                    row.Cells["Faltas"].Value = "0"; // Corregir si no es número o es negativo
                    faltas = 0;
                }

                // Aplicar lógica de bonos INMEDIATAMENTE
                AplicarLogicaBonosPorFaltas(row, faltas);

                // Recalcular salario (se re-calculará con los bonos ya actualizados)
                RecalcularSalarioFila(row);
            }
            else if (dtgv_Matriz_GenerarNomina.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                // --- CAMBIO EN CHECKBOX ---
                // Si un checkbox cambió, solo recalcular
                RecalcularSalarioFila(row);
            }
        }

        // --- NAVEGACIÓN Y OTROS ---

        private void btn_Regresar_GenerarNomina_Click(object sender, EventArgs e)
        {
            if (P_Inicio.MMenuAoE == 1)
            {
                P_Menu1 p_Menu1 = new P_Menu1();
                this.Hide();
                p_Menu1.ShowDialog();
            }
            else
            {
                if (P_Inicio.MMenuAoE == 2)
                {
                    P_Menu2 p_Menu2 = new P_Menu2();
                    this.Hide();
                    p_Menu2.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Verifica si el período seleccionado ya tiene una nómina CERRADA.
        /// </summary>
        private bool IsPeriodoClosed(DateTime periodo)
        {
            using (SqlConnection conn = BD_Conexion.ObtenerConexion())
            {
                
                // Buscar CUALQUIER registro de nómina en ese período que esté cerrado
                string query = "SELECT TOP 1 1 FROM Nomina WHERE Periodo = @Periodo AND Cerrado = 1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Periodo", new DateTime(periodo.Year, periodo.Month, 1));
                    return cmd.ExecuteScalar() != null; // Retorna true si encuentra algo
                }
            }
        }

        /// <summary>
        /// Se dispara al cambiar el mes/año en el DateTimePicker.
        /// </summary>
        private void DateTime_Periodo_ValueChanged(object sender, EventArgs e)
        {
            bool periodoCerrado = IsPeriodoClosed(DateTime_Periodo.Value);

            // Recargar la matriz para el nuevo período, indicando si está cerrado
            CargarMatriz(periodoCerrado);

            // Habilitar/Deshabilitar el botón de cierre
            btn_CierrePeriodo_GenerarNomina.Enabled = !periodoCerrado;

            // El botón de PDF individual SÍ debe funcionar en períodos cerrados
            btn_GenerarNominaInd_GenerarNomina.Enabled = true;

            if (periodoCerrado && sender != null) // 'sender != null' evita el msg al cargar el form
            {
                MessageBox.Show("Este período ya está cerrado. Solo se pueden consultar los datos.", "Período Cerrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    } // Fin de la clase P_GenerarNomina


    // --- Clases auxiliares para guardar los datos de cálculo en el Tag ---

    /// <summary>
    /// Objeto que se guardará en el Tag de cada DataGridViewRow.
    /// Contiene el ID del empleado y sus datos de cálculo.
    /// </summary>
    public class NominaRowData
    {
        public int EmpleadoID { get; set; }
        public CalculoNominaRow Calculo { get; set; }
    }

    /// <summary>
    /// Contiene los totales calculados y el desglose de importes.
    /// </summary>
    public class CalculoNominaRow
    {
        public decimal SueldoBruto { get; set; }
        public decimal SueldoNeto { get; set; }
        // Diccionario: Key = ConceptoID, Value = Importe calculado
        public Dictionary<int, decimal> Importes { get; set; }
    }
}