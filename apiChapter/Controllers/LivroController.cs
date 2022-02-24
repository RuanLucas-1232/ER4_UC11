using apiChapter.Models;
using apiChapter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace apiChapter.Controllers;

[Produces("application/json")]

[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
     private readonly LivroRepository _repository;
        public LivroController(LivroRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult ListarColecao()
        {
            return Ok(_repository.ListarLivros());
        }

        [HttpGet("{Id:int}")]
        public IActionResult ListarLivro(int Id)
        {
            return Ok(_repository.ListarLivroPesquisado(Id));
        }

        [HttpPost]
        public IActionResult CadastrarLivro(Livro livro)
        {
            _repository.CadastarLivro(livro);
            return StatusCode(201);
        }

        [HttpPut("{Id:int}")]
        public IActionResult AtualizarLivro(int Id, Livro livro)
        {
            if (livro != null)
            {
                _repository.AtualizarLivro(Id, livro);
                return StatusCode(204);
            }
            return StatusCode(400);
        }

        [HttpDelete("{Id:int}")]
        public IActionResult DeletarLivro(int Id)
        {
            _repository.Deletar(Id);
            return StatusCode(204);
        }

        
}
