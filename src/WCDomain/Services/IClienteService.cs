using System.Collections.Generic;

using WCDomain.Models;

namespace WCDomain.Services
{
    public interface IClienteService
    {
        List<dynamic> GetClientesOrdemMaiorValorTotalCompras();
        dynamic GetClienteMaiorCompraUnica(int ano);
        List<dynamic> GetClientesFies();
        Item GetVinhoRecomendado(int idCliente);
    }
}
