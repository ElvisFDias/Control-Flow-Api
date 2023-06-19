using Control_Flow_Api.Model;
using Control_Flow_Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Control_Flow_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
 
        private readonly UserService usuarioServ;

        public UsuarioController(UserService usuarioServ)
        {
            this.usuarioServ = usuarioServ;
        }

        [HttpPost]
        [Route("CriarUsuario_ComException")]
        public async Task<IActionResult> Post_CriarUsuario_ComException(Usuario user)
        {
            try
            {
                await usuarioServ.CriarUsuario_ComException(user);

                return Ok();
            }
            catch (CadastroUsuarioException)
            {
                return Conflict("Error - CriarUsuario_ComException");
            }
        }

        [HttpPost]
        [Route("CriarUsuario_SemExcecao")]
        public async Task<IActionResult> Post_CriarUsuario_SemExcecao(Usuario user)
        {
            var result = await usuarioServ.CriarUsuario_SemExcecao(user);

            if(result.Sucesso)
                return Ok();

            return Conflict("Error - CriarUsuario_SemExcecao");
        }
    }
}