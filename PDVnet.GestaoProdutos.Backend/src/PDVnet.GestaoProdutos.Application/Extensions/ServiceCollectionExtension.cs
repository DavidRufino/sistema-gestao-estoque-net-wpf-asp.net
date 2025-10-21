using Microsoft.Extensions.DependencyInjection;
using PDVnet.GestaoProdutos.Application.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Application.Extensions
{
    /// <summary>
    /// Classe estática responsável por conter métodos de extensao
    /// relacionados à configuracao dos serviços da camada de Application
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Método de extensão que adiciona a colecao de serviços (DI container)
        /// todas as dependências da camada de Application
        /// </summary>
        /// <param name="services">
        /// A colecao de serviços usada para registrar as dependências da aplicacao
        /// Normalmente acessada através de <c>builder.Services</c> no <c>Program.cs</c>
        /// </param>
        public static void AddApplication(this IServiceCollection services)
        {
            // O método AddScoped<TInterface, TImplementation>() registra o serviço com "lifetime" do tipo Scoped
            // Isso significa
            // - Uma nova instância de TImplementation é criada uma vez por requisicao HTTP
            // - Essa mesma instância é reutilizada em toda a requisicao atual.
            //
            // Neste exemplo
            // Sempre que a aplicacao requisitar uma instância de IProdutosService
            // o container de injecao de dependência fornecerá uma instância de ProdutosService
            services.AddScoped<IProdutosService, ProdutosService>();
        }
    }
}
