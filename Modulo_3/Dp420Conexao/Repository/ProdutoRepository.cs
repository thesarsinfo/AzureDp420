using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.Infrastructure.DatabaseContext;
using Dp420Conexao.Model;
using Infrastructure.DatabaseContext;
using Microsoft.Azure.Cosmos;

namespace Dp420Conexao.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ICosmosNoSQLContext _cosmo;
        private readonly Container _caixa;

        public ProdutoRepository(ICosmosNoSQLContext cosmo )
        {
            
           _cosmo = cosmo;           
           _caixa = _cosmo.consultarContainerCosmo("produto");
        }

        public async Task<Product>  CadastrarProduto ( Product produto){
            return await _caixa.CreateItemAsync(produto);
        }
        public async Task<Product>  LerProduto ( string id, PartitionKey categoryId){
            return await _caixa.ReadItemAsync<Product>(id,categoryId);
        }
        public async Task<Product>  UpdateProduto ( Product produto){           
            return await _caixa.UpsertItemAsync<Product>(produto);
        }
        public async Task<bool> ApagarProduto ( string id, PartitionKey categoryId)
        {            
            await _caixa.DeleteItemAsync<Product>(id,categoryId);
            return true;
        }
    }
}