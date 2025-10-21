using PDVnet.GestaoProdutos.Infrastructure.Extensions;
using PDVnet.GestaoProdutos.Application.Extensions;
using PDVnet.GestaoProdutos.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os no container de inje�ao de dependencia (Dependency Injection)
// Aqui � onde configuramos tudo q a aplicacao vai precisar pra rodar

// --------------------------------------------------------
// Configura�ao dos Servi�os (Dependency Injection Container)
// --------------------------------------------------------

// Adiciona suporte pros controllers da aplica�ao
// Isso permite que o ASP.NET Core entenda e direcione as requisi�oes HTTP pros controllers certos
builder.Services.AddControllers();

// -------------------------------------------------------
// Registro da Camada de Aplica�ao (Application Layer)
builder.Services.AddApplication();

// -------------------------------------------------------
// Registro da Camada de Infraestrutura (Infrastructure Layer)
// Metodo de extensao definido no projeto Infrastructure
// Ele registra todas as dependencias relacionadas ao acesso a dados, contexto do banco, etc
// Passa o builder.Configuration pra que consiga ler as configs do appsettings.json (ex: string de conexao)
builder.Services.AddInfrastructure(builder.Configuration);

// Configura�ao do OpenAPI/Swagger pra documentar os endpoints
// Mais infos em https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// --------------------------------------------------------
// Constr�i a aplica�ao (monta o pipeline e os servi�os)
var app = builder.Build();

// ------------------------------------------------------
// Faz um pre cadastro de dados fake antes da aplica�ao come�ar a atender requisi�oes
// Isso garante que o banco tenha alguns produtos iniciais
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IProdutosSeeder>();
    await seeder.SeedAsync(); // Popula o banco antes de rodar a API
}

// Configura o pipeline de requisi�oes HTTP
if (app.Environment.IsDevelopment())
{
    // Em modo dev, mapeia o OpenAPI pra facilitar o teste e documenta�ao.
    app.MapOpenApi();
}

// Middleware que redireciona requisi�oes HTTP pra HTTPS
// Ajuda a garantir comunica�ao segura (principalmente em prod)
app.UseHttpsRedirection();

// Middleware que verifica se o usuario tem autoriza�ao pra acessar os endpoints
// S� funciona se os controllers ou actions tiverem [Authorize] configurado
app.UseAuthorization();

// --------------------------------------------------------
// Mapeamento de Endpoints
// Define que as rotas serao resolvidas com base nos controllers e seus atributos
// (ex: [Route], [HttpGet], etc)
app.MapControllers();

// --------------------------------------------------------
// Executa a aplica�ao Web
// Inicia o servidor e come�a a escutar as requisi�oes HTTP.
app.Run();
