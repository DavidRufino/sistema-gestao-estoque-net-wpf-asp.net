using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PDVnet.GestaoProdutos.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVnet.GestaoProdutos.UI.ViewModels
{
    public partial class ProdutoEditorViewModel : ObservableValidator
    {
        public event EventHandler? SaveCompleted;

        [ObservableProperty]
        private string _windowTitle = "Adicionar Novo Produto";

        private string _nome = string.Empty;
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(1, ErrorMessage = "O nome é obrigatório")]
        public string Nome
        {
            get => _nome;
            set => SetProperty(ref _nome, value, true);
        }

        private string? _descricao;
        public string? Descricao
        {
            get => _descricao;
            set => SetProperty(ref _descricao, value, true);
        }

        private decimal _preco;
        [Range(0.01, double.MaxValue, ErrorMessage = "O preco deve ser maior que zero")]
        public decimal Preco
        {
            get => _preco;
            set => SetProperty(ref _preco, value, true);
        }

        private int _quantidade;
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade nao pode ser negativa")]
        public int Quantidade
        {
            get => _quantidade;
            set => SetProperty(ref _quantidade, value, true);
        }

        // Propriedade para manter o produto original (Id e DataCadastro)
        public Produto ProdutoOriginal { get; private set; }

        public ProdutoEditorViewModel(Produto? produtoParaEditar)
        {
            if (produtoParaEditar != null)
            {
                // Se estamos editando, clonamos o objeto
                ProdutoOriginal = produtoParaEditar;
                Nome = produtoParaEditar.Nome;
                Descricao = produtoParaEditar.Descricao;
                Preco = produtoParaEditar.Preco;
                Quantidade = produtoParaEditar.Quantidade;
                WindowTitle = "Editar Produto";
            }
            else
            {
                // Se for novo cria uma instancia vazia
                ProdutoOriginal = new Produto();
            }

            // Valida o estado inicial das propriedades
            ValidateAllProperties();

            // Sempre notificar o comando quando erros mudarem
            ErrorsChanged -= ProdutoEditorViewModel_ErrorsChanged;
            ErrorsChanged += ProdutoEditorViewModel_ErrorsChanged;
        }

        private void ProdutoEditorViewModel_ErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            Debug.WriteLine($"[ProdutoEditorViewModel] ProdutoEditorViewModel_ErrorsChanged");
            SaveCommand.NotifyCanExecuteChanged();
        }

        /// <summary>
        /// Comando vinculado ao botao salvar
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanSave))]
        private void Save()
        {
            // valida todas as propriedades antes de salvar
            ValidateAllProperties();
            if (HasErrors) return;
            
            // dispara o evento para notificar a View se o salvamento foi sucesso
            SaveCompleted?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Habilita/desabilita o botao salvar
        /// </summary>
        private bool CanSave()
        {
            // botao estara ativo se nao houver erros de validacao
            return !HasErrors;
        }

        /// <summary>
        /// Obter o produto final com os dados atualizados
        /// </summary>
        /// <returns></returns>
        public Produto GetProdutoFinal()
        {
            return new Produto
            {
                Id = ProdutoOriginal.Id,
                Nome = this.Nome,
                Descricao = this.Descricao,
                Preco = this.Preco,
                Quantidade = this.Quantidade,
                DataCadastro = ProdutoOriginal.DataCadastro
            };
        }
    }
}
