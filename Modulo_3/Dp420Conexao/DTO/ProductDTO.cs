using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dp420Conexao.DTO
{
    public class ProductDTO
    {
        
        public string? CategoriaId  {get;set;}
        public string? Name {get;set;}
        public double Price {get;set;}
        public string[]? Tags {get;set;}
    }
}