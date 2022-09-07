namespace ProyectoFinal.Model
{
    public class ProductoVendido
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public long Stock { get; set; }
        public int IdVenta { get; set; }
    }
}
