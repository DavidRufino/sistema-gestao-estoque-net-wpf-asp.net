using Microsoft.EntityFrameworkCore;
using PDVnet.GestaoProdutos.Domain.Entities;
using PDVnet.GestaoProdutos.Domain.Repositories;
using PDVnet.GestaoProdutos.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Infrastructure.Repositories
{
    // Implementaçao concreta da interface IProdutosRepository
    // Essa classe se comunica direto com o banco de dados usando o Entity Framewrok Core
    internal class ProdutosRepository : IProdutosRepository
    {
        // Injeta o DbContext atraves do construtor primario (recurso do C# 12)
        private readonly ProdutosDbContext _dbContext;

        public ProdutosRepository(ProdutosDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// Retorna todos os produtos do banco de dados de forma assincrona
        /// </summary>
        /// <returns>Uma lista de entidades do tipo Produto</returns>
        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            var produtoss = await this._dbContext.Produtos.ToListAsync();
            return produtoss;
        }

        /// <summary>
        /// Retorna um produto especifico com base no ID informado
        /// </summary>
        /// <param name="id">ID do produto a ser buscado</param>
        /// <returns>O produto correspondente ou null, caso nao seja encontrado</returns>
        public async Task<Produto?> GetByIdAsync(int id)
        {
            var prodtuo = await this._dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            return prodtuo;
        }

        /// <summary>
        /// Adiciona um novo produto ao banco de dados de forma assíncrona
        /// </summary>
        /// <param name="produto">A entidade Produto a ser adicionada</param>
        /// <returns>O ID do produto recém-criado.</returns>
        public async Task<int> CreateAsync(Produto produto)
        {
            // O Entity Framework Core rastreia a entidade
            // A propriedade Id será populada automaticamente pelo banco de dados após SaveChangesAsync
            // (assumindo que o Id é uma chave primária auto-incrementável)
            await this._dbContext.Produtos.AddAsync(produto);

            // Persiste a mudança no banco de dados
            await this._dbContext.SaveChangesAsync();
            
            // Retorna o ID gerado pelo banco de dados
            return produto.Id;
        }

        /// <summary>
        /// Atualiza um produto existente no banco de dados de forma assíncrona
        /// </summary>
        /// <param name="produto">A entidade Produto com os dados atualizados. O ID deve corresponder a um produto existente</param>
        public async Task UpdateAsync(Produto produto)
        {
            // O Entity Framework Core detecta que a entidade já existe e a marca como modificada
            this._dbContext.Produtos.Update(produto);

            // Persiste a mudança no banco de dados
            await this._dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Exclui um produto do banco de dados com base no ID de forma assíncrona
        /// </summary>
        /// <param name="id">O ID do produto a ser excluido</param>
        public async Task DeleteAsync(int id)
        {
            var produtoToDelete = await this._dbContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            if (produtoToDelete != null)
            {
                this._dbContext.Produtos.Remove(produtoToDelete);

                // Persiste a mudança no banco de dados
                await this._dbContext.SaveChangesAsync();
            }
        }
    }
}
