using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class NombreController : ControllerBase
    {
        [HttpGet]
        public string GetName()
        {
            return "Sistema de gestion";
        }
    }
}
