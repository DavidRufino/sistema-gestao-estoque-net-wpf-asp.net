using PDVnet.GestaoProdutos.Domain.Entities;
using PDVnet.GestaoProdutos.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Infrastructure.Seeders
{
    internal class ProdutosSeeder : IProdutosSeeder
    {
        private readonly ProdutosDbContext _dbContext;

        public ProdutosSeeder(ProdutosDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
            if (await this._dbContext.Database.CanConnectAsync())
            {
                if (!this._dbContext.Produtos.Any())
                {
                    // Gerar produtos
                    var produtos = GetProdutos();

                    // Adicionar novos produtos gerados para tabela produtos
                    this._dbContext.Produtos.AddRange(produtos);
                    
                    // salvar a tabela com novos items
                    await this._dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Produto> GetProdutos()
        {
            //new()
            //{
            //    Nome = "Nome", // NVARCHAR(100) NN
            //    Descricao = "Descricao", // NVARCHAR(255) NN
            //    Preco = 10.0, // DECIMAL(10,2) NN
            //    Quantidade = 5, // INT NN
            //    DataCadastro = DateTime.Now, // NN
            //}

            return new List<Produto>
            {
                new Produto("Smartphone X", "Smartphone de ultima geracao com câmera de 108MP.", 2500.00M, 50),
                new Produto("Fone Bluetooth Z", "Fone de ouvido sem fio com cancelamento de ruído.", 350.50M, 120),
                new Produto("Notebook Gamer Y", "Notebook potente para jogos e trabalho.", 7899.99M, 15),
                new Produto("Teclado Mecânico RGB", "Teclado mecânico com iluminacao RGB personalizável.", 450.00M, 80),
                new Produto("Mouse Ergonômico", "Mouse confortável para uso prolongado.", 120.00M, 200),
                new Produto("Monitor UltraWide 34'", "Monitor de alta resolucao para produtividade.", 1800.00M, 30),
                new Produto("Webcam Full HD", "Câmera de vídeo para chamadas e streaming.", 199.90M, 90),
                new Produto("Impressora Multifuncional", "Impressora, scanner e copiadora para escritório.", 650.00M, 40),
                new Produto("Roteador Wi-Fi 6", "Roteador de alta velocidade para rede doméstica.", 280.00M, 70),
                new Produto("Smartwatch Fit", "Relógio inteligente com monitor de saúde.", 550.00M, 60)
            };
        }

    }
}
