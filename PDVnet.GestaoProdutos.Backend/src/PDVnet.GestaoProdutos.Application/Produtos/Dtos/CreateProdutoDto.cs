using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Application.Produtos.Dtos
{
    public class CreateProdutoDto
    {
        /// <summary>
        /// Nome do produto
        /// </summary>
        [Required(ErrorMessage = "O nome do produto é obrigatorio")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome do produto nao deve estar em branco")]
        public string Nome { get; set; } = default!;

        /// <summary>
        /// Descricao detalhada do produto
        /// </summary>
        [StringLength(500, ErrorMessage = "A descricao nao pode exceder 500 caracteres")]
        public string? Descricao { get; set; }

        /// <summary>
        /// Preço unitario
        /// </summary>
        [Required(ErrorMessage = "O preço é obrigatorio")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "O preco nao pode ser negativo")]
        public decimal Preco { get; set; }

        /// <summary>
        /// Quantidade em estoque
        /// </summary>
        [Required(ErrorMessage = "A quantidade é obrigatoria")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade nao pode ser negativo")]
        public int Quantidade { get; set; }
    }
}
