using PDVnet.GestaoProdutos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Domain.Repositories
{
    // Abstracao (interface) para o acesso a dados relacionados a produtos
    // Permite que a camada de aplicacao trabalhe com os dados sem precisar saber
    // como eles sao armazenados (por exemplo, em um banco SQL, arquivo, etc.)
    public interface IProdutosRepository
    {
        // Método assíncrono para buscar todos os produtos no repositório de dados
        // Retorna uma lista de entidades de domínio do tipo Produto
        Task<IEnumerable<Produto>> GetAllAsync();

        // Método assincrono para buscar um produto específico pelo ID
        // Retorna o produto correspondente ou null caso não seja encontrado
        Task<Produto?> GetByIdAsync(int id);

        Task<int> CreateAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
    }

}
