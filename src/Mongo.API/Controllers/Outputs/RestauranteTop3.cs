using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.API.Controllers.Outputs
{
    public class RestauranteTop3
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Cozinha { get; set; }
        public string Cidade { get; set; }
        public double Estrelas { get; set; }
    }
}