using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PDVnet.GestaoProdutos.UI.Dialogs;
using PDVnet.GestaoProdutos.UI.Models;
using PDVnet.GestaoProdutos.UI.Services;
using PDVnet.GestaoProdutos.UI.Window;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows;

namespace PDVnet.GestaoProdutos.UI.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly ProdutoApiService _produtoApiService;

        [ObservableProperty]
        private int _totalProdutosCadastrados;

        [ObservableProperty]
        private decimal _valorTotalEstoque;

        [ObservableProperty]
        private ObservableCollection<Produto> _produtosBaixoEstoque = new ObservableCollection<Produto>();

        [ObservableProperty]
        private ObservableCollection<Produto> _todosProdutos = new ObservableCollection<Produto>();

        /// <summary>
        /// Produto selecionado no DataGrid
        /// </summary>
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditProdutoCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteProdutoCommand))]
        private Produto? _selectedProduto;

        [ObservableProperty]
        private bool _isLoading;

        public DashboardViewModel()
        {
            // instanciando o service
            _produtoApiService = new ProdutoApiService();
        }

        [RelayCommand]
        public async Task LoadDashboardDataAsync()
        {
            IsLoading = true;
            try
            {
                var produtosAPI = await _produtoApiService.GetAllProdutosAsync();

                // atualiza o DataGrid com lista de todos os produtos
                TodosProdutos.Clear();
                foreach (var p in produtosAPI)
                {
                    TodosProdutos.Add(p);
                }

                // O numero total de produtos cadastrados
                TotalProdutosCadastrados = TodosProdutos.Count;

                // O valor total do estoque (somatório de `Preço * Quantidade`)
                ValorTotalEstoque = TodosProdutos.Sum(p => p.Preco * p.Quantidade);

                // Um alerta para produtos com baixo estoque (quantidade menor que 5).
                ProdutosBaixoEstoque.Clear();
                foreach (var produto in TodosProdutos.Where(p => p.Quantidade < 5))
                {
                    ProdutosBaixoEstoque.Add(produto);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Nao foi possivel conectar ao servidor. Verifique se está rodando e tente novamente.\n Error: {ex.Message}", "Erro de Conexão", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro inesperado: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task AddNewProduto()
        {
            // null para indicar que é um novo produto
            var editorViewModel = new ProdutoEditorViewModel(null); 
            var editorWindow = new ProdutoEditorDialog(editorViewModel);

            // ShowDialog bloqueia a janela principal e retorna um resultado
            // true - salva
            // false - cancela
            var result = editorWindow.ShowDialog();

            if (result == true)
            {
                // obtem o produto final da ViewModel de edicao
                var produtoParaCriar = editorViewModel.GetProdutoFinal();
                await _produtoApiService.CreateProdutoAsync(produtoParaCriar);

                // recarrega a lista
                await LoadDashboardDataAsync();
            }
        }

        /// <summary>
        /// E executado se CanEditOrDelete retornar true
        /// </summary>
        /// <returns></returns>
        [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
        private async Task EditProduto()
        {
            if (SelectedProduto == null) return;

            var editorViewModel = new ProdutoEditorViewModel(SelectedProduto);
            var editorWindow = new ProdutoEditorDialog(editorViewModel);

            var result = editorWindow.ShowDialog();

            if (result == true)
            {
                var produtoParaAtualizar = editorViewModel.GetProdutoFinal();
                await _produtoApiService.UpdateProdutoAsync(produtoParaAtualizar.Id, produtoParaAtualizar);
                await LoadDashboardDataAsync();
            }
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
        private async Task DeleteProduto()
        {
            if (SelectedProduto == null) return;

            var result = MessageBox.Show($"Tem certeza que deseja excluir o produto '{SelectedProduto.Nome}'?",
                                           "Confirmar Exclusao",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                await _produtoApiService.DeleteProdutoAsync(SelectedProduto.Id);
                await LoadDashboardDataAsync();
            }
        }

        /// <summary>
        /// Determina se os botões de Editar/Deletar devem estar ativos
        /// </summary>
        /// <returns></returns>
        private bool CanEditOrDelete() => SelectedProduto != null;
    }
}
