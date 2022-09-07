using ProyectoFinal.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class UsuarioHandler
    {
        public const string connectionString = "Server=DESKTOP-1HLV6NL;Database=SistemaGestion;Trusted_Connection=True";

        public static Usuario GetUsuarioByNombreUsuario(string nombreUsuario)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryGet = "SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario";

                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", System.Data.SqlDbType.Char);
                nombreUsuarioParameter.Value = nombreUsuario;

                using (SqlCommand sqlCommand = new SqlCommand(queryGet, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return usuario;
        }

        public static Usuario GetUsuarioByNombreUsuarioAndContraseña(string nombreUsuario, string contraseña)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryGet = "SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario AND Contraseña = @contraseña";

                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", System.Data.SqlDbType.Char);
                SqlParameter contraseñaParameter = new SqlParameter("contraseña", System.Data.SqlDbType.Char);
                nombreUsuarioParameter.Value = nombreUsuario;
                contraseñaParameter.Value = contraseña;

                using (SqlCommand sqlCommand = new SqlCommand(queryGet, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return usuario;
        }

        public static bool DeleteUsuario(int idUsuario)
        {
            int numberOfRows = 0;
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryDelete = "DELETE FROM Usuario WHERE Id = @id";

                SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                idParameter.Value = idUsuario;

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);

                    numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool CreateUsuario(Usuario usuario)
        {
            Usuario usuarioExistente = new Usuario();
            int numberOfRows = 0;
            bool resultado = false;

            usuarioExistente = GetUsuarioByNombreUsuario(usuario.NombreUsuario);

            if (usuarioExistente.Id > 0)
            {
                return resultado;
                throw new Exception("Ya existe un usuario con ese nombre de usuario");
            }

            if (usuario.Nombre == null || usuario.Nombre.Length == 0 ||
                usuario.Apellido == null || usuario.Apellido.Length == 0 ||
                usuario.NombreUsuario == null || usuario.NombreUsuario.Length == 0 ||
                usuario.Contraseña == null || usuario.Contraseña.Length == 0 ||
                usuario.Mail == null || usuario.Mail.Length == 0 )
            {
                return resultado;
                throw new Exception("Complete los campos obligatorios");
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryInsert = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@nombre, @apellido, @nombreUsuario, @contraseña, @mail)";

                SqlParameter nombreParameter = new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParameter = new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mailParameter = new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail };

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    sqlCommand.Parameters.Add(mailParameter);

                    numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool ModifyUsuario(Usuario usuario)
        {
            int numberOfRows = 0;
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryUpdate = "UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @contraseña, Mail = @mail WHERE Id = @id ";

                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = usuario.Id };
                SqlParameter nombreParameter = new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParameter = new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mailParameter = new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail };

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    sqlCommand.Parameters.Add(mailParameter);

                    numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }
    }
}
