using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.DTO;
using Dp420Conexao.Model;

namespace Dp420Conexao.Service.Interfaces
{
    public interface IProductService
    {
        Task<Product> Salvar(ProductDTO produto) ;
        Task<Product> LerDocumento(ProductReadDTO produto);
        Task<Product> AtualizarDocumento(ProductReadDTO produtoReadDTO,ProductDTO produtoAtualizar);
        Task<bool> ApagarDocumento(ProductReadDTO produto);
    }
}