using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("{nombreUsuario}")]
        public Usuario GetUsuarioByNombreUsuario(String nombreUsuario)
        {
            return UsuarioHandler.GetUsuarioByNombreUsuario(nombreUsuario);
        }

        [HttpGet("{nombreUsuario}/{contraseña}")]
        public Usuario GetUsuarioByNombreUsuarioAndContraseña(String nombreUsuario, string contraseña)
        {
            return UsuarioHandler.GetUsuarioByNombreUsuarioAndContraseña(nombreUsuario,contraseña);
        }

        [HttpDelete]
        public bool DeleteUsuario([FromBody] int id)
        {
            return UsuarioHandler.DeleteUsuario(id);
        }

        [HttpPut]
        public bool ModifyUsuario([FromBody] Usuario usuario)
        {
            try
            {
                return UsuarioHandler.ModifyUsuario(new Usuario
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPost]
        public bool CreateUsuario([FromBody] Usuario usuario)
        {
            try
            {
                return UsuarioHandler.CreateUsuario(new Usuario
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
