using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaMAD.DAO
{
    public class helper
    {
        public class EmpleadoData
        {
            public int ID_Empleado { get; set; }
            public decimal SalarioDiario { get; set; }
            public decimal SalarioDiarioIntegrado { get; set; }
            public int DepID { get; set; }
            public int PuestoID { get; set; }
        }

        // Función para obtener los datos clave del empleado
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
                    return null; // Empleado no encontrado
                }
            }
        }

        // --- ¡¡ADVERTENCIA!! ---
        // --- ¡ESTAS FUNCIONES SON STUBS/DUMMIES! ---
        // Necesitas implementar la lógica fiscal real aquí.

        // Cálculo DUMMY de ISR. ¡Debes reemplazar esto!
        private decimal CalcularISR(decimal baseGravable)
        {
            // LÓGICA DE EJEMPLO: 15% de la base gravable
            // La lógica real usa tablas tarifarias (Art. 96 LISR)
            return baseGravable * 0.15m;
        }

        // Cálculo DUMMY de IMSS. ¡Debes reemplazar esto!
        private decimal CalcularIMSS(decimal sdi, int dias)
        {
            // LÓGICA DE EJEMPLO: 9% del SDI por los días
            // La lógica real (Riesgo Trabajo, Especie, Dinero, Invalidez, etc.) es MUY compleja.
            return (sdi * dias) * 0.09m;
        }
    }
}
