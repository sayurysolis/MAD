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

        /*public static List<Pue> get_Puesto()
        {
            List<DEPARTAMENTO> lista = new List<DEPARTAMENTO>();

            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM vw_DepartamentoEmpresa";
                SqlCommand comando = new SqlCommand(query, conexion);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DEPARTAMENTO depa = new DEPARTAMENTO();
                    depa.ID_Departamento = reader.GetInt32(0);
                    depa.nombre = reader.GetString(1);
                    depa.estado = reader.GetBoolean(2);
                    depa.EmpresaID = reader.GetString(3);

                    lista.Add(depa);
                }
            }

            return lista;
        }*/



    }
}
