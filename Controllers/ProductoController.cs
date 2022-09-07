using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<Producto> GetProductosByIdUsuario(int idUsuario)
        {
            return ProductoHandler.GetProductosByIdUsuario(idUsuario);
        }

        [HttpDelete("{idProducto}")]
        public void DeleteProducto(int idProducto)
        {
            ProductoHandler.DeleteProducto(idProducto);
        }

        [HttpPost]
        public void CreateProducto(Producto producto)
        {
            ProductoHandler.CreateProducto(producto);
        }

        [HttpPut]
        public void ModifyProducto(Producto producto)
        {
            ProductoHandler.ModifyProducto(producto);
        }
    }
}
