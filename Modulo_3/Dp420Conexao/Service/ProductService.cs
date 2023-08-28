using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.DTO;
using Dp420Conexao.Model;
using Dp420Conexao.Repository;
using Dp420Conexao.Service.Interfaces;

namespace Dp420Conexao.Service
{
    public class ProductService : ProductServiceInt
    {
        private readonly ProdutoRepositoryInt _produto;

        public ProductService(ProdutoRepositoryInt produto)
        {
            _produto = produto;
        }

        public Task<Product> Salvar(ProductDTO produto) {
            Product produtos = ToVO(produto);
            return _produto.cadastrarProduto(produtos);           
        }
        public Product ToVO(ProductDTO product)
        {
            Product produto = new()
            {
                categoryId = product.categoryId,
                name = product.name,
                price = product.price,
                tags = product.tags
            };
            return produto;
        }
        public ProductDTO ToDTO(Product product)
        {
            ProductDTO produtoDTO = new()
            {
                categoryId = product.categoryId,
                name = product.name,
                price = product.price,
                tags = product.tags
            };
            return produtoDTO;
        }
    }
}