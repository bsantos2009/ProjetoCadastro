using Microsoft.AspNetCore.Mvc;
using ProjetoCadastro.Models;
using ProjetoCadastro.Repositorio;
// Define o nome e onde a classe UsuarioController está localizada. Namespaces ajudam a organizar o código e evitar conflitos de nomes.
namespace ProjetoCadastro.Controllers
{
    // Criando a classe UsuarioController e herdando  da classe controller
    public class UsuarioController : Controller
    {
        /* Declara uma variável privada somente leitura do tipo UsuarioRepositorio chamada _usuarioRepositorio.
        O "readonly" indica que o valor desta variável só pode ser atribuído no construtor da classe.
        UsuarioRepositorio é uma classe do repositorio responsável por interagir com a camada de dados para gerenciar informações de usuários.*/
        private readonly UsuarioRepositorio _repositorio;

        /*Define o construtor da classe UsuarioController. 
          Recebe uma instância de UsuarioRepositorio como parâmetro (injeção de dependência).*/
        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            // O construtor é chamado quando uma nova instância de LoginController é criada.
            _repositorio = usuarioRepositorio;
        }

        // GET: Usuario/Cadastrar
        public IActionResult Cadastrar()
        {
            // Retorna a pagina
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
            // Retorna a view usuario
            return View(usuario);
        }

        // GET:Editar/
        /* Action para exibir o formulário de edição de um cliente específico (via Requisição GET)
         Este método recebe o 'id' do cliente a ser editado como parâmetro.*/
        public IActionResult Editar(int id)
        {
            // Obtém o cliente específico do repositório usando o ID fornecido.
            var usuario = _repositorio.ObterUsuarioPorId(id);

            // Verifica se o cliente foi encontrado. É uma boa prática tratar casos onde o ID é inválido.
            if (usuario == null)
                // Você pode retornar um NotFound (código de status 404) ou outra resposta apropriada.
                return NotFound();

            // Retorna a View associada a esta Action (Editar.cshtml),
            return View(usuario);
        }

        // POST:Editar
        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _repositorio.Atualizar(usuario);
                return RedirectToAction("Listar");
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
                // Autenticação bem-sucedida
                // Redireciona o usuário para a action "Index" do Controller "Home".
                return RedirectToAction("Index", "Home");
            }
           
            /* Se a autenticação falhar (usuário não encontrado ou senha incorreta):
            Adiciona um erro ao ModelState. ModelState armazena o estado do modelo e erros de validação.
            O primeiro argumento ("") indica um erro
            O segundo argumento é a mensagem de erro que será exibida ao usuário.*/
            ModelState.AddModelError("", "Email ou senha inválidos.");
            
            //retorna view Login 
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
            // Obtém o cliente específico do repositório usando o Codigo fornecido.
            _repositorio.Deletar(id);
            // Retorna a view listar
            return RedirectToAction("Listar");
        }
    }
}
