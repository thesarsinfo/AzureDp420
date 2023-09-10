
using Dp420Conexao.DTO.Login;
using Dp420Conexao.Model.Login;
using Dp420Conexao.Repository.Interfaces;
using Dp420Conexao.Service.Interfaces;

namespace Dp420Conexao.Service
{
    public class LogLoginService : ILogLoginService
    {
        private readonly ILogLoginRepository _logLoginRepository;

        public LogLoginService(ILogLoginRepository logLoginRepository)
        {
            _logLoginRepository = logLoginRepository;
        }

        public async Task<LogLogin> CriarLogLogin(LogLoginDTO logLoginDTO)
        {
            LogLogin log = LogLoginDTOVO(logLoginDTO);
            var logs = await _logLoginRepository.GetLoginSystem(log);
            if (logs.Count > 0) {
                return log;
            } else {
                return await _logLoginRepository.Save(log);
            }
        }

        private LogLogin LogLoginDTOVO(LogLoginDTO logLoginDTO)
        {
            LogLogin logLogin = new(){
                Id = Guid.NewGuid(),
                Email = logLoginDTO.Email,
                Navegador = (Navegador) logLoginDTO.Navegador,
                Localizacao = (Localizacao) logLoginDTO.Localizacao,
                Sistema = (Sistema) logLoginDTO.Sistema                
            };
            return logLogin;
        }
    }
}