using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Dp420Conexao.Infrastructure.DatabaseContext
{
    public interface CosmosNoSQLContextInt
    {
        Task IniciadorCosmo();        
        Container ConsultarContainerCosmo(string container);
    }
}