using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Infrastructure.Seeders
{
    public interface IProdutosSeeder
    {
        Task SeedAsync();
    }
}
