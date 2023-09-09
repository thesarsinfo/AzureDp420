using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.Model.Login;

namespace Dp420Conexao.Repository.Interfaces
{
    public interface ILogLoginRepository
    {
        public Task<LogLogin> Save( LogLogin log);
    }
}