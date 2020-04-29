using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Dynamic;

using WCDomain.Models;

namespace WCDomain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IApiReaderService _apiReaderService;

        private readonly HashSet<Item> _itens;
        private readonly List<Cliente> _clientes;
        private readonly List<Compra> _compras;

        public ClienteService(IApiReaderService apiReaderService)
        {
            _apiReaderService = apiReaderService ??
                throw new ArgumentNullException(nameof(apiReaderService));

            _itens = new HashSet<Item>();
            _clientes = GetClientes()?.Result;
            _compras = GetCompras()?.Result;
        }

        public List<dynamic> GetClientesFies()
            => _compras?.GroupBy(compra => compra.Cliente)
                ?.Select(compra =>
                {
                    dynamic obj = new ExpandoObject();
                    obj.cliente = compra.Key;
                    obj.quantidadeCompras = compra.Count();
                    return obj;
                })
                ?.OrderByDescending(obj => obj.quantidadeCompras)
                ?.ToList();

        public List<dynamic> GetClientesOrdemMaiorValorTotalCompras()
            => _compras?.GroupBy(compra => compra.Cliente)
                ?.Select(compra =>
                {
                    dynamic obj = new ExpandoObject();
                    obj.cliente = compra.Key;
                    obj.valorTotalCompras = compra.Sum(c => c.ValorTotal);
                    return obj;
                })
                ?.OrderByDescending(obj => obj.valorTotalCompras)
                ?.ToList();

        public dynamic GetClienteMaiorCompraUnica(int ano)
        {
            var compra = _compras?.Where(compra => compra.Data.Year == ano)?.FirstOrDefault();
            dynamic obj = new ExpandoObject();
            obj.cliente = compra?.Cliente;
            obj.valorTotalCompra = compra?.ValorTotal;
            return obj;
        }

        public Item GetVinhoRecomendado(int idCliente)
        {
            var vinhosComprados = new HashSet<Item>();
            _compras.Where(compra => compra.Cliente.Id == idCliente).ToList()
                .ForEach(compra => vinhosComprados.UnionWith(compra.Itens));

            var vinhoPreferido = vinhosComprados?.GroupBy(item => new { item.Variedade, item.Categoria })
                ?.OrderByDescending(item => item.Count())
                ?.Select(item => item.Key).FirstOrDefault();

            var vinhosNuncaComprados = _itens.Where(item => !vinhosComprados.Contains(item))
                .OrderByDescending(item => item.Preco);

            var vinhoRecomendado = vinhosNuncaComprados?
                .Where(item => item.Variedade == vinhoPreferido?.Variedade 
                    && item.Categoria == vinhoPreferido?.Categoria)
                .FirstOrDefault();

            if (vinhoRecomendado == null)
            {
                vinhoRecomendado = vinhosNuncaComprados?
                    .Where(item => item.Variedade == vinhoPreferido?.Variedade 
                        || item.Categoria == vinhoPreferido?.Categoria)
                .FirstOrDefault();
            }

            return vinhoRecomendado;
        }

        private async Task<List<Cliente>> GetClientes()
        {
            const string URI = "http://www.mocky.io/v2/598b16291100004705515ec5";
            var clientes = await _apiReaderService.ReadUriAsync<List<Cliente>>(URI);
            return clientes;
        }

        private async Task<List<Compra>> GetCompras()
        {
            const string URI = "http://www.mocky.io/v2/598b16861100004905515ec7";
            var compras = await _apiReaderService.ReadUriAsync<List<Compra>>(URI);
            compras.ForEach(compra =>
            {
                compra.Cliente = _clientes.FirstOrDefault
                    (cliente => cliente.Id == compra.IdCliente);
                _itens.UnionWith(compra.Itens);
            });
            compras?.Sort((x, y) => y.ValorTotal.CompareTo(x.ValorTotal));
            return compras;
        }

    }
}
