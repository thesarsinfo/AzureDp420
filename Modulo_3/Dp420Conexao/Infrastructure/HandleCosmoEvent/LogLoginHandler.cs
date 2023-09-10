

using Dp420Conexao.Infrastructure.DatabaseContext;
using Dp420Conexao.Model.Login;
using Microsoft.Azure.Cosmos;

namespace Dp420Conexao.Infrastructure.HandleCosmoEvent
{
    public class LogLoginHandler
    {
        private readonly ICosmosNoSQLContext _cosmo;
        private readonly Container _caixa;
        private readonly Container _caixa2;

        public LogLoginHandler(ICosmosNoSQLContext cosmo)
        {
            _cosmo = cosmo;
            _caixa = _cosmo.consultarContainerCosmo("loglogin");
            _caixa2 = _cosmo.consultarContainerCosmo("analise");
        }

        async Task HandleChanges(
            ChangeFeedProcessorContext context,
            IReadOnlyCollection<LogLogin> changes,
            CancellationToken cancellationToken)
        {
            foreach (LogLogin logLogin in changes) {
                
            }
        }

        public async Task StartChangeFeedProcessor()
        {
            var changeFeedProcessorBuilder = _caixa.GetChangeFeedProcessorBuilder<LogLogin>(
                processorName: "productsProcessor",
                onChangesDelegate: HandleChanges
            );

            var changeFeedProcessor = changeFeedProcessorBuilder
                .WithInstanceName("instance1")
                .WithLeaseContainer(_caixa2) // Use the same container for leases
                .WithPollInterval(TimeSpan.FromSeconds(300))                 
                .Build();

            // Start the change feed processor
            await changeFeedProcessor.StartAsync();
        }
    }
}
