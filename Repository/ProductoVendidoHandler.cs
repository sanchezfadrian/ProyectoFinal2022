using ProyectoFinal.Model;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class ProductoVendidoHandler
    {
        public const string connectionString = "Server=DESKTOP-1HLV6NL;Database=SistemaGestion;Trusted_Connection=True";

        public static List<ProductoVendido> GetProductosVendidosByIdUsuario(int idUsuario)
        {
            List<Producto> productos = new List<Producto>();
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();

            productos = ProductoHandler.GetProductosByIdUsuario(idUsuario);

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryGet = "SELECT * FROM ProductoVendido WHERE IdProducto = @idProducto";

                foreach (Producto producto in productos)
                {
                    SqlParameter idProductoParameter = new SqlParameter("idProducto", System.Data.SqlDbType.BigInt);
                    idProductoParameter.Value = producto.Id;

                    using (SqlCommand sqlCommand = new SqlCommand(queryGet, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(idProductoParameter);

                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    ProductoVendido productoVendido = new ProductoVendido();

                                    productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                                    productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                    productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                    productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                    productosVendidos.Add(productoVendido);
                                }
                            }
                        }
                        sqlCommand.Parameters.Clear();
                    }
                }
                sqlConnection.Close();
                return productosVendidos;
            }
        }
    }
}
