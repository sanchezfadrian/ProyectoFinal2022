using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductoVendidoController
    {
        [HttpGet("{idUsuario}")]
        public List<ProductoVendido> GetProductosVendidosByIdUsuario(int idUsuario)
        {
            return ProductoVendidoHandler.GetProductosVendidosByIdUsuario(idUsuario);
        }
    }
}
