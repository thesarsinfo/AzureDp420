
using Dp420Conexao.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Azure.Cosmos;

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
            using var client = new CosmosClient(_cosmosDbSettings.Endpoint, _cosmosDbSettings.KeyOne);
            try
            {
                Database database = await client.CreateDatabaseIfNotExistsAsync("Teste420");
                await database.CreateContainerIfNotExistsAsync("produto", "/CategoriaId", 400);
                
                
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
    }
}
