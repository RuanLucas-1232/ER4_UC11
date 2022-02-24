using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using apiChapter.Models;
using apiChapter.Repositories;
using apiChapter.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace apiChapter.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UsuarioRepository _repository;
        public LoginController(UsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult FazerLogin(LoginViewModel login)
        {
            try
            {
                Usuario usuarioBuscado = _repository.Login(login.email, login.senha);

                if (usuarioBuscado == null)
                {
                    return NotFound("E-mail e/ou senha invalidos");
                }

                var minhasClaims = new[] {
                   new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.Tipo.ToString())
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao"));
 
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var meuToken = new JwtSecurityToken(
                    issuer: "chapterApiWeb",
                    audience: "chapterApiWeb",
                    claims: minhasClaims,
                    expires: DateTime.Now.AddMinutes(60),
                
                    signingCredentials: cred
                );

                return Ok(
                        new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(meuToken)
                        });
                
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}