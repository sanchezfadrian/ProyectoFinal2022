using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<Venta> GetVentasByIdUsuario(int idUsuario)
        {
            return VentaHandler.GetVentasByIdUsuario(idUsuario);
        }

        [HttpPost("{idUsuario}")]
        public void CreateVenta([FromBody] List<Producto> productos,int idUsuario)
        {
            VentaHandler.CreateVenta(productos, idUsuario);
        }
    }
}
