using PDVnet.GestaoProdutos.Infrastructure.Extensions;
using PDVnet.GestaoProdutos.Application.Extensions;
using PDVnet.GestaoProdutos.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços no container de injeçao de dependencia (Dependency Injection)
// Aqui é onde configuramos tudo q a aplicacao vai precisar pra rodar

// --------------------------------------------------------
// Configuraçao dos Serviços (Dependency Injection Container)
// --------------------------------------------------------

// Adiciona suporte pros controllers da aplicaçao
// Isso permite que o ASP.NET Core entenda e direcione as requisiçoes HTTP pros controllers certos
builder.Services.AddControllers();

// -------------------------------------------------------
// Registro da Camada de Aplicaçao (Application Layer)
builder.Services.AddApplication();

// -------------------------------------------------------
// Registro da Camada de Infraestrutura (Infrastructure Layer)
// Metodo de extensao definido no projeto Infrastructure
// Ele registra todas as dependencias relacionadas ao acesso a dados, contexto do banco, etc
// Passa o builder.Configuration pra que consiga ler as configs do appsettings.json (ex: string de conexao)
builder.Services.AddInfrastructure(builder.Configuration);

// Configuraçao do OpenAPI/Swagger pra documentar os endpoints
// Mais infos em https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// --------------------------------------------------------
// Constrói a aplicaçao (monta o pipeline e os serviços)
var app = builder.Build();

// ------------------------------------------------------
// Faz um pre cadastro de dados fake antes da aplicaçao começar a atender requisiçoes
// Isso garante que o banco tenha alguns produtos iniciais
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IProdutosSeeder>();
    await seeder.SeedAsync(); // Popula o banco antes de rodar a API
}

// Configura o pipeline de requisiçoes HTTP
if (app.Environment.IsDevelopment())
{
    // Em modo dev, mapeia o OpenAPI pra facilitar o teste e documentaçao.
    app.MapOpenApi();
}

// Middleware que redireciona requisiçoes HTTP pra HTTPS
// Ajuda a garantir comunicaçao segura (principalmente em prod)
app.UseHttpsRedirection();

// Middleware que verifica se o usuario tem autorizaçao pra acessar os endpoints
// Só funciona se os controllers ou actions tiverem [Authorize] configurado
app.UseAuthorization();

// --------------------------------------------------------
// Mapeamento de Endpoints
// Define que as rotas serao resolvidas com base nos controllers e seus atributos
// (ex: [Route], [HttpGet], etc)
app.MapControllers();

// --------------------------------------------------------
// Executa a aplicaçao Web
// Inicia o servidor e começa a escutar as requisiçoes HTTP.
app.Run();
