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
        public static void AddEmpleado(EMPLEADOS emp)
        {
            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Empleado (
                1, DepID, PuestoID, Gerente, Contrasena,
                CURP, NSS, RFC, Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento,
                Banco, NumeroCuenta, SalarioDiario, SalarioDiarioIntegrado,
                Email, Direccion, Telefono, estatus, FechaIngreso
            ) VALUES (
                @EmpresaID, @DepID, @PuestoID, @Gerente, @Contrasena,
                @CURP, @NSS, @RFC, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento,
                @Banco, @NumeroCuenta, @SalarioDiario, @SalarioDiarioIntegrado,
                @Email, @Direccion, @Telefono, @estatus, @FechaIngreso
            )";

                SqlCommand cmd = new SqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@DepID", emp.depID);
                cmd.Parameters.AddWithValue("@PuestoID", emp.puestoID);
                cmd.Parameters.AddWithValue("@Gerente", emp.gerente);
                cmd.Parameters.AddWithValue("@Contrasena", emp.contrasena);
                cmd.Parameters.AddWithValue("@CURP", emp.CURP);
                cmd.Parameters.AddWithValue("@NSS", emp.NSS);
                cmd.Parameters.AddWithValue("@RFC", emp.RFC);
                cmd.Parameters.AddWithValue("@Nombre", emp.nombre);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", emp.apellidoP);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", emp.apellidoM);
                cmd.Parameters.AddWithValue("@FechaNacimiento", emp.fechaNacimiento);
                cmd.Parameters.AddWithValue("@Banco", emp.banco);
                cmd.Parameters.AddWithValue("@NumeroCuenta", emp.numCuenta);
                cmd.Parameters.AddWithValue("@SalarioDiario", emp.SalarioDiario);
                cmd.Parameters.AddWithValue("@SalarioDiarioIntegrado", emp.SalarioDiarioIntegrado);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Direccion", emp.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", emp.Telefono);
                cmd.Parameters.AddWithValue("@estatus", emp.estatus);
                cmd.Parameters.AddWithValue("@FechaIngreso", emp.fechaIngreso);
                cmd.ExecuteNonQuery();
            }
        }


        public static EMPLEADOS GetEmpleadoByID(int idEmpleado)
        {
            EMPLEADOS emp = null;

            using (SqlConnection conexion = BD_Conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Vista_Empleado WHERE ID_Empleado = @id";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", idEmpleado);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    emp = new EMPLEADOS
                    {
                        ID_Empleado = (int)reader["ID_Empleado"],
                        CURP = reader["CURP"].ToString(),
                        NSS = reader["NSS"].ToString(),
                        RFC = reader["RFC"].ToString(),
                        nombre = reader["Nombre"].ToString(),
                        apellidoP = reader["ApellidoPaterno"].ToString(),
                        apellidoM = reader["ApellidoMaterno"].ToString(),
                        fechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        banco = reader["Banco"].ToString(),
                        numCuenta = reader["NumeroCuenta"].ToString(),
                        SalarioDiario = Convert.ToDecimal(reader["SalarioDiario"]),
                        SalarioDiarioIntegrado = Convert.ToDecimal(reader["SalarioDiarioIntegrado"]),
                        Email = reader["Email"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        estatus = Convert.ToBoolean(reader["estatus"]),
                        fechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]),
                        gerente = Convert.ToBoolean(reader["Gerente"]),
                        // Estos vienen de la vista
                        empresaID = reader["NombreEmpresa"].ToString(),
                        depID = reader["NombreDepartamento"].ToString(),
                        puestoID = reader["NombrePuesto"].ToString()
                    };
                }
            }



            return emp;
        }


        public static void UpdateEmpleado(EMPLEADOS emp)
        {
            using (SqlConnection cn = BD_Conexion.ObtenerConexion())
            {
                string query = @"UPDATE Empleado SET 
                1,
                DepID = @DepID,
                PuestoID = @PuestoID,
                Gerente = @Gerente,
                Contrasena = @Contrasena,
                CURP = @CURP,
                NSS = @NSS,
                RFC = @RFC,
                Nombre = @Nombre,
                ApellidoPaterno = @ApellidoPaterno,
                ApellidoMaterno = @ApellidoMaterno,
                FechaNacimiento = @FechaNacimiento,
                Banco = @Banco,
                NumeroCuenta = @NumeroCuenta,
                SalarioDiario = @SalarioDiario,
                SalarioDiarioIntegrado = @SalarioDiarioIntegrado,
                Email = @Email,
                Direccion = @Direccion,
                Telefono = @Telefono,
                estatus = @estatus,
                FechaIngreso = @FechaIngreso
            WHERE ID_Empleado = @ID_Empleado";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@DepID", emp.depID);
                cmd.Parameters.AddWithValue("@PuestoID", emp.puestoID);
                cmd.Parameters.AddWithValue("@Gerente", emp.gerente);
                cmd.Parameters.AddWithValue("@Contrasena", emp.contrasena);
                cmd.Parameters.AddWithValue("@CURP", emp.CURP);
                cmd.Parameters.AddWithValue("@NSS", emp.NSS);
                cmd.Parameters.AddWithValue("@RFC", emp.RFC);
                cmd.Parameters.AddWithValue("@Nombre", emp.nombre);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", emp.apellidoP);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", emp.apellidoM);
                cmd.Parameters.AddWithValue("@FechaNacimiento", emp.fechaNacimiento);
                cmd.Parameters.AddWithValue("@Banco", emp.banco);
                cmd.Parameters.AddWithValue("@NumeroCuenta", emp.numCuenta);
                cmd.Parameters.AddWithValue("@SalarioDiario", emp.SalarioDiario);
                cmd.Parameters.AddWithValue("@SalarioDiarioIntegrado", emp.SalarioDiarioIntegrado);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Direccion", emp.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", emp.Telefono);
                cmd.Parameters.AddWithValue("@estatus", emp.estatus);
                cmd.Parameters.AddWithValue("@FechaIngreso", emp.fechaIngreso);
                cmd.ExecuteNonQuery();

            }





        }
    }
}



