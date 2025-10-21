using Microsoft.AspNetCore.Mvc;
using PDVnet.GestaoProdutos.Application.Produtos;
using PDVnet.GestaoProdutos.Application.Produtos.Dtos;

namespace PDVnet.GestaoProdutos.API.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosService _produtosService;

        public ProdutosController(IProdutosService produtosService)
        {
            this._produtosService = produtosService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await this._produtosService.GetAllProdutosAsync();
            return Ok(restaurants);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var restaurants = await this._produtosService.GetProdutosByIdAsync(id);
            if (restaurants is null) return NotFound();

            return Ok(restaurants);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduto([FromBody] CreateProdutoDto createProduto)
        {
            int id = await this._produtosService.CreateProdutoAsync(createProduto);

            // Retorna 201 Created com o ID e o corpo do protudo criado
            return CreatedAtAction(nameof(GetById), new { id }, createProduto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto([FromRoute] int id, [FromBody] UpdateProdutoDto updateProduto)
        {
            try
            {
                await this._produtosService.UpdateProdutoAsync(id, updateProduto);

                // 204 No Content para atualizacao com sucesso sem retorno de corpo
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto([FromRoute] int id)
        {
            var deleted = await this._produtosService.DeleteProdutoAsync(id);
            if (!deleted)
            {
                // Retorna 404 se o produto nao for encontrado
                return NotFound();
            }

            // Retorna 204 No Content para exclusao com sucesso
            return NoContent();
        }
    }
}
