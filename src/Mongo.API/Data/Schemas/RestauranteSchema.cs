using Mongo.API.Domain.Entities;
using Mongo.API.Domain.Enums;
using Mongo.API.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongo.API.Data.Schemas
{
    public class RestauranteSchema
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public ECozinha Cozinha { get; set; }
        public EnderecoSchema Endereco { get; set; }
    }

    public static class RestauranteSchemaExtensao
    {
        public static Restaurante ConverterParaDomain(this RestauranteSchema document)
        {
            var restaurante = new Restaurante(document.Id, document.Nome, document.Cozinha);
            var endereco = new Endereco(document.Endereco.Logradouro, document.Endereco.Numero, document.Endereco.Cidade, document.Endereco.UF, document.Endereco.Cep);
            restaurante.AtribuirEndereco(endereco);

            return restaurante;
        }
    }
}