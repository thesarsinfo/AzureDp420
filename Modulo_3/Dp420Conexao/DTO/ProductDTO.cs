using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dp420Conexao.DTO
{
    public class ProductDTO
    {
        
        public Guid categoryId  {get;set;}
        public string? name {get;set;}
        public double price {get;set;}
        public string[]? tags {get;set;}
    }
}