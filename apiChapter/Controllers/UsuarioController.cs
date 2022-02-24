using apiChapter.Models;
using apiChapter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace apiChapter.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _repository;

        public UsuarioController(UsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_repository.Listar());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario usuarioProcurado = _repository.BuscarPorId(id);

                if (usuarioProcurado == null)
                {
                    return NotFound("Usuario não encontrado!!");
                }

                return Ok(usuarioProcurado);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                _repository.CadastrarUsuario(usuario);

                return StatusCode(201);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Atualizar(int id, Usuario u)
        {
            try
            {
                _repository.Atualizar(id, u);

                return StatusCode(204);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _repository.Deletar(id);

                return StatusCode(204);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}