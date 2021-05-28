using InfSecWeb.DSA.Dtos;
using InfSecWeb.DSA.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfSecWeb.DSA
{
    [ApiController]
    [Route("api/[controller]")]
    public class DsaController: ControllerBase
    {
        private readonly DsaGenerator _dsaGenerator;
        private readonly DsaEncrypter _encrypter;

        public DsaController(DsaGenerator dsaGenerator, DsaEncrypter encrypter)
        {
            _dsaGenerator = dsaGenerator;
            _encrypter = encrypter;
        }

        [HttpGet("generateParams")]
        public IActionResult GenerateParams()
        {
            return Ok(_dsaGenerator.GenerateParameters());
        }
        
        [HttpPost("sign")]
        public IActionResult Sign([FromBody] DsaParameters parameters)
        {
            return Ok(_encrypter.Sign(parameters));
        }
        
        [HttpPost("checkSign")]
        public IActionResult CheckSign([FromBody] DsaParameters parameters)
        {
            return Ok(_encrypter.CheckSign(parameters));
        }
    }
}