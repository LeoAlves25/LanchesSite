using LanchesSite.Models;
using LanchesSite.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesSite.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            if (User.Identity.IsAuthenticated)
            {


                int totalItensPedido = 0;
                decimal precoTotalPedido = 0.0m;

                //Obtem os itens do carrinho de compra do cliente
                List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhoCompraItems();
                _carrinhoCompra.carrinhoCompraItems = itens;

                //Verificar se existem itens de pedido
                if (_carrinhoCompra.carrinhoCompraItems.Count == 0)
                {
                    ModelState.AddModelError("", "Seu carrinho está vazio. Que tal incluir um lanche...");
                }

                //Calcular o total de itens e o total do pedido
                foreach (var item in itens)
                {
                    totalItensPedido += item.Quantidade;
                    precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
                }

                //Atribui os valores obtidos ao pedido
                pedido.TotalItensPedido = totalItensPedido;
                pedido.PedidoTotal = precoTotalPedido;

                //Validar os dados do pedido
                if (ModelState.IsValid)
                {
                    //Criar o pedido e os detalhes
                    _pedidoRepository.CriarPedido(pedido);

                    //Define mensagens ao cliente
                    ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                    ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                    //Limpar o carrinho do cliente
                    _carrinhoCompra.LimparCarrinho();

                    //Exibir a view com dados do cliente e do pedido
                    return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
                }

                return View(pedido);
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
