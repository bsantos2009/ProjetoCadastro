using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjetoCadastro.Models;
using ProjetoCadastro.Repositorio;
// Define o nome e onde a classe ProdutoController está localizada. Namespaces ajudam a organizar o código e evitar conflitos de nomes.
namespace ProjetoCadastro.Controllers
{
    // Criando a classe ProdutoController e herdando  da classe controller
    public class ProdutoController : Controller
    {
        /* Declara uma variável privada somente leitura do tipo ProdutoRepositorio chamada _repositorio.
         O "readonly" indica que o valor desta variável só pode ser atribuído no construtor da classe.
         ProdutoRepositorio é uma classe do repositorio responsável por interagir com a camada de dados para gerenciar informações dos prdutos.*/
        private readonly ProdutoRepositorio _repositorio;


        /* Define o construtor da classe LoginController.
        Recebe uma instância de UsuarioRepositorio como parâmetro (injeção de dependência)*/
        public ProdutoController(ProdutoRepositorio repositorio)
        {
            /* O construtor é chamado quando uma nova instância de ProdutoController é criada.*/
            _repositorio = repositorio;
        }

        // GET:Listar
        public IActionResult Listar()
        {
            var lista = _repositorio.Listar();
            return View(lista);
        }

        // GET:Cadastrar
        /* Action para exibir o formulário de cadastro de produtos (via Requisição GET)*/
        public IActionResult Cadastrar()
        {
            //retorna a Página
            return View();
        }

        // POST:Cadastrar
        // Action que recebe e processa os dados que serão enviados pelo formulário de cadastro de produto (via Requisição POST)
        [HttpPost]
        public IActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                /* O parâmetro 'produto' recebe os dados enviados pelo formulário,
            que são automaticamente mapeados para as propriedades da classe Produto.
            Chama o método no repositório para cadastrar o novo produto no sistema.*/
                _repositorio.Cadastrar(produto);
                //redireciona para pagina Index
                return RedirectToAction("Listar");
            }

            return View(produto);
        }

        // GET:Editar/
        /* Action para exibir o formulário de edição de um produto específico (via Requisição GET)
         Este método recebe o 'id' do produto a ser editado como parâmetro.*/
        public IActionResult Editar(int id)
        {
            // Obtém o produto específico do repositório usando o ID fornecido.
            var produto = _repositorio.Listar().FirstOrDefault(p => p.Id == id);

            // Verifica se o produto foi encontrado. É uma boa prática tratar casos onde o ID é inválido.
            if (produto == null)

                // Você pode retornar um NotFound (código de status 404) ou outra resposta apropriada.
                return NotFound();

            // Retorna a View associada a esta Action (Editar.cshtml),
            return View(produto);
        }

        // POST:Editar
        // Carrega a lista de Produto que envia a alteração(post)
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
            // Obtém o produto específico do repositório usando o Codigo fornecido.
            _repositorio.Deletar(id);
            // Retorna a View de confirmação de exclusão, passando o produto como modelo.
            return RedirectToAction("Listar");
        }
    }
}
