using Dp420Conexao.DTO;
using Dp420Conexao.Infrastructure.DatabaseContext;
using Dp420Conexao.Service;
using Microsoft.AspNetCore.Mvc;

namespace Dp420Conexao.Controllers;

[ApiController]
[Route("[controller]")]
public class CosmosApiController : ControllerBase
{  

    private readonly ILogger<CosmosApiController> _logger;
    private readonly ProductService _productService;
    public CosmosApiController(ILogger<CosmosApiController> logger, ProductService service)
    {
        _productService = service;
        _logger = logger;
    }

    [HttpPost()]
    public async Task<IActionResult> PostProduto(ProductDTO produto)
    {
        var prodDTO = await _productService.Salvar(produto); 
        return Ok(prodDTO); 
    }
}
