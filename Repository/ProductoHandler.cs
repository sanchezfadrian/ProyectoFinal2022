using ProyectoFinal.Model;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoFinal.Repository
{
    public static class ProductoHandler
    {
        public const string connectionString = "Server=DESKTOP-1HLV6NL;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Producto> GetProductosByIdUsuario(int idUsuario)
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryGet = "SELECT * FROM Producto WHERE IdUsuario = @idUsuario";

                SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", System.Data.SqlDbType.BigInt);
                idUsuarioParameter.Value = idUsuario;

                using (SqlCommand sqlCommand = new SqlCommand(queryGet, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                producto.Costo = Convert.ToDouble(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);

                                productos.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return productos;
        }

        public static bool DeleteProducto(int idProducto)
        {
            int numberOfRows = 0;
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryDelete = "DELETE FROM ProductoVendido WHERE IdProducto = @idProducto; DELETE FROM Producto WHERE Id = @idProducto";

                SqlParameter idProductoParameter = new SqlParameter("idProducto", System.Data.SqlDbType.BigInt);
                idProductoParameter.Value = idProducto;

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idProductoParameter);

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

        public static bool CreateProducto(Producto producto)
        {
            int numberOfRows = 0;
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryInsert = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES (@descripciones, @costo, @precioVenta, @stock, @idUsuario)";

                SqlParameter descripcionesParameter = new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("costo", SqlDbType.BigInt) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVenta", SqlDbType.BigInt) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.BigInt) { Value = producto.Stock };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario };

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

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

        public static bool ModifyProducto(Producto producto)
        {
            int numberOfRows = 0;
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryUpdate = "UPDATE Producto SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario WHERE Id = @id ";

                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = producto.Id };
                SqlParameter descripcionesParameter = new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("costo", SqlDbType.BigInt) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVenta", SqlDbType.BigInt) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.BigInt) { Value = producto.Stock };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario };

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

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
