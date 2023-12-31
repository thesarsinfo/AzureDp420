using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.DTO;
using Dp420Conexao.Model;
using Dp420Conexao.Repository;
using Dp420Conexao.Service.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Dp420Conexao.Service
{
    public class ProductService : IProductService
    {
        private readonly IProdutoRepository _produto;

        public ProductService(IProdutoRepository produto)
        {
            _produto = produto;
        }

        public Task<Product> Salvar(ProductDTO produto) {
            Product produtos = ToVO(produto);
            return _produto.CadastrarProduto(produtos);           
        }
        public Task<TransactionalBatchResponse> SalvarBatch() {
        var response  = _produto.CriarProdutosProntos();
        return response;
        }
        public async Task<bool>  CriarProdutosProntosBulk() {
            return await _produto.CriarProdutosProntosBulk();
        }
        public async Task<Product> LerDocumento(ProductReadDTO produto) {
            PartitionKey partKey = new(produto.CategoryId);
            return await _produto.LerProduto(produto.Id.ToString(), partKey);           
        }
        public async Task<List<Product>> LerTodosProdutos() {
            string sql = "SELECT * FROM product p";
            QueryDefinition query = new (sql);  
            QueryRequestOptions options = new();          
            return await _produto.QueryProduto(query,options);           
        }
        public async Task<List<Product>> LerProdutosPaginados() {
            string sql = "SELECT * FROM product p";
            QueryDefinition query = new (sql);       
            QueryRequestOptions options = new(); 
            options.MaxItemCount = 3;     
            return await _produto.QueryProduto(query,options);           
        }
        public async Task<Product> AtualizarDocumento(ProductReadDTO produtoReadDTO,ProductDTO produtoAtualizar) {
            Product produto = await LerDocumento(produtoReadDTO);
            Product produtoAtualizado = ToVOAtualizar(produto, produtoAtualizar);              
            return await _produto.UpdateProduto(produtoAtualizado);           
        }
        public async Task<bool> ApagarDocumento(ProductReadDTO produto) {
            PartitionKey partKey = new(produto.CategoryId);
            return await _produto.ApagarProduto(produto.Id.ToString(), partKey);           
        }
        public Product ToVO(ProductDTO product)
        {
            Product produto = new()
            {
                Id = Guid.NewGuid(),
                CategoriaId = product.CategoriaId,
                Name = product.Name,
                Price = product.Price,
                Tags = product.Tags.Select(tagDto => new Tag {
                    Id = Guid.NewGuid(),
                    Name = tagDto.Name,
                    Online = tagDto.Online                
                }).ToList()
            };
            return produto;
        }
    public List<Tag> ToVO(List<TagDTO> listaTags)
    {
        List<Tag> lista = listaTags.Select(tag => new Tag
        {
            Id = tag.Id,
            Name = tag.Name,
            Online = tag.Online
        }).ToList();

        return lista;
    }


        public ProductDTO ToDTO(Product product)
        {
            ProductDTO produtoDTO = new()
            {
                CategoriaId = product.CategoriaId,
                Name = product.Name,
                Price = product.Price,
                Tags = product.Tags.Select(tag => new TagDTO {
                    Id = tag.Id,
                    Name = tag.Name,
                    Online = tag.Online                
                }).ToList()


            };
            return produtoDTO;
        }
        public Product ToVOAtualizar(Product produto, ProductDTO produtoAtualizar) {
                          
            produto.CategoriaId = (produtoAtualizar.CategoriaId != null)? produtoAtualizar.CategoriaId : produto.CategoriaId;
            produto.Name = (produtoAtualizar.Name != null)? produtoAtualizar.Name : produto.Name;
            produto.Price = (produtoAtualizar.Price != null)? produtoAtualizar.Price : produto.Price;
            produto.Tags = (produtoAtualizar.Tags != null)?  ToVO(produtoAtualizar.Tags): produto.Tags;
            
            return produto;
        }
      
    }
}