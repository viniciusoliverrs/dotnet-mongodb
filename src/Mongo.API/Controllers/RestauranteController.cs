using Microsoft.AspNetCore.Mvc;
using Mongo.API.Controllers.Inputs;
using Mongo.API.Domain.Entities;
using Mongo.API.Domain.Enums;
using Mongo.API.Domain.ValueObjects;

namespace Mongo.API.Controllers;

[ApiController]
public class RestauranteController : ControllerBase
{
    // private readonly RestauranteRepository _restauranteRepository;

    // public RestauranteController(RestauranteRepository restauranteRepository)
    // {
    //     _restauranteRepository = restauranteRepository;
    // }

    [HttpPost("restaurante")]
    public ActionResult IncluirRestaurante([FromBody] RestauranteInclusao restauranteInclusao)
    {
        var cozinha = ECozinhaHelper.ConverterDeInteiro(restauranteInclusao.Cozinha);

        var restaurante = new Restaurante(restauranteInclusao.Nome, cozinha);
        var endereco = new Endereco(
            restauranteInclusao.Logradouro,
            restauranteInclusao.Numero,
            restauranteInclusao.Cidade,
            restauranteInclusao.UF,
            restauranteInclusao.Cep);

        restaurante.AtribuirEndereco(endereco);

        if (!restaurante.Validar())
        {
            return BadRequest(
                new
                {
                    errors = restaurante.ValidationResult.Errors.Select(_ => _.ErrorMessage)
                });
        }

        // _restauranteRepository.Inserir(restaurante);

        return Ok(
            new
            {
                data = "Restaurante inserido com sucesso"
            }
        );
    }
}

