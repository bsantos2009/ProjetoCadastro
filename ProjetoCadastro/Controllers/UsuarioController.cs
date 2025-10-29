using Microsoft.AspNetCore.Mvc;
using ProjetoCadastro.Models;
using ProjetoCadastro.Repositorio;
namespace ProjetoCadastro.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _repositorio;

        public UsuarioController()
        {
            _repositorio = new UsuarioRepositorio();
        }

        // GET: Usuario/Cadastrar
        public IActionResult Cadastrar()
        {
            return View();
        }

        // POST: Usuario/Cadastrar
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _repositorio.Cadastrar(usuario);
                return RedirectToAction("Login");
            }

            return View(usuario);
        }

        // GET: Usuario/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Usuario/Login
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _repositorio.Login(email, senha);

            if (usuario != null)
            {
                

                return RedirectToAction("Index", "Home");
            }

            ViewBag.MensagemErro = "E-mail ou senha incorretos!";
            return View();
        }

        // GET: Usuario/Listar
        public IActionResult Listar()
        {
            var lista = _repositorio.Listar();
            return View(lista);
        }

        // GET: Usuario/Excluir/
        public IActionResult Excluir(int id)
        {
            _repositorio.Deletar(id);
            return RedirectToAction("Listar");
        }
    }
}
