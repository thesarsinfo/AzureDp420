using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.Infrastructure.DatabaseContext;
using Dp420Conexao.Model.Login;
using Dp420Conexao.Repository.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Dp420Conexao.Repository
{
    public class LogLoginRepository : ILogLoginRepository
    {
        private readonly ICosmosNoSQLContext _cosmo;
        private readonly Container _caixa;

        public LogLoginRepository(ICosmosNoSQLContext cosmo)
        {
           _cosmo = cosmo;           
           _caixa = _cosmo.consultarContainerCosmo("loglogin");
        }

        public async Task<LogLogin> Save( LogLogin log) {
            return await _caixa.CreateItemAsync(log);
        }
        public async Task<List<LogLogin>> GetLoginSystem(LogLogin log) {
            
            string sqlQuery = "SELECT * FROM loglogin c " +
            "WHERE c.Email = @parametroEmail " +
            "AND c.Navegador.NavegadorAgente = @parametroNavegadorAgente " +
            "AND c.Navegador.Empresa = @parametroNavegadorEmpresa " +
            "AND c.Navegador.Versão = @parametroNavegadorVersao " +
            "AND c.Navegador.Motor = @parametroNavegadorMotor " +
            "AND c.Sistema.SistemaOperacional = @parametroSistemaOperacional " +
            "AND c.Sistema.Motor = @parametroSistemaMotor";

            QueryDefinition query = new QueryDefinition(sqlQuery)
                .WithParameter("@parametroEmail", log.Email)
                .WithParameter("@parametroNavegadorAgente", log.Navegador.NavegadorAgente)
                .WithParameter("@parametroNavegadorEmpresa", log.Navegador.Empresa)
                .WithParameter("@parametroNavegadorVersao", log.Navegador.Versao)
                .WithParameter("@parametroNavegadorMotor", log.Navegador.Motor)
                .WithParameter("@parametroSistemaOperacional", log.Sistema.SistemaOperacional)
                .WithParameter("@parametroSistemaMotor", log.Sistema.Motor);            

            using FeedIterator<LogLogin> logLogin = _caixa.GetItemQueryIterator<LogLogin>(query);
            List<LogLogin> logLoginItens = new();
            while (logLogin.HasMoreResults){
                LogLogin response =  await logLogin.ReadNextAsync();
                logLoginItens.Add(response);
            }
            return logLoginItens;
        }
    }
}