using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.DTO;
using Dp420Conexao.Model;

namespace Dp420Conexao.Service.Interfaces
{
    public interface ProductServiceInt
    {
        Task<Product> Salvar(ProductDTO produto) ;
    }
}