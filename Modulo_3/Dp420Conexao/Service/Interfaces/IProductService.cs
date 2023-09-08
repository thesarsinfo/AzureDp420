using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.DTO;
using Dp420Conexao.Model;
using Microsoft.Azure.Cosmos;

namespace Dp420Conexao.Service.Interfaces
{
    public interface IProductService
    {
        Task<Product> Salvar(ProductDTO produto) ;
        Task<Product> LerDocumento(ProductReadDTO produto);
        Task<Product> AtualizarDocumento(ProductReadDTO produtoReadDTO,ProductDTO produtoAtualizar);
        Task<bool> ApagarDocumento(ProductReadDTO produto);
        Task<TransactionalBatchResponse> SalvarBatch();
        Task<bool>  CriarProdutosProntosBulk();
        Task<List<Product>> LerTodosProdutos();
        Task<List<Product>> LerProdutosPaginados();
        
    }
}