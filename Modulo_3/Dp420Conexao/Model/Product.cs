using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dp420Conexao.Model
{
    public class Product
    {
        [JsonProperty("id")]
        public  Guid Id {get;set;}
       
        public  string CategoriaId  {get;set;}
        public  string? Name {get;set;}
        public  double Price {get;set;}
        public  string[]? Tags {get;set;}
    }
}