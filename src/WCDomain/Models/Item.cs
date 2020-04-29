using System;
using System.Text.Json.Serialization;

namespace WCDomain.Models
{
    public class Item
    {
        [JsonPropertyName("produto")]
        public string Produto { get; set; }
        [JsonPropertyName("variedade")]
        public string Variedade { get; set; }
        [JsonPropertyName("pais")]
        public string Pais { get; set; }
        [JsonPropertyName("categoria")]
        public string Categoria { get; set; }
        [JsonPropertyName("safra")]
        public string Safra { get; set; }
        [JsonPropertyName("preco")]
        public decimal Preco { get; set; }

        public override bool Equals(object obj)
            => obj is Item item &&
                Produto == item.Produto &&
                Variedade == item.Variedade &&
                Pais == item.Pais &&
                Categoria == item.Categoria &&
                Safra == item.Safra &&
                Preco == item.Preco;

        public override int GetHashCode()
            => HashCode.Combine(Produto, Variedade, Pais, Categoria, Safra, Preco);

        public override string ToString()
            => $"{Produto} ({Variedade} {Categoria})";
    }
}
