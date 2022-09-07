using ProyectoFinal.Model;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoFinal.Repository
{
    public static class VentaHandler
    {
        public const string connectionString = "Server=DESKTOP-1HLV6NL;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Venta> GetVentasByIdUsuario(int idUsuario)
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryGet = "SELECT * FROM Venta WHERE IdUsuario = @idUsuario";

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
                                Venta venta = new Venta();

                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();
                                venta.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);

                                ventas.Add(venta);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return ventas;
        }

        public static bool CreateVenta(List<Producto> productos, int idUsuario)
        {
            bool resultado = false;
            Venta venta = new Venta();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryInsertVenta = "INSERT INTO Venta (Comentarios, IdUsuario) VALUES (@comentarios, @idUsuario)";

                SqlParameter comentariosParameter = new SqlParameter("comentarios", SqlDbType.VarChar) { Value = "" };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = idUsuario };

                using (SqlCommand sqlCommand = new SqlCommand(queryInsertVenta, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentariosParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                }
                using (SqlCommand sqlCommand = new SqlCommand("Select @@IDENTITY", sqlConnection))
                {
                    venta.Id = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }
                foreach (Producto producto in productos)
                {
                    string queryInsertProductoVendido = "INSERT INTO ProductoVendido (Stock, IdProducto ,IdVenta) VALUES (@stock, @idProducto, @idVenta)";

                    SqlParameter stockParameter = new SqlParameter("stock", SqlDbType.BigInt) { Value = producto.Stock };
                    SqlParameter idProductoParameter = new SqlParameter("idProducto", SqlDbType.BigInt) { Value = producto.Id };
                    SqlParameter idVentaParameter = new SqlParameter("idVenta", SqlDbType.BigInt) { Value = venta.Id };

                    using (SqlCommand sqlCommand = new SqlCommand(queryInsertProductoVendido, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(idProductoParameter);
                        sqlCommand.Parameters.Add(idVentaParameter);

                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Parameters.Clear();
                    }

                    string queryUpdateProducto = "UPDATE Producto SET Stock = Stock - @stock WHERE id = @idProducto";

                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdateProducto, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(stockParameter);
                        sqlCommand.Parameters.Add(idProductoParameter);

                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Parameters.Clear();
                    }
                }
                resultado = true;
                sqlConnection.Close();
            }
            return resultado;
        }
    }
}

