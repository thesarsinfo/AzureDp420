using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Dp420Conexao.Infrastructure.DatabaseContext
{
    public interface ICosmosNoSQLContext
    {
        public Task IniciadorCosmo();        
        public Container consultarContainerCosmo(string container);
    }
}