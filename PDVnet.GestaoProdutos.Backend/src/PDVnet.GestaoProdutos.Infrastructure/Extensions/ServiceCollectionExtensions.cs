using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PDVnet.GestaoProdutos.Domain.Repositories;
using PDVnet.GestaoProdutos.Infrastructure.Persistence;
using PDVnet.GestaoProdutos.Infrastructure.Repositories;
using PDVnet.GestaoProdutos.Infrastructure.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extensao responsavel por configurar e registrar a camada de infraestrutura da aplicaçao,
        /// incluindo o <see cref="ProdutosDbContext"/> no container de injeçao de dependencia do ASP.NET Core
        /// </summary>
        /// <param name="services">A coleçao de serviços usada pra registrar dependencias</param>
        /// <param name="configuration">A configuraçao da aplicaçao, usada pra obter a string de conexao.</param>
        /// <remarks>
        /// Este metodo utiliza o provedor de banco de dados SQL Server via Entity Framewrok Core
        /// A string de conexao deve estar definida em <c>appsettings.json</c> com a chave "ProdutosDb"
        /// </remarks>
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Recupera a connection string do appsettings.json (ou de outra fonte de config)
            // Isso define como a aplicaçao se conecta ao banco de dados (ex: SQL Server)
            // Exemplo: "Server=localhost;Database=ProdutosDb;Trusted_Connection=True;Encrypt=False;"
            // Exemplo: "Server=localhost;Database=ProdutosDb;UserId={SEU-USUARIO};Password={SUA-SENHA};Encrypt=False;"
            var conection = configuration.GetConnectionString("ProdutosDb");

            // Registra o DbContext do EF Core no container de DI
            // - ProdutosDbContext: classe customizada que representa a sessao com o banco
            // - UseSqlServer(): indica que o EF Core vai usar o SQL Server como provedor
            // - O DbContext é registrado como *scoped*, ou seja
            //   -> uma instancia por requisiçao HTTP (ideal pra apps web, evita problemas de thread)
            services.AddDbContext<ProdutosDbContext>(options => options.UseSqlServer(conection));

            // Registra a classe de seed do banco
            // - IProdutosSeeder: abstraçao da logica de seed
            // - ProdutosSeeder: implementaçao concreta que popula o DB com dados iniciais
            // - O ciclo de vida scoped garante que cada request tenha sua propria instancia
            services.AddScoped<IProdutosSeeder, ProdutosSeeder>();

            // Registra a implementaçao do repositorio
            // - IProdutosRepository: interface da camada de dominio
            // - ProdutosRepository: implementaçao concreta baseada em EF Core
            // agora o Service conseguira a cessar o CRUD
            services.AddScoped<IProdutosRepository, ProdutosRepository>();
        }

    }
}
