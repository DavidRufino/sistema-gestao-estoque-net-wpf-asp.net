using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.Domain.Entities;

public class Produto
{
    /// <summary>
    /// Construtor simples
    /// </summary>
    public Produto() { }

    /// <summary>
    /// Construtor para facilitar criacao de seed ficticio 
    /// </summary>
    public Produto(string nome, string descricao, decimal preco, int quantidadeEmEstoque)
    {
        // Regras do Negócio:
        // O `Nome`, `Preço` e `Quantidade` de um produto são campos obrigatórios.
        // O `Preço` e a `Quantidade` não podem ser valores negativos.

        // validar valores nome, preco e quantidadeEmEstoque
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do produto é obrigatorio", nameof(nome));

        // preco deve ser positivo
        if (preco <= 0) throw new ArgumentException("O preco nao pode ser negativo", nameof(preco));

        // quantidade nao pode ser negativa
        if (quantidadeEmEstoque < 0) throw new ArgumentException("A quantidade nao pode ser negativo", nameof(quantidadeEmEstoque));

        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Quantidade = quantidadeEmEstoque;
        DataCadastro = DateTime.UtcNow;
    }

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
