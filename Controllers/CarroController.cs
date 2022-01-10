using Carros.WebAPI.Dal.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carros.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private readonly ICarro _carro;
        public CarroController(ICarro carro)
        {
            this._carro = carro;
        }

        [HttpGet]
        public Task<List<Model.Carro>> Get()
        {
            return _carro.GetCarros();
        }
        [HttpGet("{id}")]
        public Task<Model.Carro> Get(string id)
        {
            return _carro.GetCarroId(id);
        }
        [HttpPost]
        public string Post([FromBody] Model.Carro carro) => _carro.AddCarro(carro);
        [HttpPut]
        public void Put([FromBody] Model.Carro carro)
        {
            _carro.UpdateCarro(carro);
        }
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _carro.DeleteCarro(id);
        }

    }
}
