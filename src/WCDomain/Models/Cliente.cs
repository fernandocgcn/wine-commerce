using System;
using System.Text.Json.Serialization;

namespace WCDomain.Models
{
    public class Cliente
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("cpf")]
        public string Cpf { get; set; }

        public override bool Equals(object obj)
            => obj is Cliente cliente && Id == cliente.Id;

        public override int GetHashCode() => HashCode.Combine(Id);

        public override string ToString() => Nome;
    }
}
