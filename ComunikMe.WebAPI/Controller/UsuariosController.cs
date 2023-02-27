using ComunikiMe.Domain;
using ComunikiMe.Domain.DTO;
using ComunikiMe.WebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ComunikiMe.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ComunikiMeWebAPIContext _context;

        public UsuariosController(ComunikiMeWebAPIContext context)
        {
            _context = context;
        }
        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Register",Name = "Register")]
        public async Task<ActionResult<Guid>> Post(RegistroDTO usuarioDTO)
        {
            var mensagem = usuarioDTO.validateRequest();
            if(!mensagem.IsNullOrEmpty())
                return BadRequest(mensagem);

            var usuarioExistente = _context.Usuario.Any(x => x.Email == usuarioDTO.Email);
            if (usuarioExistente)
                return BadRequest("Email já cadastrado");
            var isAdmin = !(_context.Usuario.Count() > 0);
            var usuario = new Usuario
            {
                Email = usuarioDTO.Email,
                Nome = usuarioDTO.Nome,
                isAdmin = isAdmin,
            };
            if (usuario.Register(usuarioDTO.Senha))
            {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                return Ok(usuario.Secret);
            }
            return StatusCode(500, "Algo inesperado ocorreu");
        }
        [HttpPost("Login",Name = "Login")]
        public async Task<ActionResult<Guid>> PostLogin(LoginDTO loginDTO)
        {
            var usuario = _context.Usuario.FirstOrDefault(x => x.Email == loginDTO.Email);
            if (usuario is null)
                return BadRequest("Dados de login incorretos");
            if (usuario.Login(loginDTO.Password))
            {
                _context.Update(usuario);
                _context.SaveChanges();
                return Ok(usuario.Secret);
            }

            return BadRequest("Dados de login incorretos");
        }

        [HttpPost("UserInfo",Name = "UserInfo")]
        public async Task<ActionResult<UsuarioInfoDTO>> PostSecret([FromBody]Guid secret)
        {
            var usuario = _context.Usuario.FirstOrDefault(x => x.Secret == secret);
            if (usuario is not null)
                return Ok(new UsuarioInfoDTO(usuario));
            return NotFound("Usuário não encontrado");
        }
    }
}
