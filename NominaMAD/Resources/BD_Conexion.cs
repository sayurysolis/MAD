using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaMAD.Resources
{
    public class BD_Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DSB_topografia;Data Source=RAGE-PC\\SQLEXPRESS");

            conexion.Open();
            return conexion;
        }

    }
}
