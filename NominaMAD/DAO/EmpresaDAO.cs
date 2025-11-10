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
    public class EmpresaDAO
    {

        public static int InsertarEmpresa(EMPRESAS empresa)
        {
            int retorno = 0;

            using (SqlConnection conexion= BD_Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("SP_AddEmpresa", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@RazonSocial", empresa.RazonSocial);
                comando.Parameters.AddWithValue("@domicilioFiscal", empresa.DomicilioFiscal);
                comando.Parameters.AddWithValue("@contacto",empresa.contacto);
                comando.Parameters.AddWithValue("@registroPatronal", empresa.registroPatronal);
                comando.Parameters.AddWithValue("@RFC", empresa.RFC);
                comando.Parameters.AddWithValue("@Fechainicio",empresa.fechaInicio);

                retorno =comando.ExecuteNonQuery();

            }
                return retorno;
        }


        public static EMPRESAS ObtenerEmpresas()
        {
            EMPRESAS empresa = null;
            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_GetEmpresa", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    empresa = new EMPRESAS
                    {
                        //ID= Convert.ToInt32(reader["EmpresaID"]),
                        RazonSocial = reader["RazonSocial"].ToString(),
                        DomicilioFiscal = reader["DomicilioFiscal"].ToString(),
                        contacto = reader["Contacto"].ToString(),
                        registroPatronal = reader["RegistroPatronal"].ToString(),
                        RFC = reader["RFC"].ToString(),
                        fechaInicio = Convert.ToDateTime(reader["FechaInicio"])
                    };
                }


            }
                return empresa;
        }
        
        // public static int borrarEmpresa(){}
        
    }
}
