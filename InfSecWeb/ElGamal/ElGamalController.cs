using System.Text.Json;
using System.Text.Json.Serialization;
using InfSecWeb.ElGamal.Dtos;
using InfSecWeb.ElGamal.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfSecWeb.ElGamal
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElGamalController: ControllerBase
    {
        private readonly ElGamalParametersGenerator _elGamalParametersGenerator;
        private readonly ElGamalEncrypter _encrypter;
        public ElGamalController(ElGamalParametersGenerator elGamalParametersGenerator, ElGamalEncrypter encrypter)
        {
            _elGamalParametersGenerator = elGamalParametersGenerator;
            _encrypter = encrypter;
        }

        [HttpGet("generateParams")]
        public IActionResult GenerateParams()
        {
            var parameters = _elGamalParametersGenerator.Generate();
            return Ok(parameters);
        }

        [HttpGet("setParams/{p}")]
        public IActionResult SetParams([FromRoute] ulong p)
        {
            var parameters = _elGamalParametersGenerator.Set(p);
            return Ok(parameters);
        }
        
        [HttpPost("encrypt")]
        public IActionResult Encrypt([FromBody] ElGamalEncryptedDto dto)
        {
            var decryptedMessage = _encrypter.Encrypt(dto);
            return Ok(JsonSerializer.Serialize(decryptedMessage));
        }
        
        [HttpPost("decrypt")]
        public IActionResult Decrypt([FromBody] ElGamalDecryptedDto dto)
        {
            return Ok(JsonSerializer.Serialize(_encrypter.Decrypt(dto)));
        }
    }
}