using Microsoft.AspNetCore.Mvc;
using ProjetoCadastro.Models;
using ProjetoCadastro.Repositorio;
namespace ProjetoCadastro.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio _repositorio;

        public ProdutoController()
        {
            _repositorio = new ProdutoRepositorio();
        }

        // GET: Produto/Listar
        public IActionResult Listar()
        {
            var lista = _repositorio.Listar();
            return View(lista);
        }

        // GET: Produto/Cadastrar
        public IActionResult Cadastrar()
        {
            return View();
        }

        // POST: Produto/Cadastrar
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

        // GET: Produto/Editar/
        public IActionResult Editar(int id)
        {
            var produto = _repositorio.Listar().FirstOrDefault(p => p.Id == id);
            if (produto == null)
                return NotFound();

            return View(produto);
        }

        // POST: Produto/Editar
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

        // GET: Produto/Excluir/
        public IActionResult Excluir(int id)
        {
            _repositorio.Deletar(id);
            return RedirectToAction("Listar");
        }
    }
}
