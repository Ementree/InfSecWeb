using System.Text.Json;
using InfSecWeb.RSA.Dtos;
using InfSecWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfSecWeb.RSA
{
    [ApiController]
    [Route("api/[controller]")]
    public class RsaController: ControllerBase
    {
        private readonly RsaParametersGenerator _rsaParametersGenerator;
        private readonly RsaEncrypter _rsaEncrypter;

        public RsaController(RsaParametersGenerator rsaParametersGenerator, RsaEncrypter rsaEncrypter)
        {
            _rsaParametersGenerator = rsaParametersGenerator;
            _rsaEncrypter = rsaEncrypter;
        }

        [HttpGet("generateParams")]
        public IActionResult GenerateParams()
        {
            return Ok(_rsaParametersGenerator.GenerateParameters());
        }

        [HttpPost("setParams")]
        public IActionResult SetParams([FromBody] RsaDto parameters)
        {
            return Ok(new RsaParameters(parameters.P, parameters.Q, parameters.E));
        }
        
        [HttpPost("encrypt")]
        public IActionResult Encrypt([FromBody] RsaEncryptedDto dto)
        {
            return Ok(JsonSerializer.Serialize(_rsaEncrypter.Encrypt(dto)));
        }
        
        [HttpPost("decrypt")]
        public IActionResult Decrypt([FromBody]RsaDecryptedDto dto)
        {
            return Ok(JsonSerializer.Serialize(_rsaEncrypter.Decrypt(dto)));
        }
    }
}