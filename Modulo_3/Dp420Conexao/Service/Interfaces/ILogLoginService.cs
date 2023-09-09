using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.DTO.Login;
using Dp420Conexao.Model.Login;

namespace Dp420Conexao.Service.Interfaces
{
    public interface ILogLoginService
    {
        public Task<LogLogin> CriarLogLogin(LogLoginDTO logLoginDTO);
        

    }
}