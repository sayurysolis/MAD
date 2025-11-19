using Microsoft.Win32;
using NominaMAD.Entidad;
using NominaMAD.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NominaMAD.DAO
{
    public class DepartamentoDAO
    {
        public static int Add_Departamento(DEPARTAMENTO dep)
        {
            int retorno = 0;
            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_AddDepartamento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@nombre", dep.nombre);
                retorno = comando.ExecuteNonQuery();
            }
            return retorno;
        }
        public static List<DEPARTAMENTO> Get_Departamento()
        {
            List<DEPARTAMENTO> lista = new List<DEPARTAMENTO>();

            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_GetDepartamento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DEPARTAMENTO depa = new DEPARTAMENTO();
                    depa.ID_Departamento = reader.GetInt32(0);
                    depa.nombre = reader.GetString(1);
                    depa.estatus = reader.GetString(2);
                    depa.EmpresaID = reader.GetString(3);
                    lista.Add(depa);
                }
            }
            return lista;
        }
        public static void EditarDepartamento(DEPARTAMENTO depa)
        {
            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_EditarDepartamento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@nombre", depa.nombre);
                comando.Parameters.AddWithValue("@ID_Departamento", depa.ID_Departamento);

                comando.ExecuteNonQuery();
            }
        }
        public static void EliminarDep(int _idDepa)
        {
            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_BajaDepartamento", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@ID_Departamento", _idDepa);

                comando.ExecuteNonQuery();
            }
        }
    }
}
    