using PDVnet.GestaoProdutos.Application.Produtos.Dtos;
using PDVnet.GestaoProdutos.Domain.Entities;

namespace PDVnet.GestaoProdutos.Application.Produtos
{
    public interface IProdutosService
    {
        Task<IEnumerable<Produto>> GetAllProdutosAsync();
        Task<Produto?> GetProdutosByIdAsync(int id);

        Task<int> CreateProdutoAsync(CreateProdutoDto dto);
        Task UpdateProdutoAsync(int id, UpdateProdutoDto dto);
        Task<bool> DeleteProdutoAsync(int id);
    }
}