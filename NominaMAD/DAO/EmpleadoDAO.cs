using NominaMAD.Entidad;
using NominaMAD.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Data Acces Objetc
namespace NominaMAD.DAO
{
    public class EmpleadoDAO
    {
        public static int AgregarEmpleado(Empleado Empleado){
            int retorno = 0;

            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                string query = "";

                SqlCommand comando = new SqlCommand(query, conexion);
                retorno =comando.ExecuteNonQuery();
               }



                return retorno;
        }



    }
}
