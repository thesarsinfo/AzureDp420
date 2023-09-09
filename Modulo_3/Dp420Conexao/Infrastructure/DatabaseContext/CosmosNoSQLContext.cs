
using System.Collections.ObjectModel;
using Dp420Conexao.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace Infrastructure.DatabaseContext
{
    public class CosmosNoSQLContext : ICosmosNoSQLContext
    {
        private readonly string databaseNome = "Teste420";
        private readonly IConfiguration _configuration;       

        private readonly CosmosDbSettings _cosmosDbSettings;        

        public CosmosNoSQLContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _cosmosDbSettings = _configuration.GetSection("CosmosDbSettings").Get<CosmosDbSettings>();
            // Agora você pode usar as propriedades de cosmosDbSettings...
        }

        public async Task IniciadorCosmo()
        {
            CosmosClientOptions options = new ()  { AllowBulkExecution = true };
            using var client = new CosmosClient(_cosmosDbSettings.Endpoint, _cosmosDbSettings.KeyOne, options);
            try
            {
                
                IndexingPolicy policy = this.DefinindoPolicy();
                ContainerProperties opcaoContainerProduto = new("produto", "/CategoriaId"){IndexingPolicy = policy};
                ContainerProperties opcaoContainerLogLogin = new("loglogin", "/Email");
                ContainerProperties opcaoContainerAnalise = new("analise","/id");
                Database database = await client.CreateDatabaseIfNotExistsAsync("Teste420");
                await database.CreateContainerIfNotExistsAsync(opcaoContainerProduto, 400);
                await database.CreateContainerIfNotExistsAsync(opcaoContainerLogLogin,400);
                await database.CreateContainerIfNotExistsAsync(opcaoContainerAnalise, 400);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar: " + ex.Message);
            }
            
        }
        private Database ConsultarCosmo()
        {
            try {
            CosmosClient client = new(_cosmosDbSettings.Endpoint, _cosmosDbSettings.KeyOne);
            Database database = client.GetDatabase(databaseNome);
            return database;
            } catch (Exception ex) {
                System.Console.WriteLine(ex.Message);
                 throw new InvalidOperationException("Erro ao conectar com o Cosmos DB", ex);
            }
            
        }
        public Container consultarContainerCosmo(string container) 
        {
            Database database = ConsultarCosmo();
            Container caixa = database.GetContainer(container);
            return caixa;
        }
        private IndexingPolicy DefinindoPolicy() 
        {
         
            IndexingPolicy politicaIndex = new()
            {
                IndexingMode = IndexingMode.Consistent,
                Automatic = true                           
            };

            IncludedPath pathIncluirUm = new() { Path = "/Price/?"};
            IncludedPath pathIncluirDois = new() {Path = "/Name/?"};
            politicaIndex.IncludedPaths.Add(pathIncluirUm);
            politicaIndex.IncludedPaths.Add(pathIncluirDois);
            ExcludedPath excludePath = new () {Path = "/Tags/[]/id/?"};
            politicaIndex.ExcludedPaths.Add(excludePath);
            CompositePath indiceComposto1 = new() {Path = "/Price/",Order = CompositePathSortOrder.Descending};
            CompositePath indiceComposto2 = new() {Path = "/Name/",Order = CompositePathSortOrder.Ascending};
            politicaIndex.CompositeIndexes.Add( new Collection<CompositePath>{indiceComposto1} );
            
            politicaIndex.CompositeIndexes.Add(new Collection<CompositePath> {indiceComposto2});
            return politicaIndex;            
        }
    }
}
