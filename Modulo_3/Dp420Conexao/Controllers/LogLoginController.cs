
using Dp420Conexao.DTO.Login;
using Dp420Conexao.Model.Login;
using Dp420Conexao.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;

namespace Dp420Conexao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogLoginController : ControllerBase
    {
        private readonly LogLoginService _logLoginService;

        public LogLoginController(LogLoginService logLoginService)
        {
            _logLoginService = logLoginService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginLog([FromBody] LogLoginDTO logLoginDTO )
        {
            LogLogin logLogin = await _logLoginService.CriarLogLogin(logLoginDTO);
            return Created("Criado com sucesso",logLogin);
        }
    }
}