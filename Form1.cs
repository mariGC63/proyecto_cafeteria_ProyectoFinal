using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using proyecto_cafeteria.interfaz_de_administrador;
using proyecto_cafeteria.interfaz_principal;
using proyecto_cafeteria.recuperar_contraseña;

namespace proyecto_cafeteria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection conn = Modulo.conexion;

        public class usuarios
        {
            public string nombre { get; set; }
            public string contraseña { get; set; }
            public string rol { get; set; }

        }
        /*private List<usuarios> usuariosFicticios = new List<usuarios>
        {
              new usuarios { nombre = "admin", contraseña = "123", rol = "admin" },
    new usuarios { nombre = "mario", contraseña = "1234", rol = "empleado" },
        };

        private bool Autenticar(string usuario, string contraseña, out string rol)
        {
            rol = null;
            var user = usuariosFicticios.FirstOrDefault(u =>
                u.nombre == usuario && u.contraseña == contraseña);

            if (user != null)
            {
                rol = user.rol;
                return true;
            }
            return false;
        }*/

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    txtUsuario.ForeColor = Color.Red;
                }

                if (string.IsNullOrWhiteSpace(txtContraseña.Text))
                {
                    txtContraseña.ForeColor = Color.Red;
                }

                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            try
            {
                conn.Open();
                string consulta = "SELECT * FROM Empleados WHERE usuario = @usuario AND contrasena = @contrasena";
                MySqlCommand comando = new MySqlCommand(consulta, conn);
                comando.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                comando.Parameters.AddWithValue("@contrasena", txtContraseña.Text);

                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string puesto = reader["puesto"].ToString();
                        int num_cuenta = Convert.ToInt32(reader["id_empleado"]);
                        conn.Close();
                        if (puesto == "admin")
                        {

                            principal_admin adminForm = new principal_admin();
                            adminForm.lbl_usuarioA.Text = num_cuenta.ToString();
                            adminForm.FormClosed += (s, args) => this.Close();
                            adminForm.Show();
                        }
                        else if (puesto == "empleado")
                        {
                            interfaz empleadoForm = new interfaz();
                            empleadoForm.lbl_usuarioE.Text = num_cuenta.ToString();
                            empleadoForm.FormClosed += (s, args) => this.Close();
                            empleadoForm.Show();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos");
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Recuperacion recuperacion = new Recuperacion();
            recuperacion.Show();
            this.Hide();
        }
    }
}
