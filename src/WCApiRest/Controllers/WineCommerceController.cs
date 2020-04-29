using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using WCDomain.Models;
using WCDomain.Services;

namespace WCApiRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WineCommerceController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public WineCommerceController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // # 1 - Liste os clientes ordenados pelo maior valor total em compras.
        [HttpGet]
        [Route("clientesordemmaiorvalortotalcompras")]
        public List<dynamic> GetClientesOrdemMaiorValorTotalCompras()
            => _clienteService.GetClientesOrdemMaiorValorTotalCompras();

        // # 2 - Mostre o cliente com maior compra única no último ano (2016).
        [HttpGet]
        [Route("clientemaiorcompraunica/{ano}")]
        public dynamic GetClienteMaiorCompraUnica(int ano)
            => _clienteService.GetClienteMaiorCompraUnica(ano);

        // # 3 - Liste os clientes mais fiéis.
        [HttpGet]
        [Route("clientesfies")]
        public List<dynamic> GetClientesFies()
            => _clienteService.GetClientesFies();

        // # 4 - Recomende um vinho para um determinado cliente a partir do histórico de compras.
        [HttpGet]
        [Route("vinhorecomendado/{idCliente}")]
        public Item GetVinhoRecomendado(int idCliente)
            => _clienteService.GetVinhoRecomendado(idCliente);
    }
}
