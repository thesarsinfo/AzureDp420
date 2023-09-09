using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.DTO.Login;

namespace Dp420Conexao.Model.Login
{
    public class Sistema
    {
        public String SistemaOperacional {get; set;} 
        public String Motor {get; set;} 
        public Sistema()
        {        }
        public static explicit operator Sistema(SistemaDTO sistemaDTO)
        {
            return new Sistema
            {
                SistemaOperacional = sistemaDTO.SistemaOperacional,          
                Motor = sistemaDTO.Motor
            };
        }
    }
}