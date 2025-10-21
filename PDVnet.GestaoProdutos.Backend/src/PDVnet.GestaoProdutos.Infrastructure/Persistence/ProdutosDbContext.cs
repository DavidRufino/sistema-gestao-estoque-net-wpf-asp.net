using Microsoft.EntityFrameworkCore;
using PDVnet.GestaoProdutos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Infrastructure.Persistence
{
    /// <summary>
    /// Classe responsável pela criacao e configuracao do banco de dados da aplicacao
    /// Essa classe herda de DbContext, fornecendo acesso às entidades e controle de persistência de dados
    /// </summary>
    internal class ProdutosDbContext : DbContext
    {
        /// <summary>
        /// Tabela Produtos
        /// </summary>
        internal DbSet<Produto> Produtos { get; set; }

        // Construtor que recebe as opções de configuracao do contexto (como string de conexão e provedores de banco)
        // A instância pode ser criada tanto manualmente quanto injetada via Dependency Injection
        public ProdutosDbContext(DbContextOptions<ProdutosDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Criar a entidade Produto
            modelBuilder.Entity<Produto>(entity =>
            {
                // deixar Id como chave primaria
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);

                // descricao pode ser null
                entity.Property(e => e.Descricao).HasMaxLength(500);
                entity.Property(e => e.Preco).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Quantidade).IsRequired();
            });
        }
    }
}
