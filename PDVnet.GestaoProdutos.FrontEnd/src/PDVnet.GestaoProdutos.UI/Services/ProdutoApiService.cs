using PDVnet.GestaoProdutos.UI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace PDVnet.GestaoProdutos.UI.Services
{
    public class ProdutoApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ProdutoApiService()
        {
            _httpClient = new();
            _httpClient.BaseAddress = new Uri("https://localhost:7200/");
            _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados no banco de dados
        /// </summary>
        public async Task<List<Produto>> GetAllProdutosAsync()
        {
            var response = await _httpClient.GetAsync("api/produtos");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var produtos = JsonSerializer.Deserialize<List<Produto>>(content, _jsonSerializerOptions);

            return produtos ?? new List<Produto>();
        }

        /// <summary>
        /// Cria e salva um novo produto no banco de dados
        /// </summary>
        public async Task<Produto> CreateProdutoAsync(Produto produto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/produtos", produto);
            response.EnsureSuccessStatusCode();
            var createdProduto = await response.Content.ReadFromJsonAsync<Produto>(_jsonSerializerOptions);
            return createdProduto ?? throw new InvalidOperationException("Não foi possível criar o produto.");
        }

        /// <summary>
        /// Atualiza os dados de um produto existente
        /// </summary>
        public async Task UpdateProdutoAsync(int id, Produto produto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/produtos/{id}", produto);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Exclui um produto do banco de dados
        /// </summary>
        public async Task DeleteProdutoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/produtos/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
