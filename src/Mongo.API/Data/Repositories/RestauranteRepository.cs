using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mongo.API.Data.Context;
using Mongo.API.Data.Schemas;
using Mongo.API.Domain.Entities;
using Mongo.API.Domain.ValueObjects;
using MongoDB.Driver;

namespace Mongo.API.Data.Repositories
{
    public class RestauranteRepository
    {
        private readonly IMongoCollection<RestauranteSchema> _restaurantes;
        public RestauranteRepository(MongoContext database)
        {
            _restaurantes = database.DB.GetCollection<RestauranteSchema>("restaurantes");
        }

        public void Inserir(Restaurante restaurante)
        {
            var document = new RestauranteSchema
            {
                Nome = restaurante.Nome,
                Cozinha = restaurante.Cozinha,
                Endereco = new EnderecoSchema
                {
                    Logradouro = restaurante.Endereco.Logradouro,
                    Numero = restaurante.Endereco.Numero,
                    Cidade = restaurante.Endereco.Cidade,
                    Cep = restaurante.Endereco.Cep,
                    UF = restaurante.Endereco.UF
                }
            };

            _restaurantes.InsertOne(document);
        }

        public async Task<IEnumerable<Restaurante>> ObterTodos()
        {
            var restaurantes = new List<Restaurante>();

            await _restaurantes.AsQueryable().ForEachAsync(d =>
            {
                var r = new Restaurante(d.Id.ToString(), d.Nome, d.Cozinha);
                var e = new Endereco(d.Endereco.Logradouro, d.Endereco.Numero, d.Endereco.Cidade, d.Endereco.UF, d.Endereco.Cep);
                r.AtribuirEndereco(e);
                restaurantes.Add(r);
            });

            return restaurantes;
        }
    }
}