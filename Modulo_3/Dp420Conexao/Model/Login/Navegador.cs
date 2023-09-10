using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.DTO.Login;

namespace Dp420Conexao.Model.Login
{
    public class Navegador
    {
        public String NavegadorAgente {get; set;}
        public String Empresa {get; set;}
        public String Versao {get; set;}
        public String Motor {get; set;}
        public Navegador()
        {
        }
         public static explicit operator Navegador(NavegadorDTO navegadorDTO)
        {
            return new Navegador
            {
                NavegadorAgente = navegadorDTO.NavegadorAgente,
                Empresa = navegadorDTO.Empresa,
                Versao = navegadorDTO.Versao,
                Motor = navegadorDTO.Motor
            };
        }
    }
}