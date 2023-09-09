using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dp420Conexao.Model.Login
{
    public class LogLogin
    {
        public Guid Id {get; set;}
        public String Email {get; set;}
        public Navegador Navegador{get; set;}
        public Localizacao Localizacao {get; set;}
        public Sistema Sistema {get; set;}


    }
}