using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Dp420Conexao.Infrastructure.DatabaseContext;
using Dp420Conexao.Model;
using Infrastructure.DatabaseContext;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Dp420Conexao.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ICosmosNoSQLContext _cosmo;
        private readonly Container _caixa;
        private TransactionalBatch batch;

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
        public async Task<List<Product>> QueryProduto ( QueryDefinition query,QueryRequestOptions opcao) {

            using FeedIterator<Product> produto = _caixa.GetItemQueryIterator<Product>(query,requestOptions: opcao);
            List<Product> products = new();
            while (produto.HasMoreResults){
                Product response = (Product) await produto.ReadNextAsync();
                products.Add(response);
            }
            return products;
        }

        public async Task<Product>  UpdateProduto ( Product produto){           
            return await _caixa.UpsertItemAsync<Product>(produto);
        }
        public async Task<bool> ApagarProduto ( string id, PartitionKey categoryId)
        {            
            await _caixa.DeleteItemAsync<Product>(id,categoryId);
            return true;
        }
        public async Task<TransactionalBatchResponse> CriarProdutosProntos() {
            batch = null;
            //Partition key sempre mantem o mesmo
            string[] categorias = {"Queijos","Queijos","Queijos","Queijos"}; 
            string[] nomes = { "mussarella", "tres queijos", "gorgozola", "provolone"};
            double[] precos = {30.05,35.25,26.89,39.56};
            List<Tag> tagsLista = new();
            {
                new Tag(new Guid("856664a0-f6a2-46f3-ad9f-ec61755e67a1"),"Nova Casa",true);
                new Tag(new Guid("67343c80-85d7-4048-bc56-bb552fb06ac4"),"Pedi pizza",true);
                new Tag (new Guid("be079986-a0e6-458a-afc6-2691a4ddf175"),"La Majiori",true);
                new Tag (new Guid("68351a29-7879-4790-9e3d-28d6d7c8f86a"),"Tetera",false);
            };
                
            PartitionKey partitionKey = new PartitionKey(categorias[0]);
            for (int i = 0; i < categorias.Length; i++) {
                Product produto = new()
                {
                    Id = Guid.NewGuid() ,
                    CategoriaId = categorias[i],
                    Name = nomes[i],
                    Price =precos[i],
                    Tags = new List<Tag>{tagsLista[i]}                                      
                };  
               
                batch = _caixa.CreateTransactionalBatch(partitionKey)
                                                                .CreateItem<Product>(produto);
            }     
            using TransactionalBatchResponse response = await batch.ExecuteAsync();      
            return response;
        }
        public async Task<bool> CriarProdutosProntosBulk() {
            //Partition key sempre mantem o mesmo            
            string[] categorias = {"Defumado","Defumado","Defumado","Defumado"}; 
            string[] nomes = { "calabresa", "presunto", "salame", "calabresa com salame"};
            double[] precos = {40.05,95.25,46.89,29.56};
            List<Task> concurrentTasks = new List<Task>();
            PartitionKey partitionKey = new PartitionKey(categorias[0]);
            for (int i = 0; i < categorias.Length; i++) {
                Product produto = new()
                {
                    Id = Guid.NewGuid() ,
                    CategoriaId = categorias[i],
                    Name = nomes[i],
                    Price =precos[i]                    
                };  
                concurrentTasks.Add(_caixa.CreateItemAsync(produto,partitionKey));
            }
            try {
                await Task.WhenAll(concurrentTasks);
                return true;
            } catch ( Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }           
        } 
    }
}