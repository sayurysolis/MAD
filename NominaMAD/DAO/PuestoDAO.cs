using NominaMAD.Entidad;
using NominaMAD.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaMAD.DAO
{
    public class PuestoDAO
    {

        public static int add_Puesto(PUESTO _puesto)
        {
            int retorno=0;

            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_AddPuesto", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@nombre", _puesto.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", _puesto.Descripcion);
                comando.Parameters.AddWithValue("@DepartamentoID", _puesto.DepartamentoID);
                retorno = comando.ExecuteNonQuery();
            }
            
            return retorno;
        }

        public static List<PUESTO> GetPuestos()
        {
            List<PUESTO> lista = new List<PUESTO>();

            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_GetPuestos", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    PUESTO p = new PUESTO
                    {
                        ID_Puesto = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.GetString(2),
                        estatus = reader.GetString(3),
                        EmpresaID = reader.GetString(4),
                        DepartamentoID = reader.GetString(5)
                    };
                    lista.Add(p);
                }
            }

            return lista;
        }

        public static string ObtenerNombrePorID(int idDepartamento)
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT nombre FROM Departamento WHERE ID_Departamento = @ID", cn);
                cmd.Parameters.AddWithValue("@ID", idDepartamento);
                return cmd.ExecuteScalar()?.ToString() ?? "";
            }
        }

        public static void EditarPuesto(PUESTO puesto)
        {
            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_EditarPuesto", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@ID_Puesto", puesto.ID_Puesto);
                comando.Parameters.AddWithValue("@Nombre", puesto.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", puesto.Descripcion);
                comando.Parameters.AddWithValue("@DepartamentoID", puesto.DepartamentoID);

                comando.ExecuteNonQuery();
            }
        }

        public static void BajaPuesto(int idPuesto)
        {
            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_BajaPuesto", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@ID_Puesto", idPuesto);

                comando.ExecuteNonQuery();
            }
        }
    }
}


 

