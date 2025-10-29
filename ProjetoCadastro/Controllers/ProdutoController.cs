using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjetoCadastro.Models;
using ProjetoCadastro.Repositorio;
namespace ProjetoCadastro.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio _repositorio;

        private readonly IConfiguration _configuration;

        public ProdutoController(IConfiguration configuration)
        {
            _configuration = configuration;
            _repositorio = new ProdutoRepositorio(_configuration);
        }

        // GET:Listar
        public IActionResult Listar()
        {
            var lista = _repositorio.Listar();
            return View(lista);
        }

        // GET:Cadastrar
        public IActionResult Cadastrar()
        {
            return View();
        }

        // POST:Cadastrar
        [HttpPost]
        public IActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _repositorio.Cadastrar(produto);
                return RedirectToAction("Listar");
            }

            return View(produto);
        }

        // GET:Editar/
        public IActionResult Editar(int id)
        {
            var produto = _repositorio.Listar().FirstOrDefault(p => p.Id == id);
            if (produto == null)
                return NotFound();

            return View(produto);
        }

        // POST:Editar
        [HttpPost]
        public IActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _repositorio.Atualizar(produto);
                return RedirectToAction("Listar");
            }

            return View(produto);
        }

        // GET:Excluir
        public IActionResult Excluir(int id)
        {
            _repositorio.Deletar(id);
            return RedirectToAction("Listar");
        }
    }
}
