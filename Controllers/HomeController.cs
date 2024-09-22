
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMVC_Tests.Models;

namespace WebMVC_Tests.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly EstoqueDbContext _context;

        public HomeController(ILogger<HomeController> logger, EstoqueDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        public ActionResult Estoque(string searchQuery)
        {
            var produtos = from p in _context.Produtos select p;

            if (!String.IsNullOrEmpty(searchQuery))
            {
                produtos = produtos.Where(p => p.Nome.ToUpper().Contains(searchQuery.ToUpper()));
            }
            return View(produtos.ToList());
        }


        public IActionResult CreateEditItem(int? id)
        {
            if (id != null)
            {
                // Editar
                var produtoDb = _context.Produtos.SingleOrDefault(produto => produto.Id == id);
                return View(produtoDb);
            }

            
            return View();
        }

        public IActionResult DeleteProduto(int id) 
        {
            var produtoDb = _context.Produtos.SingleOrDefault(produto => produto.Id == id);
            _context.Produtos.Remove(produtoDb);
            _context.SaveChanges();
            return RedirectToAction("Estoque");
        }

        public IActionResult CreateEditItemForm(Produto model) 
        {
            if(model.Id == 0)
            {
                // Criar
                _context.Produtos.Add(model);
            }
            else
            {
                // Editar
                _context.Produtos.Update(model);
            }
            

            _context.SaveChanges();

            return RedirectToAction("Estoque");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
