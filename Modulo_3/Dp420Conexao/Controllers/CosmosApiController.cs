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



        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                var produtoCriado = await _productService.Salvar(productDTO);
                return CreatedAtAction(nameof(GetProduct), new { id = produtoCriado.Id }, produtoCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/bulk")]
        public async Task<IActionResult> CriarProdutosProntosBulk()
        {
            try
            {
                var  response = await _productService.CriarProdutosProntosBulk();
                if ( response) return Ok();
                return StatusCode(502);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/todosprodutos")]
         public async Task<IActionResult> LerTodosProdutos() {
            var product = await _productService.LerTodosProdutos();
            return Ok(product);
        }
        [HttpGet("/todosprodutos/paginado")]
         public async Task<IActionResult> LerTodosProdutosPaginado() {
            var product = await _productService.LerProdutosPaginados();
            return Ok(product);
        }

        [HttpGet("{id}/{categoryId}")]
        public async Task<IActionResult> GetProduct(Guid id, string categoryId)
        {
            try
            {
                var productReadDTO = new ProductReadDTO(id, categoryId);
                var product = await _productService.LerDocumento(productReadDTO);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/{categoryId}")]
        public async Task<IActionResult> UpdateProduct(Guid id, string categoryId, [FromBody] ProductDTO productDTO)
        {
            try
            {
                var productReadDTO = new ProductReadDTO(id, categoryId);
                var updatedProduct = await _productService.AtualizarDocumento(productReadDTO, productDTO);
                if (updatedProduct == null)
                {
                    return NotFound();
                }
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/{categoryId}")]
        public async Task<IActionResult> DeleteProduct(Guid id, string categoryId)
        {
            try
            {
                var productReadDTO = new ProductReadDTO(id, categoryId);
                var deleted = await _productService.ApagarDocumento(productReadDTO);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


