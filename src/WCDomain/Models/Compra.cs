using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace WCDomain.Models
{
    public class Compra
    {
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }
        [JsonPropertyName("data")]
        public string DataTexto { private get; set; }
        [JsonIgnore]
        public DateTime Data
        {
            get => DateTime.ParseExact(DataTexto, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        }
        [JsonPropertyName("cliente")]
        public string IdClienteTexto
        {
            get => IdCliente.ToString();
            set => IdCliente = int.Parse(Regex.Replace(value, @"[^\d]", ""));
        }
        public int IdCliente { get; private set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }
        [JsonPropertyName("itens")]
        public ICollection<Item> Itens { get; set; }
        [JsonPropertyName("valorTotal")]
        public decimal ValorTotal { get; set; }

        public override bool Equals(object obj)
            => obj is Compra compra && Codigo == compra.Codigo;

        public override int GetHashCode() => HashCode.Combine(Codigo);
    }
}
