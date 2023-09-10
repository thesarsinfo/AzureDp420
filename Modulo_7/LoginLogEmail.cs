using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks.Dataflow;

namespace DP420.LoginLog
{
    public static class LoginLogEmail
    {
        [FunctionName("LoginLogEmail")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,"post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string email = data.email;
            Navegador Navegador = data.Navegador;
            Localicacao localizacao = data.Localizacao;
            Sistema sistema = data.Sistema;
            

             // Configurar as credenciais do SendGrid
            string apiKey = Environment.GetEnvironmentVariable("SendGridKey");
            var client = new SendGridClient(apiKey);

            // Configurar o conteúdo do email
            var from = new EmailAddress("seuemail@dominio.com", "Nome do Remetente");
            var subject = "Login de email";
            var to = new EmailAddress(email);
            var plainTextContent = "Conteúdo do email em texto simples";
            var htmlContent = "<strong>Conteúdo do email em HTML" +
            "Um novo login foi feito em sua conta <br/>"+
            "Navegador: " + Navegador.Empresa + Navegador.Motor + Navegador.NavegadorAgente + Navegador.Motor + "<br/>" +
            "Sistema Operacional: " + sistema.SistemaOperacional + sistema.Motor + "<br/>" +
            "Localização: " + localizacao.Ip + " "+ localizacao.Latitude + " " +localizacao.Logintude + "<br/>" +
            "</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            try
            {
                // Enviar o email
                var response = await client.SendEmailAsync(msg);
                log.LogInformation("Email enviado com sucesso.");
            }
            catch (Exception ex)
            {
                log.LogError($"Erro ao enviar o email: {ex.Message}");
            }

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
        public class LogLogin
        {
            public String Email {get; set;}
            public Navegador Navegador{get; set;}
            public Localicacao Localizacao {get; set;}
            public Sistema Sistema {get; set;}
        }
        public class Navegador
        {
            public String NavegadorAgente {get; set;}
            public String Empresa {get; set;}
            public String Versao {get; set;}
            public String Motor {get; set;}
        }
        public class Localicacao
        {
            public Double Latitude {get; set;}
            public Double Logintude{get; set;}
            public String Ip{get; set;}
        }
        public class Sistema
        {
            public String SistemaOperacional {get; set;} 
            public String Motor {get; set;} 
        }
    }
}
