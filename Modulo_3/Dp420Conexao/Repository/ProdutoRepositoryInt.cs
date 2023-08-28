using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.Model;

namespace Dp420Conexao.Repository
{
    public interface ProdutoRepositoryInt
    {
        Task<Product>  cadastrarProduto ( Product produto);
    }
}