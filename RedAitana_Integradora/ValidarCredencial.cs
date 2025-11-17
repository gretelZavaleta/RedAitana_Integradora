using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using RedAitana_Integradora.BSD;

namespace RedAitana_Integradora
{
    public static class ValidarCredencial
    {
        public static string TipoUsuario { get; private set; } = "";
        public static int IdUsuario { get; private set; } = -1;

        public static bool IniciarSesion(string username, string password)
        {
            try
            {
                var conexionBD = new ConexionMySQL();
                conexionBD.AbrirConexion();
                var conexion = conexionBD.ObtenerConexion();

                string query = "SELECT idusuariosSistema FROM usuariossistema WHERE username = @username AND password = @password";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    var result = cmd.ExecuteScalar();
                    conexionBD.CerrarConexion();

                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        IdUsuario = id;
                        TipoUsuario = username;
                        return true;
                    }
                    else
                    {
                        IdUsuario = -1;
                        TipoUsuario = "";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar credenciales: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Retorna true si tiene permiso de edición.
        /// </summary>
        public static bool PuedeEditar()
        {
            return TipoUsuario == "Administrador";
        }

        /// <summary>
        /// Solicita validación adicional si el usuario actual no es administrador.
        /// </summary>
        public static bool SolicitarValidacionAdministrador()
        {
            using (var ventana = new VentanaConfirmacion())
            {
                return ventana.ShowDialog() == DialogResult.OK;
            }
        }

    }
}
