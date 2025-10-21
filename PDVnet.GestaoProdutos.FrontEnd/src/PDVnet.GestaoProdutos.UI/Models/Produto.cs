using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.UI.Models
{
    public class Produto
    {
        /// <summary>
        /// Identificador único do produto
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do produto
        /// </summary>
        public string Nome { get; set; } = default!; // valor non-nullable

        /// <summary>
        /// Descrição detalhada do produto
        /// </summary>
        public string? Descricao { get; set; }

        /// <summary>
        /// Preço unitario
        /// </summary>
        public decimal Preco
        {
            get => _preco;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("O preco deve ser um valor positivo", nameof(Preco));
                _preco = value;
            }
        }
        private decimal _preco;

        /// <summary>
        /// Quantidade em estoque
        /// </summary>
        public int Quantidade
        {
            get => _quantidade;
            set
            {
                if (value < 0)
                    throw new ArgumentException("A quantidade deve ser um valor nao negativo", nameof(Quantidade));
                _quantidade = value;
            }
        }
        private int _quantidade;

        /// <summary>
        /// Data de cadastro automatico
        /// </summary>
        public DateTime DataCadastro { get; set; }
    }
}
