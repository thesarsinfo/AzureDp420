using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.Infrastructure.DatabaseContext;
using Dp420Conexao.Model;
using Microsoft.Azure.Cosmos;

namespace Dp420Conexao.Repository
{
    public class ProdutoRepository : ProdutoRepositoryInt
    {
        private readonly CosmosNoSQLContextInt _cosmo;

        public ProdutoRepository(CosmosNoSQLContextInt cosmo )
        {
            
           _cosmo = cosmo;
        }

        public async Task<Product>  cadastrarProduto ( Product produto){
            Container caixa = _cosmo.ConsultarContainerCosmo("Product");
            return await caixa.CreateItemAsync(produto);
        }
    }
}