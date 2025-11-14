using iTextSharp.text.io;
using Microsoft.SqlServer.Server;
using NominaMAD.Entidad;
using NominaMAD.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConceptoDAO
{ 
    public static int AgregarConcepto(Concepto concepto)
    {
        using (SqlConnection cn = BD_Conexion.ObtenerConexion())
        {
            SqlCommand cmd = new SqlCommand("sp_AddConceptos", cn);       
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Tipo", concepto.Tipo);
            cmd.Parameters.AddWithValue("@nombre", concepto.Nombre);
            cmd.Parameters.AddWithValue("@EsPorcetanje", concepto.EsPorcentaje);
            cmd.Parameters.AddWithValue("@Valor", concepto.Valor);
            cmd.Parameters.AddWithValue("@General", concepto.General);
            
            return cmd.ExecuteNonQuery(); 
        }
    }
    public static void EditarConcepto(Concepto concepto)
    {
        using (SqlConnection cn = BD_Conexion.ObtenerConexion())
        {
            SqlCommand cmd = new SqlCommand("sp_EditarConcepto", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Parámetros
            cmd.Parameters.AddWithValue("@ID_Conceptos", concepto.ID_Conceptos);
            cmd.Parameters.AddWithValue("@Tipo", concepto.Tipo);
            cmd.Parameters.AddWithValue("@nombre", concepto.Nombre);
            cmd.Parameters.AddWithValue("@EsPorcetanje" +
                "", concepto.EsPorcentaje);
            cmd.Parameters.AddWithValue("@Valor", concepto.Valor);
            cmd.Parameters.AddWithValue("@General", concepto.General);

            cmd.ExecuteNonQuery();
        }
    }

    public void BajaConcepto(int id)
    {
        using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
        {
            SqlCommand comando = new SqlCommand("sp_BajaConcepto", conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@ID_Concepto",id );

            comando.ExecuteNonQuery();
        }
    }
    public  List<Concepto> ObtenerConceptos()
    {
        List<Concepto> lista = new List<Concepto>();
        using (SqlConnection cn = BD_Conexion.ObtenerConexion())
        {

            SqlCommand cmd = new SqlCommand("sp_GetConceptos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Concepto PD = new Concepto();
                PD.ID_Conceptos = reader.GetInt32(0);
                PD.Tipo=reader.GetBoolean(1);
                PD.Nombre= reader.GetString(2);
                PD.EsPorcentaje = reader.GetBoolean(3);
                PD.Valor = reader.GetDecimal(4);
                PD.General=reader.GetBoolean(5);
                PD.Estatus= reader.GetBoolean(6);
                lista.Add(PD);
                                
            }
        }
        return lista;
    }
}
