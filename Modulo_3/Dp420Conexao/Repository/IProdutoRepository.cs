using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.Model;
using Microsoft.Azure.Cosmos;

namespace Dp420Conexao.Repository
{
    public interface IProdutoRepository
    {
        Task<Product>  CadastrarProduto ( Product produto);
        Task<Product>  LerProduto ( string id, PartitionKey categoryId);
        Task<Product>  UpdateProduto ( Product produto);
        Task<bool> ApagarProduto ( string id, PartitionKey categoryId);
        Task<TransactionalBatchResponse> CriarProdutosProntos();
        Task<bool> CriarProdutosProntosBulk();
    }
}