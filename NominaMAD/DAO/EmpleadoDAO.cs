using NominaMAD.Entidad;
using NominaMAD.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Data Acces Objetc
namespace NominaMAD.DAO
{
    public class EmpleadoDAO
    {
        public static int AddEmpleado(EMPLEADOS emp)
        {
            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_AddEmpleado", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", emp.nombre);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", emp.apellidoP);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", emp.apellidoM);
                cmd.Parameters.AddWithValue("@FechaNacimiento", emp.fechaNacimiento);

                cmd.Parameters.AddWithValue("@DepID", emp.depID);
                cmd.Parameters.AddWithValue("@PuestoID", emp.puestoID);

                cmd.Parameters.AddWithValue("@CURP", emp.CURP);
                cmd.Parameters.AddWithValue("@NSS", emp.NSS);
                cmd.Parameters.AddWithValue("@RFC", emp.RFC);

                cmd.Parameters.AddWithValue("@Banco", emp.banco);
                cmd.Parameters.AddWithValue("@NumeroCuenta", emp.numCuenta);
                cmd.Parameters.AddWithValue("@SalarioDiario", emp.SalarioDiario);
                cmd.Parameters.AddWithValue("@SalarioDiarioIntegrado", emp.SalarioDiarioIntegrado);

                cmd.Parameters.AddWithValue("@calle", emp.calle);
                cmd.Parameters.AddWithValue("@numero", emp.numero);
                cmd.Parameters.AddWithValue("@colonia", emp.colonia);
                cmd.Parameters.AddWithValue("@municipio", emp.municipio);
                cmd.Parameters.AddWithValue("@estado", emp.estado);
                cmd.Parameters.AddWithValue("@codigoPostal", emp.CodigoPostal);

                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Telefono", emp.Telefono);
                cmd.Parameters.AddWithValue("@FechaIngreso", emp.fechaIngreso);

                conexion.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<EMPLEADOS> GetEMPLEADOS()
        {
            List<EMPLEADOS> lista = new List<EMPLEADOS>();

            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_GetEmpleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    EMPLEADOS emp = new EMPLEADOS
                    {
                        ID_Empleado = Convert.ToInt32(dr["ID_Empleado"]),
                        nombre = dr["Nombre"].ToString(),
                        apellidoP = dr["ApellidoPaterno"].ToString(),
                        apellidoM = dr["ApellidoMaterno"].ToString(),
                        CURP = dr["CURP"].ToString(),
                        NSS = dr["NSS"].ToString(),
                        RFC = dr["RFC"].ToString(),
                        fechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                        banco = dr["Banco"].ToString(),
                        numCuenta = dr["NumeroCuenta"].ToString(),
                        SalarioDiario = Convert.ToDecimal(dr["SalarioDiario"]),
                        SalarioDiarioIntegrado = Convert.ToDecimal(dr["SalarioDiarioIntegrado"]),
                        Email = dr["Email"].ToString(),

                        calle = dr["calle"].ToString(),
                        numero = Convert.ToInt32(dr["numero"]),
                        colonia = dr["colonia"].ToString(),
                        municipio = dr["municipio"].ToString(),
                        estado = dr["estado"].ToString(),
                        CodigoPostal = dr["codigoPostal"].ToString(),

                        Telefono = dr["Telefono"].ToString(),
                        estatus = Convert.ToBoolean(dr["estatus"]),
                        fechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]),

                        // extras de la vista
                        empresaID = dr["NombreEmpresa"].ToString(),
                        depID = dr["NombreDepartamento"].ToString(),
                        puestoID = dr["NombrePuesto"].ToString()
                    };

                    lista.Add(emp);
                }
            }

            return lista;
        }
        public static EMPLEADOS GetEmpleadoByID(int idEmpleado)
        {
            EMPLEADOS emp = null;

            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_GetEmpleadoByID", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Empleado", idEmpleado);

                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    emp = new EMPLEADOS
                    {
                        ID_Empleado = Convert.ToInt32(dr["ID_Empleado"]),
                        nombre = dr["Nombre"].ToString(),
                        apellidoP = dr["ApellidoPaterno"].ToString(),
                        apellidoM = dr["ApellidoMaterno"].ToString(),
                        CURP = dr["CURP"].ToString(),
                        NSS = dr["NSS"].ToString(),
                        RFC = dr["RFC"].ToString(),
                        fechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                        banco = dr["Banco"].ToString(),
                        numCuenta = dr["NumeroCuenta"].ToString(),
                        SalarioDiario = Convert.ToDecimal(dr["SalarioDiario"]),
                        SalarioDiarioIntegrado = Convert.ToDecimal(dr["SalarioDiarioIntegrado"]),
                        Email = dr["Email"].ToString(),

                        calle = dr["calle"].ToString(),
                        numero = Convert.ToInt32(dr["numero"]),
                        colonia = dr["colonia"].ToString(),
                        municipio = dr["municipio"].ToString(),
                        estado = dr["estado"].ToString(),
                        CodigoPostal = dr["codigoPostal"].ToString(),

                        Telefono = dr["Telefono"].ToString(),
                        estatus = Convert.ToBoolean(dr["estatus"]),
                        fechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]),

                        // nombres descriptivos de la vista
                        empresaID = dr["NombreEmpresa"].ToString(),
                        depID = dr["NombreDepartamento"].ToString(),
                        puestoID = dr["NombrePuesto"].ToString()
                    };
                }
            }

            return emp;
        }

        public static void UpdateEmpleado(EMPLEADOS emp)
        {
            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EditarEmpleado", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Empleado", emp.ID_Empleado);
                cmd.Parameters.AddWithValue("@Nombre", emp.nombre);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", emp.apellidoP);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", emp.apellidoM);
                cmd.Parameters.AddWithValue("@FechaNacimiento", emp.fechaNacimiento);

                cmd.Parameters.AddWithValue("@DepID", emp.depID);
                cmd.Parameters.AddWithValue("@PuestoID", emp.puestoID);

                cmd.Parameters.AddWithValue("@CURP", emp.CURP);
                cmd.Parameters.AddWithValue("@NSS", emp.NSS);
                cmd.Parameters.AddWithValue("@RFC", emp.RFC);

                cmd.Parameters.AddWithValue("@Banco", emp.banco);
                cmd.Parameters.AddWithValue("@NumeroCuenta", emp.numCuenta);
                cmd.Parameters.AddWithValue("@SalarioDiario", emp.SalarioDiario);
                cmd.Parameters.AddWithValue("@SalarioDiarioIntegrado", emp.SalarioDiarioIntegrado);

                cmd.Parameters.AddWithValue("@calle", emp.calle);
                cmd.Parameters.AddWithValue("@numero", emp.numero);
                cmd.Parameters.AddWithValue("@colonia", emp.colonia);
                cmd.Parameters.AddWithValue("@municipio", emp.municipio);
                cmd.Parameters.AddWithValue("@estado", emp.estado);
                cmd.Parameters.AddWithValue("@codigoPostal", emp.CodigoPostal);

                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Telefono", emp.Telefono);
                cmd.Parameters.AddWithValue("@FechaIngreso", emp.fechaIngreso);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void EliminarEmpleado(int idEmpleado)
        {
            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_BajaEmpleado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_Empleado", idEmpleado);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}




