using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Application.Produtos.Dtos
{
    public class UpdateProdutoDto
    {
        /// <summary>
        /// Nome do produto
        /// </summary>
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome do produto nao deve estar em branco")]
        public string? Nome { get; set; }

        /// <summary>
        /// Descrição detalhada do produto
        /// </summary>
        [StringLength(500, ErrorMessage = "A descricao nao pode exceder 500 caracteres")]
        public string? Descricao { get; set; }

        /// <summary>
        /// Preço unitário
        /// </summary>
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "O preco nao pode ser negativo")]
        public decimal? Preco { get; set; }

        /// <summary>
        /// Quantidade em estoque
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade nao pode ser negativo")]
        public int? Quantidade { get; set; }
    }
}
