using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using WCDomain.Services;
using WCDomain.Models;

namespace ConfieDomainTests.Services
{
    [TestClass]
    public class ClienteServiceTests
    {
        private readonly IClienteService _clienteService;

        public ClienteServiceTests()
        {
            var clientes = new List<Cliente>
            {
                new Cliente { Id = 1, Cpf = "Cpf1", Nome = "Nome1" },
                new Cliente { Id = 2, Cpf = "Cpf2", Nome = "Nome2" },
                new Cliente { Id = 3, Cpf = "Cpf3", Nome = "Nome3" }
            };
            var compras = new List<Compra>
            {
                new Compra
                {
                    Codigo = "Codigo1",
                    DataTexto = "19-02-2016",
                    IdClienteTexto = "2",
                    ValorTotal = 100,
                    Itens = new List<Item>
                    {
                        new Item
                        {
                            Produto = "Casa Silva Reserva",
                            Categoria = "Tinto",
                            Pais = "Chile",
                            Preco = 100,
                            Safra = "2014",
                            Variedade = "Cabernet Sauvignon"
                        },
                        new Item
                        {
                            Produto = "Casa Valduga Raízes",
                            Categoria = "Tinto",
                            Pais = "Brasil",
                            Preco = 55,
                            Safra = "2013",
                            Variedade = "Cabernet Sauvignon"
                        }
                    }
                },
                new Compra 
                { 
                    Codigo = "Codigo2", 
                    DataTexto = "19-02-2016", 
                    IdClienteTexto = "1",
                    ValorTotal = 100,
                    Itens = new List<Item>
                    {
                        new Item 
                        { 
                            Produto = "Casa Silva Reserva",
                            Categoria = "Tinto",
                            Pais = "Chile",
                            Preco = 100,
                            Safra = "2014",
                            Variedade = "Cabernet Sauvignon"
                        }
                    }
                },
                new Compra
                {
                    Codigo = "Codigo3",
                    DataTexto = "19-02-2016",
                    IdClienteTexto = "1",
                    ValorTotal = 100,
                    Itens = new List<Item>
                    {
                        new Item
                        {
                            Produto = "Casa Silva Reserva",
                            Categoria = "Tinto",
                            Pais = "Chile",
                            Preco = 100,
                            Safra = "2014",
                            Variedade = "Cabernet Sauvignon"
                        }
                    }
                }
            };

            var mock = new Mock<IApiReaderService>();
            mock.Setup(p => p.ReadUriAsync<List<Cliente>>(It.IsAny<string>())).Returns(Task.Run(() => clientes));
            mock.Setup(p => p.ReadUriAsync<List<Compra>>(It.IsAny<string>())).Returns(Task.Run(() => compras));

            _clienteService = new ClienteService(mock.Object);
        }

        [TestMethod]
        public void GetClientesOrdemMaiorValorTotalCompras()
        {
            var clientes = _clienteService.GetClientesOrdemMaiorValorTotalCompras();

            Assert.AreEqual(clientes[0].valorTotalCompras, 200);
            Assert.AreEqual(clientes[1].valorTotalCompras, 100);
        }

        [TestMethod]
        public void GetClienteMaiorCompraUnica()
        {
            var cliente = _clienteService.GetClienteMaiorCompraUnica(2016);

            Assert.AreEqual(cliente.valorTotalCompra, 100);
        }

        [TestMethod]
        public void GetClientesFies()
        {
            var clientes = _clienteService.GetClientesFies();

            Assert.AreEqual(clientes[0].quantidadeCompras, 2);
            Assert.AreEqual(clientes[1].quantidadeCompras, 1);
        }

        [TestMethod]
        public void GetVinhoRecomendado()
        {
            var item = _clienteService.GetVinhoRecomendado(1);

            Assert.AreEqual(item.Produto, "Casa Valduga Raízes");
        }
    }
}
