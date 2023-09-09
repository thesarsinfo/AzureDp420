using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dp420Conexao.DTO.Login;

namespace Dp420Conexao.Model.Login
{
    public class Localizacao
    {
        public Double Latitude {get; set;}
        public Double Logintude{get; set;}
        public String Ip{get; set;}
        public Localizacao(){}

        public static explicit operator Localizacao(LocalicazaoDTO v)
        {
            return new Localizacao 
            {
                Latitude = v.Latitude,
                Logintude = v.Logintude,
                Ip = v.Ip
            };
        }
    }
}