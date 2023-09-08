using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
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
        public List<Tag> Tags { get; set; }

        public static explicit operator Product(FeedResponse<Product> v)
        {
            throw new NotImplementedException();
        }
    }
}