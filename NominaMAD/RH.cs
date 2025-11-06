using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NominaMAD
{
    public partial class P_RH : Form
    {
        string Conexion = "Data Source=LUISMTZ\\SQLEXPRESS;Initial Catalog=Nomina;Integrated Security=True";
        public P_RH()
        {
            InitializeComponent();
        }

        private void P_RH_Load(object sender, EventArgs e)
        {
            txt_Usuario_RH.MaxLength = 50;
            txt_Contra_RH.MaxLength = 15;
            MostrarEmpleadosRecursosHumanos();
            txt_Usuario_RH.Enabled = false;
            txt_Contra_RH.Enabled=false;
            btn_AcepartAgre_RH.Visible=false;
            btn_Modificar_RH.Visible=false;
            btn_CancelarMOD_RH.Visible=false;
            btn_AcepatarMOD_RH.Visible =false;

        }

        private void dgv_RH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        //private void MostrarEmpleadosRecursosHumanos()
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection cn = new SqlConnection(Conexion))
        //    {
        //        string query = "SELECT id_Empleado, NombreEmpleado, ApelPaternoEmpleado, ApelMaternoEmpleado, Puesto " +
        //                       "FROM Empleado " +
        //                       "WHERE id_Departamento = 1"; // Solo empleados de Recursos Humanos

        //        SqlDataAdapter da = new SqlDataAdapter(query, cn);
        //        da.SelectCommand.CommandType = CommandType.Text;
        //        cn.Open();
        //        da.Fill(dt);
        //        dgv_RH.DataSource = dt; // Asignar el DataTable al DataGridView
        //    }
        //}

        private void MostrarEmpleadosRecursosHumanos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                string query = @"
    SELECT e.id_Empleado, e.NombreEmpleado, e.ApelPaternoEmpleado, e.ApelMaternoEmpleado, p.NombrePuesto
    FROM Empleado e
    JOIN Puestos p ON e.id_Puesto = p.id_Puesto
    WHERE e.id_Departamento = 1 AND e.Activo = 1"; // Solo empleados activos de Recursos Humanos

                SqlDataAdapter da = new SqlDataAdapter(query, cn);
                da.SelectCommand.CommandType = CommandType.Text;
                cn.Open();
                da.Fill(dt);
                dgv_RH.DataSource = dt; // Asignar el DataTable al DataGridView
            }
        }

        private void btn_Agregar_RH_Click(object sender, EventArgs e)
        {
            // Verificar que haya un empleado seleccionado en el DataGridView
            if (dgv_RH.SelectedRows.Count > 0)
            {
                // Obtener el ID del empleado seleccionado
                int idEmpleado = Convert.ToInt32(dgv_RH.SelectedRows[0].Cells["id_Empleado"].Value);

                // Verificar que los campos de usuario y contraseña no estén vacíos
                if (!string.IsNullOrEmpty(txt_Usuario_RH.Text) && !string.IsNullOrEmpty(txt_Contra_RH.Text))
                {
                    string usuario = txt_Usuario_RH.Text;
                    string contrasena = txt_Contra_RH.Text;

                    // Verificar si el usuario ya existe en la base de datos
                    using (SqlConnection cn = new SqlConnection(Conexion))
                    {
                        string queryCheckUser = "SELECT COUNT(*) FROM UsuariosRH WHERE usuario = @usuario";
                        using (SqlCommand cmdCheckUser = new SqlCommand(queryCheckUser, cn))
                        {
                            cmdCheckUser.Parameters.AddWithValue("@usuario", usuario);

                            cn.Open();
                            int count = Convert.ToInt32(cmdCheckUser.ExecuteScalar());
                            cn.Close();

                            if (count > 0)
                            {
                                MessageBox.Show("El nombre de usuario ya está en uso. Por favor, elija otro nombre de usuario.");
                                return; // Salir de la función si el usuario ya existe
                            }
                        }

                        // Insertar los datos en la tabla UsuariosRH
                        string query = "INSERT INTO UsuariosRH (usuario, contrase, id_empleado) VALUES (@usuario, @contrase, @id_empleado)";
                        using (SqlCommand cmd = new SqlCommand(query, cn))
                        {
                            cmd.Parameters.AddWithValue("@usuario", usuario);
                            cmd.Parameters.AddWithValue("@contrase", contrasena);
                            cmd.Parameters.AddWithValue("@id_empleado", idEmpleado);

                            cn.Open();
                            cmd.ExecuteNonQuery();
                            cn.Close();
                        }
                    }

                    MessageBox.Show("Usuario de RH agregado exitosamente.");
                    // Configuración de botones y limpieza de campos
                    txt_Usuario_RH.Enabled = false;
                    txt_Contra_RH.Enabled = false;
                    btn_Agre_RH.Visible = false;
                    btn_AcepartAgre_RH.Visible = false;
                    btn_Modificar_RH.Visible = false;
                    btn_CancelarMOD_RH.Visible = false;
                    btn_AcepatarMOD_RH.Visible = false;
                    txt_Contra_RH.Clear();
                    txt_Usuario_RH.Clear();
                }
                else
                {
                    MessageBox.Show("Por favor, complete los campos de Usuario y Contraseña.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un empleado de la lista.");
            }

        }

        private void dgv_RH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que la celda seleccionada sea válida
            if (e.RowIndex >= 0)
            {
                // Obtener el id_Empleado de la fila seleccionada
                int idEmpleado = Convert.ToInt32(dgv_RH.Rows[e.RowIndex].Cells["id_Empleado"].Value);

                // Conectar a la base de datos y buscar el usuario y contraseña asociados al empleado
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    cn.Open();
                    string query = "SELECT Usuario, contrase FROM UsuariosRH WHERE id_empleado = @idEmpleado";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Si se encuentra el usuario y contraseña, asignarlos a los TextBox
                                txt_Usuario_RH.Text = reader["Usuario"].ToString();
                                txt_Contra_RH.Text = reader["contrase"].ToString();
                                btn_Modificar_RH.Visible = true;
                                btn_Agre_RH.Visible =false;
                            }
                            else
                            {
                                // Si no se encuentra, limpiar los TextBox
                                txt_Usuario_RH.Text = "";
                                txt_Contra_RH.Text = "";
                                MessageBox.Show("No se encontraron datos de usuario para este empleado.");
                                btn_Modificar_RH.Visible = false;
                                btn_Agre_RH.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        private void btn_Agre_RH_Click(object sender, EventArgs e)
        {
            txt_Usuario_RH.Enabled = true;
            txt_Contra_RH.Enabled = true;
            btn_Agre_RH.Visible = false;
            btn_AcepartAgre_RH.Visible = true;
            btn_Modificar_RH.Visible = false;
            btn_CancelarMOD_RH.Visible = true;
            btn_AcepatarMOD_RH.Visible = false;
            txt_Contra_RH.Clear();
            txt_Usuario_RH.Clear();
        }

        private void btn_CancelarMOD_RH_Click(object sender, EventArgs e)
        {
            txt_Usuario_RH.Enabled = false;
            txt_Contra_RH.Enabled = false;
            btn_Agre_RH.Visible = false;
            btn_AcepartAgre_RH.Visible = false;
            btn_Modificar_RH.Visible = false;
            btn_CancelarMOD_RH.Visible = false;
            btn_AcepatarMOD_RH.Visible = false;
            txt_Contra_RH.Clear();
            txt_Usuario_RH.Clear();
        }

        private void btn_Modificar_RH_Click(object sender, EventArgs e)
        {
            txt_Usuario_RH.Enabled = true;
            txt_Contra_RH.Enabled = true;
            btn_Agre_RH.Visible = false;
            btn_AcepartAgre_RH.Visible = false;
            btn_Modificar_RH.Visible = false;
            btn_CancelarMOD_RH.Visible = true;
            btn_AcepatarMOD_RH.Visible = true;
        }

        private void btn_AcepatarMOD_RH_Click(object sender, EventArgs e)
        {
            if (dgv_RH.SelectedRows.Count > 0)
            {
                int idEmpleado = Convert.ToInt32(dgv_RH.SelectedRows[0].Cells["id_Empleado"].Value);
                string nuevoUsuario = txt_Usuario_RH.Text.Trim();
                string nuevaContrasena = txt_Contra_RH.Text.Trim();

                if (string.IsNullOrEmpty(nuevoUsuario) || string.IsNullOrEmpty(nuevaContrasena))
                {
                    MessageBox.Show("Por favor, complete los campos de Usuario y Contraseña.");
                    return;
                }

                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    cn.Open();

                    // Verificar si el nombre de usuario ya existe para otro empleado
                    string queryCheckUser = "SELECT COUNT(*) FROM UsuariosRH WHERE usuario = @usuario AND id_empleado != @idEmpleado";
                    using (SqlCommand cmdCheckUser = new SqlCommand(queryCheckUser, cn))
                    {
                        cmdCheckUser.Parameters.AddWithValue("@usuario", nuevoUsuario);
                        cmdCheckUser.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                        int count = Convert.ToInt32(cmdCheckUser.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("El nombre de usuario ya está en uso por otro empleado. Por favor, elija otro nombre.");
                            return; // Salir de la función si el usuario ya existe para otro empleado
                        }
                    }

                    // Actualizar los datos del usuario en la tabla UsuariosRH
                    string queryUpdate = "UPDATE UsuariosRH SET usuario = @usuario, contrase = @contrasena WHERE id_empleado = @idEmpleado";
                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, cn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@usuario", nuevoUsuario);
                        cmdUpdate.Parameters.AddWithValue("@contrasena", nuevaContrasena);
                        cmdUpdate.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                        cmdUpdate.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Datos actualizados correctamente.");
                // Limpieza de los campos
                txt_Usuario_RH.Enabled = false;
                txt_Contra_RH.Enabled = false;
                btn_Agre_RH.Visible = false;
                btn_AcepartAgre_RH.Visible = false;
                btn_Modificar_RH.Visible = false;
                btn_CancelarMOD_RH.Visible = false;
                btn_AcepatarMOD_RH.Visible = false;
               
                txt_Usuario_RH.Clear();
                txt_Contra_RH.Clear();
            }
            else
            {
                MessageBox.Show("Seleccione un empleado para modificar los datos.");
            }
        }

        private void btn_Regresar_RHbutton1_Click(object sender, EventArgs e)
        {
            if (P_Inicio.MMenuAoE == 1)
            {
                P_Menu1 p_Menu1 = new P_Menu1();
                // Ocultar el formulario actual (Form1)
                this.Hide();

                // Mostrar el nuevo formulario
                p_Menu1.ShowDialog();

            }
            else
            {
                if (P_Inicio.MMenuAoE == 2)
                {
                    P_Menu2 p_Menu2 = new P_Menu2();
                    // Ocultar el formulario actual (Form1)
                    this.Hide();

                    // Mostrar el nuevo formulario
                    p_Menu2.ShowDialog();

                }
            }
        }
    }
}
