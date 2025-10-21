using Microsoft.Extensions.Logging;
using PDVnet.GestaoProdutos.Application.Produtos.Dtos;
using PDVnet.GestaoProdutos.Domain.Entities;
using PDVnet.GestaoProdutos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Application.Produtos
{
    internal class ProdutosService : IProdutosService
    {
        private readonly IProdutosRepository _produtosRepository;
        private readonly ILogger<ProdutosService> _logger;

        public ProdutosService(IProdutosRepository produtosRepository, ILogger<ProdutosService> logger)
        {
            this._logger = logger;
            this._produtosRepository = produtosRepository;
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
        {
            this._logger.LogInformation($"[ProdutosService] GetAllProdutosAsync");
            var restaurants = await _produtosRepository.GetAllAsync();
            return restaurants;
        }

        /// <summary>
        /// Busca um produto específico pelo seu ID
        /// </summary>
        public async Task<Produto?> GetProdutosByIdAsync(int id)
        {
            this._logger.LogInformation($"[ProdutosService] GetProdutosByIdAsync Produto {id}");
            var restaurants = await _produtosRepository.GetByIdAsync(id);
            return restaurants;
        }

        /// <summary>
        /// Cria e salva um novo produto no banco de dados
        /// </summary>
        public async Task<int> CreateProdutoAsync(CreateProdutoDto dto)
        {
            _logger.LogInformation($"[ProdutosService] Adicionado novo produto: {dto.Nome}");

            var newProduto = new Produto(dto.Nome, dto.Descricao ?? string.Empty, dto.Preco, dto.Quantidade);
            newProduto.DataCadastro = DateTime.UtcNow; // Garante a data de cadastro

            var id = await _produtosRepository.CreateAsync(newProduto);
            return id;
        }

        /// <summary>
        /// Atualiza os dados de um produto existente
        /// </summary>
        public async Task UpdateProdutoAsync(int id, UpdateProdutoDto dto)
        {
            _logger.LogInformation($"[ProdutosService] Tentando atualizar produto {id}");

            var produto = await _produtosRepository.GetByIdAsync(id);
            if (produto is null)
            {
                // Produto nao encontrado
                _logger.LogWarning($"[ProdutosService] Produto {id} nao encontrado para atualizacao.");
                throw new KeyNotFoundException($"Produto com ID {id} nao encontrado.");
            }

            // Mapeia o DTO para a entidade, atualizando apenas os campos fornecidos
            produto.Nome = dto.Nome ?? produto.Nome;
            produto.Descricao = dto.Descricao ?? produto.Descricao;
            produto.Preco = dto.Preco ?? produto.Preco;
            produto.Quantidade = dto.Quantidade ?? produto.Quantidade;

            await _produtosRepository.UpdateAsync(produto);
            _logger.LogInformation($"[ProdutosService] Produto {id} atualizado com sucesso.");
        }

        /// <summary>
        /// Exclui um produto do banco de dados
        /// </summary>
        public async Task<bool> DeleteProdutoAsync(int id)
        {
            _logger.LogInformation($"[ProdutosService] Tentando excluir produto {id}");

            var produto = await _produtosRepository.GetByIdAsync(id);
            if (produto is null)
            {
                // Produto nao encontrado
                _logger.LogWarning($"[ProdutosService] Produto {id} nao encontrado para exclusão.");
                return false; 
            }

            await _produtosRepository.DeleteAsync(id);
            _logger.LogInformation($"[ProdutosService] Produto {id} excluido com sucesso.");
            return true;
        }
    }
}
