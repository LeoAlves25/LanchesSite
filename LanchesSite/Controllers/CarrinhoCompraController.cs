using LanchesSite.Models;
using LanchesSite.Repositories.Interfaces;
using LanchesSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesSite.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.carrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()

            };
            return View(carrinhoCompraVM);
        }

        //[Authorize]
        public IActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
        {
            if (User.Identity.IsAuthenticated)
            {

                var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheId);

                if (lancheSelecionado != null)
                {
                    _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
                }
                return RedirectToAction("Index");

            }

            return RedirectToAction("Login", "Account");

        }

        //[Authorize]
        public IActionResult RemoverItemDoCarrinhoCompra(int lancheId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheId);

                if (lancheSelecionado != null)
                {
                    _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
                }
                return RedirectToAction("Index");


            }

            return RedirectToAction("Login", "Account");

        }
    }

}
