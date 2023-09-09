using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.Infrastructure.DatabaseContext;
using Dp420Conexao.Model.Login;
using Dp420Conexao.Repository.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Dp420Conexao.Repository
{
    public class LogLoginRepository : ILogLoginRepository
    {
        private readonly ICosmosNoSQLContext _cosmo;
        private readonly Container _caixa;

        public LogLoginRepository(ICosmosNoSQLContext cosmo)
        {
           _cosmo = cosmo;           
           _caixa = _cosmo.consultarContainerCosmo("loglogin");
        }

        public async Task<LogLogin> Save( LogLogin log) {
            return await _caixa.CreateItemAsync(log);
        }
    }
}